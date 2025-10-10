<template>
	<!-- BEGIN: Advanced Filters Popup -->
	<teleport
		:to="`#q-modal-${modalId}-header`"
		:key="domKey"
		v-if="(showPopup || showInline) && showHeader">
		<div>
			<h2
				class="c-modal__header-title"
				:id="`q-modal-${modalId}_title`">
				{{ texts.advancedFiltersText }}
			</h2>
		</div>
	</teleport>

	<teleport
		:to="`#q-modal-${modalId}-body`"
		:key="domKey"
		v-if="(showPopup || showInline) && showBody">
		<template
			v-for="(editFilter, filterIdx) in editFilters"
			:key="filterIdx">
			<div
				v-if="isValidFilter(editFilter, searchableColumns)"
				:id="'filter_' + filterIdx">
				<hr v-if="filterIdx > 0" />
				<div :class="['search-filter-form', { 'selected-filter': selectedFilterIdx === filterIdx }]">
					<!-- BEGIN: Edit filter -->
					<!-- BEGIN: Filter name, state, delete -->
					<q-row>
						<q-col>
							<q-switch
								v-model="editFilter.active"
								size="small"
								show-state-labels
								:true-label="texts.activeText"
								:false-label="texts.inactiveText" />
						</q-col>
					</q-row>
					<!-- END: Filter name, state, delete -->
					<div class="search-filter-conds no-gutters">
						<!-- BEGIN: Conditions -->
						<template
							v-for="(_, conditionIdx) in editFilter.conditions"
							:key="conditionIdx">
							<div
								v-if="isValidFilterCondition(editFilter, conditionIdx, searchableColumns)"
								class="row no-gutters mb-2">
								<div class="col-12">
									<label v-if="getValidFilterConditionRelativeIndex(editFilter, conditionIdx, searchableColumns) === 0">{{ texts.createFilterText }}</label>
									<label v-else>{{ texts.orText }}</label>
								</div>
								<div class="filter-input-container">
									<base-input-structure
										:id="getFilterIdBase(filterIdx, conditionIdx) + '_field'"
										data-control-type="field">
										<q-select
											v-model="editFilter.conditions[conditionIdx].field"
											size="large"
											:items="searchableColumnOptions"
											:texts="texts"
											@update:model-value="setFilterDefaultOperator(editFilter, conditionIdx, searchableColumns)" />
									</base-input-structure>
								</div>

								<div class="filter-input-container">
									<base-input-structure
										:id="getFilterIdBase(filterIdx, conditionIdx) + '_operator'"
										data-control-type="operator">
										<q-select
											v-model="editFilter.conditions[conditionIdx].operator"
											size="large"
											:items="getFilterOperatorOptions(editFilter, conditionIdx, filterOperators, searchableColumns)"
											:texts="texts"
											@update:model-value="setFilterDefaultValues(editFilter, conditionIdx, searchableColumns)" />
									</base-input-structure>
								</div>

								<div
									class="filter-input-container"
									:data-search-type="getFilterColumnFromName(editFilter, conditionIdx, searchableColumns)?.searchFieldType">
									<base-input-structure
										:id="getFilterIdBase(filterIdx, conditionIdx) + '_value'"
										data-control-type="value">
										<component
											:is="getFilterInputComponent(editFilter, conditionIdx, searchableColumns)"
											v-for="(__, valueIdx) in getFilterValueCount(editFilter, conditionIdx, searchableColumns)"
											:key="editFilter.conditions[conditionIdx].field + '_' + valueIdx"
											:table-name="tableName + '_filters'"
											:row-index="filterIdx"
											:column-name="conditionIdx + '_' + valueIdx"
											:options="{
												...getFilterColumnFromName(editFilter, conditionIdx, searchableColumns),
												...{ keyIsValue: true },
												component: 'grid-base-input-structure',
												errorDisplayType: 'text',
												teleport: true
											}"
											:classes="[
												getFilterColumnFromName(editFilter, conditionIdx, searchableColumns).currency !== undefined
													? ''
													: 'filter-input-field'
											]"
											:container-classes="['filter-value-container']"
											size="large"
											:value="editFilter.conditions[conditionIdx].values[valueIdx]"
											:raw-value="editFilter.conditions[conditionIdx].values[valueIdx]"
											:placeholder="getFilterPlaceholder(editFilter, conditionIdx, searchableColumns)"
											:error-messages="getFilterValueErrorMessages(filterIdx, conditionIdx, valueIdx)"
											:texts="texts"
											:locale="locale"
											@update="setFilterConditionValue(editFilter, conditionIdx, valueIdx, $event)">
										</component>
									</base-input-structure>
								</div>

								<div class="filter-input-container">
									<q-button
										:title="texts.removeConditionText"
										:disabled="getValidFilterConditionCount(editFilter, searchableColumns) < 2"
										@click="removeCondition(editFilter, conditionIdx)">
										<q-icon icon="remove" />
									</q-button>
								</div>
							</div>
						</template>
						<!-- END: Conditions -->
					</div>
					<div class="actions">
						<q-button
							:label="texts.createConditionText"
							:title="texts.createConditionText"
							@click="addCondition(editFilter, editFilter.conditions.length, searchableColumns)">
							<q-icon icon="add" />
						</q-button>
						<q-button
							v-if="mode !== 'new'"
							:title="texts.deleteFilterText"
							:label="texts.deleteFilterText"
							@click="removeFilter(filterIdx)">
							<q-icon icon="remove" />
						</q-button>
					</div>
					<!-- END: Edit filter -->
				</div>
			</div>
		</template>
	</teleport>

	<teleport
		:to="`#q-modal-${modalId}-footer`"
		:key="domKey"
		v-if="(showPopup || showInline) && showFooter">
		<div class="actions float-right">
			<q-button
				variant="bold"
				:title="mode === 'new' ? texts.applyFilterText : texts.applyFiltersText"
				:label="mode === 'new' ? texts.applyFilterText : texts.applyFiltersText"
				data-control-type="save"
				@click="saveFilters(editFilters)">
				<q-icon icon="ok" />
			</q-button>

			<q-button
				v-if="mode === 'editAll'"
				:title="texts.deleteFiltersText"
				:label="texts.deleteFiltersText"
				@click="removeFilters">
				<q-icon icon="remove" />
			</q-button>

			<q-button
				@click="fnHidePopup()"
				:title="texts.cancelText"
				:label="texts.cancelText"
				data-control-type="cancel">
				<q-icon icon="cancel" />
			</q-button>
		</div>
	</teleport>
	<!-- END: Advanced Filters Popup -->
</template>

<script>
	import cloneDeep from 'lodash-es/cloneDeep'

	import searchFilterDataModule from '@/api/genio/searchFilterData.js'
	import listFunctions from '@/mixins/listFunctions.js'
	import BaseInputStructure from '@/components/inputs/BaseInputStructure.vue'

	export default {
		name: 'QTableAdvancedFilters',

		emits: [
			'show-popup',
			'hide-popup',
			'add-advanced-filter',
			'edit-advanced-filters',
			'set-advanced-filter-state',
			'remove-advanced-filter',
			'remove-all-advanced-filters',
			'remove-column-filter'
		],

		components: {
			BaseInputStructure
		},

		inheritAttrs: false,

		props: {
			/**
			 * An object containing signals that can trigger different actions within the component.
			 */
			signal: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The identifier for the modal element that wraps the advanced filters popup.
			 */
			modalId: {
				type: String,
				required: true
			},

			/**
			 * Localized text strings to display within the popup, such as buttons and titles.
			 */
			texts: {
				type: Object,
				required: true
			},

			/**
			 * The name of the table or data set these filters are targeting.
			 */
			tableName: {
				type: String,
				required: true
			},

			/**
			 * An array of column definitions which the advanced filters can be applied to.
			 */
			columns: {
				type: Array,
				default: () => []
			},

			/**
			 * An array of filter objects that represents the advanced filters' current configuration.
			 */
			filters: {
				type: Array,
				default: () => []
			},

			/**
			 * A set of predefined filter operators used in building the filter conditions (e.g., 'contains', 'equals').
			 */
			filterOperators: {
				type: Object,
				default: () => new searchFilterDataModule.SearchFilterConditionOperators()
			},

			/**
			 * The mode of the popup, which can be 'new' for adding new filters or 'editAll' for modifying existing filters.
			 */
			mode: {
				type: String,
				default: 'new'
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

		data() {
			return {
				showPopup: false,
				showInline: false,
				showHeader: true,
				showBody: true,
				showFooter: true,
				domKey: 0,
				editFilters: [],
				selectedFilterIdx: null,
				validationErrorFieldIndex: []
			}
		},

		mounted() {
			this.updateFilters()
		},

		computed: {
			searchableColumns() {
				return listFunctions.getSearchableColumns(this.columns)
			},

			searchableColumnOptions() {
				return listFunctions.getSearchableColumnOptions(this.searchableColumns)
			},

			validationErrorMessages() {
				return this.validationErrorFieldIndex.map((x) => x.message)
			}
		},

		methods: {
			getFilterOperatorOptions: listFunctions.getFilterOperatorOptions,
			isValidFilterCondition: listFunctions.isValidFilterCondition,
			isValidFilter: listFunctions.isValidFilter,
			getValidFilterConditionRelativeIndex: listFunctions.getValidFilterConditionRelativeIndex,
			getValidFilterConditionCount: listFunctions.getValidFilterConditionCount,

			//Show popup
			fnShowPopup() {
				this.$emit('show-popup', this.modalId)
				this.$nextTick().then(() => {
					this.showPopup = true
					this.domKey++
					this.initValidation()
					if (this.mode === 'new') {
						this.selectNewFilter()
					}
				})
			},

			//Hide popup
			fnHidePopup() {
				this.$emit('hide-popup', this.modalId)
			},

			/**
			 * Full column name
			 * @param {object} column
			 */
			columnFullName(column) {
				return listFunctions.columnFullName(column)
			},

			/**
			 * Get column of condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @param {Array} searchableColumns
			 * @returns {Object}
			 */
			getFilterColumnFromName(filter, conditionIdx, searchableColumns) {
				return listFunctions.getFilterColumnFromName(filter, conditionIdx, searchableColumns)
			},

			/**
			 * Get operators for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {Object}
			 */
			getFilterOperators(filter, conditionIdx, searchableColumns) {
				return listFunctions.getFilterOperators(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Get number of values for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {Object}
			 */
			getFilterValueCount(filter, conditionIdx, searchableColumns) {
				return listFunctions.getFilterValueCount(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Get input component for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {string}
			 */
			getFilterInputComponent(filter, conditionIdx, searchableColumns) {
				return listFunctions.getFilterInputComponent(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Get placeholder for condition input by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @returns {string}
			 */
			getFilterPlaceholder(filter, conditionIdx, searchableColumns) {
				return listFunctions.getFilterPlaceholder(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Select default operator for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 */
			setFilterDefaultOperator(filter, conditionIdx, searchableColumns) {
				return listFunctions.setFilterDefaultOperator(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Set default values for condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 */
			setFilterDefaultValues(filter, conditionIdx, searchableColumns) {
				return listFunctions.setFilterDefaultValues(this.filterOperators, filter, conditionIdx, searchableColumns)
			},

			/**
			 * Set value of condition by index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @param {number} valueIdx : index
			 * @param {object} value : value
			 */
			setFilterConditionValue(filter, conditionIdx, valueIdx, value) {
				return listFunctions.setFilterConditionValue(filter, conditionIdx, valueIdx, value)
			},

			/**
			 * Set filter data in internal property for editing
			 */
			updateFilters() {
				if (this.mode === 'new') {
					this.selectNewFilter()
				} else {
					this.editFilters = cloneDeep(this.filters)
					if (this.signal.columnFilter)
						this.editFilters.push(this.signal.columnFilter)
				}
			},

			/**
			 * Initialize new filter
			 */
			initNewFilter() {
				this.editFilters = []
				//Create new filter
				this.editFilters.push(listFunctions.searchFilter('', true, []))
				//Add first condition (each filter must have at least 1 condition)
				this.addCondition(this.editFilters[0], 0, this.searchableColumns)
			},

			/**
			 * Save filters
			 * @param {Array} filters
			 */
			saveFilters(filters) {
				//Validate filters
				this.validationErrorFieldIndex = this.getValidationErrorFieldIndex(filters, this.columns)
				//Not all filters are valid, don't save
				if (this.validationErrorFieldIndex.length > 0) {
					return
				}

				//If there is a column filter being turned into an advanced filter, remove the column filter
				if (this.signal.columnName) {
					this.$emit('remove-column-filter', this.signal.columnName)
				}

				//Filters are valid, save
				//If adding new filter
				if (this.mode === 'new') {
					this.$emit('add-advanced-filter', filters[0])
					this.selectNewFilter()
				}
				//If saving existing filters
				else {
					this.$emit('edit-advanced-filters', filters)
				}

				this.fnHidePopup()
			},

			/**
			 * Set advanced filter state
			 * @param {number} selectedFilterIdx : index
			 * @param {boolean} active : active state
			 */
			setFilterState(selectedFilterIdx, active) {
				//Set filter state
				this.$emit('set-advanced-filter-state', [selectedFilterIdx, active])

				this.selectNewFilter()
			},

			/**
			 * Remove advanced filter
			 * @param {number} selectedFilterIdx : index
			 */
			removeFilter(selectedFilterIdx) {
				if (selectedFilterIdx === undefined || selectedFilterIdx === null) {
					return
				}

				this.editFilters.splice(selectedFilterIdx, 1)
			},

			/**
			 * Remove filters
			 * @param {Array} filters
			 */
			removeFilters() {
				this.$emit('remove-all-advanced-filters')
				this.fnHidePopup()
			},

			/**
			 * Add condition at index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 * @param {Array} searchableColumns
			 */
			addCondition(filter, conditionIdx, searchableColumns) {
				listFunctions.searchFilterAddCondition(
					filter,
					conditionIdx,
					listFunctions.searchFilterCondition('', true, listFunctions.columnFullName(this.searchableColumns[0]), '', [])
				)
				this.setFilterDefaultOperator(filter, conditionIdx, searchableColumns)
			},

			/**
			 * Remove condition at index
			 * @param {Object} filter
			 * @param {number} conditionIdx : index
			 */
			removeCondition(filter, conditionIdx) {
				listFunctions.searchFilterRemoveCondition(filter, conditionIdx)
			},

			/**
			 * Get formatted string representing filter name for a table cell.
			 * @param table {Object}
			 * @param row {Object}
			 * @param column {Object}
			 * @param options {Object}
			 * @returns String
			 */
			filterNameDisplayCell(table, row, column, options) {
				const value = listFunctions.textDisplayCell(table, row, column, options)
				if (value.length > 0) {
					return value
				}
				return listFunctions.getFilterName(this.filterOperators, row.Fields, this.searchableColumns, this.texts.orText)
			},

			/**
			 * Select filter to edit
			 * @param {string} rowKey : rowKey
			 * @returns {string}
			 */
			selectFilter(rowKey) {
				this.editFilter = cloneDeep(this.filters[rowKey])
				this.selectedFilterIdx = rowKey

				this.$nextTick().then(() => {
					//Scroll to filter controls
					const filterForm = document.getElementById('filter_' + this.selectedFilterIdx)
					if (filterForm) {
						filterForm.scrollIntoView({ block: 'end', inline: 'nearest' })
					}
				})
			},

			/**
			 * Unselect filters and set new filter as filter to edit
			 * @returns {string}
			 */
			selectNewFilter() {
				this.selectedFilterIdx = null
				this.initNewFilter()
			},

			/**
			 * Custom actions
			 * @param {Object} emitAction
			 * @returns {string}
			 */
			executeAction(emitAction) {
				if (emitAction.id === 'insert') {
					this.selectNewFilter()
				}
			},

			/**
			 * Initialize validation
			 */
			initValidation() {
				this.validationErrorFieldIndex = []
			},

			/**
			 * Get validation error information
			 * @param {Array} filters
			 * @param {Array} columns
			 * @returns {Array}
			 */
			getValidationErrorFieldIndex(filters, columns) {
				const validationErrorFieldIndex = []
				let conditionStates = []
				//Iterate filters
				for (let filterIdx = 0; filterIdx < filters.length; filterIdx++) {
					const filter = filters[filterIdx]
					conditionStates = listFunctions.filterValidate(filter, columns)
					//Iterate filter conditions
					for (const conditionIdx in conditionStates) {
						const conditionState = conditionStates[conditionIdx]
						if (conditionState.State !== 'VALID') {
							//Iterate values
							for (const valueIdx in conditionState.ValueStates) {
								const valueState = conditionState.ValueStates[valueIdx]
								if (valueState !== 'VALID') {
									validationErrorFieldIndex.push({
										elemId: this.tableName + '_filters_' + filterIdx + '_' + conditionIdx + '_' + valueIdx,
										fieldType: conditionState.Type,
										message: conditionState.Label + ' ' + this.texts.isRequired
									})
								}
							}
						}
					}
				}
				return validationErrorFieldIndex
			},

			/**
			 * Get value field error messages
			 * @param {number} filterIdx
			 * @param {number} conditionIdx
			 * @param {number} valueIdx
			 * @returns {Array}
			 */
			getFilterValueErrorMessages(filterIdx, conditionIdx, valueIdx) {
				const errors = this.validationErrorFieldIndex.filter(
					(error) => error.elemId === this.tableName + '_filters_' + filterIdx + '_' + conditionIdx + '_' + valueIdx
				)
				return errors.map((error) => error.message)
			},

			/**
			 * Focus on a field by error index
			 * @param {string} id
			 */
			focusErrorField(id) {
				const fieldInfo = this.validationErrorFieldIndex[id]
				if (!fieldInfo) return

				let elemId = fieldInfo.elemId
				if (fieldInfo.fieldType === 'Date') {
					elemId = 'dp-input-' + elemId
				}
				const elem = document.getElementById(elemId)
				if (!elem) return

				elem.focus()
			},

			/**
			 * Get the base part of a filter control ID.
			 * @param {number} filterIdx
			 * @param {number} conditionIdx
			 * @returns {string}
			 */
			getFilterIdBase(filterIdx, conditionIdx) {
				return 'filter_' + filterIdx + '_' + conditionIdx
			}
		},

		watch: {
			signal: {
				handler(newValue) {
					for (const key in newValue) {
						switch (key) {
							case 'show':
								if (newValue.show) {
									this.fnShowPopup()
								}
								break
							case 'selectedFilterIdx':
								if (newValue.selectedFilterIdx !== null) {
									if (newValue.selectedFilterIdx < 0) {
										this.selectFilter(this.editFilters.length + newValue.selectedFilterIdx)
									} else {
										this.selectFilter(newValue.selectedFilterIdx)
									}
								}
								break
							case 'columnFilter':
								this.updateFilters()
								break;
							default:
								if (['showInline', 'showHeader', 'showBody', 'showFooter'].includes(key)) {
									this[key] = newValue[key]
								}
								break
						}
					}
					if (!this.editFilters) {
						this.selectNewFilter()
					}
				},
				deep: true
			},

			searchableColumns: {
				handler() {
					if (this.mode === 'new') {
						this.selectNewFilter()
					}
				},
				deep: true
			},

			filters: {
				handler() {
					this.updateFilters()
				},
				deep: true
			},

			showInline: {
				handler(newValue) {
					if (newValue === true) {
						this.initValidation()
					}
				},
				deep: true
			}
		}
	}
</script>
