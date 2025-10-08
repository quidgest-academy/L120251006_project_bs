<template>
	<teleport
		v-if="isReady"
		to="#q-modal-see-more-f-favorimovietitle-body">
		<q-row-container>
			<q-table
				v-bind="listCtrl"
				v-on="listCtrl.handlers" />
		</q-row-container>
	</teleport>
</template>

<script>
	/* eslint-disable @typescript-eslint/no-unused-vars */
	import { computed } from 'vue'
	import { mapActions } from 'pinia'
	import _merge from 'lodash-es/merge'

	import { useGenericDataStore } from '@quidgest/clientapp/stores'
	import { useNavDataStore } from '@quidgest/clientapp/stores'
	import VueNavigation from '@/mixins/vueNavigation.js'
	import ListHandlers from '@/mixins/listHandlers.js'
	import { navigationProperties } from '@/mixins/navHandlers.js'
	import { TableListControl } from '@/mixins/fieldControl.js'
	import listFunctions from '@/mixins/listFunctions.js'
	import listColumnTypes from '@/mixins/listColumnTypes.js'

	import { loadResources } from '@/plugins/i18n.js'
	import asyncProcM from '@quidgest/clientapp/composables/async'

	import netAPI from '@quidgest/clientapp/network'
	import qApi from '@/api/genio/quidgestFunctions.js'
	import qFunctions from '@/api/genio/projectFunctions.js'
	import qProjArrays from '@/api/genio/projectArrays.js'
	import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'
	import qEnums from '@quidgest/clientapp/constants/enums'
	import { removeModal } from '@/utils/layout'
	/* eslint-enable @typescript-eslint/no-unused-vars */

	import ViewModelBase from '@/mixins/viewModelBase.js'

	const requiredTextResources = ['F_FAVORIMOVIETITLE____SeeMore', 'hardcoded', 'messages']

	export default {
		name: 'FFavorimovietitleSeeMore',

		inheritAttrs: false,

		emits: [
			'close',
			'see-more-choice'
		],

		mixins: [
			navigationProperties,
			VueNavigation,
			ListHandlers
		],

		props: {
			/**
			 * Unique identifier for the control.
			 */
			id: String,

			/**
			 * The limits to which this "See more" control is subjected.
			 */
			limits: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The id of the current navigation.
			 */
			navigationId: {
				type: String,
				default: ''
			}
		},

		expose: [],

		data()
		{
			return {
				isReady: false,

				componentOnLoadProc: asyncProcM.getProcListMonitor('F_FAVORIMOVIETITLE____SeeMore', false),

				interfaceMetadata: {
					id: 'F_FAVORIMOVIETITLE____SeeMore', // Used for resources
					requiredTextResources
				},

				menuInfo: {
					acronym: 'F_FAVORIMOVIETITLE____SeeMore',
					name: 'F_FAVORIMOVIETITLE____SeeMore',
					controller: 'FAVOR',
					action: 'F_FAVORI_MovieValTitle'
				},

				listCtrl: new TableListControl(this.getListConfig(), this),

				// Basic view model to handle access to GLOB, if necessary.
				model: new ViewModelBase(this),
			}
		},

		created()
		{
			this.componentOnLoadProc.addImmediateBusy(loadResources(this, requiredTextResources))

			this.listCtrl.init()
			this.onTableDBDataChanged()

			this.componentOnLoadProc.once(() => {
				this.isReady = true
				this.listCtrl.initData()
			}, this)
		},

		mounted()
		{
			// Listens for changes to the DB and updates the list accordingly.
			this.$eventHub.onMany(this.listCtrl.globalEvents, this.onTableDBDataChanged)

			const modalProps = {
				id: 'see-more-f-favorimovietitle',
				headerTitle: computed(() => this.Resources.MOVIES48538),
				closeButtonEnable: true,
				hideFooter: true,
				dismissWithEsc: true,
				dismissAction: this.close,
				isActive: true,
				returnElement: 'F_FAVORIMOVIETITLE____see-more_button'
			}
			this.setModal(modalProps)
		},

		beforeUnmount()
		{
			// Removes the listeners.
			this.$eventHub.offMany(this.listCtrl.globalEvents, this.onTableDBDataChanged)
			this.listCtrl.destroy()
			this.componentOnLoadProc.destroy()

			removeModal('see-more-f-favorimovietitle')
		},

		methods: {
			...mapActions(useGenericDataStore, [
				'setModal'
			]),

			...mapActions(useNavDataStore, [
				'setParamValue',
				'setEntryValue'
			]),

			close()
			{
				this.$emit('close')
			},

			onTableDBDataChanged()
			{
				const params = {
					id: this.id || null,
					limits: this.limits,
					tableConfiguration: listFunctions.getTableConfiguration(this.listCtrl)
				}

				this.listCtrl.fetchListData(params)
			},

			handleRowAction(eventData)
			{
				if (eventData.id === 'see-more-choice')
				{
					let rowKey = eventData?.rowKeyPath
					if (Array.isArray(eventData?.rowKeyPath) && eventData?.rowKeyPath.length > 0)
						rowKey = eventData?.rowKeyPath[eventData?.rowKeyPath.length - 1]

					this.$emit('see-more-choice', rowKey)
				}
				else
					this.onTableListExecuteAction(this.listCtrl, eventData)
			},

			getListConfig()
			{
				const vm = this
				const listProps = {
					configuration: {
						controller: 'FAVOR',
						action: 'F_favori_MovieValTitle',
						hasDependencies: false,
						isInCollapsible: false,
						columnsOriginal: [
							new listColumnTypes.TextColumn({
								order: 1,
								name: 'ValTitle',
								area: 'MOVIE',
								field: 'TITLE',
								label: computed(() => this.Resources.TITLE21885),
								dataLength: 80,
								scrollData: 80,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
						],
						config: {
							name: 'F_favori_MovieValTitle',
							serverMode: true,
							pkColumn: 'ValCodmovie',
							tableAlias: 'MOVIE',
							tableNamePlural: computed(() => this.Resources.MOVIES48538),
							viewManagement: '',
							showLimitsInfo: true,
							tableTitle: '',
							showAlternatePagination: true,
							permissions: {
							},
							searchBarConfig: {
								visibility: true,
								searchOnPressEnter: true
							},
							filtersVisible: true,
							allowColumnFilters: true,
							allowColumnSort: true,
							generalCustomActions: [
							],
							groupActions: [
							],
							customActions: [
							],
							MCActions: [
							],
							rowClickAction: {
								id: 'see-more-choice',
								name: 'see-more-choice',
							},
							formsDefinition: {
							},
							defaultSearchColumnName: 'ValTitle',
							defaultSearchColumnNameOriginal: 'ValTitle',
							defaultColumnSorting: {
								columnName: 'ValTitle',
								sortOrder: 'asc'
							}
						},
						groupFilters: [
							{
								id: 'applyUniqueValuesCondition',
								isMultiple: true,
								filters: [
									{
										id: 'applyUniqueValuesCondition_true',
										key: true,
										value: computed(() => this.Resources.DISPONIVEIS47523),
										selected: true
									}
								],
								value: true
							},
						],
						globalEvents: ['changed-MOVIE'],
						uuid: 'F_favori_F_favori_MovieValTitle',
						allSelectedRows: 'false',
						handlers: {
							rowAction: vm.handleRowAction
						},
						fixedControlLimits: vm.limits
					}
				}

				return listProps.configuration
			}
		}
	}
</script>
