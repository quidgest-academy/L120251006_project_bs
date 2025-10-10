<template>
	<q-table-config
		:table-ctrl="listCtrl"
		modal-id="config"
		v-bind="listCtrl.config"
		v-on="tableConfigHandlers"
		:signal="listCtrl.subSignals.config"
		:texts="listCtrl.texts" />

	<q-table-column-config
		v-if="isListVisible"
		modal-id="column-config"
		v-bind="listCtrl.config"
		v-on="tableColumnConfigHandlers"
		:signal="listCtrl.subSignals.columnConfig"
		:columns="listCtrl.columns"
		:default-search-column-name="listCtrl.config.defaultSearchColumnName"
		:texts="listCtrl.texts" />

	<q-table-advanced-filters
		v-if="listCtrl.config.allowAdvancedFilters"
		modal-id="advanced-filters"
		v-bind="listCtrl.config"
		v-on="tableAdvancedFilters"
		:signal="listCtrl.subSignals.advancedFilters"
		:table-name="listCtrl.config.name"
		:columns="listCtrl.columns"
		:filters="listCtrl.advancedFilters"
		mode="editAll"
		:texts="listCtrl.texts"
		:locale="listCtrl.locale"
		:filter-operators="filterOperators" />

	<q-table-advanced-filters
		v-if="listCtrl.config.allowAdvancedFilters"
		modal-id="advanced-filters-new"
		v-bind="listCtrl.config"
		v-on="tableAdvancedFilters"
		:signal="listCtrl.subSignals.advancedFiltersNew"
		:table-name="listCtrl.config.name"
		:columns="listCtrl.columns"
		:filters="listCtrl.advancedFilters"
		mode="new"
		:texts="listCtrl.texts"
		:locale="listCtrl.locale"
		:filter-operators="filterOperators" />

	<q-table-view-save
		modal-id="view-save"
		v-bind="listCtrl.config"
		v-on="tableViewSaveHandlers"
		:signal="listCtrl.subSignals.viewSave"
		:config-names="listCtrl.config.userTableConfigNames"
		:texts="listCtrl.texts" />

	<q-table-views
		modal-id="views"
		v-bind="listCtrl.config"
		v-on="tableViewHandlers"
		:signal="listCtrl.subSignals.views"
		:config-names="listCtrl.config.userTableConfigNames"
		:config-name-default="listCtrl.config.userTableConfigNameDefault"
		:texts="listCtrl.texts" />
</template>

<script>
	import { toValue } from 'vue'

	import searchFilterDataModule from '@/api/genio/searchFilterData'

	import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'

	import QTableConfig from './QTableConfig.vue'
	import QTableColumnConfig from './QTableColumnConfig.vue'
	import QTableAdvancedFilters from './QTableAdvancedFilters.vue'
	import QTableViewSave from './QTableViewSave.vue'
	import QTableViews from './QTableViews.vue'

	export default {
		name: 'QTableExtraExtension',

		emits: [
			'signal-component',
			'show-popup',
			'hide-popup',
			'set-property',
			'set-info-message',
			'save-column-config',
			'apply-column-config',
			'reset-column-config',
			'toggle-text-wrap',
			'add-advanced-filter',
			'edit-advanced-filters',
			'set-advanced-filter-state',
			'remove-advanced-filter',
			'remove-column-filter',
			'save-view',
			'rename-view',
			'copy-view',
			'select-view',
			'view-action'
		],

		components: {
			QTableConfig,
			QTableColumnConfig,
			QTableAdvancedFilters,
			QTableViewSave,
			QTableViews
		},

		inheritAttrs: false,

		props: {
			/**
			 * Control object containing necessary state and configuration properties for a list-style view of the table component.
			 */
			listCtrl: {
				type: Object,
				required: true
			},

			/**
			 * A set of operator definitions used for creating and managing advanced filters in the table.
			 */
			filterOperators: {
				type: Object,
				default: () => new searchFilterDataModule.SearchFilterConditionOperators()
			}
		},

		expose: [],

		setup(props, ctx)
		{
			const emitEvent = ctx.emit
			const _confUserTableConfigName = toValue(props.listCtrl.config.userTableConfigName)
			const alertProps = {
				type: 'success',
				message: `${props.listCtrl.texts.tableViewSaveSuccess}`,
				icon: 'ok',
				pinned: true
			}

			const emitEventCallbackParams = function(callbackParams)
			{
				emitEvent(callbackParams.eventName, callbackParams.eventData)
			}

			const saveViewOpenView = function(callbackParams)
			{
				emitEvent('save-view', {
					name: _confUserTableConfigName,
					isSelected: -1
				})
				emitEventCallbackParams(callbackParams)
			}

			const emitViewEvent = function(eventName, eventData)
			{
				if (
					eventName === undefined || eventName === null || eventName === ''
					|| eventData?.name === undefined || eventData?.name === null || eventData?.name === ''
				)
					return

				switch(eventData?.name)
				{
					case 'SHOW':
						//Opening a view, confirm whether to save changes to current view
						if(props.listCtrl.confirmChanges && !props.listCtrl.readonly)
						{
							genericFunctions.displayMessage(
								`${props.listCtrl.texts.wantToSaveChangesToView}`,
								'warning',
								null,
								{
									confirm: {
										label: `${props.listCtrl.texts.saveText}`,
										action: () => { saveViewOpenView({ eventName: eventName, eventData: eventData }) }
									},
									cancel: {
										label: `${props.listCtrl.texts.discard}`,
										action: () => { emitEventCallbackParams({ eventName: eventName, eventData: eventData }) }
									}
								}
							)
						}
						else
							emitEvent(eventName, eventData)
						break
					case 'RENAME':
						emitEvent(
							'signal-component',
							'viewSave',
							{ mode: eventData?.name, renameFromName: eventData.rowValue },
							true
						)
						emitEvent('signal-component', 'config', { selectedTab: 'view-save' }, false)
						break
					case 'DUPLICATE':
						emitEvent(
							'signal-component',
							'viewSave',
							{ mode: eventData?.name, copyFromName: eventData.rowValue },
							true
						)
						emitEvent('signal-component', 'config', { selectedTab: 'view-save' }, false)
						break
					case 'DELETE':
						//Deleting a view, confirm
						genericFunctions.displayMessage(
							`${props.listCtrl.texts.wantToDelete}`,
							'warning',
							null,
							{
								confirm: {
									label: `${props.listCtrl.texts.deleteText}`,
									action: () => { emitEventCallbackParams({ eventName: eventName, eventData: eventData }) }
								},
								cancel: {
									label: `${props.listCtrl.texts.cancelText}`,
									action: null
								}
							}
						)
						break
					default:
						emitEvent(eventName, eventData)
						break
				}
			}


			return {
				tableConfigHandlers: {
					showPopup: (eventData) => emitEvent('show-popup', eventData),
					hidePopup: (eventData) => emitEvent('hide-popup', eventData),
					signalComponent: (...args) => emitEvent('signal-component', ...args)
				},

				tableColumnConfigHandlers: {
					showPopup: (eventData) => emitEvent('show-popup', eventData),
					hidePopup: (eventData) =>
					{
						emitEvent('hide-popup', eventData)
						emitEvent('signal-component', 'config', { show: false })
					},
					setProperty: (...args) => emitEvent('set-property', ...args),
					applyColumnConfig: (eventData) => emitEvent('apply-column-config', eventData),
					resetColumnConfig: (eventData) => emitEvent('reset-column-config', eventData),
					toggleTextWrap: (eventData) => emitEvent('toggle-text-wrap', eventData)
				},

				tableAdvancedFilters: {
					showPopup: (eventData) => emitEvent('show-popup', eventData),
					hidePopup: (eventData) =>
					{
						emitEvent('hide-popup', eventData)
						emitEvent('signal-component', 'config', { show: false })
					},
					addAdvancedFilter: (eventData) => emitEvent('add-advanced-filter', eventData),
					editAdvancedFilters: (eventData) => emitEvent('edit-advanced-filters', eventData),
					setAdvancedFilterState: (eventData) => emitEvent('set-advanced-filter-state', eventData),
					removeAdvancedFilter: (eventData) => emitEvent('remove-advanced-filter', eventData),
					removeAllAdvancedFilters: () => emitEvent('remove-all-advanced-filters'),
					removeColumnFilter: (eventData) => emitEvent('remove-column-filter', eventData)
				},

				tableViewSaveHandlers: {
					showPopup: (eventData) => emitEvent('show-popup', eventData),
					hidePopup: (eventData) =>
					{
						emitEvent('hide-popup', eventData)
						emitEvent('signal-component', 'config', { show: false })
					},
					setProperty: (...args) => emitEvent('set-property', ...args),
					saveView: (eventData) => {
						emitEvent('save-view', eventData)
						emitEvent('set-info-message', alertProps)
					},
					renameView: (eventData) => emitEvent('rename-view', eventData),
					copyView: (eventData) => {
						emitEvent('copy-view', eventData)
						emitEvent('set-info-message', alertProps)
					}
				},

				tableViewHandlers: {
					showPopup: (eventData) => emitEvent('show-popup', eventData),
					hidePopup: (eventData) =>
					{
						emitEvent('hide-popup', eventData)
						emitEvent('signal-component', 'config', { show: false })
					},
					selectView: (eventData) => emitEvent('select-view', eventData),
					viewAction: (eventData) => emitViewEvent('view-action', eventData)
				}
			}
		},

		computed: {
			isListVisible()
			{
				// FIXME: remove by implementing a view mode manager
				// QTable SHOULD ONLY IMPLEMENT TABLE LOGIC!!! => DOES NOT CARE ABOUT VIEWMODES
				return !this.listCtrl.viewModes?.length || this.listCtrl.activeViewModeId === 'LIST'
			}
		},

		beforeUnmount()
		{
			this.tableConfigHandlers = null
			this.tableColumnConfigHandlers = null
			this.tableAdvancedFilters = null
			this.tableViewSaveHandlers = null
			this.tableViewHandlers = null
		}
	}
</script>
