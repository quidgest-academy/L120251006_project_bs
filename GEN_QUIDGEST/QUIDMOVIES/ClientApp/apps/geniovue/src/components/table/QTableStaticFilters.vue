<template>
	<!-- BEGIN: Static Filters -->
	<div
		:id="`${menuName}_filters`"
		class="filters-container">
		<!-- BEGIN: Active Filters -->
		<q-row
			v-if="hasActiveFilters"
			align="center"
			:gutter="4"
			:data-testid="`${menuName}_active_filters`">
			<!-- BEGIN: Checkbox options -->
			<q-col>
				<span>{{ texts.state }}:</span>
			</q-col>
			<q-col
				v-for="(filter, filterId) in activeFilters.options"
				:key="filterId">
				<q-checkbox
					:model-value="filter.selected"
					size="small"
					:id="filter.id"
					:label="filter.value"
					:disabled="disabled"
					@update:model-value="updateActiveFilterSelected(filterId, $event)" />
			</q-col>
			<!-- END: Checkbox options -->
			<!-- BEGIN: Date -->
			<q-col>
				<div class="q-table-static-filters__date">
					<q-label :for="dateValueFilter.id">
						{{ texts.onDate }}
					</q-label>
					<q-date-time-picker
						:id="dateValueFilter.id"
						:model-value="dateValueFilter.value"
						:size="dateValueFilter.type === 'date' ? 'small' : 'medium'"
						:format="dateFormat"
						:locale="locale"
						:disabled="disabled"
						@update:model-value="updateActiveFilterDateValue" />
				</div>
			</q-col>
			<!-- END: Date -->
		</q-row>
		<!-- END: Active Filters -->

		<!-- BEGIN: Group Filters -->
		<q-row
			v-for="(entry, groupIndex) in groupFilters"
			:key="groupIndex"
			class="static-filter-box"
			:data-testid="`${menuName}_group_filters`">
			<!-- BEGIN: Checkbox options -->
			<template v-if="groupFilterIsMultiple(entry)">
				<q-col
					v-for="(filter, filterIndex) in entry.filters"
					:key="filterIndex"
					cols="auto">
					<q-checkbox
						:id="filter.id"
						:model-value="filter.selected"
						size="small"
						:label="filter.value"
						:disabled="disabled"
						@update:model-value="updateGroupFilterSelected(groupIndex, filterIndex, $event)" />
				</q-col>
			</template>
			<!-- END: Checkbox options -->
			<!-- BEGIN: Radio button options -->
			<q-col v-else>
				<q-radio-group
					:model-value="entry.value"
					orientation="horizontal"
					:disabled="disabled"
					@update:model-value="updateGroupFilterValue(groupIndex, $event)">
					<q-radio-button
						v-for="radio in entry.filters"
						:key="radio.key"
						:value="radio.key"
						:label="radio.value" />
				</q-radio-group>
			</q-col>
			<!-- END: Radio button options -->
		</q-row>
		<!-- END: Group Filters -->
	</div>
	<!-- END: Static Filters -->
</template>

<script>
	export default {
		name: 'QTableStaticFilters',

		emits: {
			'update:activeFilters': (payload) => typeof payload === 'object',
			'update:groupFilters': (payload) => Array.isArray(payload)
		},

		inheritAttrs: false,

		props: {
			/**
			 * The unique name identifying this specific set of filters, typically associated with a table or view.
			 */
			menuName: {
				type: String,
				default: ''
			},

			/**
			 * An object representing active filters, which can be a mixture of various filter types including boolean or date.
			 */
			activeFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * An array representing groups of filters that apply globally to the data set, affecting all columns.
			 */
			groupFilters: {
				type: Array,
				default: () => []
			},

			/**
			 * Localization or custom text strings that are used within the static filters interface, aiding in text consistency and localization.
			 */
			texts: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Current system locale
			 */
			locale: {
				type: String,
				default: 'en-US'
			},

			/**
			 * Date formats
			 */
			dateFormats: {
				type: Object,
				required: true
			},

			/** Whether the control is disabled. */
			disabled: {
				type: Boolean,
				default: false
			}
		},

		expose: [],

		computed: {
			/**
			 * The date filter.
			 */
			dateValueFilter()
			{
				return !this.hasActiveFilters ? { value: '' } : this.activeFilters.dateValue
			},

			/**
			 * The date format.
			 */
			dateFormat()
			{
				return this.dateFormats[this.dateValueFilter.type]
			},


			/**
			 * True if there's an active filter, false otherwise.
			 */
			hasActiveFilters()
			{
				return Object.keys(this.activeFilters).length > 0
			}
		},

		methods: {
			/**
			 * Determine if multiple filters can be selected in group of filters.
			 * @param entry {Object}
			 * @returns Boolean
			 */
			groupFilterIsMultiple(entry)
			{
				return entry.isMultiple === true
			},

			/**
			 * Emit new active filters value.
			 * @param value {Object}
			 */
			emitActiveFilter(value)
			{
				this.$emit('update:activeFilters', value)
			},

			/**
			 * Emit new group filters value.
			 * @param value {Object}
			 */
			emitGroupFilter(value)
			{
				this.calcGroupFilterValues(value)
				this.$emit('update:groupFilters', value)
			},

			/**
			 * Update active filters.
			 * @param filterId {String}
			 * @param value {Boolean}
			 */
			updateActiveFilterSelected(filterId, value)
			{
				const options = { ...this.activeFilters.options }
				options[filterId].selected = value

				const activeFilters = {
					...this.activeFilters,
					options
				}

				this.emitActiveFilter(activeFilters)
			},

			/**
			 * Update active filters.
			 * @param value {Object}
			 */
			updateActiveFilterDateValue(value)
			{
				const dateValue = { ...this.activeFilters.dateValue }
				dateValue.value = value

				const activeFilters = {
					...this.activeFilters,
					dateValue
				}

				this.emitActiveFilter(activeFilters)
			},

			/**
			 * Update group filters.
			 * @param groupIndex {Int}
			 * @param filterIndex {Int}
			 * @param value {Object}
			 */
			updateGroupFilterSelected(groupIndex, filterIndex, value)
			{
				const filters = [...this.groupFilters[groupIndex].filters]
				filters[filterIndex] = {
					...filters[filterIndex],
					selected: value
				}

				const groupFilters = [...this.groupFilters]
				groupFilters[groupIndex] = {
					...groupFilters[groupIndex],
					filters
				}

				this.emitGroupFilter(groupFilters)
			},

			/**
			 * Update group filters.
			 * @param groupIndex {Int}
			 * @param value {Object}
			 */
			updateGroupFilterValue(groupIndex, value)
			{
				const groupFilters = [...this.groupFilters]
				groupFilters[groupIndex] = {
					...groupFilters[groupIndex],
					value
				}

				this.emitGroupFilter(groupFilters)
			},

			/**
			 * Get value for all radio button groups based on filter model.
			 * @param groupFilters {Object}
			 */
			calcGroupFilterValues(groupFilters)
			{
				for (const entry of groupFilters)
				{
					if (entry.isMultiple === true && typeof entry.value === 'string')
						entry.value = this.checkboxValue(entry)
				}
			},

			/**
			 * Get value for radio button group based on filter model.
			 * @param entry {Object}
			 * @returns String
			 */
			checkboxValue(entry)
			{
				if (!entry.isMultiple)
					return ''

				let multipleFilterValue = ''

				for (const filter of entry.filters)
				{
					if (filter.selected !== false)
						multipleFilterValue += filter.key
				}

				return multipleFilterValue
			}
		}
	}
</script>
