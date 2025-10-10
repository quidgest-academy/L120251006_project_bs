<template>
	<div class="column-filter-header">
		<div class="column-filter-header__btns">
			<q-icon
				v-if="sortDirection"
				:icon="sortDirectionIcon" />
			<q-icon
				v-if="hasFilter"
				icon="filter" />
			<q-button
				:id="buttonId"
				ref="triggerBtn"
				:class="triggerBtnClasses"
				variant="text"
				size="small"
				borderless
				:title="texts.columnActionsText"
				:disabled="disabled"
				data-table-action-selected="false"
				tabindex="-1">
				<q-icon icon="expand" />
			</q-button>
		</div>
	</div>

	<q-popover
		:id="getTableColumnDropdownId(this.tableName, this.column.name)"
		class="column-filter-popover"
		v-model="showOverlay"
		:anchor="overlayAnchor"
		placement="bottom-start"
	>
		<div
			ref="contentRef"
			class="column-filter-form"
			tabindex="-1"
		>
			<div
				class="column-filter-form__header">
				<span>
					{{ texts.columnActionsText }}
				</span>
			</div>
			<div
				v-if="hasSort"
				class="column-filter-form__sort">
				<label>
					{{ texts.sortText }}
				</label>
				<q-toggle-group
					:model-value="sortDirection"
					class="column-filter-form__sort-btns">
					<q-toggle-group-item
						:id="getTableColumnDropdownSortAscId(this.tableName, this.column.name)"
						:label="texts.ascendingText"
						:title="texts.sortAscendingText"
						data-control-type="sort-asc"
						@click="sort('asc')">
						<q-icon icon="sort-ascending" />
					</q-toggle-group-item>
					<q-toggle-group-item
						:id="getTableColumnDropdownSortDescId(this.tableName, this.column.name)"
						:label="texts.descendingText"
						:title="texts.sortDescendingText"
						data-control-type="sort-desc"
						@click="sort('desc')">
						<q-icon icon="sort-descending" />
					</q-toggle-group-item>
				</q-toggle-group>
			</div>

			<!-- BEGIN: Column filter conditions -->
			<div
				v-if="hasSearch"
				class="search-filter-conds"
				:data-search-type="column.searchFieldType">
				<div
					v-for="(cond, conditionIdx) in editFilter.conditions"
					class="column-filter-condition"
					:key="conditionIdx">
					<div v-if="conditionIdx > 0">
						{{ texts.orText }}
					</div>

					<base-input-structure
						:id="operatorContainerId"
						data-control-type="operator">
						<q-select
							v-model="editFilter.conditions[conditionIdx].operator"
							size="block"
							:label="texts.showRecordsWhereText"
							:items="getFilterItems(conditionIdx)"
							:texts="texts"
							inline
							@update:model-value="setFilterDefaultValues(conditionIdx)" />
					</base-input-structure>

					<div>
						<component
							:is="getFilterInputComponent(conditionIdx)"
							v-for="(val, valueIdx) in getFilterValueCount(conditionIdx)"
							:key="getFilterInputKey(conditionIdx, valueIdx)"
							size="block"
							:table-name="tableName"
							:row-index="'h_' + conditionIdx + '_' + valueIdx"
							:column-name="getFilterColumnFromName(conditionIdx).name"
							:options="getFilterInputOptions(conditionIdx)"
							:classes="getFilterInputClass(conditionIdx)"
							:container-classes="['filter-value-container']"
							:value="editFilter.conditions[conditionIdx].values[valueIdx]"
							:raw-value="editFilter.conditions[conditionIdx].values[valueIdx]"
							:placeholder="getFilterPlaceholder(conditionIdx)"
							:error-messages="getValueErrorMessages(valueIdx)"
							:texts="texts"
							:locale="locale"
							data-control-type="value"
							@update="setFilterConditionValue(conditionIdx, valueIdx, $event)"
							@loaded="getFilterValueCount(conditionIdx)" />
					</div>
				</div>
				<div class="column-filter-actions">
					<q-button
						:id="getTableColumnDropdownSaveId(this.tableName, this.column.name)"
						variant="bold"
						size="small"
						:title="texts.activateFilterText"
						data-control-type="save"
						@click="saveFilter">
						<q-icon icon="ok" />
					</q-button>

					<q-button
						:id="getTableColumnDropdownRemoveId(this.tableName, this.column.name)"
						size="small"
						:title="texts.deactivateFilterText"
						data-control-type="remove"
						@click="removeFilter">
						<q-icon icon="remove" />
					</q-button>

					<q-button
						v-if="allowAdvancedFilters"
						:id="getTableColumnDropdownToAdvancedId(this.tableName, this.column.name)"
						size="small"
						:title="texts.moveToAdvancedFiltersText"
						data-control-type="move-to-advanced"
						@click="moveFilterToAdvancedFilters">
						<q-icon
							icon="advanced-filters"
							class="search-filters-icon" />
					</q-button>
				</div>
				<!-- END: Column filter conditions -->
			</div>
		</div>
	</q-popover>
</template>

<script>
	import cloneDeep from 'lodash-es/cloneDeep'

	import searchFilterDataModule from '@/api/genio/searchFilterData.js'
	import listFunctions from '@/mixins/listFunctions.js'

	import BaseInputStructure from '@/components/inputs/BaseInputStructure.vue'

	export default {
		name: 'QTableColumnFilters',

		emits: [
			'update-sort',
			'edit-column-filter',
			'remove-column-filter',
			'add-advanced-filter',
			'show-advanced-filters'
		],

		components: {
			BaseInputStructure
		},

		inheritAttrs: false,

		props: {
			/**
			 * Flag indicating whether column filters are allowed in the table.
			 */
			allowColumnFilters: {
				type: Boolean,
				default: false
			},

			/**
			 * Flag indicating whether column sorting is allowed in the table.
			 */
			allowColumnSort: {
				type: Boolean,
				default: false
			},

			/**
			 * Flag indicating whether advanced filters are allowed in the table.
			 */
			allowAdvancedFilters: {
				type: Boolean,
				default: false
			},

			/**
			 * Configuration object representing the column to which the sorting and filtering applies.
			 */
			column: {
				type: Object,
				required: true
			},

			/**
			 * A flag indicating whether the table is in a read-only state, which may affect the ability to filter or sort.
			 */
			disabled: {
				type: Boolean,
				default: false
			},

			/**
			 * An object representing the current filter settings for a particular column, if any.
			 */
			filter: {
				type: Object,
				default: () => ({})
			},

			/**
			 * A predefined set of filter operators used to construct the filter conditions.
			 */
			filterOperators: {
				type: Object,
				default: () => new searchFilterDataModule.SearchFilterConditionOperators()
			},

			/**
			 * The sorting direction, if any, of this column.
			 */
			sortDirection: {
				type: String,
				default: null
			},

			/**
			 * An array of columns that are searchable, used for setting up column-specific filters.
			 */
			searchableColumns: {
				type: Array,
				default: () => []
			},

			/**
			 * The unique name associated with the table instance.
			 */
			tableName: {
				type: String,
				default: ''
			},

			/**
			 * Object containing localized text strings for sorting and filtering related actions.
			 */
			texts: {
				type: Object,
				required: true
			},

			/**
			 * Current system locale
			 */
			locale: {
				type: String,
				default: 'en-US'
			}
		},

		expose: [],

		data()
		{
			return {
				showOverlay: false,
				editFilter: {},
				validationErrors: []
			}
		},

		mounted()
		{
			// Clear validation errors
			this.validationErrors = []
		},

		computed: {
			sortDirectionIcon()
			{
				return this.sortDirection === 'asc' ? 'sort-ascending' : 'sort-descending'
			},

			hasFilter()
			{
				return Object.keys(this.filter).length > 0
			},

			hasSearch() {
				return this.allowColumnFilters && this.isSearchableColumn(this.column)
			},

			hasSort() {
				return this.allowColumnSort && this.isSortableColumn(this.column)
			},

			triggerBtnClasses() {
				const classes = ['column-filter-btn__dropdown']
				if (this.showOverlay)
					classes.push('column-filter-btn__dropdown-open')

				return classes
			},

			buttonId() {
				return this.getTableColumnDropdownToggleId(this.tableName, this.column.name)
			},

			operatorContainerId() {
				return `${this.tableName}_${this.column.name}_filter_operator`
			},

			overlayAnchor() {
				return `#${this.buttonId}`
			}
		},

		methods: {
			getFilterOperatorOptions: listFunctions.getFilterOperatorOptions,
			getTableColumnDropdownId: listFunctions.getTableColumnDropdownId,
			getTableColumnDropdownToggleId: listFunctions.getTableColumnDropdownToggleId,
			getTableColumnDropdownSortAscId: listFunctions.getTableColumnDropdownSortAscId,
			getTableColumnDropdownSortDescId: listFunctions.getTableColumnDropdownSortDescId,
			getTableColumnDropdownSaveId: listFunctions.getTableColumnDropdownSaveId,
			getTableColumnDropdownRemoveId: listFunctions.getTableColumnDropdownRemoveId,
			getTableColumnDropdownToAdvancedId: listFunctions.getTableColumnDropdownToAdvancedId,

			/**
			 * Determine if column is sortable
			 * @param column {Object}
			 * @returns Boolean
			 */
			isSortableColumn(column)
			{
				return listFunctions.isSortableColumn(column)
			},

			/**
			 * Determine if column is searchable
			 * @param column {Object}
			 * @returns boolean
			 */
			isSearchableColumn(column)
			{
				return listFunctions.isSearchableColumn(column)
			},

			/**
			 * Full column name
			 * @param {object} column
			 */
			columnFullName(column)
			{
				return listFunctions.columnFullName(column)
			},

			/**
			 * Sort by column
			 * @param {string} order
			 */
			sort(order) {
				const newOrder = this.sortDirection === order ? undefined : order;
				this.$emit('update-sort', this.column.name, newOrder);
				this.closeOverlay()
			},

			getFilterItems(conditionIdx) {
				return this.getFilterOperatorOptions(this.editFilter, conditionIdx, this.filterOperators, this.searchableColumns)
			},

			/**
			 * Get the key of a filter input by its index
			 * @param {number} conditionIdx - condition index
			 * @param {number} valueIdx - value index
			 * @returns {string} The input's key
			 */
			getFilterInputKey(conditionIdx, valueIdx) {
				return this.editFilter.conditions[conditionIdx].field + '_' + valueIdx
			},

			getFilterInputOptions(conditionIdx) {
				return {
					...this.getFilterColumnFromName(conditionIdx),
					...{ keyIsValue: true },
					component: 'grid-base-input-structure',
					errorDisplayType: 'text'
				}
			},

			getFilterInputClass(conditionIdx) {
				const hasCurrency = this.getFilterColumnFromName(conditionIdx).currency !== undefined
				return hasCurrency ? [] : ['filter-input-field']

			},

			/**
			 * Get column of condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @param {Array} searchableColumns
			 * @returns {Object}
			 */
			getFilterColumnFromName(conditionIdx)
			{
				return listFunctions.getFilterColumnFromName(
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Get operators for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {Object}
			 */
			getFilterOperators(filter, conditionIdx, searchableColumns)
			{
				return listFunctions.getFilterOperators(
					this.filterOperators,
					filter,
					conditionIdx,
					searchableColumns
				)
			},

			/**
			 * Get number of values for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {Object}
			 */
			getFilterValueCount(conditionIdx)
			{
				return listFunctions.getFilterValueCount(
					this.filterOperators,
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Get input component for condition by index
			 * @param {number} conditionIdx : index
			 * @returns {string}
			 */
			getFilterInputComponent(conditionIdx)
			{
				return listFunctions.getFilterInputComponent(
					this.filterOperators,
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Get placeholder for condition input by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {string}
			 */
			getFilterPlaceholder(conditionIdx)
			{
				return listFunctions.getFilterPlaceholder(
					this.filterOperators,
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Select default operator for condition by index
			 * @param {number} conditionIdx : index
			 */
			setFilterDefaultOperator(conditionIdx)
			{
				return listFunctions.setFilterDefaultOperator(
					this.filterOperators,
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Set default values for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 */
			setFilterDefaultValues(conditionIdx)
			{
				return listFunctions.setFilterDefaultValues(
					this.filterOperators,
					this.editFilter,
					conditionIdx,
					this.searchableColumns
				)
			},

			/**
			 * Set value of condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @param {number} valueIdx : index
			 * @param {object} value : value
			 */
			setFilterConditionValue(conditionIdx, valueIdx, value)
			{
				return listFunctions.setFilterConditionValue(this.editFilter, conditionIdx, valueIdx, value)
			},

			/**
			 * Initialize new filter
			 */
			initNewFilter()
			{
				// Create new filter and add condition
				this.editFilter = listFunctions.searchFilter('', true, [])

				listFunctions.searchFilterAddCondition(
					this.editFilter,
					0,
					listFunctions.searchFilterCondition(
						'',
						true,
						listFunctions.columnFullName(this.column),
						'',
						[]
					)
				)
				this.setFilterDefaultOperator(0)
			},

			/**
			 * Initialize filter
			 */
			initFilter()
			{
				if (this.allowColumnFilters)
				{
					if (this.hasFilter)
						this.editFilter = cloneDeep(this.filter)
					else
						this.initNewFilter()
				}
			},

			/**
			 * Save filter
			 */
			saveFilter()
			{
				// Validate condition
				if (!this.validateFilter(this.editFilter, this.column))
					return

				// Save filter
				this.$emit(
					'edit-column-filter',
					listFunctions.columnFullName(this.column),
					this.editFilter
				)

				this.closeOverlay()
			},

			/**
			 * Remove filter
			 */
			removeFilter()
			{
				// Clear validation errors
				this.validationErrors = []
				// Remove filter
				this.$emit('remove-column-filter', listFunctions.columnFullName(this.column))

				this.closeOverlay()
			},

			/**
			 * Move filter to advanced filters
			 */
			moveFilterToAdvancedFilters()
			{
				// Validate condition
				if (!this.validateFilter(this.editFilter, this.column))
					return

				// Open advanced filters
				this.$emit('show-advanced-filters', -1, this.editFilter, listFunctions.columnFullName(this.column))

				this.closeOverlay()
			},

			/**
			 * Validates the filter
			 * @param {Array} filter
			 * @param {Array} column
			 * @returns {Boolean} Whether the filter is valid or not
			 */
			validateFilter(filter, column)
			{
				const validationErrors = []
				const conditionStates = listFunctions.filterValidate(filter, [column])
				let isValid = true

				if (conditionStates.length > 0)
				{
					const conditionState = conditionStates[0]
					if (conditionState.State === 'INVALID')
						isValid = false

					// Iterate values
					for (const valueIdx in conditionState.ValueStates)
					{
						const valueState = conditionState.ValueStates[valueIdx]
						let message = ''

						if (valueState === 'EMPTY')
							message = conditionState.Label + ' ' + this.texts.isRequired

						validationErrors.push({
							message: message
						})
					}
				}

				this.validationErrors = validationErrors
				return isValid
			},

			/**
			 * Get value field error messages
			 * @param {number} idx
			 * @returns {Array}
			 */
			getValueErrorMessages(idx)
			{
				const messages = this.validationErrors?.[idx]?.message
				return messages ? [messages] : []
			},

			closeOverlay() {
				this.showOverlay = false
			}
		},

		watch: {
			showOverlay(val) {
				if (val)
					this.initFilter()
			}
		}
	}
</script>
