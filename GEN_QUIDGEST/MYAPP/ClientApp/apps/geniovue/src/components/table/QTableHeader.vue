<template>
	<thead class="c-table__head">
		<tr
			ref="headerRowRef"
			v-bind="$attrs"
			:index="rowIndex">
			<slot
				name="columns"
				:columns="columns">
				<th
					v-for="column in columns"
					:key="column.name"
					:class="columnClasses(column)"
					:aria-sort="getTableColumnSort(column, columnSorting, true)"
					:data-column-name="column.name"
					@mousedown="onColumnMouseDown"
					@mousemove="onColumnMouseMove"
					@mouseup="onColumnMouseUp">
					<!-- BEGIN: TABLE LIST TOTALIZER TITLE COLUMN -->
					<div
						v-if="isTotalizerColumn(column)"
						class="column-header-content">
						<q-icon icon="sigma" />
					</div>
					<!-- BEGIN: FOR: TABLE LIST ROW ACTIONS -->
					<div
						v-else-if="isActionsColumn(column) || isDragAndDropColumn(column)"
						class="column-header-content">
						<q-icon icon="actions" />
						<span class="hidden-elem">{{ column.label }}</span>
					</div>
					<!-- END: FOR: TABLE LIST ROW ACTIONS -->
					<!-- BEGIN: Checklist header cell content -->
					<div
						v-else-if="isChecklistColumn(column)"
						class="column-header-content">
						<slot
							:name="'column_' + getCellSlotName(column)"
							:column="column">
							<q-action-list
								:disabled="readonly || rowCount < 1"
								:dropdown-options="dropdownOptions"
								:texts="texts"
								:actions="checklistActions"
								data-table-action-selected="false"
								tabindex="-1"
								@click:action="checklistAction">
								<template #customDropdownButton>
									<q-table-checklist-checkbox
										:value="false"
										:table-name="tableName" />
								</template>
							</q-action-list>
						</slot>
					</div>
					<!-- END: Checklist header cell content -->
					<!-- BEGIN: Extended row action column -->
					<div
						v-else-if="isExtendedActionsColumn(column)"
						class="extended-row-header">
						<slot
							:name="getCellSlotName(column)"
							:column="column">
							<span
								v-if="hasExtendedAction('remove-reset')"
								:key="column.name">
								<q-button
									:title="texts.resetText"
									data-table-action-selected="false"
									tabindex="-1"
									@click="$emit('unselect-all-rows')">
									<q-icon icon="reset" />
								</q-button>
							</span>
						</slot>
					</div>
					<!-- END: Extended row action column -->
					<!-- BEGIN: Header cell content -->
					<div
						v-else
						class="column-header-content">
						<!-- BEGIN: Header cell title -->
						<div class="column-header-text">
							<slot
								:name="'column_' + getCellSlotName(column)"
								:column="column">
								{{ column.label }}
							</slot>
							<q-table-column-filters
								v-if="((allowColumnFilters && isSearchableColumn(column)) || (allowColumnSort && isSortableColumn(column)))"
								:allow-column-filters="allowColumnFilters"
								:allow-column-sort="allowColumnSort"
								:allow-advanced-filters="allowAdvancedFilters"
								:column="column"
								:disabled="disabled"
								:filter="filters[columnFullName(column)]"
								:filter-operators="filterOperators"
								:searchable-columns="searchableColumns"
								:sort-direction="getTableColumnSort(column, columnSorting)"
								:table-name="tableName"
								:texts="texts"
								:locale="locale"
								@update-sort="(...args) => $emit('update-sort', ...args)"
								@edit-column-filter="
									(...args) => $emit('edit-column-filter', ...args)
								"
								@remove-column-filter="
									(...args) => $emit('remove-column-filter', ...args)
								"
								@add-advanced-filter="
									(...args) => $emit('add-advanced-filter', ...args)
								"
								@show-advanced-filters="
									(...args) => $emit('show-advanced-filters', ...args)
								" />
						</div>
						<!-- END: Header cell title -->
					</div>
					<!-- END: Header cell content -->
				</th>
			</slot>
		</tr>

		<tr
			v-if="props.loading"
			class="q-table-list-progress">
			<th :colspan="columns.length">
				<div class="q-table-list-progress__loader">
					<q-line-loader />
				</div>
			</th>
		</tr>
	</thead>
</template>

<script setup>
	// Components
	import QTableColumnFilters from './QTableColumnFilters.vue'

	// Utils
	import searchFilterDataModule from '@/api/genio/searchFilterData'
	import { getTableColumnSort } from '@/mixins/listFunctions.js'
	import has from 'lodash-es/has'
	import includes from 'lodash-es/includes'
	import { computed, inject, ref, useTemplateRef } from 'vue'

	defineOptions({
		name: 'QTableHeader',
		inheritAttrs: false
	})

	const emit = defineEmits([
		'column-resize',
		'update-sort',
		'unselect-all-rows',
		'edit-column-filter',
		'remove-column-filter',
		'add-advanced-filter',
		'show-advanced-filters',
		'check-all-rows',
		'check-current-page-rows',
		'check-none-rows'
	])

	const props = defineProps({
		/**
		 * Localized text strings to be used within the table header component.
		 */
		texts: {
			type: Object,
			required: true
		},

		/**
		 * An array containing column configurations, each object defines a column's properties in the table.
		 */
		columns: {
			type: Array,
			default: () => []
		},

		/**
		 * The object representing the current column sorting.
		 */
		columnSorting: {
			type: Object,
			default: () => ({})
		},

		/**
		 * The unique name associated with the table instance.
		 */
		tableName: {
			type: String,
			default: ''
		},

		/**
		 * Flag indicating whether the table is currently in read-only mode.
		 */
		readonly: {
			type: Boolean,
			default: false
		},

		/**
		 * Flag indicating whether filters are allowed on table columns.
		 */
		allowColumnFilters: {
			type: Boolean,
			default: false
		},

		/**
		 * Flag indicating whether sorting is allowed on table columns.
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
		 * An array of columns that can be used for search filtering.
		 */
		searchableColumns: {
			type: Array,
			default: () => []
		},

		/**
		 * The details of existing filters currently applied on the table columns.
		 */
		filters: {
			type: Object,
			default: () => ({})
		},

		/**
		 * A predefined set of operator definitions used in filter conditions.
		 */
		filterOperators: {
			type: Object,
			default: () => new searchFilterDataModule.SearchFilterConditionOperators()
		},

		/**
		 * The total count of rows in the table.
		 */
		rowCount: {
			type: Number,
			default: 0
		},

		/**
		 * Whether the table is loading.
		 */
		loading: {
			type: Boolean
		},

		/**
		 * Whether the header content is disabled.
		 */
		disabled: {
			type: Boolean,
			default: false
		},

		/**
		 * The row index. Can be a multi-index which has the index for each level (in tree tables) separated by underscores.
		 */
		rowIndex: {
			type: String,
			default: 'h'
		},

		/**
		 * Current system locale.
		 */
		locale: {
			type: String,
			default: 'en-US'
		}
	})

	const mouseDown = ref(false)
	const mouseMove = ref(false)

	// Template refs
	const headerRowRef = useTemplateRef('headerRowRef')

	const getCellSlotName = inject('getCellSlotName')
	const isSortableColumn = inject('isSortableColumn')
	const isSearchableColumn = inject('isSearchableColumn')
	const isActionsColumn = inject('isActionsColumn')
	const isChecklistColumn = inject('isChecklistColumn')
	const isDragAndDropColumn = inject('isDragAndDropColumn')
	const isExtendedActionsColumn = inject('isExtendedActionsColumn')
	const isTotalizerColumn = inject('isTotalizerColumn')
	const hasExtendedAction = inject('hasExtendedAction')
	const columnFullName = inject('columnFullName')

	const dropdownOptions = computed(() => ({
		icon: 'unchecked',
		borderless: true,
		variant: 'text',
		placement: 'bottom-start',
		class: 'q-dropdown-toggle'
	}))

	const checklistActions = computed(() => [
		{ id: 'all', title: props.texts.allRecordsText, icon: { icon: 'apply' } },
		{ id: 'page', title: props.texts.currentPageText, icon: { icon: 'check' } },
		{ id: 'none', title: props.texts.noneText, icon: { icon: 'remove' } }
	])

	/**
	 * Get CSS classes for this column
	 * @param column {Object}
	 * @returns String
	 */
	function columnClasses(column) {
		const classes = []

		const alignments = ['text-justify', 'text-right', 'text-left', 'text-center']
		if (
			has(column, 'columnTextAlignment') &&
			includes(alignments, column.columnTextAlignment)
		) {
			classes.push(column.columnTextAlignment)
		}

		if (has(column, 'columnHeaderClasses')) {
			classes.push(column.columnHeaderClasses)
		}

		return classes.join(' ')
	}

	/**
	 * Fired on mouse down on header element
	 */
	function onColumnMouseDown() {
		mouseDown.value = true
		mouseMove.value = false
	}

	/**
	 * Fired on mouse move on header element
	 */
	function onColumnMouseMove() {
		mouseMove.value = true
	}

	/**
	 * Fired on mouse up on header element
	 */
	function onColumnMouseUp() {
		if (mouseDown.value && mouseMove.value) {
			emit('column-resize')
		}
		mouseDown.value = false
		mouseMove.value = false
	}

	/**
	 * Executes the action selected in the dropdown
	 * @param $event the click event from the dropdown
	 */
	function checklistAction(event) {
		const action =
			event.id === 'all'
				? 'check-all-rows'
				: event.id === 'page'
					? 'check-current-page-rows'
					: 'check-none-rows'
		emit(action)
	}

	defineExpose({
		headerRowRef
	})
</script>
