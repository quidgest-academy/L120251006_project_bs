<template>
	<!-- BEGIN: Active Filters -->
	<div
		class="c-table__active-filters">
		<template v-for="(advancedFilter, advancedFilterIdx) in advancedFilters">
			<q-badge
				v-if="isValidFilter(advancedFilter, searchableColumns)"
				:key="advancedFilterIdx"
				:class="['q-table-list__filter', 'q-table-list__filter--advanced', { 'q-table-list__filter--inactive': !advancedFilter.active }]"
				pill
				variant="tonal"
				size="large"
				:title="advancedFilter.active ? getFilterName(filterOperators, advancedFilter, searchableColumns, texts.orText) : texts.inactiveFilterText"
				data-testid="advanced-filter"
				:data-column-id="advancedFilterIdx"
				@click="editAdvancedFilters(advancedFilterIdx)">
				<q-icon
					icon="advanced-filters"
					class="search-filters-icon" />
				{{ getFilterName(filterOperators, advancedFilter, searchableColumns, texts.orText) }}
				<q-icon icon="pencil" />
			</q-badge>
		</template>
		<template v-for="(columnFilter, columnFilterKey) in columnFilters">
			<q-badge
				v-if="isValidFilter(columnFilter, searchableColumns)"
				:key="columnFilterKey"
				class="q-table-list__filter"
				pill
				variant="tonal"
				size="large"
				color="primary"
				:title="getFilterName(filterOperators, columnFilter, searchableColumns, texts.orText)"
				data-testid="column-filter"
				:data-column-id="columnFilterKey"
				@click="removeColumnFilter(columnFilterKey)">
				{{ getFilterName(filterOperators, columnFilter, searchableColumns, texts.orText) }}
				<q-icon icon="remove" />
			</q-badge>
		</template>
		<template v-for="(searchBarFilter, searchBarFilterKey) in searchBarFilters">
			<q-badge
				v-if="isValidFilter(searchBarFilter, searchableColumns)"
				:key="searchBarFilterKey"
				class="q-table-list__filter"
				pill
				variant="tonal"
				size="large"
				color="primary"
				:title="getFilterName(filterOperators, searchBarFilter, searchableColumns, texts.orText)"
				data-testid="search-bar-filter"
				:data-column-id="searchBarFilterKey"
				@click="$emit('remove-search-bar-filter', searchBarFilterKey)">
				{{ getFilterName(filterOperators, searchBarFilter, searchableColumns, texts.orText) }}
				<q-icon icon="remove" />
			</q-badge>
		</template>
		<q-badge
			v-if="hasFiltersActive"
			class="q-table-list__filter"
			pill
			color="primary"
			variant="outlined"
			size="large"
			:title="texts.removeAllText"
			data-testid="remove-filter"
			@click="removeCustomFilters">
			{{ texts.removeAllText }}
		</q-badge>
	</div>
	<!-- END: Active Filters -->
</template>

<script>
	import listFunctions from '@/mixins/listFunctions.js'

	import searchFilterDataModule from '@/api/genio/searchFilterData'

	export default {
		name: 'QTableCurrentFilters',

		emits: [
			'signal-component',
			'show-advanced-filters',
			'remove-column-filter',
			'remove-search-bar-filter',
			'remove-custom-filters'
		],

		props: {
			/**
			 * Localized text strings to be used within the component for labels, titles, and accessibility.
			 */
			texts: {
				type: Object,
				required: true
			},

			/**
			 * Array of columns that can be searched, which determines which filters are applicable and how they are displayed.
			 */
			searchableColumns: {
				type: Array,
				default: () => []
			},

			/**
			 * Array of advanced filter objects, each containing specific filtering criteria intended for more complex queries.
			 */
			advancedFilters: {
				type: Array,
				default: () => []
			},

			/**
			 * Object where each key corresponds to a column's API field name, and its value is the filter applied to that column.
			 */
			columnFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Object mapping each field with a filter applied from the global search bar to its respective search criteria.
			 */
			searchBarFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Flag indicating whether any non-static filters are currently active, affecting the displayed set of data.
			 */
			hasFiltersActive: {
				type: Boolean,
				default: false
			},

			/**
			 * Predefined set of filter operators which describe how to apply filters to the data (e.g., 'equals', 'contains').
			 */
			filterOperators: {
				type: Object,
				default: () => new searchFilterDataModule.SearchFilterConditionOperators()
			}
		},

		expose: [],

		methods: {
			getFilterName: listFunctions.getFilterName,
			isValidFilter: listFunctions.isValidFilter,

			/**
			 * Edit advanced filters, highlighting selected filter
			 * @param {Number} advancedFilterIdx
			 */
			editAdvancedFilters(advancedFilterIdx)
			{
				this.$emit('signal-component', 'config', { show: true, selectedTab: 'advanced-filters' }, true)
				this.$emit('signal-component', 'advancedFilters', { selectedFilterIdx: advancedFilterIdx }, true)
			},

			/**
			 * Remove column filter
			 * @param {String} columnFilterKey
			 */
			removeColumnFilter(columnFilterKey)
			{
				this.$emit('remove-column-filter', columnFilterKey)
			},

			/**
			 * Remove custom filters
			 */
			removeCustomFilters()
			{
				this.$emit('remove-custom-filters')
			}
		}
	}
</script>
