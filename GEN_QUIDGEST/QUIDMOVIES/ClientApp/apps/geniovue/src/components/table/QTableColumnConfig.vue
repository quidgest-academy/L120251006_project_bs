<template>
	<!-- BEGIN: Column Config Popup -->
	<teleport
		:to="`#q-modal-${modalId}-header`"
		:key="domKey"
		v-if="(showPopup || showInline) && showHeader">
		<div>
			<h4 class="c-modal__header-title">{{ texts.configureColumns }}</h4>
		</div>
	</teleport>

	<teleport
		:to="`#q-modal-${modalId}-body`"
		:key="domKey"
		v-if="(showPopup || showInline) && showBody">
		<q-row>
			<q-col>
				<q-switch
					:model-value="hasTextWrap"
					size="small"
					show-state-labels
					:label="texts.lineBreak"
					:true-label="texts.yesLabel"
					:false-label="texts.noLabel"
					@update:model-value="toggleTextWrap" />
			</q-col>
		</q-row>

		<q-table
			:rows="tableConf.rows"
			:columns="tableConf.columnsCfgCols"
			:config="tableConf.config"
			:total-rows="tableConf.totalRows"
			:header-level="1"
			:texts="texts"
			@update-external="(...args) => updateExternal(...args)"
			@set-row-index-property="(...args) => setRowIndexProperty(tableConf, ...args)"
			@row-reorder="onTableListRowReorder">
		</q-table>

		<div class="visible-columns-counter">
			{{ texts.visibleColumnsText }}: {{ visibleColumns }} {{ texts.ofText.toLowerCase() }} {{ tableConf.rows.length }}
			<q-button
				id="popover_invisibleColumnHelpButton"
				:title="texts?.showHelp"
				class="btn-popover"
				variant="ghost"
				color="neutral">
				<q-icon icon="help" />
			</q-button>
			<q-popover
				anchor="#popover_invisibleColumnHelpButton"
				:text="texts.invisibleColumnsHelpText" />
		</div>
	</teleport>

	<teleport
		:to="`#q-modal-${modalId}-footer`"
		:key="domKey"
		v-if="(showPopup || showInline) && showFooter">
		<div class="actions float-right">
			<q-button
				id="reset-column-config-btn"
				:label="texts.resetText"
				:title="texts.resetText"
				@click="resetColumnConfig">
				<q-icon icon="reset-definitions" />
			</q-button>

			<q-button
				id="apply-column-config-btn"
				variant="bold"
				data-modal-close="true"
				data-modal-form="true"
				:label="texts.applyText"
				:title="texts.applyText"
				@click="applyColumnConfig()">
				<q-icon icon="apply" />
			</q-button>

			<q-button
				id="cancel-column-config-btn"
				data-dismiss="modal"
				:label="texts.cancelText"
				:title="texts.cancelText"
				@click="fnHidePopup()">
				<q-icon icon="cancel" />
			</q-button>
		</div>
	</teleport>
	<!-- END: Column Config Popup -->
</template>

<script>
	import { computed } from 'vue'
	import _find from 'lodash-es/find'

	import { inputSize } from '@quidgest/clientapp/constants/enums'
	import listFunctions from '@/mixins/listFunctions.js'

	import QTable from './QTable.vue'

	export default {
		name: 'QTableColumnConfig',

		emits: [
			'show-popup',
			'hide-popup',
			'set-property',
			'apply-column-config',
			'reset-column-config',
			'toggle-text-wrap'
		],

		components: {
			QTable
		},

		inheritAttrs: false,

		props: {
			/**
			 * An object that contains signals to manage the state and behavior of the column config popup.
			 */
			signal: {
				type: Object,
				default: () => ({})
			},

			/**
			 * The unique identifier for the modal that will host the column configuration content.
			 */
			modalId: {
				type: String,
				required: true
			},

			/**
			 * An object with the texts that are used in the popup, allowing for localization of UI elements like titles and buttons.
			 */
			texts: {
				type: Object,
				required: true
			},

			/**
			 * An array of column configuration objects used to toggle visibility and order within the table.
			 */
			columns: {
				type: Array,
				default: () => []
			},

			/**
			 * Determines if the processing and changes to column configurations should depend on server-side functionality.
			 */
			serverMode: {
				type: Boolean,
				default: false
			},

			/**
			 * Indicates whether the table rows should be presented with text wrapping.
			 */
			hasTextWrap: {
				type: Boolean,
				default: false
			},

			/**
			 * The name of the column that has been designated as the default search column.
			 */
			defaultSearchColumnName: {
				type: String,
				required: true
			},

			/**
			 * The base path to the directory containing resources related to the column configuration component.
			 */
			resourcesPath: {
				type: String,
				required: true
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
				tableConf: {
					rows: [],
					columnsCfgCols: [
						{
							label: computed(() => this.texts.orderText),
							name: 'order',
							dataDisplay: listFunctions.numericDisplayCell,
							dataOnChange: listFunctions.reCalcCellOrder,
							isOrderingColumn: true,
							columnClasses: 'c-table__cell-numeric row-numeric thead-order',
							columnHeaderClasses: 'c-table__head-numeric thead-order',
							component: 'q-edit-numeric',
							componentOptions: {
								label: computed(() => this.texts.orderText),
								maxDigits: 3,
								isDecimal: false,
								readonly: false,
								size: inputSize.mini
							}
						},
						{
							label: computed(() => this.texts.nameOfColumnText),
							name: 'name',
							dataDisplay: listFunctions.textDisplayCell
						},
						{
							label: computed(() => this.texts.defaultKeywordSearchText),
							name: 'defaultSearch',
							optionGroupName: 'defaultSearch',
							dataDisplay: listFunctions.radioDisplayCell,
							component: 'q-edit-radio',
							checkedValue: ''
						},
						{
							label: computed(() => this.texts.visibleText),
							name: 'visibility',
							dataDisplay: listFunctions.booleanDisplayCell,
							component: 'q-edit-boolean',
							rerenderRowsOnNextChange: false
						}
					],
					totalRows: 0,
					config: {
						name: 'column_config',
						tableTitle: '',
						globalSearch: {
							visibility: false
						},
						allowColumnConfig: false,
						showFooter: false,
						perPage: 0,
						hasRowDragAndDrop: true,
						showRowDragAndDropOption: false,
						allowFileExport: false,
						allowFileImport: false,
						resourcesPath: this.resourcesPath
					}
				},
				defaultSearchColumnNameCfg: ''
			}
		},

		mounted() {
			//Set column config rows. Must be done when loading and when table columns change.
			//Can't be a computed property because, the way it is calculated, it will not update when the table columns change.
			this.tableConf.rows = this.getColumnsCfgRows()
			this.tableConf.config.perPage = this.tableConf.rows.length

			this.setDefaultSearchColumnName()
		},

		beforeUnmount()
		{
			if(this.tableConf?.rows instanceof Array)
				this.tableConf.rows.length = 0
			this.tableConf = null
		},

		computed: {
			visibleColumns() {
				return this.tableConf.rows.filter((column) => {
					return column.Fields.visibility
				}).length
			}
		},

		methods: {
			setRowIndexProperty: listFunctions.setRowIndexProperty,

			//Show popup
			fnShowPopup() {
				this.$emit('show-popup', this.modalId)
				this.$nextTick().then(() => {
					this.showPopup = true
					this.domKey++
				})
			},

			//Hide popup
			fnHidePopup() {
				this.$emit('hide-popup', this.modalId)
			},

			/**
			 * Create column config array
			 * @returns Array
			 */
			getColumnsCfgRows() {
				const rows = []

				//Iterate columns
				let thisIdx = 1
				let column = {}
				let colOrder = 1
				for (const idx in this.columns) {
					column = this.columns[idx]

					// Columns with a false show-when condition shouldn't be displayed here - visibility is determined by the condition, not the user
					if (!column.visibilityEval) continue

					const columnCfg = {
						Rownum: 0,
						Fields: {}
					}

					//Row value
					columnCfg.Value = column.formField || column.name

					//Column name
					columnCfg.Fields.name = column.label

					//Column order
					columnCfg.Fields.order = colOrder++

					//Column as default search option
					columnCfg.Fields.defaultSearch = false
					if (listFunctions.isSearchableColumn(column)) {
						columnCfg.Fields.defaultSearch = true
					}

					//Column visibility
					columnCfg.Fields.visibility = listFunctions.isVisibleColumn(column) ? 1 : 0

					//Other fields not displayed
					columnCfg.Fields.primaryKey = column.primaryKey
					columnCfg.Fields.userPrimaryKey = column.userPrimaryKey
					columnCfg.Fields.table = column.table || column.area.toLowerCase()
					columnCfg.Fields.alias = column.alias || columnCfg.Fields.name
					columnCfg.Fields.formField = column.formField || column.name

					//RowKey and Rownum
					columnCfg.rowKey = columnCfg.Rownum = thisIdx++

					rows.push(columnCfg)
				}

				return rows
			},

			/**
			 * Apply the column configuration
			 */
			applyColumnConfig() {
				//Emit data to script which calls apply function
				this.$emit('apply-column-config', { columnOrder: this.tableConf.rows, defaultSearchColumn: this.defaultSearchColumnNameCfg })

				//Hide popup
				this.fnHidePopup()
			},

			/**
			 * Reset the column configuration
			 */
			resetColumnConfig() {
				//Update internal rows
				//Needed for resetting if changes have not been applied
				this.tableConf.rows = this.getColumnsCfgRows()
				this.tableConf.config.perPage = this.tableConf.rows.length

				this.$emit('reset-column-config')

				//Hide popup
				this.fnHidePopup()
			},

			/**
			 * Set the default search column
			 */
			setDefaultSearchColumnName() {
				const defaultSearchColumn = this.tableConf.columnsCfgCols.find((x) => x.name === 'defaultSearch')
				defaultSearchColumn.checkedValue = this.defaultSearchColumnName
				this.defaultSearchColumnNameCfg = this.defaultSearchColumnName
			},

			/**
			 * Toggle text wrap in cells
			 */
			toggleTextWrap() {
				//Emit toggle text wrap
				this.$emit('toggle-text-wrap')
			},

			/**
			 * Called when updating the value of a cell
			 * @param row {Object}
			 * @param column {Object}
			 * @param value {Object}
			 * @returns
			 */
			updateExternal(row, column, event) {
				if (column.name === 'defaultSearch') {
					this.defaultSearchColumnNameCfg = event
				}
			},

			onTableListRowReorder(eObj) {
				const row = listFunctions.getRowByKeyPath(this.tableConf.rows, eObj?.rowKey),
					orderingColumn = _find(this.tableConf.columnsCfgCols, (col) => col.isOrderingColumn === true)

				listFunctions.setTableCellValue(this.tableConf, row, orderingColumn, eObj.index + 1)
				listFunctions.reCalcCellOrder(this.tableConf, row, orderingColumn)
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
							default:
								if (['showInline', 'showHeader', 'showBody', 'showFooter'].includes(key)) {
									this[key] = newValue[key]
								}
								break
						}
					}
				},
				deep: true
			},

			columns: {
				handler() {
					this.tableConf.rows = this.getColumnsCfgRows()
					this.tableConf.config.perPage = this.tableConf.rows.length
				},
				deep: true
			},

			defaultSearchColumnName: {
				handler() {
					this.setDefaultSearchColumnName()
				},
				deep: true
			}
		}
	}
</script>
