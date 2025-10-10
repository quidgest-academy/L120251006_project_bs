import _isEmpty from 'lodash-es/isEmpty'
import _merge from 'lodash-es/merge'
import { mapActions, mapState } from 'pinia'
import { v4 as uuidv4 } from 'uuid'

import { MAIN_HISTORY_BRANCH_ID, NetworkAPI } from '@quidgest/clientapp/network'
import eventBus from '@quidgest/clientapp/plugins/eventBus'
import {
	useGenericDataStore,
	useNavDataStore,
	useSystemDataStore,
	useUserDataStore
} from '@quidgest/clientapp/stores'
import { normalizeRouteForSaveNavigation } from '@quidgest/clientapp/utils/genericFunctions'

import { systemInfo } from '@/systemInfo'
import { goBack as _goBack } from '@/utils/navigation'

/**
 * Creates a new navigation level, based on the current Vue context.
 * @param {Object} context - Current Vue context.
 * @returns The navigation ID of the newly created level.
 */
function createNavigationLevel(context) {
	const systemDataStore = useSystemDataStore()
	const genericDataStore = useGenericDataStore()
	const navDataStore = useNavDataStore()

	let historyBranchId = MAIN_HISTORY_BRANCH_ID

	const isWizard = context.$route.meta.isWizardStep

	if (context.isNested || isWizard) {
		if (!_isEmpty(context.historyBranchId))
			historyBranchId = context.historyBranchId

		let branchExist = navDataStore.navigation.history.has(historyBranchId),
			nestedData = _merge({
				isNested: true,
				params: {
					id: context.id,
					mode: context.mode,
					modes: context.modes,
					culture: systemDataStore.system.currentLang,
					system: systemDataStore.system.currentSystem,
					module: systemDataStore.system.currentModule
				}
			}, context.nestedRouteParams),
			wizardRoute = {
				location: context.$route.name,
				params: context.$route.params
			}

		if (branchExist) {
			let newId = isWizard && !context.isNested ? historyBranchId : uuidv4()
			navDataStore.beforeRequestContext(newId)

			let newBranch = navDataStore.navigation.getHistory(newId),
				parentNavigationContext = navDataStore.navigation.getHistory(historyBranchId),
				historyLevel = {
					navigationId: newBranch.navigationId,
					options: isWizard ? wizardRoute : nestedData,
					previousLevel: parentNavigationContext.currentLevel
				}

			navDataStore.addHistoryLevel(historyLevel)
			historyBranchId = newBranch.navigationId
		}
		else {
			historyBranchId = uuidv4()
			navDataStore.beforeRequestContext(historyBranchId)

			let newBranch = navDataStore.navigation.getHistory(historyBranchId),
				historyLevel = {
					navigationId: newBranch.navigationId,
					options: isWizard ? wizardRoute : nestedData
				}

			navDataStore.addHistoryLevel(historyLevel)
		}
	}
	else if (typeof context.$route.name === 'string') {
		// If it's a form and the "modes" aren't passed to it, it means this isn't a normal route change.
		// The route change was probably triggered by the user inserting a new url in the address bar,
		// therefore, we clear the navigation and current menu path.
		if (context.$route.meta.routeType === 'form' && typeof context.$route.params.modes !== 'string' && context.$route.params.isControlled !== 'true') {

			// In this type of menus, we need to load the first menu of the tree
			// Because it this type of menu always has the first menu active at minimum so the double navbar won't be empty
			const loadFirst = systemInfo.layout.MenuStyle === "double_navbar";

			genericDataStore.resetMenuPath(loadFirst)
			navDataStore.clearHistory()
		}

		historyBranchId = context.$route.params.historyBranchId || MAIN_HISTORY_BRANCH_ID
		navDataStore.beforeRequestContext(historyBranchId)

		let menuOrder = ((context.$route || {}).meta || {}).order,
			navigation = navDataStore.navigation.getHistory(historyBranchId)

		if (!_isEmpty(menuOrder)) {
			// If the current menu belongs to a different tree from the ones on the navigation history, we clear the history.
			let clearNav = false

			for (let historyLevel of navigation.convertToCollection()) {
				if (_isEmpty(historyLevel.properties.routeBranch) || menuOrder.startsWith(historyLevel.properties.routeBranch))
					continue

				clearNav = true
				break
			}

			if (clearNav) {
				const historyData = {
					location: `home-${systemDataStore.system.currentModule}`,
					params: {
						...context.$route.params,
						isPopup: 'false'
					}
				}

				if (historyBranchId === MAIN_HISTORY_BRANCH_ID)
					navDataStore.clearHistory()
				navDataStore.addHistoryLevel({ navigationId: historyBranchId, options: historyData })
			}
		}

		let options = normalizeRouteForSaveNavigation(context.$route)
		navDataStore.addHistoryLevel({ navigationId: historyBranchId, options })
	}

	if (!context.isNested) {
		eventBus.emit('navigation-id-change', {
			navigationId: historyBranchId
		})
	}

	return historyBranchId
}

export const navigationProperties = {
	data() {
		return {
			netAPI: new NetworkAPI(this.navigationId)
		}
	},

	computed: {
		...mapState(useUserDataStore, [
			'valuesOfPHEs'
		]),

		/**
		 * The current navigation.
		 */
		navigation() {
			const navDataStore = useNavDataStore()
			return navDataStore.navigation.getHistory(this.navigationId)
		},

		/**
		 * The previous navigation.
		 */
		previousNav() {
			const navDataStore = useNavDataStore()
			const previousNav = navDataStore.previousNav

			return previousNav !== null ? previousNav.getHistory() : null
		},

		/**
		 * The containers state in the store, based on the current navigation ID.
		 */
		containersState() {
			return this.getContainersState(this.navigationId)
		},

		/**
		 * The current control in the store, based on the current navigation ID.
		 */
		currentControl() {
			return this.getCurrentControl(this.navigationId)
		}
	},

	methods: {
		...mapActions(useNavDataStore, [
			'getContainersState',
			'getCurrentControl',
			'storeContainerState',
			'setCurrentControl',
			'removeCurrentControl'
		])
	},

	watch: {
		'navigationId'(newValue) {
			this.netAPI = new NetworkAPI(newValue)
		}
	}
}

/*****************************************************************
 * This mixin should be used by all forms and menus, in order to *
 * ensure that a new navigation level exists before making any   *
 * server requests.                                              *
 *****************************************************************/
export default {
	mixins: [navigationProperties],

	beforeCreate() {
		this.navigationId = createNavigationLevel(this)
	},

	methods: {
		...mapActions(useNavDataStore, [
			'addHistoryLevel',
			'removeHistoryLevel',
			'removeHistoryLevels',
			'removeNavigationLevelsUpTo',
			'clearHistory',
			'setParamValue',
			'removeParamValue',
			'setEntryValue',
			'removeEntryValue',
			'clearEntries',
			'setNavProperties',
			'removeNavProperty',
			'retrievePreviousNav'
		]),

		/**
		 * Goes back to the previous navigation level, if it exists.
		 */
		goBack() {
			_goBack(this.navigationId, this.$route.meta.hasInitialPHE)
		},

		/**
		 * Creates a new navigation level, based on the current Vue context.
		 * @returns The navigation ID of the newly created level.
		 */
		createNavigationLevel() {
			return createNavigationLevel(this)
		}
	}
}
