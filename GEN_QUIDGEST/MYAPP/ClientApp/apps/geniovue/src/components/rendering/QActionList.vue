<template>
	<div class="q-action-list">
		<!-- INLINE -->
		<!-- Groups with display: 'inline' or dropdown groups with only one item -->
		<q-button-group
			v-for="(group, index) in inlineGroups"
			:key="index"
			:disabled="group.disabled"
			:borderless="group.borderless">
			<q-button
				v-for="action in actionsByGroup[group.id]"
				:key="action.id"
				:class="group.customClass"
				:title="action.title"
				:label="action.icon ? '' : action.title"
				:disabled="action.disabled"
				:size="group.size"
				v-bind="$attrs"
				@click="() => selectAction(action.id)">
				<q-icon
					v-if="action.icon"
					v-bind="action.icon" />
			</q-button>

			<div
				v-if="group.separator"
				class="action-sep" />
		</q-button-group>

		<!-- DROPDOWN -->
		<!-- Groups with multiple items and display: 'dropdown' -->
		<template v-if="dropdownGroups.length !== 0">
			<q-button
				ref="dropdownActivator"
				data-type="options-button"
				:aria-label="texts.actionMenuTitle"
				v-bind="{ ...dropdownOptions, ...$attrs }">
				<slot name="customDropdownButton">
					<q-icon :icon="dropdownOptions.icon" />
				</slot>
			</q-button>

			<q-dropdown-menu
				:activator="dropdownActivator?.$el"
				:items="dropdownActions"
				:groups="dropdownGroups"
				:placement="dropdownOptions.placement"
				@select="selectAction" />
		</template>
	</div>
</template>

<script>
	import { QDropdownMenu } from '@quidgest/ui/components'

	export default {
		name: 'QActionList',

		expose: [],

		components: {
			QDropdownMenu
		},

		emits: ['click:action'],

		inheritAttrs: false,

		data()
		{
			return {
				dropdownActivator: undefined,
				defaultGroup: {
					id: 'default_group',
					title: undefined,
					display: 'dropdown',
					disabled: false,
					size: 'small',
					borderless: true,
					separator: false,
					customClass: undefined
				}
			}
		},

		props: {
			/**
			 * A list of actions to show.
			 */
			actions: {
				type: Array,
				default: () => []
			},

			/**
			 * The list of groups used to separate the actions.
			 */
			actionGroups: {
				type: Array,
				default: () => []
			},

			/**
			 * Indicates whether the actions are in a read-only state.
			 */
			readonly: {
				type: Boolean,
				default: false
			},

			/**
			 * The options for the dropdown button
			 */
			dropdownOptions: {
				type: Object,
				default: () => ({
					size: 'small',
					label: '',
					icon: 'more-items',
					borderless: false,
					placement: 'bottom-start',
					class: ''
				})
			},

			/**
			 * Localized text strings to be used within the component (for labels, headers, etc.).
			 */
			texts: {
				type: Object,
				required: true
			}
		},

		mounted()
		{
			this.linkDropdownToButton()
		},

		updated()
		{
			this.linkDropdownToButton()
		},

		computed: {
			/**
			 * Computes the list of actions that are visible
			 */
			visibleActions()
			{
				return this.actions.filter((action) =>
					action.isVisible !== false && (!this.readonly || action.isInReadOnly)
				)
			},

			/**
			 * Computes the list of groups - groups from props + 1 group for actions without group
			 */
			groups()
			{
				return [...this.actionGroups, this.defaultGroup]
			},

			/**
			 * Computes the actions by each group id
			 */
			actionsByGroup()
			{
				return this.groups.reduce((acc, grp) => {
					acc[grp.id] = this.visibleActions.filter((act) => (act.group ?? 'default_group') === grp.id)
					return acc
				}, {})
			},

			/**
			 * Computes the list of groups to be displayed inline
			 */
			inlineGroups()
			{
				if (this.numberDropdownActions <= 1)
					return this.groups
				return this.groups.filter((grp) => grp.display !== 'dropdown')
			},

			/**
			 * Computes the number of actions to show inside the dropdown
			 */
			numberDropdownActions()
			{
				let dropdownActionsNum = 0
				this.groups
					.filter((grp) => grp.display === 'dropdown')
					.forEach((grp) => {
						dropdownActionsNum += this.actionsByGroup[grp.id].length
					})
				return dropdownActionsNum
			},

			/**
			 * Computes the list of groups that are to be shown in the dropdown
			 */
			dropdownGroups()
			{
				if (this.numberDropdownActions <= 1)
					return []

				return this.groups
					.filter((grp) => grp.display === 'dropdown')
					.map((grp) => ({ id: grp.id, title: grp.title }))
			},

			/**
			 * Computes the list of actions for the dropdown convert the visible actions
			 */
			dropdownActions()
			{
				return this.dropdownGroups.flatMap((grp) =>
					this.actionsByGroup[grp.id]?.map((act) => ({
						key: act.id,
						label: act.title,
						icon: act.icon,
						disabled: act.disabled,
						group: grp.id
					})) ?? []
				)
			}
		},

		methods: {
			/**
			 * Links the HTMLElement ref to the activator
			 */
			linkDropdownToButton()
			{
				this.dropdownActivator = this.$refs.dropdownActivator
			},

			/**
			 * Executes an action predefined behaviour
			 * @param action - the action to execute
			 */
			selectAction(actionID)
			{
				const action = this.visibleActions.find((act) => act.id === actionID)
				if (action)
					this.$emit('click:action', action)
			}
		}
	}
</script>
