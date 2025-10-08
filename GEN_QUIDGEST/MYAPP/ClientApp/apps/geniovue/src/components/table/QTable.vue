<template>
	<!-- TODO configurable header title position -->
	<div
		:id="controlId"
		:class="['q-table-list', ...classes, sizeClass, $attrs.class, isListVisible ? tableModeClasses : null, { 'q-table-list--loading': !loaded }]"
		:data-loading="!loaded">
		<div
			v-show="showHeader"
			class="page-header row no-gutters justify-content-between">
			<!-- BEGIN: Table controls -->
			<div
				v-if="tableTitle.length > 0 || hasActionBar || $slots.tableTitle"
				class="c-action-bar">
				<!-- BEGIN: Table title -->
				<div class="table-title c-table__title">
					<component
						v-if="hasLabel"
						:is="headerTag"
						:id="labelId">
						<slot
							name="tableTitle"
							:table-title="tableTitle">
							{{ tableTitle }}
						</slot>
					</component>
					<q-popover-help
						v-if="popoverText"
						:help-control="helpControl"
						:id="id"
						:label="tableTitle"
						:texts="texts" />
				</div>
				<!-- END: Table title -->
				<!-- BEGIN: Extra menus -->
				<div
					v-if="hasActionBar"
					class="c-action-bar__menu">
					<!-- BEGIN: Saved views menu -->
					<q-select
						v-if="showSavedViews"
						:model-value="selectedViewId"
						:items="savedViewsOptions"
						:texts="texts"
						:groups="[
							{ id: 'user', title: '' },
							{ id: 'system', title: '' }
						]"
						size="small"
						item-value="id"
						item-label="text"
						@update:model-value="confirmAndSetSelectedViewById" />
					<!-- END: Saved views menu -->
					<!-- BEGIN: Configuration menu / button -->
					<q-dropdown-menu
						v-if="showConfigMenu"
						:id="configMenuId"
						:texts="{ title: texts.tableConfig }"
						:options="configOptionsUse"
						:button-options="{ borderless: true }"
						:button-classes="['dropdown-toggle']"
						@selected="emitConfigAction">
						<component
							:is="confirmChanges ? 'q-badge-indicator' : 'v-fragment'"
							color="highlight">
							<q-icon icon="table-configuration" />
						</component>
					</q-dropdown-menu>
					<!-- END: Configuration menu / button -->
					<!-- BEGIN: Toggle show/hide filters -->
					<q-switch
						v-if="hasFilters"
						:model-value="filtersVisible"
						size="small"
						:label="texts.activeFiltersTitle"
						:true-label="texts.showText"
						:false-label="texts.hideText"
						show-state-labels
						@update:model-value="toggleShowFilters" />
					<!-- END: Toggle show/hide filters -->
					<q-table-view-mode-config
						:model-value="$props.activeViewModeId"
						:view-modes="$props.viewModes"
						:texts="$props.texts"
						@update:model-value="(newVal) => emitEvent('update:active-view-mode', newVal)" />
					<!-- BEGIN: Row reorder toggle -->
					<q-button
						v-if="showRowDragAndDropOption && !readonly"
						:id="controlId + '-row-reorder-btn'"
						borderless
						:title="texts.rowDragAndDropTitle"
						@click="toggleShowRowDragAndDrop">
						<q-icon icon="reorder" />
					</q-button>
					<!-- END: Row reorder toggle -->
					<q-button-group>
						<!-- BEGIN: Export menu -->
						<q-table-export
							v-if="allowFileExport"
							:options="exportOptions"
							:texts="texts"
							@export-data="exportData">
						</q-table-export>
						<!-- END: Export menu -->
						<!-- BEGIN: Import menu -->
						<q-table-import
							v-if="allowFileImport"
							modal-id="data-import"
							:options="importOptions"
							:template-options="importTemplateOptions"
							:data-import-response="dataImportResponse"
							:server-mode="serverMode"
							:texts="texts"
							@import-data="importData"
							@show-import-popup="emitEvent('show-popup', importModalProps)"
							@hide-import-popup="emitEvent('hide-popup', 'data-import')"
							@export-template="(format) => exportTemplate(format)">
						</q-table-import>
						<!-- END: Import menu -->
					</q-button-group>
				</div>
				<!-- END: Extra menus -->
			</div>
			<!-- END: Table controls -->
			<template v-if="showSearchBar">
				<!-- BEGIN: Global search text -->
				<q-table-search
					v-if="searchBarConfig.visibility"
					:table-title="tableTitle"
					:searchable-columns="searchableColumns"
					:placeholder="`${texts.searchText} ${defaultSearchColumnLabel}`"
					:search-prop-value="searchValue"
					:classes="searchBarConfig.classes"
					:case-sensitive="searchBarConfig.caseSensitive"
					:search-on-press-enter="searchBarConfig.searchOnPressEnter"
					:search-debounce-rate="searchBarConfig.searchDebounceRate"
					:show-refresh-button="searchBarConfig.showRefreshButton"
					:show-reset-button="searchBarConfig.showResetButton"
					:texts="texts"
					:disabled="!loaded"
					:message="searchBarConfig.message"
					:signal="signalSearch"
					@set-message="(...args) => emitEvent('set-property', ['config', 'searchBarConfig', 'message'], ...args)"
					@clear-global-search="clearGlobalSearch"
					@search-by-column="searchByColumn"
					@search-by-all-columns="searchByAllColumns"
					@emit-search="searchByColumn(this.defaultSearchColumn, $event)"
					@reset-query="resetQuery">
					<template #extra-buttons>
						<!-- BEGIN: Advanced Filters -->
						<q-button
							v-if="allowAdvancedFilters"
							:title="texts.advancedFiltersText"
							:disabled="!loaded"
							data-action-key="add-advanced-filter"
							@click="showAdvancedFiltersNew">
							<q-icon
								icon="advanced-filters"
								class="search-filters-icon" />
						</q-button>
						<!-- END: Advanced Filters -->
					</template>
				</q-table-search>
				<!-- END: Global search text -->
			</template>
			<q-tooltip-help
				v-if="tooltipText"
				:help-control="helpControl"
				:anchor="anchorId"
				:label="tableTitle" />

			<q-info-banner-help
				v-if="hasInfoBanner"
				:help-control="helpControl"
				:id="id" />
		</div>

		<q-subtitle-help
			v-if="subtitleText"
			:help-control="helpControl"
			:id="id" />

		<component
			:is="isListVisible && !(rowComponent === 'q-form-container' && formName !== '') ? 'div' : 'v-fragment'"
			class="table-and-filters-wrapper">
			<!-- BEGIN: Filters -->
			<div
				v-if="hasFilters"
				v-show="filtersVisible"
				class="c-table__filter-row">
				<q-table-static-filters
					v-if="hasStaticFilters"
					:texts="texts"
					:menu-name="config.name"
					:group-filters="groupFilters"
					:active-filters="activeFilters"
					:date-formats="dateFormats"
					@update:active-filters="updateActiveFilters"
					@update:group-filters="updateGroupFilters" />

				<!-- Condition "searchableColumns.length > 0" needed because of delay in loading columns -->
				<q-table-current-filters
					v-if="searchableColumns.length > 0"
					:searchable-columns="searchableColumns"
					:advanced-filters="advancedFilters"
					:column-filters="columnFilters"
					:search-bar-filters="searchBarFilters"
					:has-filters-active="hasCustomFilters"
					:texts="texts"
					:filter-operators="filterOperators"
					@signal-component="(...args) => emitEvent('signal-component', ...args)"
					@show-advanced-filters="(...args) => emitEvent('show-advanced-filters', ...args)"
					@remove-column-filter="removeColumnFilter"
					@remove-search-bar-filter="removeSearchBarFilter"
					@remove-custom-filters="removeAllCustomFilters" />
			</div>
			<!-- END: Filters -->
			<div
				v-if="isListVisible"
				:class="['table-responsive-wrapper', tableWrapperClasses, { 'text-nowrap': !hasTextWrap }]"
				ref="tableWrapperElem">
				<div
					v-if="actionsPlacement === 'above' || generalActionsPlacement === 'above'"
					class="c-action-bar">
					<q-table-record-actions-menu
						:btn-permission="rowSelected?.btnPermission ?? { insertBtnDisabled: !canInsert }"
						:action-visibility="rowSelected?.actionVisibility ?? {}"
						:crud-actions="actionsPlacement === 'above' ? crudActions : null"
						:custom-actions="actionsPlacement === 'above' ? customActions : null"
						:general-actions="generalActionsPlacement === 'above' ? generalActions : null"
						:general-custom-actions="generalActionsPlacement === 'above' ? generalCustomActions : null"
						:actions-placement="actionsPlacement"
						:show-row-action-icon="showRowActionIcon"
						:show-general-action-icon="showGeneralActionIcon"
						:show-row-action-text="showRowActionText"
						:show-general-action-text="showGeneralActionText"
						:readonly="tableIsReadonly"
						display="inlineAll"
						:enable-row-actions="rowSelected !== undefined && rowSelected !== null"
						:texts="texts"
						@row-action="
							(emitAction) =>
								emitEvent('row-action', {
									...emitAction,
									row: rowSelected,
									rowKeyPath: rowSelected?.rowKeyPath ?? '',
									rowValue: rowSelected?.Value ?? ''
								})
						" />
				</div>
				<div v-if="rowComponent === 'q-form-container' && formName !== ''">
					<template v-if="permissions.canView !== undefined && permissions.canView !== null ? permissions.canView : true">
						<div
							v-for="(row, index) in vbtRows"
							:key="row.rowKey"
							class="multiform c-multiform__section">
							<component
								:is="rowComponent"
								:id="row.rowKey"
								:form-data="rowFormProps[index]"
								:row-component-props="{
									...rowComponentProps,
									permissions: permissions,
									actionsPlacement: 'actionsPlacement'
								}"
								:resources-path="config.resourcesPath"
								@edit="
									rowComponentProps.parentFormMode === 'EDIT' &&
										(permissions.canEdit !== undefined && permissions.canEdit !== null ? permissions.canEdit : true)
										? onMultiformSelect(row)
										: null
								"
								@deselect="emitEvent('set-array-sub-prop-where', 'rowFormProps', 'id', row.rowKey, 'mode', 'SHOW')">
							</component>
						</div>
						<div
							v-if="newRowID !== ''"
							class="multiform c-multiform__section">
							<component
								:is="rowComponent"
								:id="newRowID"
								:form-data="{ form: formName, id: newRowID, mode: 'NEW' }"
								:row-component-props="rowComponentPropsInsert"
								:resources-path="config.resourcesPath"
								@insert-form="(...args) => emitEvent('insert-form', ...args)"
								@cancel-insert="(...args) => emitEvent('cancel-insert', ...args)">
							</component>
						</div>
					</template>
				</div>
				<div
					v-else
					:class="['table-responsive', tableContainerClasses]"
					ref="tableContainerElem"
					tabindex="0"
					@keydown="tableOnKeyDown"
					@focusout="tableOnFocusout">
					<!-- FOR: COLUMN RESIZE, uses ref property -->
					<table
						:class="['c-table', tableClasses]"
						ref="tableElem"
						:aria-label="tableTitle"
						:role="type === 'TreeList' ? 'treegrid' : null">
						<!-- BEGIN: Header -->
						<q-table-header
							ref="headerRowElem"
							:columns="topLevelColumns"
							row-index="h"
							:column-sorting="columnSorting"
							:table-name="name"
							:readonly="tableIsReadonly"
							:allow-column-filters="allowColumnFilters && !hasRowDragAndDrop"
							:allow-column-sort="allowColumnSort && !hasRowDragAndDrop"
							:allow-advanced-filters="allowAdvancedFilters"
							:searchable-columns="searchableColumns"
							:filters="columnFilters"
							:filter-operators="filterOperators"
							:row-count="rowCount"
							:texts="texts"
							:locale="locale"
							:loading="!loaded"
							:disabled="!loaded"
							@update-sort="changeInitialSort"
							@check-all-rows="checkAllRows"
							@check-current-page-rows="checkCurrentPageRows"
							@check-none-rows="checkNoneRows"
							@unselect-all-rows="emitEvent('unselect-all-rows')"
							@edit-column-filter="(...args) => editColumnFilter(...args)"
							@remove-column-filter="(...args) => removeColumnFilter(...args)"
							@add-advanced-filter="(...args) => emitEvent('add-advanced-filter', ...args)"
							@show-advanced-filters="
								(idx, columnFilter, columnName) => {
									emitEvent('signal-component', 'config', { show: true, selectedTab: 'advanced-filters' }, true)
									emitEvent('signal-component', 'advancedFilters', { columnFilter: columnFilter, columnName: columnName, selectedFilterIdx: idx }, true)
								}
							"
							@column-resize="onColumnResize()"
							@focusin="rowOnFocusin($event)"
							@focusout="rowOnFocusout($event)">
						</q-table-header>
						<!-- END: Header -->
						<tbody
							class="c-table__body"
							ref="tbody"
							data-testid="table-body">
							<!-- BEGIN: data rows -->
							<template v-if="vbtRows.length > 0">
								<component
									:is="rowComponent"
									v-for="(row, index) in vbtRows"
									:key="row.rowKey + '_' + rowDomKey"
									ref="rowElems"
									:table-name="name"
									:id="row.rowKey"
									:row="row"
									:columns="vbtColumns"
									:column-hierarchy="columnHierarchy"
									:row-key-path="[row.rowKey]"
									:row-index="index"
									:navigated-row-key-path="navigatedRowKeyPath"
									:prop-row-classes="getRowClasses(row)"
									:unique-id="uniqueId"
									:is-valid="rowIsValid(row)"
									:row-title="getRowTitle(row)"
									:crud-actions="crudActions"
									:custom-actions="customActions"
									:general-actions="generalActions"
									:general-custom-actions="generalCustomActions"
									:actions-placement="actionsPlacement"
									:general-actions-placement="generalActionsPlacement"
									:show-row-action-icon="showRowActionIcon"
									:show-general-action-icon="showGeneralActionIcon"
									:show-row-action-text="showRowActionText"
									:show-general-action-text="showGeneralActionText"
									:readonly="tableIsReadonly"
									:bg-color-selected="rowBgColorSelected"
									:row-selected-for-group="isRowSelected(row)"
									:cell-titles="getRowCellDataTitles(row, vbtColumns)"
									:has-row-drag-and-drop="hasRowDragAndDrop"
									:sort-order-column="sortOrderColumn"
									:expand-icon="expandIcon"
									:collapse-icon="collapseIcon"
									:row-action-display="rowActionDisplay"
									:disable-checkbox="blockTableCheck"
									:row-key-to-scroll="rowKeyToScroll"
									:texts="texts"
									:resources-path="config.resourcesPath"
									@row-click="(...args) => executeRowClickAction(...args)"
									@row-action="(emitAction) => emitEvent('row-action', emitAction)"
									@row-reorder="rowReorder"
									@execute-action="(...args) => emitEvent('execute-action', ...args)"
									@cell-action="(...args) => executeActionCell(...args)"
									@remove-row="removeRow(row.rowKey)"
									@toggle-row-selected="toggleRowSelectMultiple(row)"
									@update="(...args) => updateCell(...args)"
									@update-external="(...args) => emitEvent('update-external', ...args)"
									@toggle-show-children="setChildRowsVisibility"
									@go-to-row="(...args) => goToRow(...args)"
									@navigate-row="(...args) => navigateToRowByMultiIndex(...args)"
									@focusin="rowOnFocusin($event)"
									@focusout="rowOnFocusout($event)"
									@loaded="onRowLoaded"
									@sub-rows-loaded="onSubRowsLoaded" />
							</template>
							<!-- BEGIN: No results row -->
							<tr
								v-else-if="loaded"
								class="c-table__row--empty"
								data-testid="table-row">
								<td :colspan="headerColSpan">
									<slot name="empty-results">
										<img
											v-if="emptyRowImgPath"
											:src="emptyRowImgPath"
											:alt="texts.emptyText" />
										{{ texts.emptyText }}
									</slot>
								</td>
							</tr>
							<template v-else>
								<tr
									v-for="i in 3"
									:key="i"
									data-testid="table-row">
									<td
										v-for="j in headerColSpan"
										:key="j">
										<q-skeleton-loader type="text" />
									</td>
								</tr>
							</template>

							<!-- END: No results row -->
							<!-- END: data rows -->
						</tbody>
						<tfoot v-if="hasNewRecordRow || hasColumnTotalizers">
							<q-table-row
								v-if="hasNewRecordRow"
								:table-name="name"
								:key="newRow.Rownum"
								:row="newRow"
								:columns="vbtColumns"
								row-index="new"
								unique-id="new_row"
								:bg-color-selected="rowBgColorSelected"
								:cell-titles="getRowCellDataTitles(newRow, vbtColumns)"
								@update="(...args) => setCellValue(...args)">
								<template #checklist>
									<q-button
										:label="texts.removeText"
										:title="texts.removeText"
										@click="emitRowsDelete(rowsSelected)">
										<q-icon icon="remove" />
									</q-button>
								</template>
								<template #actions>
									<q-button
										:label="texts.insertText"
										:title="texts.insertText"
										@click="emitRowAdd(newRow)">
										<q-icon icon="add" />
									</q-button>
								</template>
							</q-table-row>

							<!-- BEGIN: Column totalizers -->
							<q-table-column-totalizers
								v-if="hasColumnTotalizers"
								:class="totalizersClasses"
								:columns="vbtColumns"
								:totalizers="columnTotalizers"
								:multiple-selection="showRowsSelectedTotalizer"
								:rows="vbtRows"
								:selected-rows="rowsSelected"
								:current-page="page"
								:all-selected="vbtAllSelected"
							/>
							<!-- END: Column totalizers -->
						</tfoot>
					</table>
				</div>
				<!-- BEGIN: Footer -->
				<!-- Adding class hiddenFooter since using the v-if to hide the footer is causing an issue (scrolling down causes the page to go up and down) -->
				<div
					v-show="showFooter && !isFooterEmpty"
					class="c-table__footer-out"
					ref="tableFooterElem">
					<q-table-footer
						:pagination-placement="paginationPlacement"
						:pagination="showPagination && !hasRowDragAndDrop"
						:show-per-page-menu="perPageMenuVisible"
						:current-page-rows-length="currentPageRowsLength"
						:filtered-rows-length="filteredRowsLength"
						:original-rows-length="originalRowsLength"
						:page="page"
						v-model:per-page="perPage"
						:per-page-options="unselectedPerPageOptions"
						:has-more="hasMore"
						:show-rows-selected-count="showRowsSelectedCount"
						:rows-selected-count="rowsSelectedCount"
						:has-row-select-actions="hasRowSelectActions"
						:group-actions="groupActions"
						:show-record-count="showRecordCount"
						:row-count="rowCount"
						:show-alternate-pagination="showAlternatePagination"
						:num-visibile-pagination-buttons="numVisibilePaginationButtons"
						:show-limits="showLimits"
						:table-limits="tableLimits"
						:table-id="controlId"
						:table-name-plural="tableNamePlural"
						:texts="texts"
						:disabled="!loaded"
						@update:page="(emitAction) => goToPage(emitAction)"
						@update:per-page="setPerPage($event)"
						@group-action="rowGroupAction($event)">
						<template #row-general-actions>
							<q-table-record-actions-menu
								v-if="generalActionsPlacement === 'below'"
								:btn-permission="{ insertBtnDisabled: !canInsert }"
								:general-actions="generalActions"
								:general-custom-actions="generalCustomActions"
								:actions-placement="actionsPlacement"
								:readonly="tableIsReadonly"
								:enable-general-actions="loaded"
								display="inline"
								:texts="texts"
								@row-action="(emitAction) => emitEvent('row-action', emitAction)" />
						</template>
					</q-table-footer>
				</div>
				<!-- END: Footer -->
			</div>
			<template v-else-if="activeViewMode">
				<component
					v-if="activeViewMode.props"
					:is="`q-${activeViewMode.type}`"
					:texts="texts"
					v-bind="activeViewMode.props"
					v-on="activeViewMode.handlers ?? {}">
					<template #empty-image>
						<img
							v-if="emptyRowImgPath"
							:src="emptyRowImgPath"
							:alt="texts.emptyText" />
					</template>
					<template #empty-text>
						{{ texts.emptyText }}
					</template>
				</component>
				<!-- BEGIN: Footer -->
				<!-- Adding class hiddenFooter since using the v-if to hide the footer is causing an issue (scrolling down causes the page to go up and down) -->
				<div
					v-show="showFooter && !isFooterEmpty"
					:class="['c-sr__footer-out', 'c-table__footer-out']">
					<q-table-footer
						:pagination="showPagination"
						:show-per-page-menu="false"
						:current-page-rows-length="currentPageRowsLength"
						:filtered-rows-length="filteredRowsLength"
						:original-rows-length="originalRowsLength"
						:page="page"
						v-model:per-page="perPage"
						:per-page-options="unselectedPerPageOptions"
						:has-more="hasMore"
						:show-rows-selected-count="showRowsSelectedCount"
						:rows-selected-count="rowsSelectedCount"
						:has-row-select-actions="hasRowSelectActions"
						:group-actions="groupActions"
						:show-record-count="showRecordCount"
						:row-count="rowCount"
						:show-alternate-pagination="showAlternatePagination"
						:num-visibile-pagination-buttons="numVisibilePaginationButtons"
						:show-limits="showLimits"
						:table-limits="tableLimits"
						:table-id="controlId"
						:table-name-plural="tableNamePlural"
						:texts="texts"
						:disabled="!loaded"
						@update:page="(emitAction) => goToPage(emitAction)"
						@update:per-page="setPerPage($event)"
						@group-action="rowGroupAction($event)">
						<template #row-general-actions>
							<q-table-record-actions-menu
								v-if="generalActionsPlacement === 'below' && !activeViewMode.implementsOwnInsert"
								:btn-permission="{ insertBtnDisabled: !canInsert }"
								:general-actions="generalActions"
								:general-custom-actions="generalCustomActions"
								:actions-placement="actionsPlacement"
								:readonly="tableIsReadonly"
								:enable-general-actions="loaded"
								display="inline"
								:texts="texts"
								@row-action="emitEvent('row-action', $event)" />
						</template>
					</q-table-footer>
				</div>
				<!-- END: Footer -->
			</template>
		</component>
	</div>
</template>

<script>
	import { defineAsyncComponent, markRaw } from 'vue'
	import cloneDeep from 'lodash-es/cloneDeep'
	import filter from 'lodash-es/filter'
	import find from 'lodash-es/find'
	import findIndex from 'lodash-es/findIndex'
	import get from 'lodash-es/get'
	import has from 'lodash-es/has'
	import isEmpty from 'lodash-es/isEmpty'
	import orderBy from 'lodash-es/orderBy'
	import mergeWith from 'lodash-es/mergeWith'

	import Sortable from 'sortablejs'

	import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'
	import listFunctions from '@/mixins/listFunctions.js'
	import HelpControl from '@/mixins/helpControls.js'

	import ColumnResizeable from '@/api/genio/columnResizeable.js'
	import searchFilterData from '@/api/genio/searchFilterData.js'

	// The texts needed by the component.
	const DEFAULT_TEXTS = {
		actionMenuTitle: 'Actions',
		emptyText: 'No data to show',
		removeText: 'remove',
		importButtonTitle: 'Import',
		templateButtonTitle: 'Select a template to use.',
		submitText: 'Submit',
		applyText: 'Apply',
		closeText: 'Close',
		dropToUpload: 'Drag the file to upload',
		okText: 'OK',
		pendingRecords: 'Warning: This record is pending, you must edit or delete it.',
		saveText: 'Save',
		viewText: 'View',
		deleteText: 'Delete',
		duplicateText: 'Duplicate',
		confirmText: 'Confirm',
		cancelText: 'Cancel',
		discard: 'Discard',
		resetText: 'Reset',
		insertText: 'Insert',
		tableConfig: 'Table configuration',
		configureColumns: 'Configure columns',
		viewModeConfigButtonTitle: 'View options',
		toListViewButtonTitle: 'List view',
		toAlternativeViewButtonTitle: 'Alternative view',
		orderText: 'Order',
		nameOfColumnText: 'Column name',
		visibleText: 'Visible',
		searchTextTitle: 'Search box',
		searchText: 'Search',
		forText: 'for',
		ofText: 'of',
		orText: 'Or',
		allFieldsText: 'all fields',
		showText: 'Show',
		hideText: 'Hide',
		filtersText: 'Filters',
		limitsButtonTitle: 'Limit',
		limitsListTitlePrepend: 'The information in the list of',
		limitsListTitleAppend: 'is limited by',
		textRowsSelected: 'selected record(s)',
		groupActionsText: 'Group actions',
		advancedFiltersText: 'Advanced filters',
		moveToAdvancedFiltersText: 'Move to advanced filters',
		applyFilterText: 'Apply filter',
		createFilterText: 'Create filter',
		filterNameText: 'Filter name',
		createConditionText: 'Create condition',
		removeConditionText: 'Remove condition',
		savedFiltersText: 'Saved filters',
		saveFilterText: 'Save filter',
		deleteFilterText: 'Delete filter',
		activateFilterText: 'Activate filter',
		deactivateFilterText: 'Deactivate filter',
		columnActionsText: 'Column actions',
		sortText: 'Sort',
		ascendingText: 'Ascending',
		descendingText: 'Descending',
		sortAscendingText: 'Sort ascending',
		sortDescendingText: 'Sort descending',
		staticFiltersTitle: 'Global filters',
		activeFiltersTitle: 'Active filters',
		removeAllText: 'Remove all',
		rowDragAndDropTitle: 'Reorder',
		exportButtonTitle: 'Export',
		defaultKeywordSearchText: 'Default search',
		lineBreak: 'Line break',
		yesLabel: 'Yes',
		noLabel: 'No',
		activeText: 'Active',
		inactiveText: 'Inactive',
		inactiveFilterText: 'Inactive filter',
		showRecordsWhereText: 'Show records when',
		visibleColumnsText: 'Visible columns',
		invisibleColumnsHelpText: 'Invisible columns are not searchable.',
		saveViewText: 'Save view',
		viewManagerText: 'View manager',
		clearResizeText: 'Clear resize',
		viewExistsText: 'This view already exists.',
		wantToOverwriteText: 'Do you want to overwrite it?',
		tableViewSaveSuccess: 'Table view saved successfully',
		viewNameText: 'View name',
		setDefaultViewText: 'Set as default view',
		defaultViewText: 'Default view',
		downloadTemplateText: 'Download the excel template file by clicking the button below',
		fillTemplateFileText: 'Fill the file with the necessary information',
		importTemplateFileText: 'After filling the file click on the submit button to import it',
		allRecordsText: 'All',
		currentPageText: 'Current page',
		noneText: 'None',
		loading: 'Loading data...',
		onDate: 'On:',
		state: 'State',
		first: 'First',
		last: 'Last',
		previous: 'Previous',
		next: 'Next',
		MoveUp: 'Move up',
		MoveDown: 'Move down',
		rowAddNewAfter: 'Add new row after this one',
		rowDragDropReorder: 'Drag and drop to change row order',
		rowExpand: 'Expand row',
		rowCollapse: 'Collapse row',
		close: 'Close',
		download: 'Download',
		placeholder: 'Choose...',
		clearValue: 'Clear value',
		showOptions: 'Show options'
	}

	export default {
		name: 'QTable',

		emits: [
			'add-advanced-filter',
			'cancel-insert',
			'cell-action',
			'close-view',
			'execute-action',
			'fetch-qtable-all-selected',
			'hide-popup',
			'insert-form',
			'loaded',
			'on-change-query',
			'on-export-data',
			'on-export-template',
			'on-import-data',
			'remove-all-advanced-filters',
			'remove-row',
			'reset-query',
			'row-action',
			'row-add',
			'row-edit',
			'row-group-action',
			'row-reorder',
			'rows-delete',
			'rows-loaded',
			'save-view',
			'go-to-row',
			'select-row',
			'select-rows',
			'set-array-sub-prop-where',
			'set-info-message',
			'set-property',
			'set-qtable-all-selected',
			'set-row-index-property',
			'show-advanced-filters',
			'show-popup',
			'signal-component',
			'toggle-rows-drag-drop',
			'tree-load-branch-data',
			'unselect-all-rows',
			'unselect-row',
			'update-cell',
			'update-config',
			'update-external',
			'update-list-visible',
			'update:active-view-mode',
			'update:activeFilters',
			'update:groupFilters',
			'view-action',
			'set-confirm-changes'
		],

		components: {
			QDropdownMenu: defineAsyncComponent(() => import('@/components/QDropdownMenu.vue')),
			QTableStaticFilters: defineAsyncComponent(() => import('./QTableStaticFilters.vue')),
			QTableCurrentFilters: defineAsyncComponent(() => import('./QTableCurrentFilters.vue')),
			QTableHeader: defineAsyncComponent(() => import('./QTableHeader.vue')),
			QTableFooter: defineAsyncComponent(() => import('./QTableFooter.vue')),
			QTableSearch: defineAsyncComponent(() => import('./QTableSearch.vue')),
			QTableRecordActionsMenu: defineAsyncComponent(() => import('./QTableRecordActionsMenu.vue')),
			QTableChecklistCheckbox: defineAsyncComponent(() => import('./QTableChecklistCheckbox.vue')),
			QTableExport: defineAsyncComponent(() => import('./QTableExport.vue')),
			QTableImport: defineAsyncComponent(() => import('./QTableImport.vue')),
			QTableRow: defineAsyncComponent(() => import('./QTableRow.vue')),
			QTreeTableRow: defineAsyncComponent(() => import('./QTreeTableRow.vue')),
			QPopoverHelp: defineAsyncComponent(() => import('@/components/QPopoverHelp.vue')),
			QTooltipHelp: defineAsyncComponent(() => import('@/components/QTooltipHelp.vue')),
			QSubtitleHelp: defineAsyncComponent(() => import('@/components/QSubtitleHelp.vue')),
			QInfoBannerHelp: defineAsyncComponent(() => import('@/components/QInfoBannerHelp.vue'))
		},

		inheritAttrs: false,

		mixins: [HelpControl],

		props: {
			/**
			 * The unique identifier for the table component instance.
			 */
			id: String,

			/**
			 * Flag indicating if the label is to be displayed.
			 */
			hasLabel: {
				type: Boolean,
				default: true
			},

			/**
			 * The array of data objects that represents each row of data in the table.
			 */
			rows: {
				type: Array,
				default: () => []
			},

			/**
			 * The configuration for the table columns, including header names, keys, and additional properties.
			 */
			columns: {
				type: Array,
				required: true
			},

			/**
			 * The list of column totalizers, identified by the columns's area and field
			 */
			columnTotalizers: {
				type: Array,
				default: () => []
			},

			/**
			 * The component used to render individual table rows.
			 */
			rowComponent: {
				type: String,
				default: 'q-table-row'
			},

			/**
			 * The type of the table, which can affect rendering or features (e.g., 'List', 'TreeList').
			 */
			type: {
				type: String,
				default: 'List'
			},

			/**
			 * The total number of rows available, which may be utilized for server-side pagination.
			 */
			totalRows: {
				type: Number,
				default: 0
			},

			/**
			 * Flag indicating whether there are more pages of data available for server-side pagination.
			 */
			hasMorePages: {
				type: Boolean,
				default: false
			},

			/**
			 * Custom configuration object to setup various aspects of the table component's behavior and appearance.
			 */
			config: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Localization and customization of textual content within the table component.
			 */
			texts: {
				type: Object,
				validator: (value) => genericFunctions.validateTexts(DEFAULT_TEXTS, value),
				default: () => DEFAULT_TEXTS
			},

			/**
			 * Indicates whether the table is in a read-only or interactive state.
			 */
			readonly: {
				type: Boolean,
				default: false
			},

			/**
			 * Custom classes added to the table's surrounding container element.
			 */
			classes: {
				type: [Array, String],
				default: () => []
			},

			/**
			 * Custom classes added to the table's surrounding container element, only when in normal table mode.
			 */
			tableModeClasses: {
				type: [Array, String],
				default: () => []
			},

			/**
			 * Custom classes added directly to the <table> element.
			 */
			tableClasses: {
				type: [Object, String],
				default: () => ({})
			},

			/**
			 * Custom classes added to the container that usually includes the table and possibly other elements like pagination.
			 */
			tableContainerClasses: {
				type: [Object, String],
				default: () => ({})
			},

			/**
			 * Custom classes added to the wrapper element surrounding the table.
			 */
			tableWrapperClasses: {
				type: [Object, String],
				default: () => ({})
			},

			/**
			 * Size of the table.
			 */
			size: {
				type: String,
				default: ''
			},

			/**
			 * An array of actions that can be performed on the entire table, such as adding or exporting rows.
			 */
			actions: {
				type: Array,
				default: () => []
			},

			/**
			 * Search value to use for searching all columns.
			 */
			searchValue: {
				type: String,
				default: ''
			},

			/**
			 * Definition of advanced filters that may apply more complex conditions to the table data.
			 */
			advancedFilters: {
				type: Array,
				default: () => []
			},

			/**
			 * Definition of filters applied to individual table columns, often representing a subset of the total filters being used.
			 */
			columnFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Definition of filters applied to individual table columns from the search bar, often representing a subset of the total filters being used.
			 */
			searchBarFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Definition of global filters that apply generalized filtering criteria outside individual columns.
			 */
			groupFilters: {
				type: Array,
				default: () => []
			},

			/**
			 * Object representing filters that are currently active and affecting the table data.
			 */
			activeFilters: {
				type: Object,
				default: () => ({})
			},

			/**
			 * Object representing the column sorting.
			 */
			columnSorting: {
				type: Object,
				default: () => ({})
			},

			/**
			 * An array representing the different limits that may apply to the table data or query results.
			 */
			tableLimits: {
				type: Array,
				default: () => []
			},

			/**
			 * The response payload from an import action, which could include status messages or data validation errors.
			 */
			dataImportResponse: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The header level (e.g., h1, h2) used for the table's accessible title region.
			 */
			headerLevel: {
				type: Number,
				default: 1
			},

			/**
			 * A map of selected rows by their keys, often used for batch actions or manipulation of multiple rows.
			 */
			rowsSelected: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The name of the icon to be used for expand functionality within collapsible rows or sections.
			 */
			expandIcon: {
				type: String,
				default: 'square-plus'
			},

			/**
			 * The name of the icon to be used for collapse functionality within collapsible rows or sections.
			 */
			collapseIcon: {
				type: String,
				default: 'square-minus'
			},

			/**
			 * Array of different view modes available for the table, such as a list view or card view.
			 */
			viewModes: {
				type: Array,
				default: () => []
			},

			/**
			 * The identifier for the table's current view mode out of the possible options defined in viewModes.
			 */
			activeViewModeId: {
				type: String,
				default: ''
			},

			/**
			 * The name of the form to be potentially used or referenced within row-level actions or edit states.
			 */
			formName: {
				type: String,
				default: ''
			},

			/**
			 * Object properties to be associated with each individual row component, such as form states or additional configuration.
			 */
			rowFormProps: {
				type: Array,
				default: () => []
			},

			/**
			 * Additional properties to be passed down to row components, which can include permissions or event handlers.
			 */
			rowComponentProps: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The identifier for a row that may be added to the table data as a new entry.
			 */
			newRowID: {
				type: String,
				default: ''
			},

			/**
			 * Indicates if any pending changes need to be confirmed by the user before proceeding.
			 */
			confirmChanges: {
				type: Boolean,
				default: false
			},

			/**
			 * An object for passing signals to the component, often used for higher-level state management or control.
			 */
			signal: {
				type: Object,
				default: () => ({})
			},

			/**
			 * A predefined set of operators used for creating or managing filters within the component.
			 */
			filterOperators: {
				type: Object,
				default: () => new searchFilterData.SearchFilterConditionOperators()
			},

			/**
			 * A flag to indicate the 'select all' state is active for the table.
			 */
			allSelectedRows: {
				type: String,
				default: 'false'
			},

			/**
			 * Object with properties for the header row.
			 */
			headerRow: {
				type: Object,
				default: () => ({
					isNavigated: false
				})
			},

			/**
			 * Indicates if the component has finished loading necessary data and can be interacted with or not.
			 */
			loaded: {
				type: Boolean,
				default: true
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
				unmountedComponent: false,

				controlId: this.id || this.config.name || `q-table-${this._.uid}`,
				vbtRows: [],
				vbtColumns: [],
				columnHierarchy: [],
				name: '',
				page: 1,
				perPage: 10,
				perPageDefault: 10,
				showRecordCount: false,
				showRowsSelectedTotalizer: false,
				numVisibilePaginationButtons: undefined,
				tempFilteredResults: [],
				pagination: true,
				tableTitle: '',
				tableNamePlural: '',
				searchBarConfig: {
					classes: '',
					visibility: false,
					caseSensitive: false,
					showRefreshButton: true,
					showResetButton: false,
					showClearButton: false,
					searchOnPressEnter: true,
					searchDebounceRate: 60,
					init: {
						value: ''
					}
				},
				allowColumnSort: false,
				allowColumnFilters: false,
				allowColumnResize: true,
				allowAdvancedFilters: false,
				perPageOptions: [],
				serverMode: false,
				numRows: 0,
				cardMode: false,
				isFirstTime: true,
				isResponsive: false,
				preservePageOnDataChange: false,
				canEmitQueries: false,
				changeQueryOnLoad: false,
				canInsert: true,
				crudActions: [],
				customActions: [],
				generalActions: [],
				generalCustomActions: [],
				addAction: {},
				rowClickAction: {},
				rowActionClasses: {},
				rowActionDisplay: 'dropdown',
				showRowActionIcon: true,
				showGeneralActionIcon: true,
				showRowActionText: true,
				showGeneralActionText: true,
				actionsPlacement: 'left',
				generalActionsPlacement: 'below',
				rowClickActionInternal: '',
				extendedActions: [],
				paginationPlacement: 'left',
				lcid: 'pt-PT',
				system: 0,
				pkColumn: '',
				menuForJump: '',
				groupActions: [],
				showRowsSelectedCount: false,
				showAlternatePagination: false,
				dateFormats: {
					date: 'dd/MM/yyyy',
					dateTime: 'dd/MM/yyyy HH:mm',
					dateTimeSeconds: 'dd/MM/yyyy HH:mm:ss',
					hours: 'HH:mm',
					use12Hour: false
				},
				rowBgColorSelected: '#E0E0E0',
				filtersVisible: false,
				staticFiltersVisible: true,
				allowFileExport: false,
				exportOptions: [],
				allowFileImport: false,
				importOptions: [],
				importTemplateOptions: [],
				showLimitsInfo: false,
				showImportResponsePopup: false,
				showFooter: true,
				resizableGrid: null,
				hasTextWrap: false,
				hasRowDragAndDrop: false,
				showRowDragAndDropOption: false,
				fieldsEditable: false,
				hasNewRecordRow: false,
				newRow: {
					Rownum: -1,
					Fields: {}
				},
				rowKeyToScroll: '',
				userTableConfigName: '',
				userTableConfigNames: [],
				defaultColumnSorting: {
					columnName: '',
					sortOrder: 'asc'
				},
				defaultSearchColumnName: '',
				columnSizes: [],
				columnResizeOptions: {},
				tableContainerElem: {},
				importModalProps: {
					id: 'data-import',
					props: {
						modalWidth: 'md',
						hideHeader: true,
						closeButtonEnable: true
					}
				},
				configOptions: [],
				configOptionsUse: [],
				permissions: {},
				signalSearch: {},

				// SortableJS plugin instance
				sortablePlugin: null,

				blockTableCheck: false,
				vbtAllSelected: this.allSelectedRows === 'true',
				/**
				 * Row key path of navigated row
				 */
				navigatedRowKeyPath: null,
				/**
				 * Index of navigated row in the array of all navigable row elements
				 */
				navRowIndex: null,
				/**
				 * Whether to navigate to the element that should be navigated to when the component is updated
				 */
				setNavOnUpdate: false,
				/**
				 * Array of all navigable row elements
				 */
				navRowElems: [],
				/**
				 * Index of the first data row in the array of all navigable row elements.
				 * Can be 0 or 1 depending on whether the header row has navigable elements.
				 */
				firstDataRowIndex: 0,
				/**
				 * Number of rows that need to be rendered when the row data changes
				 */
				rowsToLoad: 0,
				/**
				 * Used to change the key for each row component to cause them to re-render when changing row data.
				 * All rows are re-rendered so that when the number of rendered rows matches the number of row objects
				 * loaded, an emit can be done to signal that the rendering is totally done.
				 * Used by things that depend on certain HTML elements existing.
				 */
				rowDomKey: 0,
				/**
				 * Whether the rows should be re-rendered when the row data changes.
				 */
				rerenderRowsOnNextChange: true
			}
		},

		provide()
		{
			return {
				getValueFromRow: this.getValueFromRow,
				getCellSlotName: this.getCellSlotName,
				canShowColumn: this.canShowColumn,
				isSortableColumn: this.isSortableColumn,
				isSearchableColumn: this.isSearchableColumn,
				isActionsColumn: this.isActionsColumn,
				isExtendedActionsColumn: this.isExtendedActionsColumn,
				isChecklistColumn: this.isChecklistColumn,
				isDragAndDropColumn: this.isDragAndDropColumn,
				isTotalizerColumn: this.isTotalizerColumn,
				isDataColumn: this.isDataColumn,
				getRowClasses: this.getRowClasses,
				getRowTitle: this.getRowTitle,
				rowIsValid: this.rowIsValid,
				hasDataAction: this.hasDataAction,
				hasExtendedAction: this.hasExtendedAction,
				getCellDataDisplay: this.getCellDataDisplay,
				getRowCellDataTitles: this.getRowCellDataTitles,
				isRowSelected: this.isRowSelected,
				rowWithoutChildren: this.rowWithoutChildren,
				columnFullName: this.columnFullName
			}
		},

		created()
		{
			this.initConfig()
		},

		mounted()
		{
			/*
				This can't be moved to 'data'
				because of the order of assignment of the other copies of rows.

				The 'tempFilteredResults' is not filled...
			*/
			this.vbtRows = this.rows

			// FOR: GETTING NAVIGABLE ROW ELEMENTS
			// Track number of rows to render
			this.setRowsToLoad()

			if (this.showRowDragAndDropOption) this.perPage = -1

			// Add columns to "new record" row
			if (this.hasNewRecordRow) this.initRow(this.newRow)

			//FOR: ROW ACTIONS, EXTENDED ACTIONS, COLUMN ORDER AND VISIBILITY
			//Must be called when loading or changing columns
			this.updateColumns()

			if (this.searchBarConfig.visibility) this.$nextTick().then(() => this.initGlobalSearch())

			this.$nextTick().then(() => {
				if (!this.serverMode)
				{
					this.filter(false, true)
				} else
				{
					this.canEmitQueries = true

					if (this.changeQueryOnLoad !== false) this.changeQuery()

					this.emitEvent('loaded')
				}
			})

			this.handleShiftKey()

			this.$nextTick().then(() => {
				if (this.$refs.tableElem && this.$refs.tableContainerElem)
				{
					//FOR: COLUMN RESIZE
					this.applyColumnResizeable()

					//FOR: COLUMN SIZES
					this.setColumnSizes(this.columnSizes)
				}
			})

			if (!this.readonly && this.rowsSelectableMultiple)
			{
				//Check if everything is supposed to be selected
				this.initHeaderSelector()
			}

			this.initRowsDragAndDrop()
		},

		updated()
		{
			//DOM reference must be copied to property to it can be updated when the DOM element finally exists
			//and so the property can be passed as a prop to other componenents
			//so they will react to the change (when the DOM element finally exists)
			this.tableContainerElem = Array.isArray(this.$refs.tableContainerElem) ? this.$refs.tableContainerElem[0] : this.$refs.tableContainerElem

			// Update table navigation properties
			if (this.setNavOnUpdate)
			{
				this.emitEvent('set-property', ['config', 'setNavOnUpdate'], false)

				this.$nextTick().then(() => {
					this.navigateToTableRowAction('first')
				})
			}
		},

		beforeUnmount()
		{
			this.unmountedComponent = true

			window.removeEventListener('keyup', this.handleShiftKeyOnSelectStart)
			window.removeEventListener('keydown', this.handleShiftKeyOnSelectStart)
			document.removeEventListener('selectstart', this.documentOnSelectStart)

			this.destroyRowsDragAndDrop()

			if (this.resizableGrid)
			{
				this.resizableGrid.destroy()
				this.resizableGrid = null
			}

			this.tableContainerElem = null

			this.navRowElems.length = 0

			if (this.vbtColumns?.length > 0) {
				this.vbtColumns.forEach(column => {
					if (typeof column.destroy === 'function')
						column.destroy()
				})
			}
			this.vbtColumns.splice(0)

			this.columnHierarchy.length = 0
			this.vbtRows = null
		},

		computed: {
			/**
			 * Determine if the table is in a read-only state.
			 */
			tableIsReadonly()
			{
				return this.readonly || !this.loaded
			},

			/**
			 * Determine if the table header should be displayed.
			 */
			showHeader()
			{
				return this.tableTitle.length > 0 || this.hasActionBar || this.showSearchBar || this.$slots.tableTitle
			},

			/**
			 * Get the total number of rows to be displayed.
			 */
			rowCount()
			{
				if (!this.serverMode) return this.tempFilteredResults.length

				return this.totalRows
			},

			/**
			 * Determine if there are columns with totalizers.
			 */
			hasColumnTotalizers()
			{
				return this.columns.some(column => column.totalizer && listFunctions.isVisibleColumn(column)) && this.vbtRows.length > 0
			},

			/**
			 * Get CSS classes for the row of totalizers.
			 * @returns The class list.
			 */
			totalizersClasses()
			{
				const classes = ['q-table-list__column-totalizers']

				if (this.hasNewRecordRow)
					classes.push('q-table-list__column-totalizers--new-record')

				return classes
			},

			/**
			 * Get CSS classes for the table size.
			 * @returns The class list.
			 */
			sizeClass()
			{
				const sizes = ['large', 'xlarge', 'xxlarge']

				return sizes.includes(this.size) ? `q-table-list--${this.size}` : ''
			},

			/**
			 * Get top-level columns based on the hierarchy or default to the full column list.
			 */
			topLevelColumns()
			{
				const columns = this.columnHierarchy[0] ?? this.vbtColumns
				return columns.filter((c) => this.canShowColumn(c))
			},

			/**
			 * Determine the icon used for toggling filters visibility.
			 */
			toggleFiltersIcon()
			{
				return this.filtersVisible ? 'collapse' : 'expand'
			},

			/**
			 * Determine if there are options for reordering columns.
			 */
			hasColumnReorder()
			{
				return this.config.hasCustomColumns
			},

			/**
			 * Calculate total number of pages based on the current per-page setting and the number of filtered results.
			 */
			totalPages()
			{
				return Math.ceil(this.rowCount / this.perPage)
			},

			/**
			 * Determine if there are more pages beyond the current one.
			 */
			hasMore()
			{
				if (!this.serverMode) return this.page < this.totalPages

				return this.hasMorePages
			},

			/**
			 * Get the total number of filtered results before pagination.
			 */
			filteredResultsCount()
			{
				return this.tempFilteredResults.length
			},

			/**
			 * Determine if there is a unique identifier column among the table columns.
			 */
			uniqueId()
			{
				let uniqueId = ''

				if (!this.hasUniqueId)
				{
					uniqueId = 'vbtId'
					return uniqueId
				}

				this.vbtColumns.some((column) => {
					if (has(column, 'uniqueId') && column.uniqueId === true)
					{
						uniqueId = column.name
						return true
					}
				})

				return uniqueId
			},

			/**
			 * Determine if the table columns include a unique identifier column.
			 */
			hasUniqueId()
			{
				return this.vbtColumns.some((column) => has(column, 'uniqueId') && column.uniqueId === true)
			},

			/**
			 * Get the unique identifier for the configuration menu.
			 */
			configMenuId()
			{
				return this.controlId + '-config-menu-btn'
			},

			/**
			 * Define the HTML level tag to be used for the table header.
			 */
			headerTag()
			{
				return 'h' + this.headerLevel
			},

			/**
			 * Get the length of the current page's row collection.
			 */
			currentPageRowsLength()
			{
				return this.vbtRows.length
			},

			/**
			 * Get the length of the filtered results set.
			 */
			filteredRowsLength()
			{
				return this.rowCount
			},

			/**
			 * Get the length of the original rows set without any filtering.
			 */
			originalRowsLength()
			{
				return this.serverMode ? this.rowCount : this.rows.length
			},

			/**
			 * Determine the total colspan value for the header based on visible columns.
			 */
			headerColSpan()
			{
				return this.topLevelColumns.length
			},

			/**
			 * Determine if the search bar is visible.
			 */
			showSearchBar()
			{
				return this.searchBarConfig.visibility
			},

			/**
			 * Determine if the table has multiple pages.
			 */
			hasMultiplePages()
			{
				/*
				 * Normal paging just needs the total number of pages.
				 * Alternate paging needs the page number and hasMore to calculate if there are multiple pages.
				 */
				return this.totalPages > 1 || this.page > 1 || this.hasMore
			},

			/**
			 * Determine if pagination should be displayed.
			 */
			showPagination()
			{
				return this.pagination && this.hasMultiplePages
			},

			/**
			 * Determine if rows can be selected (multiple).
			 */
			rowsSelectableMultiple()
			{
				return this.rowClickActionInternal === 'selectMultiple'
			},

			/**
			 * Determine if rows can be selected (single).
			 */
			rowsSelectableSingle()
			{
				return this.rowClickActionInternal === 'selectSingle'
			},

			/**
			 * Determine if all rows are selected in the checklist column.
			 */
			isSelectedAllRows()
			{
				return Object.keys(this.rows).every((key) => this.rowsSelected[this.rows[key].rowKey])
			},

			/**
			 * Number of rows selected
			 */
			rowsSelectedCount()
			{
				return Object.keys(this.rowsSelected).length
			},

			/**
			 * Gets the list of columns that are searchable.
			 */
			searchableColumns()
			{
				return listFunctions.getSearchableColumns(this.columns)
			},

			/**
			 * Gets the list of columns that are sortable.
			 */
			sortableColumns()
			{
				return listFunctions.getSortableColumns(this.columns)
			},

			/**
			 * Determine if any filters are available on the table.
			 */
			hasFilters()
			{
				return this.hasStaticFilters || this.hasCustomFilters
			},

			/**
			 * Determine if there are any advanced, column-specific, or search bar filters available.
			 */
			hasCustomFilters()
			{
				return this.hasAdvancedFilters || this.hasColumnFilters || this.hasSearchBarFilters
			},

			/**
			 * Determine if any of the custom filters are currently active.
			 */
			hasCustomFiltersActive()
			{
				return this.hasAdvancedFiltersActive || this.hasColumnFilters || this.hasSearchBarFilters
			},

			/**
			 * Determine if any of the static filters are defined.
			 */
			hasStaticFilters()
			{
				return this.groupFilters.length > 0 || Object.keys(this.activeFilters).length > 0
			},

			/**
			 * Determine if any of the static filters are actively selected.
			 */
			hasStaticFiltersActive()
			{
				// Iterate group filters
				for (const key in this.groupFilters)
				{
					const curFilter = this.groupFilters[key]

					for (const idx in curFilter.filters)
					{
						const curSubFilter = curFilter.filters[idx]

						if (curSubFilter.selected !== false) return true
					}
				}

				// Iterate active filters
				for (const key in this.activeFilters.options)
				{
					const curFilter = this.activeFilters.options[key]

					if (curFilter.selected !== false) return true
				}

				return false
			},

			/**
			 * Generates a title based on whether the filters are currently displayed.
			 */
			showHideFiltersTitle()
			{
				return `${this.texts.showText}/${this.texts.hideText} ${this.texts.filtersText}`
			},

			/**
			 * Determine if any advanced filters are defined.
			 */
			hasAdvancedFilters()
			{
				return this.advancedFilters.filter(filter => this.isValidFilter(filter, this.searchableColumns))?.length > 0
			},

			/**
			 * Determine if any advanced filters are currently active.
			 */
			hasAdvancedFiltersActive()
			{
				return this.advancedFiltersActive.length > 0
			},

			/**
			 * Provides a list of all currently active advanced filters.
			 */
			advancedFiltersActive()
			{
				const advancedFiltersActive = []

				for (const idx in this.advancedFilters)
				{
					const filter = this.advancedFilters[idx]

					if (filter.active !== false) advancedFiltersActive.push(filter)
				}

				return advancedFiltersActive
			},

			/**
			 * Determine if any column-specific filters have been defined.
			 */
			hasColumnFilters()
			{
				return Object.values(this.columnFilters).filter(filter => this.isValidFilter(filter, this.searchableColumns))?.length > 0
			},

			/**
			 * Determine if any filters applied through the search bar are active.
			 */
			hasSearchBarFilters()
			{
				return Object.values(this.searchBarFilters).filter(filter => this.isValidFilter(filter, this.searchableColumns))?.length > 0
			},

			/**
			 * Identify if rows are fully loaded and searchable.
			 */
			hasTableViewLoaded()
			{
				return (
					this.config.userTableConfigName !== undefined &&
					this.config.userTableConfigName !== null &&
					this.config.userTableConfigName !== ''
				)
			},

			/**
			 * Determine if the table has a row click action.
			 */
			hasRowClickAction()
			{
				return !isEmpty(this.rowClickAction)
			},

			/**
			 * Determine if the table has row actions.
			 */
			hasRowActions()
			{
				return this.crudActions.length > 0 || this.customActions.length > 0 || this.hasNewRecordRow
			},

			/**
			 * Determine if the table has extended row actions.
			 */
			hasExtendedRowActions()
			{
				return typeof this.extendedActions !== 'undefined' && Array.isArray(this.extendedActions) && this.extendedActions.length > 0
			},

			/**
			 * Determine if the table has row select actions.
			 */
			hasRowSelectActions()
			{
				return this.rowsSelectableMultiple && (this.groupActions.length > 0 || this.menuForJump !== '')
			},

			/**
			 * Determine if the table limits information should be visible.
			 */
			hasLimits()
			{
				if (!this.tableLimits) return false
				return Object.keys(this.tableLimits).length > 0
			},

			/**
			 * Determine if the table limits information should be visible.
			 */
			showLimits()
			{
				if (!this.hasLimits) return false
				return this.showLimitsInfo
			},

			/**
			 * The currently configured view mode component or undefined if none.
			 */
			activeViewMode()
			{
				return this.viewModes.find((viewMode) => viewMode.id === this.activeViewModeId)
			},

			/**
			 * Determine if the user is allowed to customize the view modes.
			 */
			hasViewModeToggle()
			{
				return this.viewModes.length > 1
			},

			/**
			 * Determine if the table has action bar.
			 */
			hasActionBar()
			{
				return (
					this.showConfigMenu ||
					this.hasFilters ||
					this.showRowDragAndDropOption ||
					this.allowFileExport ||
					this.allowFileImport ||
					this.hasViewModeToggle
				)
			},

			/**
			 * Determine the column used for ordering the rows.
			 */
			sortOrderColumn()
			{
				for (const idx in this.columns)
				{
					const column = this.columns[idx]

					if (column.isOrderingColumn !== undefined && column.isOrderingColumn !== false) return column
				}
				return null
			},

			/**
			 * Get the default search column based on configuration.
			 */
			defaultSearchColumn()
			{
				return listFunctions.getDefaultSearchColumn(this.columns, this.defaultSearchColumnName)
			},

			/**
			 * Get the label of the default search column.
			 */
			defaultSearchColumnLabel()
			{
				if (isEmpty(this.defaultSearchColumn?.label)) return ''
				return this.defaultSearchColumn.label
			},

			/**
			 * Determine if the list view is enabled.
			 */
			isListVisible()
			{
				// FIXME: remove by implementing a view mode manager
				// QTable SHOULD ONLY IMPLEMENT TABLE LOGIC!!! => DOES NOT CARE ABOUT VIEWMODES
				return !this.viewModes.length || this.activeViewModeId === 'LIST'
			},

			/**
			 * Get the row component properties with overrides for inserting rows.
			 */
			rowComponentPropsInsert()
			{
				const obj = cloneDeep(this.rowComponentProps)

				obj.formButtonsOverride.saveBtn.emitAction = {
					name: 'insert-form',
					params: []
				}
				obj.formButtonsOverride.resetCancelBtn.emitAction = {
					name: 'cancel-insert',
					params: []
				}

				return obj
			},

			/**
			 * Get the currently selected row.
			 */
			rowSelected()
			{
				const rowIdStr = Object.keys(this.rowsSelected)[0]
				if (rowIdStr === undefined || rowIdStr === null) return null

				const rowId = rowIdStr.split(',')

				const rowSelected = listFunctions.getRowByKeyPath(this.rows, rowId)

				if (rowSelected !== undefined && rowSelected !== null) rowSelected.rowKeyPath = rowId

				return rowSelected
			},

			/**
			 * Determine whether the table footer is empty or not.
			 */
			isFooterEmpty()
			{
				return (
					!this.showRowsSelectedCount &&
					!this.hasRowSelectActions &&
					!this.showRecordCount &&
					!this.showPagination &&
					!this.showLimits &&
					listFunctions.numArrayVisibleActions(this.generalActions, this.tableIsReadonly) === 0 &&
					listFunctions.numArrayVisibleActions(this.generalCustomActions, this.tableIsReadonly) === 0
				)
			},

			/**
			 * Get options array of saved views.
			 */
			savedViewsOptions()
			{
				if (!this.userTableConfigNames) return []

				let viewIdx = 1
				const savedViewsOptions = []

				for (const idx in this.userTableConfigNames)
				{
					const configName = this.userTableConfigNames[idx]
					savedViewsOptions.push({
						id: viewIdx,
						text: configName,
						key: viewIdx++,
						value: configName,
						group: 'user'
					})
				}

				savedViewsOptions.push({
					id: 0,
					text: this.texts.baseTable,
					separatorBefore: true,
					key: 0,
					value: this.texts.baseTable,
					group: 'system'
				})

				return savedViewsOptions
			},

			/**
			 * Determine whether to show the list of saved table views.
			 */
			showSavedViews()
			{
				return this.config.allowManageViews && this.savedViewsOptions.length > 1
			},

			/**
			 * The ID of the selected table view.
			 */
			selectedViewId()
			{
				return this.getViewIdByName(this.userTableConfigName)
			},

			/**
			 * Determine whether to show table view configuration dropdown menu.
			 */
			showConfigMenu()
			{
				return (
					this.config.allowManageViews || (this.config.allowColumnConfiguration && this.isListVisible) || (this.config.allowAdvancedFilters && this.hasAdvancedFilters)
				)
			},

			/**
			 * Get unselected options for the `perPage` dropdown.
			 */
			unselectedPerPageOptions()
			{
				const perPageOptions = this.perPageOptions.concat([this.perPageDefault]).sort((a, b) => a - b)
				return perPageOptions.filter((item, pos) => item !== this.perPage && perPageOptions.indexOf(item) === pos)
			},

			/**
			 * Determine if the per page menu should be visible.
			 */
			perPageMenuVisible()
			{
				return listFunctions.getPerPageMenuVisible(
					this.perPageOptions,
					this.perPageDefault,
					this.rowCount,
					this.page,
					this.showAlternatePagination,
					this.hasMorePages
				)
			},

			/**
			 * Get the path to the image to be used for an empty row.
			 */
			emptyRowImgPath()
			{
				if (!this.config.resourcesPath || !this.config.emptyRowImg) return ''

				let resourcesPath = this.config.resourcesPath
				if (resourcesPath[resourcesPath.length - 1] !== '/') resourcesPath += '/'

				return resourcesPath + this.config.emptyRowImg
			},

			labelId() {
				return `label_${this.controlId}`
			},

			hasInfoBanner() {
				if(!this.helpControl) return false
				return this.helpControl.shortHelp.type === 'Info-banner' || this.helpControl.detailedHelp?.type === 'Info-banner'
			},
		},

		methods: {
			getValueFromRow: listFunctions.getCellValue,
			getCellSlotName: listFunctions.getCellSlotName,
			isSortableColumn: listFunctions.isSortableColumn,
			isActionsColumn: listFunctions.isActionsColumn,
			isExtendedActionsColumn: listFunctions.isExtendedActionsColumn,
			isChecklistColumn: listFunctions.isChecklistColumn,
			isDragAndDropColumn: listFunctions.isDragAndDropColumn,
			isTotalizerColumn: listFunctions.isTotalizerColumn,
			isDataColumn: listFunctions.isDataColumn,
			hasExtendedAction: listFunctions.hasExtendedAction,
			hasDataAction: listFunctions.hasDataAction,
			rowWithoutChildren: listFunctions.rowWithoutChildren,
			isValidFilter: listFunctions.isValidFilter,

			emitEvent(event, ...rawArgs)
			{
				if (!this.unmountedComponent)
					this.$emit(event, ...rawArgs)
				// To facilitate debugging during development
				else if (import.meta.env.DEV)
					// eslint-disable-next-line no-console
					console.warn(`The QTable component is emitting the event already after unmount: ${event}`)
			},

			/**
			 * Sets all data properties from props passed in
			 */
			initConfig()
			{
				// Merge properties of config prop
				// Replace arrays with the new values instead of merging
				mergeWith(this, this.config, (objValue, srcValue) => {
					if(Array.isArray(objValue) && Array.isArray(srcValue))
						return srcValue
				})
			},

			/**
			 * Get navigation row index from the row's multi-index
			 * @param multiIndex {string} Row multi-index
			 */
			getNavRowIndexFromMultiIndex(multiIndex)
			{
				return this.navRowElems.findIndex((elem) => {
					return elem?.getAttribute('index')?.toString() === multiIndex.toString()
				})
			},

			/**
			 * Get table action sub-elements of a given element
			 * @param element {DOMElement} DOM element
			 */
			getTableRowActionElements(element)
			{
				const actionElements = []

				// Set main element as first if it has the attribute
				if(element?.hasAttribute('data-table-action-selected'))
					actionElements.push(element)

				// Get sub-elements
				const actionSubElementsNodeList = element?.querySelectorAll("[data-table-action-selected]")
				if(actionSubElementsNodeList !== undefined && actionSubElementsNodeList !== null)
					actionElements.push(...Array.from(actionSubElementsNodeList))

				return actionElements
			},

			/**
			 * Reset table action properties of sub-elements of a given element
			 * @param rowIndex {number} Row index
			 */
			resetTableRowActionProperties(rowIndex)
			{
				if(rowIndex === undefined || rowIndex === null)
					return

				const rowElems = this.navRowElems
				if(rowElems.length > 0)
				{
					// Clear the state for all action elements in this row
					const actionElements = this.getTableRowActionElements(rowElems[rowIndex])
					actionElements.forEach((actionElement) => {
						actionElement.setAttribute('data-table-action-selected', 'false')
					})
				}
			},

			/**
			 * Reset table action state
			 * @param rowIndex {number} Row index
			 */
			resetTableRowActionState(rowIndex)
			{
				// Reset action element properties
				this.resetTableRowActionProperties(rowIndex)

				// If focus is left on an element that is inconsistent with the navigation state
				// focus on the table container element
				if(document.activeElement?.getAttribute('data-table-action-selected') === 'false')
				{
					const tableContainerElem = this.getTableContainerElement()
					tableContainerElem?.focus()
				}
			},

			/**
			 * Focus on the next or previous action sub-element of a given element
			 * @param actionElements {DOMElement} DOM element
			 * @param direction {string} Direction to move focus ("first", "next" or "previous")
			 */
			focusTableRowActionElement(actionElements, direction = "first")
			{
				if(actionElements === undefined || actionElements === null || actionElements.length === 0)
					return

				let isFocused = false

				// Set value to add or subtract from index based on direction value
				const directionIdx = direction === "next" ? 1 : -1

				// Find focused element and focus on the next one if it exists
				actionElements.every((actionElement, index, array) => {
					// If element is focused
					if(actionElement.getAttribute('data-table-action-selected') === 'true')
					{
						// If focusing on the first element, set all others to false
						if(direction === 'first')
						{
							actionElement.setAttribute('data-table-action-selected', 'false')
							return true
						}

						isFocused = true

						// Prevent going out of bounds for adjacentActionElement
						if((directionIdx === 1 && index >= array.length - 1)
							|| (directionIdx === -1 && index === 0))
							return false

						const adjacentActionElement = actionElements[index + directionIdx]
						if(typeof adjacentActionElement.focus === 'function')
						{
							actionElement.setAttribute('data-table-action-selected', 'false')
							adjacentActionElement.setAttribute('data-table-action-selected', 'true')
							adjacentActionElement.focus()
							return false
						}
					}
					// If element is not focused go to next iteration
					return true
				})

				// None of the elements are focused. Focus on the first one.
				if(!isFocused && typeof actionElements[0].focus === 'function')
				{
					actionElements[0].setAttribute('data-table-action-selected', 'true')
					actionElements[0].focus()
				}
			},

			/**
			 * Navigate to next or previous row action
			 * @param direction {string} Direction to move focus ("first", "next" or "previous")
			 */
			navigateToTableRowAction(direction)
			{
				// Find action elements and focus on adjacent one
				const rowElems = this.navRowElems
				if(rowElems.length === 0)
					return
				const actionElements = this.getTableRowActionElements(rowElems[this.navRowIndex])
				this.focusTableRowActionElement(actionElements, direction)
			},

			/**
			 * Navigate to row
			 * @param index {number} Index of row to navigate to
			 */
			navigateToRow(index)
			{
				const rowElems = this.navRowElems

				// Check index bounds
				if(index < 0 || index >= rowElems?.length)
					return

				// Navigate to row
				if(this.navRowIndex === undefined || this.navRowIndex === null)
					this.navRowIndex = 0

				if(index !== this.navRowIndex)
				{
					// Reset state, including properties, of action elements of current row
					this.resetTableRowActionState(this.navRowIndex)
				}

				this.navRowIndex = index

				// Find action elements and focus on first one
				this.$nextTick().then(()=> {
					this.navigateToTableRowAction('first')
				})
			},

			/**
			 * Navigate to row by multi-index
			 * @param multiIndex {string} Row multi-index
			 */
			navigateToRowByMultiIndex(multiIndex)
			{
				this.navigateToRow(this.getNavRowIndexFromMultiIndex(multiIndex))
			},

			/**
			 * Reset table navigation state
			 */
			resetNavigationState()
			{
				// Reset state, including properties, of action elements of current row
				this.resetTableRowActionState(this.navRowIndex)
				// Reset row index
				this.navRowIndex = null
			},

			/**
			 * Focusout handler for table
			 * @param event {object} event object
			 */
			tableOnFocusout(event)
			{
				const tableContainerElem = this.getTableContainerElement()
				// If focus is on the table or sub-element of the table
				// Logically the focus is on the table
				if(event.relatedTarget === tableContainerElem
					|| tableContainerElem.contains(event.relatedTarget))
					return

				this.resetNavigationState()
			},

			/**
			 * Keydown handler for table
			 * FOR: TABLE KEYBOARD OPERATION
			 * @param event {object} Event object
			 */
			tableOnKeyDown(event)
			{
				const key = event?.key
				const tableContainerElem = this.getTableContainerElement()

				switch(key)
				{
					case "Tab":
					case "Escape":
						this.resetNavigationState()
						// If the focused element is the table
						if(event.target === tableContainerElem)
							return
						// If the focused element is a sub-element of the table
						tableContainerElem?.focus()
						event.preventDefault()
						break
					case "ArrowUp":
						// Navigate to the previous row
						this.navigateToRow(this.navRowIndex - 1)
						event.preventDefault()
						break
					case "ArrowDown":
						// Navigate to the first row if not navigated to any row, other wise navigate to the next row
						if(this.navRowIndex === undefined || this.navRowIndex === null || isNaN(this.navRowIndex))
							this.navigateToRow(this.firstDataRowIndex)
						else
							this.navigateToRow(this.navRowIndex + 1)
						event.preventDefault()
						break
					case "ArrowLeft":
						// Navigate to the previous action element
						this.navigateToTableRowAction('previous')
						event.preventDefault()
						break
					case "ArrowRight":
						// Navigate to the next action element
						this.navigateToTableRowAction('next')
						event.preventDefault()
						break
					case "Home":
						// Navigate to first data row
						this.navigateToRow(this.firstDataRowIndex)
						event.preventDefault()
						break
					case "End":
						// Navigate to last data row
						this.navigateToRow(this.navRowElems?.length - 1)
						event.preventDefault()
						break
					case "Insert":
						// Insert new record
						this.emitEvent('row-action', { id: 'insert' })
						event.preventDefault()
						break
					case "PageUp":
						// Go to previous page
						if(this.page > 1)
							this.goToPage(this.page - 1)
						event.preventDefault()
						break
					case "PageDown":
						// Go to previous page
						if(this.hasMore)
							this.goToPage(this.page + 1)
						event.preventDefault()
						break
				}
			},

			/**
			 * FOR: TABLE KEYBOARD OPERATION
			 * Focusin handler for table rows
			 * Used to set keyboard navigation state when focusing on a row or row sub-element
			 * so keyboard navigation can continue from there
			 * @param event {object} Event object
			 */
			rowOnFocusin(event)
			{
				// Actual element mousedown happens on
				const element = event?.target

				// Get action element that mousedown happened on or propagated to
				// since mousedown can originate on sub-elements
				let actionElement = element
				// If element that get's focused is a parent element of the element
				while(!actionElement?.hasAttribute('data-table-action-selected') && actionElement?.parentElement)
					actionElement = actionElement.parentElement

				// Get row element
				let rowElement = element
				while(rowElement?.tagName !== 'TR' && rowElement?.parentElement)
					rowElement = rowElement.parentElement

				// Get multi-index of row
				const multiIndex = rowElement.getAttribute('index')

				// Set row as the current navigated row
				this.navRowIndex = this.getNavRowIndexFromMultiIndex(multiIndex)

				// Reset properties of action elements of current row
				this.resetTableRowActionProperties(this.navRowIndex)

				// Set navigation state on this element
				if(actionElement?.hasAttribute('data-table-action-selected'))
					actionElement.setAttribute('data-table-action-selected', true)

				// Prevent re-rendering rows when changing the row navigated property
				this.rerenderRowsOnNextChange = false

				// Set the row property that signals if the row is navigated
				if(multiIndex === 'h')
					this.emitEvent('set-property', ['headerRow', 'isNavigated'], true)
				else
					this.emitEvent('set-row-index-property', multiIndex, 'isNavigated', true)
			},

			/**
			 * FOR: TABLE KEYBOARD OPERATION
			 * Focusout handler for table rows
			 * Used to update keyboard navigation state when focusing away from a row or row sub-element
			 * so keyboard navigation can continue from there
			 * @param event {object} Event object
			 */
			rowOnFocusout(event)
			{
				// Actual element focusout happens on
				const element = event?.target

				// Get row element
				let rowElement = element
				while(rowElement?.tagName !== 'TR' && rowElement?.parentElement)
					rowElement = rowElement.parentElement

				// Element focus went to, if any
				const elementFocused = event?.relatedTarget

				// If element focus went to is in the row, navigation stays on the row
				if(rowElement.contains(elementFocused))
					return

				// Reset properties of action elements
				this.resetTableRowActionProperties(this.navRowIndex)

				//Get multi-index of row
				const multiIndex = rowElement.getAttribute('index')

				// Prevent re-rendering rows when changing the row navigated property
				this.rerenderRowsOnNextChange = false

				// Set the row property that signals if the row is navigated
				if(multiIndex === 'h')
					this.emitEvent('set-property', ['headerRow', 'isNavigated'], false)
				else
					this.emitEvent('set-row-index-property', multiIndex, 'isNavigated', false)
			},

			/**
			 * Go to page
			 * @param pageNumber {number}
			 */
			goToPage(pageNumber)
			{
				this.page = pageNumber

				if (!this.serverMode) this.paginateFilter()
				else this.changeQuery(pageNumber)

				if (!this.readonly && this.rowsSelectableMultiple) this.initHeaderSelector()
			},

			/**
			 * Set records per page
			 * @param perPage {number}
			 */
			setPerPage(perPage)
			{
				this.perPage = perPage

				// Update property in table model object
				this.emitEvent('set-property', ['config', 'perPageSelected'], this.perPage)

				if (!this.serverMode)
				{
					const doPaginateFilter = this.page === 1

					if (!this.preservePageOnDataChange) this.page = 1

					if (doPaginateFilter) this.paginateFilter()
				} else
				{
					this.emitEvent('set-confirm-changes', true)
					this.changeQuery()
				}
			},

			/**
			 * Initialize search
			 * @param {string} emitSearch
			 */
			initGlobalSearch(emitSearch = false)
			{
				// Update property in table model object
				this.emitEvent('set-property', ['searchValue'], this.searchBarConfig.init.value)

				if (emitSearch) this.updateSearch()
			},

			/**
			 * Initialize search
			 */
			initHeaderSelector()
			{
				this.emitEvent('fetch-qtable-all-selected', this.id)

				//If less than three records, disable button
				if (this.vbtAllSelected)
				{
					this.checkCurrentPageRows(true)
					this.disableAllChecks()
				}
			},

			/**
			 * Update sorting data
			 * @param {string} columnName Column name
			 * @param {string} sortOrder Sort direction
			 */
			updateSortQuery(columnName, sortOrder)
			{
				this.emitEvent('set-property', ['columnSorting'], { columnName, sortOrder })
			},

			/**
			 * Change sorting
			 * @param {string} columnName Column name
			 * @param {string} sortOrder Sort direction
			 */
			changeSort(columnName, sortOrder)
			{
				// Change sorting data in the table model object
				this.updateSortQuery(columnName, sortOrder)

				// Change sorting
				if (this.serverMode)
					this.changeQuery()
				else
					this.sort()
			},

			/**
			 * Change sorting used when loading the table
			 * @param {string} columnName Column name
			 * @param {string} sortOrder Sort direction
			 */
			changeInitialSort(columnName, sortOrder)
			{
				this.emitEvent('set-confirm-changes', true)
				this.changeSort(columnName, sortOrder)
			},

			/**
			 * Inititalize row
			 * @param row {Object}
			 * @param rowRum {number}
			 */
			initRow(row, rowRum)
			{
				if (rowRum === undefined) row.Rownum = -1
				else row.Rownum = rowRum

				row.Fields = {}
			},

			/**
			 * Updates to do when columns are changed
			 * FOR: ROW ACTIONS, EXTENDED ACTIONS, COLUMN ORDER AND VISIBILITY
			 * Must be called when loading or changing columns
			 */
			updateColumns()
			{
				//Put references to columns in vbtColumns
				this.vbtColumns.forEach(column => {
					if(typeof column.destroy === 'function')
						column.destroy()
				})
				this.vbtColumns.splice(0)

				for (const column of this.columns)
					this.vbtColumns.push(column.clone?.() ?? cloneDeep(column))

				//FOR: TABLE LIST ROW ACTIONS
				//BEGIN: Add row actions column
				if (this.hasRowActions)
				{
					const actionsColumn = {
						label: this.texts.actionMenuTitle,
						name: 'actions',
						slotName: 'actions',
						dataType: 'Action',
						isActions: true,
						columnClasses: 'row-actions',
						columnHeaderClasses: 'thead-actions',
						rowTextAlignment: 'text-center',
						columnTextAlignment: 'text-center'
					}

					if (this.actionsPlacement === 'right')
						this.vbtColumns.push(actionsColumn)
					else if (this.actionsPlacement === 'left')
						this.vbtColumns.unshift(actionsColumn)
				}
				//END: Add row actions column

				//BEGIN: Add checklist column
				const checklistColumn = {
					label: '',
					name: 'Checkbox',
					slotName: 'checklist',
					dataType: 'Checkbox',
					isChecklist: true,
					columnClasses: 'row-checklist',
					checkListName: 'CheckList Name',
					checkListTitle: 'CheckList Title'
				}

				if (this.rowsSelectableMultiple !== false)
					this.vbtColumns.unshift(checklistColumn)
				//END: Add checklist column

				//FOR: EXTENDED ROW ACTIONS
				//Add row actions column
				if (this.hasExtendedRowActions)
				{
					this.vbtColumns.unshift({
						label: '',
						name: 'ExtendedAction',
						dataType: 'ExtendedAction',
						isExtendedActions: true,
						columnClasses: 'row-extended-actions',
						columnHeaderClasses: 'thead-actions'
					})
				}

				//FOR: TABLE LIST ROW DRAG AND DROP
				//BEGIN: Add drag and drop column
				const dragColumn = {
					label: this.texts.rowDragAndDropTitle,
					name: 'dragAndDrop',
					slotName: 'dragAndDrop',
					isDragAndDrop: true,
					columnClasses: 'row-orders',
					columnHeaderClasses: 'thead-actions',
					rowTextAlignment: 'text-center',
					columnTextAlignment: 'text-center'
				}

				if (this.showRowDragAndDropOption || this.hasRowDragAndDrop)
					this.vbtColumns.unshift(dragColumn)
				//END: Add drag and drop column

				//BEGIN: Add totalizer title column (only if all columns in the table are data columns)
				if (this.columns.some(column => column.totalizer && listFunctions.isVisibleColumn(column))) {
					if (this.vbtColumns.every(column => this.isDataColumn(column))) {
						const totalizerColumn = {
							label: '',
							name: 'totalizer',
							slotName: 'totalizer',
							isTotalizer: true,
							columnClasses: 'row-orders',
							columnHeaderClasses: 'thead-actions',
							rowTextAlignment: 'text-center',
							columnTextAlignment: 'text-center'
						}

						this.vbtColumns.unshift(totalizerColumn)
					}

					// Find first non data column - this is the column that will have the ´Total´ title in the totalizer row
					const firstNonDataColumnIdx = this.vbtColumns.findIndex(column => !this.isDataColumn(column))

					this.vbtColumns[firstNonDataColumnIdx].isTotalizerTitle = true
				}
				//END: Add totalizer column

				// If tree table, create column hierarchy
				if (this.type === 'TreeList')
					this.columnHierarchy = listFunctions.getColumnHierarchy(this.vbtColumns)

				this.$nextTick().then(() => {
					if (this.$refs.tableElem && this.$refs.tableContainerElem)
					{
						//FOR: COLUMN RESIZE
						this.applyColumnResizeable()

						//FOR: COLUMN SIZES
						this.setColumnSizes(this.columnSizes)
					}
				})
			},

			//FOR: COLUMN RESIZE
			/**
			 * Apply the column resizable feature
			 */
			applyColumnResizeable()
			{
				if (this.resizableGrid)
					this.resizableGrid.destroy()

				if (this.isListVisible && this.allowColumnResize)
				{
					this.resizableGrid = markRaw(
						new ColumnResizeable(
							this.$refs.tableElem,
							this.columnResizeOptions,
							this.$refs.tableFooterElem,
							this.$refs.tableContainerElem,
							this.$refs.tableWrapperElem
						)
					)

					this.resizableGrid.init()
				}
			},

			/**
			 * Get CSS class for table action element
			 * @param action {Object}
			 * @returns CSS class
			 */
			getActionButtonClass(action)
			{
				return has(action, 'class') ? action.class : 'btn-secondary'
			},

			/**
			 * Sort by column
			 */
			sort()
			{
				if (this.columnSorting.columnName && this.columnSorting.sortOrder)
				{
					this.tempFilteredResults = orderBy(
						this.tempFilteredResults,
						(row) => {
							const value = get(row.Fields, this.columnSorting.columnName)
							return value?.toString().toLowerCase() ?? ''
						},
						[this.columnSorting.sortOrder]
					)
				}

				this.paginateFilter()
			},

			/**
			 * Call search function
			 */
			updateSearch()
			{
				if (this.serverMode) this.changeQuery()
				else this.filter(!this.preservePageOnDataChange)
			},

			/**
			 * Filter rows (built-in method)
			 * @param resetPage {Boolean}
			 * @param isInit {Boolean}
			 * @returns Array (success) or Boolean (?)
			 */
			filter(resetPage = true, isInit = false)
			{
				const res = filter(this.rows, () => true)

				this.tempFilteredResults = res

				// Do global search only if global search text is not empty and
				// filtered results is also not empty
				if ((this.searchValue !== '' || this.hasSearchBarFilters) && this.rowCount !== 0)
				{
					this.tempFilteredResults = this.search(this.tempFilteredResults)
				}

				this.sort()
				if (resetPage || this.rowCount === 0)
				{
					this.page = 1
				} else if (!isInit)
				{
					const newTotalPage = Math.ceil(this.rowCount / this.perPage)
					this.page = this.page <= newTotalPage ? this.page : newTotalPage
				}
			},

			/**
			 * Search rows (built-in method)
			 * @param tempFilteredResults {Object}
			 * @returns Array (success) or Boolean (?)
			 */
			search(tempFilteredResults)
			{
				const globalSearchResults = filter(tempFilteredResults, (row) => {
					let flag = false

					this.vbtColumns.some((vbt_column) =>
					{
						let searchValue = this.searchValue
						if (this.hasSearchBarFilters)
						{
							//Skip fields that do not have corresponding column filters
							if (this.searchBarFilters[vbt_column.area + '.' + vbt_column.field] === undefined)
							{
								return
							} else
							{
								searchValue = this.searchBarFilters[vbt_column.area + '.' + vbt_column.field].conditions[0].values[0]
							}
						}

						let value = this.getCellDataSearch(row, vbt_column, {})

						if (value === null || typeof value === 'undefined')
						{
							value = ''
						}

						if (typeof value !== 'string')
						{
							value = value.toString()
						}

						if (typeof searchValue !== 'string')
						{
							searchValue = searchValue.toString()
						}

						if (!this.searchBarConfig.caseSensitive)
						{
							value = value.toLowerCase()
							searchValue = searchValue.toLowerCase()
						}

						if (value.indexOf(searchValue) > -1)
						{
							flag = true
							return
						}
					})

					return flag
				})

				return globalSearchResults
			},

			/**
			 * Get array of rows for current page (built-in method)
			 * @returns Array of rows
			 */
			paginateFilter()
			{
				if (this.pagination)
				{
					const start = (this.page - 1) * this.perPage
					const end = start + this.perPage
					this.vbtRows = this.tempFilteredResults.slice(start, end)
				} else
				{
					this.vbtRows = [...this.tempFilteredResults]
				}
			},

			//FOR: TABLE LIST ACTIONS
			/**
			 * Returns true if the action is to be called and false otherwise (if false then selects the row clicked)
			 * @param row {Object}
			 */
			doClickAction(row)
			{
				if (!this.loaded) return false

				switch (this.rowClickActionInternal)
				{
					case 'selectSingle':
						this.toggleRowSelectSingle(row)
						return false
					case 'selectMultiple':
						if (!this.readonly)
							this.toggleRowSelectMultiple(row)
						return false
					default:
						return true
				}
			},

			/**
			 * Emit event and object representing action
			 * @param row {Object}
			 * @param column {Object}
			 */
			executeActionCell(row, column)
			{
				const emitAction = { row: row }

				if (column !== undefined) emitAction['column'] = column

				this.emitEvent('cell-action', emitAction)
			},

			//FOR: EXTENDED ROW ACTIONS
			disableAllChecks()
			{
				this.blockTableCheck = true
			},

			enableAllChecks()
			{
				this.blockTableCheck = false
			},

			/**
			 * Check all rows in "every" checklist column page
			 * @returns Boolean
			 */
			checkAllRows()
			{
				if (!this.vbtAllSelected)
				{
					this.checkCurrentPageRows()

					this.emitEvent('set-qtable-all-selected', { isSelected: true, id: this.id })
					this.vbtAllSelected = true

					//Make sure no one can uncheck rows
					this.disableAllChecks()
				}
			},

			/**
			 * Check all rows in current checklist column page
			 * @returns Boolean
			 */
			checkCurrentPageRows(isInit = false)
			{
				if (this.vbtAllSelected && !isInit)
				{
					this.emitEvent('set-qtable-all-selected', { isSelected: false, id: this.id })
					this.vbtAllSelected = false

					//Re-enable rows
					this.enableAllChecks()
				}

				let key = '',
					row = {}

				const rowKeysArray = {}

				//Check all visible rows if they aren't checked already
				if (!this.isSelectedAllRows)
				{
					for (key in this.rows)
					{
						row = this.rows[key]

						//If row is not already selected
						if (!this.isRowSelected(row))
						{
							//Add to selected rows
							rowKeysArray[row.rowKey] = true
						}
					}
					this.emitEvent('select-rows', rowKeysArray)
				}
			},

			/**
			 * Uncheck all rows in "every" checklist column page
			 * @returns Boolean
			 */
			checkNoneRows()
			{
				if (this.vbtAllSelected)
				{
					this.emitEvent('set-qtable-all-selected', { isSelected: false, id: this.id })
					this.vbtAllSelected = false

					//Re-enable rows
					this.enableAllChecks()
				}

				this.emitEvent('unselect-all-rows')
			},

			//FOR: TABLE LIST COLUMN SUPPORT FORMS
			/**
			 * Determine if column has a support form associated
			 * @param column {Object}
			 * @returns Boolean
			 */
			columnHasSupportForm(column)
			{
				return has(column, 'supportForm') && column.supportForm.length > 0
			},

			/**
			 * Get primary key of row
			 * @param row {Object}
			 * @returns String
			 */
			getRowPk(row)
			{
				if (row.Fields === undefined || this.pkColumn === undefined)
				{
					return null
				}
				return row.Fields[this.pkColumn]
			},

			/**
			 * Get primary key of row
			 * @param row {Object}
			 * @returns String
			 */
			getRowKey(row)
			{
				return this.getRowPk(row) || row.Rownum
			},

			//FOR: Formatting field data
			/**
			 * Get formatted string representing cell value. Calls formatting function based on column data type.
			 * @param row {Object}
			 * @param column {Object}
			 * @param options {Object} [optional]
			 * @returns String(plain text or HTML)
			 */
			getCellDataDisplay(row, column, options)
			{
				if (options !== undefined)
				{
					return listFunctions.getCellValueDisplay(this, row, column, options)
				}

				return listFunctions.getCellValueDisplay(this, row, column)
			},

			/**
			 * Get string for title attribute of each cell in a row
			 * @param row {Object}
			 * @param columns {Object}
			 * @param options {Object} [optional]
			 * @returns String
			 */
			getRowCellDataTitles(row, columns, options)
			{
				const cellTitles = {}

				if (options !== undefined)
				{
					options = {}
				}

				for (const col in columns)
				{
					const column = columns[col]

					// Columns with multiple values will display a tooltip for each badge instead of a single tooltip for the entire cell.
					if (column.isHtmlField || column.multipleValues) cellTitles[column.name] = ''
					else cellTitles[column.name] = listFunctions.getCellValueDisplay(this, row, column, options)
				}

				return cellTitles
			},

			/**
			 * Get string for search value of cell data
			 * @param row {Object}
			 * @param column {Object}
			 * @param options {Object} [optional]
			 * @returns String
			 */
			getCellDataSearch(row, column, options)
			{
				if (options !== undefined)
				{
					return listFunctions.getCellValueSearch(this, row, column, options)
				}

				return listFunctions.getCellValueSearch(this, row, column)
			},

			//BEGIN: Row methods
			/**
			 * Determine if row is in a valid state
			 * @param row {Object}
			 * @returns String
			 */
			rowIsValid(row)
			{
				if (this.config.rowValidation)
				{
					if (this.config.rowValidation.fnValidate(row) === false)
					{
						//Row is not valid
						return false
					}
				}
				return true
			},

			/**
			 * Get row CSS classes
			 * @param row {Object}
			 * @returns String
			 */
			getRowClasses(row, columnsLevel = 0)
			{
				const rowClasses = []

				if (this.hasRowClickAction) rowClasses.push('c-table__row--clickable')

				if (this.rowIsValid(row) === false) rowClasses.push(this.config.rowValidation.class ?? 'c-table__row--pending')
				else if (columnsLevel > 0) rowClasses.push('q-tree-table-row')

				return rowClasses.join(' ')
			},

			/**
			 * Get row title (for title attribute)
			 * @param row {Object}
			 * @returns String
			 */
			getRowTitle(row)
			{
				if (this.rowIsValid(row) === false)
				{
					return this.config.rowValidation.message.value
				}
				return ''
			},

			//FOR: ROW CLICK ACTION (default click action or select row)
			/**
			 * Do action when clicking on row: default click action (emit) or select row
			 * @param row {Object}
			 */
			executeRowClickAction(row)
			{
				if (this.blockTableCheck || this.hasRowDragAndDrop)
				{
					return
				}

				//Remove child rows
				row = this.rowWithoutChildren(row)

				//Row actions that do not emit data outside of the component
				if (!this.doClickAction(row)) return

				//Execute default row action
				const rowKeyPath = listFunctions.getRowKeyPath(this.rows, row)
				if (Object.keys(this.rowClickAction).length > 0) this.emitEvent('row-action', { id: this.rowClickAction.id, rowKeyPath })
				else this.emitEvent('row-action', { rowKeyPath })
			},

			/**
			 * Toggle selecting/deselecting single row
			 * @param row {Object}
			 */
			toggleRowSelectSingle(row)
			{
				const rowKeyPath = listFunctions.getRowKeyPath(this.rows, row)

				//If row is already selected, remove from selected rows
				if (this.isRowSelected(row))
				{
					this.emitEvent('unselect-row', rowKeyPath)
				} else
				{
					//Add to selected rows
					this.emitEvent('select-row', { rowKeyPath, multipleSelection: false })
				}
			},

			/**
			 * Toggle selecting/deselecting row (add to or remove from selected rows array)
			 * @param row {Object}
			 */
			toggleRowSelectMultiple(row)
			{
				const rowKeyPath = listFunctions.getRowKeyPath(this.rows, row)

				//If row is already selected, remove from selected rows
				if (this.isRowSelected(row))
				{
					this.emitEvent('unselect-row', rowKeyPath)
				} else
				{
					//If row is not already selected, add to selected rows
					this.emitEvent('select-row', { rowKeyPath, multipleSelection: true })
				}
			},

			/**
			 * Determine if row is selected
			 * @param row {Object}
			 * @returns Boolean
			 */
			isRowSelected(row)
			{
				const rowKeyPath = listFunctions.getRowKeyPath(this.rows, row)

				for (const x in this.rowsSelected)
				{
					if (x.toString() === rowKeyPath.toString())
					{
						return true
					}
				}
				return false
			},

			//FOR: EXTENDED ROW ACTIONS
			/**
			 * Emit event to remove selected row
			 * @param rowKey {String}
			 * @returns Boolean
			 */
			removeRow(rowKey)
			{
				this.emitEvent('remove-row', rowKey)
			},
			//END: Row methods

			/**
			 * Get sum of values of column in rows
			 * @param column {Object}
			 * @param selectedOnly {Boolean} total only selected rows
			 * @returns Number
			 */
			getColumnTotalValue(column, selectedOnly)
			{
				if (column.showTotal)
				{
					let total = 0
					let row = {}

					if (selectedOnly === undefined)
					{
						selectedOnly = false
					}

					//Sum column values of rows
					if (column.showTotal !== false)
					{
						for (const idx in this.rows)
						{
							row = this.rows[idx]

							if (selectedOnly === false || this.isRowSelected(row))
							{
								total += this.getValueFromRow(row, column)
							}
						}

						return total
					}
				}
				return ''
			},

			/**
			 * Submit selected rows
			 * @param event {String}
			 * @returns
			 */
			rowGroupAction(event)
			{
				this.emitEvent('row-group-action', {
					rowsSelected: this.rowsSelected,
					allSelected: this.vbtAllSelected,
					action: event.action
				})
			},

			/**
			 * Clear search (built-in method)
			 */
			clearGlobalSearch()
			{
				// Update property in table model object
				this.emitEvent('set-property', ['searchValue'], '')
				this.updateSearch()
			},

			/**
			 * Reset search query
			 */
			resetQuery()
			{
				this.clearSearchBarFilters()

				//Clear search bar
				if (this.searchValue?.length > 0)
					// Update property in table model object
					this.emitEvent('set-property', ['searchValue'], '')

				this.emitEvent('reset-query')
				this.updateSearch()
			},

			/**
			 * Emit search event (built-in method)
			 * @param searchValue {String}
			 */
			emitSearch(searchValue)
			{
				// Update property in table model object
				this.emitEvent('set-property', ['searchValue'], searchValue)
				this.updateSearch()
			},

			/**
			 * Emit search query event (update page if necessary)
			 * @param page {Number}
			 */
			changeQuery(page)
			{
				// Update page number
				if (typeof page === 'undefined')
				{
					if (!this.preservePageOnDataChange)
						page = 1
					else
						page = this.page
				}

				// Update page property in table model object
				this.emitEvent('set-property', ['config', 'page'], page)

				// Signal change in query
				if (this.serverMode && this.canEmitQueries)
					this.emitEvent('on-change-query')
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

			//BEGIN: FOR: SEARCH FILTERS
			/**
			 * Edit column filter
			 * @param {string} fullColumnName
			 * @param {Object} filter
			 */
			editColumnFilter(fullColumnName, filter)
			{
				// Clone column filters and replace the one being edited
				const columnFilters = cloneDeep(this.columnFilters)
				columnFilters[fullColumnName] = filter

				// Set property in table model object
				this.emitEvent('set-property', ['columnFilters'], columnFilters)
				this.emitEvent('set-confirm-changes', true)

				this.updateSearch()
			},

			/**
			 * Remove column filter
			 * @param {string} fullColumnName
			 */
			removeColumnFilter(fullColumnName)
			{
				// Clone column filters and remove this
				const columnFilters = cloneDeep(this.columnFilters)
				delete columnFilters[fullColumnName]

				// Set property in table model object
				this.emitEvent('set-property', ['columnFilters'], columnFilters)
				this.emitEvent('set-confirm-changes', true)

				this.updateSearch()
			},

			/**
			 * Clear all column filters
			 * @param {string} emitSearch
			 */
			clearColumnFilters(emitSearch = false)
			{
				// Set property in table model object
				this.emitEvent('set-property', ['columnFilters'], {})

				if (emitSearch)
				{
					this.updateSearch()
				}
			},

			/**
			 * Remove search bar filter
			 * @param {string} fullColumnName
			 */
			removeSearchBarFilter(fullColumnName)
			{
				// Clone search bar filters without this one and set as the new value
				const searchBarFilters = cloneDeep(this.searchBarFilters)
				delete searchBarFilters[fullColumnName]

				// Update property in table model object
				this.emitEvent('set-property', ['searchBarFilters'], searchBarFilters)

				this.updateSearch()
				this.signalSearch = { resetQuery: true }
			},

			/**
			 * Clear all search bar filters
			 * @param {string} emitSearch
			 */
			clearSearchBarFilters(emitSearch = false)
			{
				// Update property in table model object
				this.emitEvent('set-property', ['searchBarFilters'], {})
				if (emitSearch)
				{
					this.updateSearch()
				}
			},

			/**
			 * Search by a column for a value
			 * @param column {Object}
			 * @param value {String}
			 */
			searchByColumn(column, value)
			{
				// Normalize value
				value = column.getNormalizedValue(value)

				const columnName = listFunctions.columnFullName(column)
				const operator = searchFilterData.searchBarOperator(column.searchFieldType, value)

				// Create hashtable with this column name as the key and the filter as the value
				const searchBarFilters = {}
				searchBarFilters[columnName] = listFunctions.searchFilter('', true, [
					listFunctions.searchFilterCondition('', true, listFunctions.columnFullName(column), operator, [value])
				])

				// Update properties in table model object
				this.emitEvent('set-property', ['searchBarFilters'], searchBarFilters)
				this.emitEvent('set-property', ['searchValue'], '')

				this.updateSearch()
			},

			/**
			 * Search all columns for a value
			 * @param value {String}
			 */
			searchByAllColumns(value)
			{
				this.clearSearchBarFilters()
				this.emitSearch(value)
			},

			/**
			 * Remove all advanced filters and remove other custom filters
			 */
			removeAllCustomFilters()
			{
				this.clearColumnFilters()
				this.clearSearchBarFilters()

				// Reset sorting to default without triggering a reload
				// The reload will be triggered by removing the filters
				this.updateSortQuery(this.defaultColumnSorting.columnName, this.defaultColumnSorting.sortOrder)

				//Also reloads table
				this.emitEvent('remove-all-advanced-filters')
			},

			/**
			 * Show advanced filters interface
			 */
			showAdvancedFilters()
			{
				this.emitEvent(
					'signal-component',
					'config',
					{
						show: true,
						selectedTab: 'advanced-filters',
						returnElement: this.configMenuId
					},
					true
				)
				this.emitEvent('signal-component', 'advancedFilters', { columnFilter: null, columnName: null, selectedFilterIdx: undefined }, true)
			},

			/**
			 * Show advanced filters interface for creating a new filter
			 */
			showAdvancedFiltersNew()
			{
				this.emitEvent('signal-component', 'advancedFiltersNew', { show: true, selectedFilterIdx: undefined }, true)
			},
			//END: FOR: SEARCH FILTERS

			/**
			 * Emit action to export table data
			 * @param format {String}
			 * @returns Boolean
			 */
			exportData(format)
			{
				const payload = {
					format: format
				}
				this.emitEvent('on-export-data', payload)
			},

			/**
			 * Emit action to import table data
			 * @param format {String}
			 * @returns Boolean
			 */
			importData(payload)
			{
				this.emitEvent('on-import-data', payload)
			},

			/**
			 * Emit action to download template file
			 * @param format {String}
			 * @returns Boolean
			 */
			exportTemplate(format)
			{
				const payload = {
					format: format
				}
				this.emitEvent('on-export-template', payload)
			},

			/**
			 * Update the active filters
			 * @param activeFilters {Object}
			 */
			updateActiveFilters(activeFilters)
			{
				this.emitEvent('update:activeFilters', activeFilters)
			},

			/**
			 * Update the group filters
			 * @param groupFilters {Object}
			 */
			updateGroupFilters(groupFilters)
			{
				this.emitEvent('update:groupFilters', groupFilters)
			},

			/**
			 * Get value for radio button group based on filter model
			 * @param entry {Object}
			 * @returns String
			 */
			radioValue(entry)
			{
				return this.groupFilterIsMultiple(entry) ? '' : filter.value
			},

			/**
			 * listener for events when shift key is pressed
			 */
			documentOnSelectStart(event)
			{
				return !(event.key === 'Shift' && event.shiftKey === true)
			},

			/**
			 * listener for events when shift key is pressed
			 */
			handleShiftKeyOnSelectStart()
			{
				document.addEventListener('selectstart', this.documentOnSelectStart)
			},

			/**
			 * Add event listeners for events when shift key is pressed
			 */
			handleShiftKey()
			{
				window.addEventListener('keyup', this.handleShiftKeyOnSelectStart)
				window.addEventListener('keydown', this.handleShiftKeyOnSelectStart)
			},

			/**
			 * Determine if column is visible
			 * @param column {Object}
			 * @returns Boolean
			 */
			canShowColumn(column)
			{
				//FOR: DRAG AND DROP COLUMNS
				if ((this.isDragAndDropColumn(column) && !this.hasRowDragAndDrop) || (this.isActionsColumn(column) && this.hasRowDragAndDrop))
					return false
				//For all columns
				return listFunctions.isVisibleColumn(column)
			},

			/**
			 * Toggle showing/hiding filters
			 */
			toggleShowFilters()
			{
				this.filtersVisible = !this.filtersVisible
				this.emitEvent('set-property', ['config', 'filtersVisible'], this.filtersVisible)
			},

			/**
			 * Toggle showing/hiding static filters
			 */
			toggleShowStaticFilters()
			{
				this.staticFiltersVisible = !this.staticFiltersVisible
			},

			/**
			 * Emit to add new row
			 * @param row {Object}
			 * @returns
			 */
			emitRowAdd(row)
			{
				this.emitEvent('row-add', row)

				//Clear object for new row
				this.initRow(this.newRow)
			},

			/**
			 * Emit to update row values
			 * @param row {Object}
			 * @returns
			 */
			emitRowEdit(row)
			{
				this.emitEvent('row-edit', row)
			},

			/**
			 * Emit to delete rows
			 * @param rowKeys {Object}
			 * @returns
			 */
			emitRowsDelete(rowKeys)
			{
				this.emitEvent('rows-delete', rowKeys)

				//Clear checked rows so it doesn't have keys of records that don't exist
				this.emitEvent('unselect-all-rows')
			},

			/**
			 * Set the value of a cell
			 * @param row {Object}
			 * @param column {Object}
			 * @param value {Object}
			 * @returns
			 */
			setCellValue(row, column, value)
			{
				listFunctions.setCellValue(row, column, value)
			},

			/**
			 * Set the value of a cell
			 * @param row {Object}
			 * @param column {Object}
			 * @param value {Object}
			 * @returns
			 */
			setTableCellValue(row, column, value)
			{
				listFunctions.setTableCellValue(this, row, column, value)
			},

			/**
			 * Set the value of a cell
			 * @param row {Object}
			 * @param column {Object}
			 * @param options {Object}
			 * @returns
			 */
			cellOnChange(row, column, options)
			{
				listFunctions.cellOnChange(this, row, column, options)
			},

			/**
			 * Called when updating the value of a cell
			 * @param row {Object}
			 * @param column {Object}
			 * @param value {Object}
			 * @returns
			 */
			updateCell(row, column, event)
			{
				this.emitEvent('update-cell', { row: row, column: column, value: event })

				// Optionally prevent rerendering rows when changing cell values
				if(column.rerenderRowsOnNextChange === false)
					this.rerenderRowsOnNextChange = false
				else
					this.rerenderRows()

				//Set cell value in component data
				this.setCellValue(row, column, event)

				//Set cell value in external table data
				this.setTableCellValue(row, column, event)

				//Call method set to run when changing data in this column
				this.cellOnChange(row, column, {})

				if (this.fieldsEditable)
				{
					this.emitRowEdit(row, column, event)
				}

				if(this.hasRowDragAndDrop && column.isOrderingColumn)
				{
					const index = listFunctions.getCellValue(row, column)
					this.navigateToRow(parseInt(index) - 1)
				}
			},

			/**
			 * Toggle text wrap in cells
			 */
			toggleTextWrap()
			{
				this.hasTextWrap = !this.hasTextWrap
			},

			/**
			 * Toggle row reorder by drag and drop
			 */
			toggleShowRowDragAndDrop()
			{
				if (!this.hasRowDragAndDrop)
				{
					// Sort by the ordering column
					if (this.sortOrderColumn !== undefined && this.sortOrderColumn !== null)
					{
						// Change sorting without saving to table configuration
						this.changeSort(this.sortOrderColumn.name, 'asc')
					}
				}

				this.emitEvent('toggle-rows-drag-drop')

				if (!this.hasRowDragAndDrop)
				{
					this.setPerPage(-1)
				}

				// FOR: GETTING NAVIGABLE ROW ELEMENTS
				// Rerender rows and re'calculate navigable rows
				this.rerenderRows()
			},

			/**
			 * Get the table DOM element
			 * @returns
			 */
			getTableElement()
			{
				const tableElem = this.$refs.tableElem

				if (tableElem === undefined || tableElem === null) return null

				if (Array.isArray(tableElem))
				{
					if (tableElem.length < 1) return null
					return tableElem[0]
				}

				return tableElem
			},

			/**
			 * Get the column header DOM elements
			 * @returns
			 */
			getColumnHeaderElements()
			{
				const columnHeaderDOMElems = []
				//Get table element
				const tableElem = this.getTableElement()
				if (tableElem === undefined || tableElem === null)
				{
					return columnHeaderDOMElems
				}
				//Get row elements
				const rowElems = tableElem.getElementsByTagName('TR')
				if (rowElems === undefined || rowElems === null || rowElems.length < 1)
				{
					return columnHeaderDOMElems
				}
				//Get column header elements
				const columnHeaderElems = rowElems[0].getElementsByTagName('TH')
				if (columnHeaderElems === undefined || columnHeaderElems === null || columnHeaderElems.length < 1)
				{
					return columnHeaderDOMElems
				}
				//Add DOM elements to array
				for (let idx = 0; idx < columnHeaderElems.length; idx++)
				{
					columnHeaderDOMElems.push(columnHeaderElems.item(idx))
				}
				return columnHeaderDOMElems
			},

			/**
			 * Get the column size configuration
			 * @returns
			 */
			getColumnSizes()
			{
				//Get column header elements
				const columnHeaderElems = this.getColumnHeaderElements()

				//Get column sizes
				const columnSizes = {}
				let column = {}
				for (const idx in columnHeaderElems)
				{
					column = columnHeaderElems[idx]
					if (column.style.width.length > 0)
					{
						columnSizes[column.getAttribute('data-column-name')] = column.style.width
					}
				}

				//Get table size
				let tableSize = ''
				const tableElem = this.getTableElement()

				if (tableElem !== undefined && tableElem !== null)
				{
					tableSize = tableElem.style.width
				}

				return { columnSizes: columnSizes, tableSize: tableSize }
			},

			/**
			 * Set the column sizes in the DOM elements
			 * @param columnSizes {Array}
			 * @returns
			 */
			setColumnSizes(columnSizes)
			{
				//Set to default if null
				if (
					columnSizes === undefined ||
					columnSizes === null ||
					columnSizes.columnSizes === undefined ||
					columnSizes.columnSizes === null ||
					columnSizes.tableSize === undefined ||
					columnSizes.tableSize === null
				)
				{
					columnSizes = { columnSizes: {}, tableSize: '' }
				}

				//Get table element
				const tableElem = this.getTableElement()
				if (tableElem === undefined || tableElem === null)
				{
					return
				}

				//Set table size
				tableElem.style.width = columnSizes.tableSize

				//Get column header elements
				const columnHeaderElems = this.getColumnHeaderElements()

				//Set column sizes
				let column = {}
				let columnSize = {}
				for (const idx in columnHeaderElems)
				{
					column = columnHeaderElems[idx]

					columnSize = columnSizes.columnSizes[column.getAttribute('data-column-name')]
					if (columnSize !== undefined && columnSize !== null)
					{
						if (columnSize.length > 0)
						{
							column.style.width = columnSize
						}
					} else
					{
						column.style.width = null
					}
				}

				//Set table footer size
				this.setTableFooterSize()

				//Set table container element size
				const tableContainerElement = this.getTableContainerElement()
				if (tableContainerElement === undefined || tableContainerElement === null) return

				if (columnSizes.tableSize === '') tableContainerElement.style.width = null
			},

			/**
			 * Get the table wrapper DOM element
			 * @returns
			 */
			getTableWrapperElement()
			{
				const tableWrapperElem = this.$refs.tableWrapperElem
				if (tableWrapperElem === undefined || tableWrapperElem === null)
				{
					return null
				}
				if (Array.isArray(tableWrapperElem))
				{
					if (tableWrapperElem.length < 1)
					{
						return null
					}
					return tableWrapperElem[0]
				}

				return tableWrapperElem
			},

			/**
			 * Get the table container DOM element
			 * @returns
			 */
			getTableContainerElement()
			{
				const tableContainerElem = this.$refs.tableContainerElem
				if (tableContainerElem === undefined || tableContainerElem === null)
				{
					return null
				}
				if (Array.isArray(tableContainerElem))
				{
					if (tableContainerElem.length < 1)
					{
						return null
					}
					return tableContainerElem[0]
				}

				return tableContainerElem
			},

			/**
			 * Get the table footer DOM element
			 * @returns
			 */
			getTableFooterElement()
			{
				const tableFooterElem = this.$refs.tableFooterElem
				if (tableFooterElem === undefined || tableFooterElem === null)
				{
					return null
				}
				if (Array.isArray(tableFooterElem))
				{
					if (tableFooterElem.length < 1)
					{
						return null
					}
					return tableFooterElem[0]
				}

				return tableFooterElem
			},

			/**
			 * Set the size of the table footer DOM element
			 * @returns
			 */
			setTableFooterSize()
			{
				const tableElem = this.getTableElement()
				if (tableElem === undefined || tableElem === null)
				{
					return
				}
				const tableWrapperElem = this.getTableWrapperElement()
				if (tableWrapperElem === undefined || tableWrapperElem === null)
				{
					return
				}
				const tableFooterElem = this.getTableFooterElement()
				if (tableFooterElem === undefined || tableFooterElem === null)
				{
					return
				}

				if (tableElem.offsetWidth < tableWrapperElem.offsetWidth)
				{
					tableFooterElem.style.width = tableElem.offsetWidth + 'px'
				} else
				{
					tableFooterElem.style.width = null
				}
			},

			/**
			 * Save current view
			 * @returns
			 */
			saveCurrentView()
			{
				this.emitEvent('save-view', {
					name: this.config.userTableConfigName,
					isSelected: -1
				})

				const alertProps = {
					type: 'success',
					message: this.texts.tableViewSaveSuccess,
					icon: 'ok'
				}
				this.emitEvent('set-info-message', alertProps)
				this.emitEvent('set-confirm-changes', false)
				this.emitEvent('update-config')
			},

			/**
			 * Close current view
			 * @returns
			 */
			closeCurrentView()
			{
				this.emitEvent('close-view', {})
			},

			/**
			 * Fired on column resize
			 * @returns
			 */
			onColumnResize()
			{
				this.emitEvent('set-property', ['config', 'columnSizes'], this.getColumnSizes())
				this.emitEvent('set-confirm-changes', true)
				this.emitEvent('update-config')
			},

			/**
			 * Fired on multiform select
			 * @param row {Object}
			 * @returns
			 */
			onMultiformSelect(row)
			{
				for (const idx in this.rowFormProps)
				{
					if (this.rowFormProps[idx].mode === 'EDIT')
					{
						return
					}
				}
				this.emitEvent('set-array-sub-prop-where', 'rowFormProps', 'id', row.rowKey, 'mode', 'EDIT', 'SHOW')
			},

			/**
			 * Handler for reordering rows with keyboard.
			 * @param evt {Object} { row, sortOrderColumn, shiftValue }
			 * @returns
			 */
			rowReorder(eventData)
			{
				// Emit for row reorder by the server
				const rowKey = this.getRowKey(eventData.row),
					currentIndex = findIndex(this.vbtRows, (row) => this.getRowKey(row) === rowKey),
					index = currentIndex + eventData.shiftValue
				this.emitEvent('row-reorder', { rowKey, index })
				// Set table navigation to the row
				this.navigateToRow(parseInt(index))
				this.emitEvent('set-property', ['config', 'setNavOnUpdate'], true)
				// FOR: GETTING NAVIGABLE ROW ELEMENTS
				// Make rows re-mount
				this.rerenderRows()
			},

			/**
			 * Handler for reordering rows with drag and drop.
			 * @param evt {Object}
			 * @returns
			 */
			onRowDragAndDrop(evt)
			{
				if (evt.newIndex !== evt.oldIndex)
				{
					// Emit for row reorder by the server
					const rowIndex = parseInt(evt.item.getAttribute('index')),
						row = this.vbtRows[rowIndex],
						rowKey = row ? row.rowKey : null,
						index = evt.newIndex
					if (!rowKey) return

					// Removes the class to prevent the selection of other rows text,
					// added when starting the drag
					this.$refs.tableElem.style.userSelect = 'unset'

					this.emitEvent('row-reorder', { rowKey, index })
					// Set table navigation to the row
					this.navigateToRow(parseInt(index))
					this.emitEvent('set-property', ['config', 'setNavOnUpdate'], true)
					// FOR: GETTING NAVIGABLE ROW ELEMENTS
					// Make rows re-mount
					this.rerenderRows()
				}
			},

			destroyRowsDragAndDrop()
			{
				if (this.sortablePlugin && this.sortablePlugin.destroy)
				{
					this.sortablePlugin.destroy()
				}
				this.sortablePlugin = null
			},

			initRowsDragAndDrop()
			{
				this.destroyRowsDragAndDrop()
				try
				{
					if (this.hasRowDragAndDrop)
					{
						this.sortablePlugin = Sortable.create(Array.isArray(this.$refs.tbody) ? this.$refs.tbody[0] : this.$refs.tbody, {
							draggable: 'tr',
							dataIdAttr: 'id',
							direction: 'vertical',
							chosenClass: 'row-grabbed',
							ghostClass: 'row-ghost',
							dragClass: 'row-drag',
							handle: '.c-table__drag',
							animation: 300,
							forceFallback: true,
							fallbackClass: 'row-drag',
							// Adds a class to prevent the selection of other rows text
							onStart: () => this.$refs.tableElem.style.userSelect = 'none',
							onEnd: this.onRowDragAndDrop
						})
					}
				} catch {
					this.destroyRowsDragAndDrop()
				}
			},

			/**
			 * Scroll to row
			 * @param rowId {String}
			 * @param behavior {String}
			 * @returns
			 */
			scrollToRow(rowId, behavior = 'smooth')
			{
				const elem = document.getElementById(rowId)
				if (!elem) return
				const wrapper = this.tableContainerElem
				const dist = elem.offsetTop - 160
				wrapper?.scroll?.({ top: dist, left: 0, behavior })
			},

			/**
			 * Scroll to and select row
			 * @param rowKeyPath {Array/String}
			 * @param rowId {String}
			 * @returns
			 */
			goToRow(rowKeyPath, rowId)
			{
				this.emitEvent('go-to-row', rowKeyPath)

				this.scrollToRow(rowId)
			},

			/**
			 * Emits for table configuration menu
			 * @param id {string}
			 */
			emitConfigAction(id)
			{
				const eObj = find(this.configOptions, ['id', id])
				if (!eObj) return

				// If saving changes to current table view
				if (eObj.id === 'viewSaveChanges') this.saveCurrentView()
				// Other options that open the table configuration pop-up and need an element ID
				// to signal which tab will be selected in the pop-up
				else if (eObj.elementId)
				{
					if (eObj.elementId === 'advanced-filters') this.showAdvancedFilters()
					else
					{
						// For saving a new configuration, the mode must be specified
						// since multiple actions used the same interface that will be opened
						if (eObj.id === 'viewSave')
						{
							this.emitEvent(
								'signal-component',
								'viewSave',
								{ mode: 'SAVE' },
								true
							)
						}
						this.emitEvent('signal-component', 'config', {
							show: true,
							selectedTab: eObj.elementId,
							returnElement: this.configMenuId
						})
					}
				}
			},

			/**
			 * Get view by ID
			 * @param id {number}
			 */
			getViewById(id)
			{
				return find(this.savedViewsOptions, ['id', id])
			},

			/**
			 * Get view ID by Name
			 * @param name {string}
			 */
			getViewIdByName(name)
			{
				const view = find(this.savedViewsOptions, ['value', name])
				if (!view) return 0
				return view.id
			},

			/**
			 * Set view as selected view by ID
			 * @param id {number}
			 */
			setSelectedViewById(id)
			{
				if (id === 0)
				{
					this.closeCurrentView()
				} else
				{
					const selectedViewInfo = find(this.savedViewsOptions, ['key', id])
					if (!selectedViewInfo) return
					//Emit data to script which calls apply function
					this.emitEvent('view-action', { name: 'SHOW', rowValue: selectedViewInfo.value })
				}
			},

			/**
			 * Save view and set view as selected view by ID
			 * @param id {number}
			 */
			saveViewOpenView(id)
			{
				this.saveCurrentView()
				this.setSelectedViewById(id)
			},

			/**
			 * Confirm whether to save if there are changes and set view as selected view by ID
			 * @param id {number}
			 */
			confirmAndSetSelectedViewById(id)
			{
				if (this.confirmChanges && !this.readonly)
				{
					const buttons = {
						confirm: {
							label: this.texts.saveText,
							action: () => { this.saveViewOpenView(id) }
						},
						cancel: {
							label: this.texts.discard,
							action: () => { this.setSelectedViewById(id) }
						}
					}
					genericFunctions.displayMessage(`${this.texts.wantToSaveChangesToView}`, 'warning', null, buttons)
				} else
				{
					this.setSelectedViewById(id)
				}
			},

			setChildRowsVisibility(eventData)
			{
				if (eventData.show === true && eventData.row?.alreadyLoaded === false)
				{
					// Prevent rows from re-rendering when changing the property to show sub-rows
					this.emitEvent('set-property', ['config', 'rerenderRowsOnNextChange'], false)

					this.emitEvent('tree-load-branch-data', { row: eventData.row })
				}
			},

			/**
			 * Get all navigable row elements
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			getNavRowElements()
			{
				// Get main row elements
				const tableElem = this.getTableElement()
				const navRowElemsNL = tableElem.querySelectorAll("tr[data-table-action-selected]")
				const navRowElems = Array.from(navRowElemsNL)

				// Add header row if it has focusable elements
				const headerRowElement = this.$refs.headerRowElem?.headerRowRef
				const headerRowActionElements = this.getTableRowActionElements(headerRowElement)
				if(headerRowActionElements.length > 0)
					navRowElems.unshift(headerRowElement)

				return navRowElems
			},

			/**
			 * Set all navigable row elements
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			setNavRowElements()
			{
				/**
				 * Check if table element exists first. If not, cancel.
				 * This can happen if the table is already being unmounted because
				 * the rows are unmounted and changes to the rows trigger this function.
				 */
				const tableElem = this.getTableElement()
				if(tableElem === undefined || tableElem === null)
					return

				this.navRowElems = this.getNavRowElements()

				/**
				 * Set index of first data row.
				 * An index of 'h' is used for the header row.
				 * If element 0 of the array is the header row, the first data row is element 1
				 */
				if(this.navRowElems.length > 0 && this.navRowElems[0].getAttribute('index') === 'h')
					this.firstDataRowIndex = 1
				else
					this.firstDataRowIndex = 0
			},

			/**
			 * Set number of rows to render from row data loaded
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			setRowsToLoad()
			{
				// Track number of rows to render
				this.rowsToLoad = this.vbtRows?.length
			},

			/**
			 * Re-render rows
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			rerenderRows()
			{
				// Track number of rows to render
				this.setRowsToLoad()
				// Make rows re-mount
				this.rowDomKey++
			},

			/**
			 * Called when a row is rendered
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			onRowLoaded()
			{
				this.rowsToLoad--
				if(this.rowsToLoad === 0)
					this.onRowsLoaded()
			},

			/**
			 * Called when rows are rendered
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			onRowsLoaded()
			{
				this.emitEvent('rows-loaded')
				this.setNavRowElements()
			},

			/**
			 * Called when all sub-rows are loaded
			 * FOR: GETTING NAVIGABLE ROW ELEMENTS
			 */
			onSubRowsLoaded()
			{
				this.emitEvent('rows-loaded')
				this.setNavRowElements()
			}
		},
		//END: methods

		watch: {
			'rows': {
				handler(newVal)
				{
					this.vbtRows = newVal

					if (!this.serverMode)
					{
						this.filter(!this.preservePageOnDataChange, !this.isFirstTime)
					} else
					{
						if (this.preservePageOnDataChange)
						{
							const predictedTotalPage = Math.ceil(this.rowCount / this.perPage)
							if (predictedTotalPage !== 0)
							{
								this.page = this.page <= predictedTotalPage ? this.page : predictedTotalPage
							} else
							{
								this.page = 1
							}
						}
					}
					this.isFirstTime = false

					if (this.vbtAllSelected) this.checkCurrentPageRows(true)

					// Track number of rows to render
					this.setRowsToLoad()

					if(this.rerenderRowsOnNextChange)
					{
						// FOR: GETTING NAVIGABLE ROW ELEMENTS
						// Rerender rows and re-calculate navigable rows
						this.rerenderRows()
					}
					else
					{
						this.rerenderRowsOnNextChange = true
						this.emitEvent('set-property', ['config', 'rerenderRowsOnNextChange'], true)
					}
				},
				deep: true
			},

			'columns': {
				handler()
				{
					//FOR: ROW ACTIONS, EXTENDED ACTIONS, COLUMN ORDER AND VISIBILITY
					//Must be called when loading or changing columns
					this.updateColumns()
				},
				deep: true
			},

			'config.columnSizes': {
				handler()
				{
					this.setColumnSizes(this.config.columnSizes)
				}
			},

			'dataImportResponse': {
				handler(newVal)
				{
					if (this.serverMode)
					{
						//Reload table if import is successful
						if (typeof newVal.success !== 'undefined')
						{
							if (newVal.success !== false)
							{
								this.changeQuery()
							}
						}
					}
				},
				deep: true
			},

			activeViewModeId()
			{
				if (this.isListVisible)
				{
					this.$nextTick().then(() => {
						if (this.$refs.tableElem && this.$refs.tableContainerElem)
						{
							//FOR: COLUMN RESIZE
							this.applyColumnResizeable()

							//FOR: COLUMN SIZES
							this.setColumnSizes(this.columnSizes)
						}
					})
				}
			},

			signal: {
				handler(newValue)
				{
					if (newValue.saveCurrentView !== undefined)
					{
						this.saveCurrentView()
					}
				},
				deep: true
			},

			'config': {
				handler()
				{
					this.initConfig()
				},
				deep: true
			},

			'hasRowDragAndDrop'(newValue)
			{
				if (newValue) this.initRowsDragAndDrop()
				else this.destroyRowsDragAndDrop()
			},

			'allSelectedRows': {
				handler(newValue) {
					if (newValue === "true")
						this.checkAllRows()
					else
						this.checkNoneRows()
				},
				deep: true,
				immediate: true
			}
		}
	}
</script>
