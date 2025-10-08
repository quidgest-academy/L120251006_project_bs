import { defineAsyncComponent } from 'vue'

// Quidgest UI
import {
	QAccordion,
	QAccordionItem,
	QBadge,
	QBadgeIndicator,
	QButton,
	QButtonGroup,
	QCard,
	QCarousel,
	QCheckbox,
	QCol,
	QCombobox,
	QCollapsible,
	QColorPicker,
	QDialogProvider,
	QField,
	QInputGroup,
	QIcon,
	QIconImg,
	QIconFont,
	QIconSvg,
	QOverlay,
	QPopover,
	QPropertyList,
	QLabel,
	QLineLoader,
	QList,
	QRadioButton,
	QRadioGroup,
	QRow,
	QSelect,
	QSkeletonLoader,
	QSpinnerLoader,
	QSwitch,
	QTextArea,
	QTextField,
	QToggle,
	QToggleGroup,
	QToggleGroupItem,
	QTooltip
} from '@quidgest/ui/components'

import {
	QFilter,
	QFilterCheckbox,
	QFilterDropdown,
	QFilterRadio
} from '@quidgest/clientapp/components'

export const components = {
	// UI package controls
	QAccordion,
	QAccordionItem,
	QBadge,
	QBadgeIndicator,
	QButton,
	QButtonGroup,
	QCard,
	QCarousel,
	QCheckbox,
	QCol,
	QCombobox,
	QCollapsible,
	QColorPicker,
	QDialogProvider,
	QField,
	QInputGroup,
	QIcon,
	QIconImg,
	QIconFont,
	QIconSvg,
	QLabel,
	QLineLoader,
	QList,
	QOverlay,
	QPopover,
	QPropertyList,
	QRadioButton,
	QRadioGroup,
	QRow,
	QSelect,
	QSkeletonLoader,
	QSpinnerLoader,
	QSwitch,
	QTextArea,
	QTextField,
	QToggle,
	QToggleGroup,
	QToggleGroupItem,
	QTooltip,

	// Clientapp controls
	QFilter,
	QFilterCheckbox,
	QFilterDropdown,
	QFilterRadio,

	// Wrapper controls
	QControlWrapper: defineAsyncComponent(() => import('./ControlWrapper.vue')),
	VFragment: defineAsyncComponent(() => import('./VFragment.vue')),

	// Static controls
	QStaticText: defineAsyncComponent(() => import('./QStaticText.vue')),
	QPageBusyState: defineAsyncComponent(() => import('./QPageBusyState.vue')),
	QInfoMessage: defineAsyncComponent(() => import('./QInfoMessage.vue')),
	QMarkdownViewer: defineAsyncComponent(() => import('./markdown/QMarkdownViewer.vue')),

	// Input controls
	BaseInputStructure: defineAsyncComponent(() => import('./inputs/BaseInputStructure.vue')),
	QPasswordInput: defineAsyncComponent(() => import('./inputs/PasswordInput.vue')),
	QNumericInput: defineAsyncComponent(() => import('./inputs/NumericInput.vue')),
	QCheckListInput: defineAsyncComponent(() => import('./inputs/CheckListInput.vue')),
	QMask: defineAsyncComponent(() => import('./inputs/QMask.vue')),
	QLookup: defineAsyncComponent(() => import('./inputs/QLookup.vue')),
	GridBaseInputStructure: defineAsyncComponent(() => import('./inputs/GridBaseInputStructure.vue')),
	QDateTimePicker: defineAsyncComponent(() => import('./inputs/QDateTimePicker.vue')),
	QTextEditor: defineAsyncComponent(() => import('./inputs/QTextEditor.vue')),
	QMultiCheckBoxesInput: defineAsyncComponent(() => import('./inputs/MultiCheckBoxesInput.vue')),
	QDocument: defineAsyncComponent(() => import('./inputs/document/QDocument.vue')),
	QImage: defineAsyncComponent(() => import('./inputs/image/QImage.vue')),
	QCodeEditor: defineAsyncComponent(() => import('./inputs/code/QCodeEditor.vue')),
	QMarkdownEditor: defineAsyncComponent(() => import('./markdown/QMarkdownEditor.vue')),

	// Container controls
	QGroupBoxContainer: defineAsyncComponent(() => import('./containers/GroupBoxContainer.vue')),
	QGroupCollapsible: defineAsyncComponent(() => import('./containers/QGroupCollapsible.vue')),
	QRowContainer: defineAsyncComponent(() => import('./containers/RowContainer.vue')),
	QTabContainer: defineAsyncComponent(() => import('./containers/TabContainer.vue')),
	QModalContainer: defineAsyncComponent(() => import('./containers/QModalContainer.vue')),
	QFormContainer: defineAsyncComponent(() => import('./containers/QFormContainer.vue')),
	QWizard: defineAsyncComponent(() => import('./containers/wizard/QWizard.vue')),
	QAnchorContainerHorizontal: defineAsyncComponent(() => import('./containers/QAnchorContainerHorizontal.vue')),
	QAnchorContainerVertical: defineAsyncComponent(() => import('./containers/QAnchorContainerVertical.vue')),
	QAnchorElement: defineAsyncComponent(() => import('./containers/QAnchorElement.vue')),
	QKanbanCard: defineAsyncComponent(() => import('./containers/QKanbanCard.vue')),
	QKanbanHeader: defineAsyncComponent(() => import('./containers/QKanbanHeader.vue')),
	QKanban: defineAsyncComponent(() => import('./containers/QKanban.vue')),

	// Rendering controls
	// Render components are used by tables to display fields.
	// Edit components are used by advanced filters, column filters and editable fields in normal tables
	// (different than the ones in the editable table lists).
	QActionList: defineAsyncComponent(() => import('./rendering/QActionList.vue')),
	QRenderArray: defineAsyncComponent(() => import('./rendering/QRenderArray.vue')),
	QRenderBoolean: defineAsyncComponent(() => import('./rendering/QRenderBoolean.vue')),
	QRenderData: defineAsyncComponent(() => import('./rendering/QRenderData.vue')),
	QRenderHyperlink: defineAsyncComponent(() => import('./rendering/QRenderHyperlink.vue')),
	QRenderHtml: defineAsyncComponent(() => import('./rendering/QRenderHtml.vue')),
	QRenderMarkdown: defineAsyncComponent(() => import('./rendering/QRenderMarkdown.vue')),
	QRenderImage: defineAsyncComponent(() => import('./rendering/QRenderImage.vue')),
	QRenderDocument: defineAsyncComponent(() => import('./rendering/QRenderDocument.vue')),
	QEditText: defineAsyncComponent(() => import('./rendering/QEditText.vue')),
	QEditTextMultiline: defineAsyncComponent(() => import('./rendering/QEditTextMultiline.vue')),
	QEditNumeric: defineAsyncComponent(() => import('./rendering/QEditNumeric.vue')),
	QEditBoolean: defineAsyncComponent(() => import('./rendering/QEditBoolean.vue')),
	QEditDatetime: defineAsyncComponent(() => import('./rendering/QEditDatetime.vue')),
	QEditEnumeration: defineAsyncComponent(() => import('./rendering/QEditEnumeration.vue')),
	QEditCheckList: defineAsyncComponent(() => import('./rendering/QEditCheckList.vue')),
	QEditRadio: defineAsyncComponent(() => import('./rendering/QEditRadio.vue')),

	// Complex controls
	QPasswordMeter: defineAsyncComponent(() => import('./rendering/QPasswordMeter.vue')),
	QProgress: defineAsyncComponent(() => import('./rendering/QProgress.vue')),
	QTimeline: defineAsyncComponent(() => import('./timeline/QTimeline.vue')),
	QDashboard: defineAsyncComponent(() => import('./dashboard/QDashboard.vue')),

	// Special renderings
	QCards: defineAsyncComponent(() => import('./rendering/cards/QCards.vue')),

	// Table components
	QTable: defineAsyncComponent(() => import('./table/QTable.vue')),
	QTableRecordActionsMenu: defineAsyncComponent(() => import('./table/QTableRecordActionsMenu.vue')),
	QTableSearch: defineAsyncComponent(() => import('./table/QTableSearch.vue')),
	QTableExport: defineAsyncComponent(() => import('./table/QTableExport.vue')),
	QTableImport: defineAsyncComponent(() => import('./table/QTableImport.vue')),
	QTableStaticFilters: defineAsyncComponent(() => import('./table/QTableStaticFilters.vue')),
	QTablePagination: defineAsyncComponent(() => import('./table/QTablePagination.vue')),
	QTablePaginationAlt: defineAsyncComponent(() => import('./table/QTablePaginationAlt.vue')),
	QTableLimitInfo: defineAsyncComponent(() => import('./table/QTableLimitInfo.vue')),
	QTableChecklistCheckbox: defineAsyncComponent(() => import('./table/QTableChecklistCheckbox.vue')),
	QTableColumnFilters: defineAsyncComponent(() => import('./table/QTableColumnFilters.vue')),
	QTableColumnTotalizers: defineAsyncComponent(() => import('./table/QTableColumnTotalizers.vue')),
	QTableCurrentFilters: defineAsyncComponent(() => import('./table/QTableCurrentFilters.vue')),
	QTableActions: defineAsyncComponent(() => import('./table/QTableActions.vue')),
	QTableConfig: defineAsyncComponent(() => import('./table/QTableConfig.vue')),
	QTableViews: defineAsyncComponent(() => import('./table/QTableViews.vue')),
	QTableViewSave: defineAsyncComponent(() => import('./table/QTableViewSave.vue')),
	QTableExtraExtension: defineAsyncComponent(() => import('./table/QTableExtraExtension.vue')),
	QTableViewModeConfig: defineAsyncComponent(() => import('./table/QTableViewModeConfig.vue')),
	QGridTableList: defineAsyncComponent(() => import('./table/QGridTableList.vue')),
	QCheckListExtension: defineAsyncComponent(() => import('./extensions/CheckListExtension.vue')),

	// Other components
	QValidationSummary: defineAsyncComponent(() => import('@/views/shared/QValidationSummary.vue'))
}

export const componentNames = Object.keys(components)

export default {
	install: (app) => {
		for (let i in components)
			app.component(i, components[i])
	}
}
