import { computed } from 'vue'

class BaseResources
{
	constructor(fnGetResource)
	{
		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'showHelp', {
			get: () => this._fnGetResource('MOSTRAR_AJUDA54733'),
			enumerable: true
		})
	}

	destroy()
	{
		this._fnGetResource = null
	}
}

class TableListMainResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'actionMenuTitle', {
			get: () => this._fnGetResource('ACOES22599'),
			enumerable: true
		})
		Object.defineProperty(this, 'emptyText', {
			get: () => this._fnGetResource('SEM_DADOS_PARA_MOSTR24928'),
			enumerable: true
		})
		Object.defineProperty(this, 'removeText', {
			get: () => this._fnGetResource('REMOVER50666'),
			enumerable: true
		})
		Object.defineProperty(this, 'importButtonTitle', {
			get: () => this._fnGetResource('IMPORTAR64751'),
			enumerable: true
		})
		Object.defineProperty(this, 'templateButtonTitle', {
			get: () => this._fnGetResource('SELECIONE_O_TEMPLATE17055'),
			enumerable: true
		})
		Object.defineProperty(this, 'submitText', {
			get: () => this._fnGetResource('SUBMETER21206'),
			enumerable: true
		})
		Object.defineProperty(this, 'applyText', {
			get: () => this._fnGetResource('APLICAR33981'),
			enumerable: true
		})
		Object.defineProperty(this, 'closeText', {
			get: () => this._fnGetResource('FECHAR32496'),
			enumerable: true
		})
		Object.defineProperty(this, 'okText', {
			get: () => this._fnGetResource('OK15819'),
			enumerable: true
		})
		Object.defineProperty(this, 'pendingRecords', {
			get: () => this._fnGetResource('ATENCAO__ESTA_FICHA_24725'),
			enumerable: true
		})
		Object.defineProperty(this, 'dropToUpload', {
			get: () => this._fnGetResource('ARRASTE_O_FICHEIRO_A37155'),
			enumerable: true
		})
		Object.defineProperty(this, 'saveText', {
			get: () => this._fnGetResource('GRAVAR45301'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewText', {
			get: () => this._fnGetResource('CONSULTAR57388'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteText', {
			get: () => this._fnGetResource('ELIMINAR21155'),
			enumerable: true
		})
		Object.defineProperty(this, 'duplicateText', {
			get: () => this._fnGetResource('DUPLICAR09748'),
			enumerable: true
		})
		Object.defineProperty(this, 'renameText', {
			get: () => this._fnGetResource('MUDAR_O_NOME17369'),
			enumerable: true
		})
		Object.defineProperty(this, 'confirmText', {
			get: () => this._fnGetResource('CONFIRMAR09808'),
			enumerable: true
		})
		Object.defineProperty(this, 'cancelText', {
			get: () => this._fnGetResource('CANCELAR49513'),
			enumerable: true
		})
		Object.defineProperty(this, 'discard', {
			get: () => this._fnGetResource('DESCARTAR04620'),
			enumerable: true
		})
		Object.defineProperty(this, 'resetText', {
			get: () => this._fnGetResource('REPOR35852'),
			enumerable: true
		})
		Object.defineProperty(this, 'insertText', {
			get: () => this._fnGetResource('INSERIR43365'),
			enumerable: true
		})
		Object.defineProperty(this, 'tableConfig', {
			get: () => this._fnGetResource('DEFINICOES_DA_TABELA04919'),
			enumerable: true
		})
		Object.defineProperty(this, 'baseTable', {
			get: () => this._fnGetResource('TABELA_BASE39739'),
			enumerable: true
		})
		Object.defineProperty(this, 'baseTableAsDefault', {
			get: () => this._fnGetResource('TABELA_BASE_POR_OMIS45720'),
			enumerable: true
		})
		Object.defineProperty(this, 'configureColumns', {
			get: () => this._fnGetResource('CONFIGURAR_COLUNAS42252'),
			enumerable: true
		})
		Object.defineProperty(this, 'configureFilters', {
			get: () => this._fnGetResource('CONFIGURAR_FILTROS27188'),
			enumerable: true
		})
		Object.defineProperty(this, 'manageViews', {
			get: () => this._fnGetResource('GERIR_VISTAS48777'),
			enumerable: true
		})
		Object.defineProperty(this, 'createView', {
			get: () => this._fnGetResource('CRIAR_VISTA61829'),
			enumerable: true
		})
		Object.defineProperty(this, 'saveChanges', {
			get: () => this._fnGetResource('GRAVAR_ALTERACOES12886'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewModeConfigButtonTitle', {
			get: () => this._fnGetResource('OPCOES_DE_VISUALIZAC22988'),
			enumerable: true
		})
		Object.defineProperty(this, 'toListViewButtonTitle', {
			get: () => this._fnGetResource('MUDAR_PARA_VISTA_EM_03626'),
			enumerable: true
		})
		Object.defineProperty(this, 'toAlternativeViewButtonTitle', {
			get: () => this._fnGetResource('MUDAR_PARA_VISTA_ALT46064'),
			enumerable: true
		})
		Object.defineProperty(this, 'orderText', {
			get: () => this._fnGetResource('ORDEM38897'),
			enumerable: true
		})
		Object.defineProperty(this, 'nameOfColumnText', {
			get: () => this._fnGetResource('NOME_DA_COLUNA14566'),
			enumerable: true
		})
		Object.defineProperty(this, 'visibleText', {
			get: () => this._fnGetResource('VISIVEL07768'),
			enumerable: true
		})
		Object.defineProperty(this, 'searchTextTitle', {
			get: () => this._fnGetResource('CAIXA_DE_PESQUISA53870'),
			enumerable: true
		})
		Object.defineProperty(this, 'searchText', {
			get: () => this._fnGetResource('PESQUISAR34506'),
			enumerable: true
		})
		Object.defineProperty(this, 'forText', {
			get: () => this._fnGetResource('POR12741'),
			enumerable: true
		})
		Object.defineProperty(this, 'ofText', {
			get: () => this._fnGetResource('OF21852'),
			enumerable: true
		})
		Object.defineProperty(this, 'allFieldsText', {
			get: () => this._fnGetResource('TODOS_OS_CAMPOS47279'),
			enumerable: true
		})
		Object.defineProperty(this, 'showText', {
			get: () => this._fnGetResource('MOSTRAR50268'),
			enumerable: true
		})
		Object.defineProperty(this, 'hideText', {
			get: () => this._fnGetResource('ESCONDER31385'),
			enumerable: true
		})
		Object.defineProperty(this, 'filtersText', {
			get: () => this._fnGetResource('FILTROS01340'),
			enumerable: true
		})
		Object.defineProperty(this, 'fieldIsRequired', {
			get: () => this._fnGetResource('O_CAMPO__0__E_OBRIGA36687'),
			enumerable: true
		})
		Object.defineProperty(this, 'isRequired', {
			get: () => this._fnGetResource('E_OBRIGATORIO35368'),
			enumerable: true
		})
		Object.defineProperty(this, 'limitsButtonTitle', {
			get: () => this._fnGetResource('LIMITE12596'),
			enumerable: true
		})
		Object.defineProperty(this, 'limitsListTitlePrepend', {
			get: () => this._fnGetResource('A_INFORMACAO_NA_LIST25711'),
			enumerable: true
		})
		Object.defineProperty(this, 'limitsListTitleAppend', {
			get: () => this._fnGetResource('ESTA_LIMITADA_POR50241'),
			enumerable: true
		})
		Object.defineProperty(this, 'textRowsSelected', {
			get: () => this._fnGetResource('REGISTO_S__SELECIONA64172'),
			enumerable: true
		})
		Object.defineProperty(this, 'groupActionsText', {
			get: () => this._fnGetResource('ACOES_COLETIVAS25162'),
			enumerable: true
		})
		Object.defineProperty(this, 'advancedFiltersText', {
			get: () => this._fnGetResource('FILTROS_AVANCADOS32501'),
			enumerable: true
		})
		Object.defineProperty(this, 'applyFilterText', {
			get: () => this._fnGetResource('APLICAR_FILTRO50221'),
			enumerable: true
		})
		Object.defineProperty(this, 'applyFiltersText', {
			get: () => this._fnGetResource('APLICAR_FILTROS22808'),
			enumerable: true
		})
		Object.defineProperty(this, 'createFilterText', {
			get: () => this._fnGetResource('CRIAR_FILTRO61015'),
			enumerable: true
		})
		Object.defineProperty(this, 'filterNameText', {
			get: () => this._fnGetResource('NOME_DO_FILTRO02285'),
			enumerable: true
		})
		Object.defineProperty(this, 'orText', {
			get: () => this._fnGetResource('OU11765'),
			enumerable: true
		})
		Object.defineProperty(this, 'createConditionText', {
			get: () => this._fnGetResource('CRIAR_CONDICAO10949'),
			enumerable: true
		})
		Object.defineProperty(this, 'removeConditionText', {
			get: () => this._fnGetResource('REMOVER_CONDICAO64117'),
			enumerable: true
		})
		Object.defineProperty(this, 'savedFiltersText', {
			get: () => this._fnGetResource('FILTROS_GRAVADOS05983'),
			enumerable: true
		})
		Object.defineProperty(this, 'saveFilterText', {
			get: () => this._fnGetResource('GRAVAR_FILTRO16375'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteFilterText', {
			get: () => this._fnGetResource('REMOVER_FILTRO51662'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteFiltersText', {
			get: () => this._fnGetResource('REMOVER_FILTROS62153'),
			enumerable: true
		})
		Object.defineProperty(this, 'activateFilterText', {
			get: () => this._fnGetResource('ACTIVAR_FILTRO21924'),
			enumerable: true
		})
		Object.defineProperty(this, 'deactivateFilterText', {
			get: () => this._fnGetResource('DESACTIVAR_FILTRO34573'),
			enumerable: true
		})
		Object.defineProperty(this, 'columnActionsText', {
			get: () => this._fnGetResource('ACOES_DA_COLUNA45080'),
			enumerable: true
		})
		Object.defineProperty(this, 'sortText', {
			get: () => this._fnGetResource('ORDENAR00426'),
			enumerable: true
		})
		Object.defineProperty(this, 'ascendingText', {
			get: () => this._fnGetResource('ASCENDENTE30808'),
			enumerable: true
		})
		Object.defineProperty(this, 'descendingText', {
			get: () => this._fnGetResource('DESCENDENTE19792'),
			enumerable: true
		})
		Object.defineProperty(this, 'sortAscendingText', {
			get: () => this._fnGetResource('ORDENAR_ASCENDENTE32130'),
			enumerable: true
		})
		Object.defineProperty(this, 'sortDescendingText', {
			get: () => this._fnGetResource('ORDENAR_DESCENDENTE63669'),
			enumerable: true
		})
		Object.defineProperty(this, 'moveToAdvancedFiltersText', {
			get: () => this._fnGetResource('MOVER_PARA_FILTROS_A24438'),
			enumerable: true
		})
		Object.defineProperty(this, 'staticFiltersTitle', {
			get: () => this._fnGetResource('FILTROS_GLOBAIS30027'),
			enumerable: true
		})
		Object.defineProperty(this, 'activeFiltersTitle', {
			get: () => this._fnGetResource('FILTROS_ATIVOS07219'),
			enumerable: true
		})
		Object.defineProperty(this, 'removeAllText', {
			get: () => this._fnGetResource('REMOVER_TODOS43893'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowDragAndDropTitle', {
			get: () => this._fnGetResource('REORDENAR52758'),
			enumerable: true
		})
		Object.defineProperty(this, 'exportButtonTitle', {
			get: () => this._fnGetResource('EXPORTAR35632'),
			enumerable: true
		})
		Object.defineProperty(this, 'defaultKeywordSearchText', {
			get: () => this._fnGetResource('PESQUISA_PREDEFINIDA03529'),
			enumerable: true
		})
		Object.defineProperty(this, 'lineBreak', {
			get: () => this._fnGetResource('QUEBRA_DE_LINHA53477'),
			enumerable: true
		})
		Object.defineProperty(this, 'yesLabel', {
			get: () => this._fnGetResource('SIM28552'),
			enumerable: true
		})
		Object.defineProperty(this, 'noLabel', {
			get: () => this._fnGetResource('NAO06521'),
			enumerable: true
		})
		Object.defineProperty(this, 'activeText', {
			get: () => this._fnGetResource('ACTIVE03270'),
			enumerable: true
		})
		Object.defineProperty(this, 'inactiveText', {
			get: () => this._fnGetResource('INACTIVE23138'),
			enumerable: true
		})
		Object.defineProperty(this, 'inactiveFilterText', {
			get: () => this._fnGetResource('FILTRO_INATIVO54001'),
			enumerable: true
		})
		Object.defineProperty(this, 'showRecordsWhereText', {
			get: () => this._fnGetResource('MOSTRAR_REGISTOS_QUA55160'),
			enumerable: true
		})
		Object.defineProperty(this, 'visibleColumnsText', {
			get: () => this._fnGetResource('COLUNAS_VISIVEIS27717'),
			enumerable: true
		})
		Object.defineProperty(this, 'invisibleColumnsHelpText', {
			get: () => this._fnGetResource('COLUNAS_INVISIVEIS_N46371'),
			enumerable: true
		})
		Object.defineProperty(this, 'selectView', {
			get: () => this._fnGetResource('SELECIONAR_VISTA16672'),
			enumerable: true
		})
		Object.defineProperty(this, 'saveViewText', {
			get: () => this._fnGetResource('GUARDAR_VISTA35229'),
			enumerable: true
		})
		Object.defineProperty(this, 'savedView', {
			get: () => this._fnGetResource('VISTA_GRAVADA55829'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewManagerText', {
			get: () => this._fnGetResource('GESTOR_DE_VISTAS43375'),
			enumerable: true
		})
		Object.defineProperty(this, 'clearResizeText', {
			get: () => this._fnGetResource('LIMPAR_REDIMENSIONAM00007'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewExistsText', {
			get: () => this._fnGetResource('ESSA_VISTA_JA_EXISTE52743'),
			enumerable: true
		})
		Object.defineProperty(this, 'wantToOverwriteText', {
			get: () => this._fnGetResource('DESEJA_SUBSTITUI_LA_25718'),
			enumerable: true
		})
		Object.defineProperty(this, 'wantToSaveChanges', {
			get: () => this._fnGetResource('QUER_GRAVAR_AS_ALTER22033'),
			enumerable: true
		})
		Object.defineProperty(this, 'wantToDelete', {
			get: () => this._fnGetResource('TEM_A_CERTEZA_QUE_QU37043'),
			enumerable: true
		})
		Object.defineProperty(this, 'wantToSaveChangesToView', {
			get: () => this._fnGetResource('SALVAR_AS_ALTERACOES51739'),
			enumerable: true
		})
		Object.defineProperty(this, 'tableViewSaveSuccess', {
			get: () => this._fnGetResource('VISUALIZACAO_DE_TABE40128'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewNameText', {
			get: () => this._fnGetResource('NOME_DA_VISTA31135'),
			enumerable: true
		})
		Object.defineProperty(this, 'setDefaultViewText', {
			get: () => this._fnGetResource('DEFINIR_COMO_VISTA_P09954'),
			enumerable: true
		})
		Object.defineProperty(this, 'defaultViewText', {
			get: () => this._fnGetResource('VISTA_PREDEFINIDA61222'),
			enumerable: true
		})
		Object.defineProperty(this, 'downloadTemplateText', {
			get: () => this._fnGetResource('FACA_O_DOWNLOAD_DO_A43406'),
			enumerable: true
		})
		Object.defineProperty(this, 'fillTemplateFileText', {
			get: () => this._fnGetResource('PREENCHA_O_FICHEIRO_42287'),
			enumerable: true
		})
		Object.defineProperty(this, 'importTemplateFileText', {
			get: () => this._fnGetResource('APOS_PREENCHER_O_FIC53348'),
			enumerable: true
		})
		Object.defineProperty(this, 'allRecordsText', {
			get: () => this._fnGetResource('TODOS59977'),
			enumerable: true
		})
		Object.defineProperty(this, 'currentPageText', {
			get: () => this._fnGetResource('PAGINA_ATUAL46671'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowsPerPage', {
			get: () => this._fnGetResource('LINHAS_POR_PAGINA55027'),
			enumerable: true
		})
		Object.defineProperty(this, 'gotToPage', {
			get: () => this._fnGetResource('IR_PARA_PAGINA12084'),
			enumerable: true
		})
		Object.defineProperty(this, 'noneText', {
			get: () => this._fnGetResource('NENHUM21531'),
			enumerable: true
		})
		Object.defineProperty(this, 'loading', {
			get: () => this._fnGetResource('A_CARREGAR___34906'),
			enumerable: true
		})
		Object.defineProperty(this, 'onDate', {
			get: () => this._fnGetResource('EM_32327'),
			enumerable: true
		})
		Object.defineProperty(this, 'state', {
			get: () => this._fnGetResource('ESTADO07788'),
			enumerable: true
		})
		Object.defineProperty(this, 'first', {
			get: () => this._fnGetResource('PRIMEIRA43991'),
			enumerable: true
		})
		Object.defineProperty(this, 'last', {
			get: () => this._fnGetResource('ULTIMA04868'),
			enumerable: true
		})
		Object.defineProperty(this, 'previous', {
			get: () => this._fnGetResource('ANTERIOR34904'),
			enumerable: true
		})
		Object.defineProperty(this, 'next', {
			get: () => this._fnGetResource('PROXIMO29858'),
			enumerable: true
		})
		Object.defineProperty(this, 'total', {
			get: () => this._fnGetResource('TOTAL49307'),
			enumerable: true
		})
		Object.defineProperty(this, 'MoveUp', {
			get: () => this._fnGetResource('MOVER_PARA_CIMA46136'),
			enumerable: true
		})
		Object.defineProperty(this, 'MoveDown', {
			get: () => this._fnGetResource('MOVER_PARA_BAIXO46489'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowAddNewAfter', {
			get: () => this._fnGetResource('ADICIONAR_NOVA_LINHA13110'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowDragDropReorder', {
			get: () => this._fnGetResource('ARRASTE_E_LARGUE_PAR46711'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowExpand', {
			get: () => this._fnGetResource('EXPANDIR_LINHA32265'),
			enumerable: true
		})
		Object.defineProperty(this, 'rowCollapse', {
			get: () => this._fnGetResource('COLAPSAR_LINHA03427'),
			enumerable: true
		})
		Object.defineProperty(this, 'select', {
			get: () => this._fnGetResource('SELECIONAR08804'),
			enumerable: true
		})
		Object.defineProperty(this, 'close', {
			get: () => this._fnGetResource('FECHAR32496'),
			enumerable: true
		})
		Object.defineProperty(this, 'download', {
			get: () => this._fnGetResource('DESCARREGAR58418'),
			enumerable: true
		})
		Object.defineProperty(this, 'placeholder', {
			get: () => this._fnGetResource('ESCOLHA___40245'),
			enumerable: true
		})
		Object.defineProperty(this, 'clearValue', {
			get: () => this._fnGetResource('REMOVER_VALOR43780'),
			enumerable: true
		})
		Object.defineProperty(this, 'showOptions', {
			get: () => this._fnGetResource('MOSTRAR_OPCOES64064'),
			enumerable: true
		})
		Object.defineProperty(this, 'importing', {
			get: () => this._fnGetResource('A_IMPORTAR___42816'),
			enumerable: true
		})
		Object.defineProperty(this, 'exporting', {
			get: () => this._fnGetResource('A_EXPORTAR___20494'),
			enumerable: true
		})
	}
}

class ImportExportResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'pdf', {
			get: () => this._fnGetResource('FORMATO_DE_DOCUMENTO48724'),
			enumerable: true
		})
		Object.defineProperty(this, 'ods', {
			get: () => this._fnGetResource('FOLHA_DE_CALCULO__OD46941'),
			enumerable: true
		})
		Object.defineProperty(this, 'xlsx', {
			get: () => this._fnGetResource('FOLHA_DE_CALCULO_EXC59518'),
			enumerable: true
		})
		Object.defineProperty(this, 'csv', {
			get: () => this._fnGetResource('VALORES_SEPARADOS_PO10397'),
			enumerable: true
		})
		Object.defineProperty(this, 'xml', {
			get: () => this._fnGetResource('FORMATO_XML__XML_44251'),
			enumerable: true
		})
		Object.defineProperty(this, 'xlsxDownloadTemplate', {
			get: () => this._fnGetResource('DOWNLOAD_DE_TEMPLATE48385'),
			enumerable: true
		})

		this.exportOptions = computed(() => [
			{ id: 'pdf', text: this.pdf },
			{ id: 'ods', text: this.ods },
			{ id: 'xlsx', text: this.xlsx },
			{ id: 'csv', text: this.csv },
			{ id: 'xml', text: this.xml }
		])

		this.importOptions = computed(() => [
			{ id: 'xlsx', text: this.xlsx }
		])

		this.importTemplateOptions = computed(() => [
			{ id: 'xlsx', text: this.xlsxDownloadTemplate }
		])
	}
}

class MultipleValuesExtensionResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'placeholder', {
			get: () => this._fnGetResource('PROCURAR_EM64879'),
			enumerable: true
		})
		Object.defineProperty(this, 'addButtonTitle', {
			get: () => this._fnGetResource('ADICIONA17889'),
			enumerable: true
		})
		Object.defineProperty(this, 'searchInputTitle', {
			get: () => this._fnGetResource('PROCURAR15982'),
			enumerable: true
		})
	}
}

class LookupResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'placeholder', {
			get: () => this._fnGetResource('ESCOLHA___40245'),
			enumerable: true
		})
		Object.defineProperty(this, 'noData', {
			get: () => this._fnGetResource('SEM_DADOS_PARA_MOSTR24928'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewDetails', {
			get: () => this._fnGetResource('VIEW_DETAILS09924'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewMoreOptions', {
			get: () => this._fnGetResource('VER_MAIS32592'),
			enumerable: true
		})
		Object.defineProperty(this, 'clearValue', {
			get: () => this._fnGetResource('REMOVER_VALOR43780'),
			enumerable: true
		})
		Object.defineProperty(this, 'showOptions', {
			get: () => this._fnGetResource('MOSTRAR_OPCOES64064'),
			enumerable: true
		})
	}
}

class DateTimeResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'clearValue', {
			get: () => this._fnGetResource('REMOVER_VALOR43780'),
			enumerable: true
		})
	}
}

class DocumentResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'downloadLabel', {
			get: () => this._fnGetResource('DESCARREGAR58418'),
			enumerable: true
		})
		Object.defineProperty(this, 'attachLabel', {
			get: () => this._fnGetResource('ANEXAR20848'),
			enumerable: true
		})
		Object.defineProperty(this, 'submitLabel', {
			get: () => this._fnGetResource('SUBMETER21206'),
			enumerable: true
		})
		Object.defineProperty(this, 'editLabel', {
			get: () => this._fnGetResource('EDITAR11616'),
			enumerable: true
		})
		Object.defineProperty(this, 'chooseFileLabel', {
			get: () => this._fnGetResource('ESCOLHER_FICHEIRO44506'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteLabel', {
			get: () => this._fnGetResource('APAGAR04097'),
			enumerable: true
		})
		Object.defineProperty(this, 'propertyLabel', {
			get: () => this._fnGetResource('PROPRIEDADES45924'),
			enumerable: true
		})
		Object.defineProperty(this, 'versionsLabel', {
			get: () => this._fnGetResource('VERSOES25682'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewAllLabel', {
			get: () => this._fnGetResource('VER_TODAS___44710'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteLastLabel', {
			get: () => this._fnGetResource('APAGAR_ULTIMA25492'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteHistoryLabel', {
			get: () => this._fnGetResource('APAGAR_HISTORICO26221'),
			enumerable: true
		})
		Object.defineProperty(this, 'nameLabel', {
			get: () => this._fnGetResource('NOME__48276'),
			enumerable: true
		})
		Object.defineProperty(this, 'sizeLabel', {
			get: () => this._fnGetResource('TAMANHO__48454'),
			enumerable: true
		})
		Object.defineProperty(this, 'extensionLabel', {
			get: () => this._fnGetResource('EXTENSAO__24742'),
			enumerable: true
		})
		Object.defineProperty(this, 'authorLabel', {
			get: () => this._fnGetResource('AUTOR__36547'),
			enumerable: true
		})
		Object.defineProperty(this, 'createdDateLabel', {
			get: () => this._fnGetResource('DATA_DE_CRIACAO__05001'),
			enumerable: true
		})
		Object.defineProperty(this, 'createdOnLabel', {
			get: () => this._fnGetResource('DATA_DE_CRIACAO16914'),
			enumerable: true
		})
		Object.defineProperty(this, 'currentVersionLabel', {
			get: () => this._fnGetResource('VERSAO_ATUAL__01161'),
			enumerable: true
		})
		Object.defineProperty(this, 'editedByLabel', {
			get: () => this._fnGetResource('EM_EDICAO_POR__14850'),
			enumerable: true
		})
		Object.defineProperty(this, 'okLabel', {
			get: () => this._fnGetResource('OK15819'),
			enumerable: true
		})
		Object.defineProperty(this, 'yesLabel', {
			get: () => this._fnGetResource('SIM28552'),
			enumerable: true
		})
		Object.defineProperty(this, 'noLabel', {
			get: () => this._fnGetResource('NAO06521'),
			enumerable: true
		})
		Object.defineProperty(this, 'filesSubmission', {
			get: () => this._fnGetResource('SUBMISSAO_DE_FICHEIR50281'),
			enumerable: true
		})
		Object.defineProperty(this, 'noFileSelected', {
			get: () => this._fnGetResource('NENHUM_FICHEIRO_SELE48024'),
			enumerable: true
		})
		Object.defineProperty(this, 'fileSizeError', {
			get: () => this._fnGetResource('O_FICHEIRO_SELECIONA49645'),
			enumerable: true
		})
		Object.defineProperty(this, 'extensionError', {
			get: () => this._fnGetResource('EXTENSAO_INVALIDA__E46375'),
			enumerable: true
		})
		Object.defineProperty(this, 'submitHeaderLabel', {
			get: () => this._fnGetResource('SELECCIONE_O_FICHEIR54804'),
			enumerable: true
		})
		Object.defineProperty(this, 'unlockHeaderLabel', {
			get: () => this._fnGetResource('DESBLOQUEAR__IGNORA_48447'),
			enumerable: true
		})
		Object.defineProperty(this, 'submitFilesHeaderLabel', {
			get: () => this._fnGetResource('SUBMETER__DESBLOQUEI57783'),
			enumerable: true
		})
		Object.defineProperty(this, 'majorVersionLabel', {
			get: () => this._fnGetResource('VERSAO_PRINCIPAL03233'),
			enumerable: true
		})
		Object.defineProperty(this, 'minorVersionLabel', {
			get: () => this._fnGetResource('VERSAO_SECUNDARIA37682'),
			enumerable: true
		})
		Object.defineProperty(this, 'cancelLabelValue', {
			get: () => this._fnGetResource('CANCELAR49513'),
			enumerable: true
		})
		Object.defineProperty(this, 'version', {
			get: () => this._fnGetResource('VERSAO61228'),
			enumerable: true
		})
		Object.defineProperty(this, 'documentLabel', {
			get: () => this._fnGetResource('DOCUMENTO60418'),
			enumerable: true
		})
		Object.defineProperty(this, 'bytesLabel', {
			get: () => this._fnGetResource('BYTES25864'),
			enumerable: true
		})
		Object.defineProperty(this, 'author', {
			get: () => this._fnGetResource('AUTOR45670'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteHeaderLabel', {
			get: () => this._fnGetResource('TEM_A_CERTEZA_QUE_QU37043'),
			enumerable: true
		})
		Object.defineProperty(this, 'fileChoose', {
			get: () => this._fnGetResource('ESCOLHER_UM_FICHEIRO15154'),
			enumerable: true
		})
		Object.defineProperty(this, 'documentManagement', {
			get: () => this._fnGetResource('GESTAO_DE_FICHEIROS44758'),
			enumerable: true
		})
		Object.defineProperty(this, 'viewAll', {
			get: () => this._fnGetResource('VER_TODAS10532'),
			enumerable: true
		})
		Object.defineProperty(this, 'closeLabel', {
			get: () => this._fnGetResource('FECHAR32496'),
			enumerable: true
		})
		Object.defineProperty(this, 'theLastVersionWillEliminate', {
			get: () => this._fnGetResource('A_ULTIMA_VERSAO_VAI_40630'),
			enumerable: true
		})
		Object.defineProperty(this, 'allTheVersionsExceptLastWillEliminate', {
			get: () => this._fnGetResource('TODAS_AS_VERSOES_EXC52356'),
			enumerable: true
		})
		Object.defineProperty(this, 'uploadDocVersionHeader', {
			get: () => this._fnGetResource('VERSOES_DO_DOCUMENTO34166'),
			enumerable: true
		})
		Object.defineProperty(this, 'createDocument', {
			get: () => this._fnGetResource('CRIAR_DOCUMENTO55731'),
			enumerable: true
		})
		Object.defineProperty(this, 'editingDocument', {
			get: () => this._fnGetResource('ESTE_DOCUMENTO_ENCON39456'),
			enumerable: true
		})
		Object.defineProperty(this, 'pendingDocumentVersion', {
			get: () => this._fnGetResource('ESTA_VERSAO_DO_DOCUM23227'),
			enumerable: true
		})
		Object.defineProperty(this, 'errorProcessingRequest', {
			get: () => this._fnGetResource('OCORREU_UM_ERRO_AO_P53091'),
			enumerable: true
		})
	}
}

class ImageResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'submitLabel', {
			get: () => this._fnGetResource('SUBMETER21206'),
			enumerable: true
		})
		Object.defineProperty(this, 'editLabel', {
			get: () => this._fnGetResource('EDITAR11616'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteLabel', {
			get: () => this._fnGetResource('APAGAR04097'),
			enumerable: true
		})
		Object.defineProperty(this, 'fileSizeError', {
			get: () => this._fnGetResource('O_FICHEIRO_SELECIONA49645'),
			enumerable: true
		})
		Object.defineProperty(this, 'extensionError', {
			get: () => this._fnGetResource('EXTENSAO_INVALIDA__E46375'),
			enumerable: true
		})
		Object.defineProperty(this, 'editImage', {
			get: () => this._fnGetResource('EDITAR_IMAGEM49158'),
			enumerable: true
		})
		Object.defineProperty(this, 'cropWarning', {
			get: () => this._fnGetResource('ATENCAO__AO_GRAVAR_E10841'),
			enumerable: true
		})
		Object.defineProperty(this, 'dropToUpload', {
			get: () => this._fnGetResource('ARRASTE_O_FICHEIRO_A37155'),
			enumerable: true
		})
		Object.defineProperty(this, 'save', {
			get: () => this._fnGetResource('GRAVAR45301'),
			enumerable: true
		})
		Object.defineProperty(this, 'cancel', {
			get: () => this._fnGetResource('CANCELAR49513'),
			enumerable: true
		})
		Object.defineProperty(this, 'zoomIn', {
			get: () => this._fnGetResource('ZOOM_IN39873'),
			enumerable: true
		})
		Object.defineProperty(this, 'zoomOut', {
			get: () => this._fnGetResource('ZOOM_OUT31562'),
			enumerable: true
		})
		Object.defineProperty(this, 'moveImageLeft', {
			get: () => this._fnGetResource('MOVER_IMAGEM_PARA_A_24246'),
			enumerable: true
		})
		Object.defineProperty(this, 'moveImageRight', {
			get: () => this._fnGetResource('MOVER_IMAGEM_PARA_A_35927'),
			enumerable: true
		})
		Object.defineProperty(this, 'moveImageUp', {
			get: () => this._fnGetResource('MOVER_IMAGEM_PARA_CI59923'),
			enumerable: true
		})
		Object.defineProperty(this, 'moveImageDown', {
			get: () => this._fnGetResource('MOVER_IMAGEM_PARA_BA49541'),
			enumerable: true
		})
		Object.defineProperty(this, 'rotateLeft', {
			get: () => this._fnGetResource('VIRAR_A_ESQUERDA33355'),
			enumerable: true
		})
		Object.defineProperty(this, 'rotateRight', {
			get: () => this._fnGetResource('VIRAR_A_DIREITA03453'),
			enumerable: true
		})
		Object.defineProperty(this, 'flipHorizontal', {
			get: () => this._fnGetResource('VIRAR_NA_HORIZONTAL60231'),
			enumerable: true
		})
		Object.defineProperty(this, 'flipVertical', {
			get: () => this._fnGetResource('VIRAR_NA_VERTICAL20218'),
			enumerable: true
		})
		Object.defineProperty(this, 'deleteHeaderLabel', {
			get: () => this._fnGetResource('TEM_A_CERTEZA_QUE_QU37043'),
			enumerable: true
		})
		Object.defineProperty(this, 'yesLabel', {
			get: () => this._fnGetResource('SIM28552'),
			enumerable: true
		})
		Object.defineProperty(this, 'noLabel', {
			get: () => this._fnGetResource('NAO06521'),
			enumerable: true
		})
		Object.defineProperty(this, 'close', {
			get: () => this._fnGetResource('FECHAR32496'),
			enumerable: true
		})
		Object.defineProperty(this, 'download', {
			get: () => this._fnGetResource('DESCARREGAR58418'),
			enumerable: true
		})
	}
}

class DashboardResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'editButtonTitle', {
			get: () => this._fnGetResource('EDITAR11616'),
			enumerable: true
		})
		Object.defineProperty(this, 'compactButtonTitle', {
			get: () => this._fnGetResource('COMPACTAR38838'),
			enumerable: true
		})
		Object.defineProperty(this, 'saveButtonTitle', {
			get: () => this._fnGetResource('GRAVAR45301'),
			enumerable: true
		})
		Object.defineProperty(this, 'cancelButtonTitle', {
			get: () => this._fnGetResource('CANCELAR49513'),
			enumerable: true
		})
		Object.defineProperty(this, 'helpText', {
			get: () => this._fnGetResource('PARA_ADICIONAR_UM_WI63588'),
			enumerable: true
		})
		Object.defineProperty(this, 'addButtonTitle', {
			get: () => this._fnGetResource('ADICIONAR14072'),
			enumerable: true
		})
		Object.defineProperty(this, 'addWidgetText', {
			get: () => this._fnGetResource('ADICIONAR_WIDGET21299'),
			enumerable: true
		})
		Object.defineProperty(this, 'noRecordsText', {
			get: () => this._fnGetResource('SEM_REGISTOS62529'),
			enumerable: true
		})
		Object.defineProperty(this, 'noDataText', {
			get: () => this._fnGetResource('SEM_DADOS_PARA_MOSTR24928'),
			enumerable: true
		})
		Object.defineProperty(this, 'previousPageText', {
			get: () => this._fnGetResource('PAGINA_ANTERIOR17471'),
			enumerable: true
		})
		Object.defineProperty(this, 'nextPageText', {
			get: () => this._fnGetResource('PAGINA_SEGUINTE34153'),
			enumerable: true
		})
		Object.defineProperty(this, 'removeButtonText', {
			get: () => this._fnGetResource('REMOVER14367'),
			enumerable: true
		})
		Object.defineProperty(this, 'refreshButtonText', {
			get: () => this._fnGetResource('ATUALIZAR22496'),
			enumerable: true
		})
	}
}

class TimelineResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'reset', {
			get: () => this._fnGetResource('REPOR35852'),
			enumerable: true
		})
		Object.defineProperty(this, 'daily', {
			get: () => this._fnGetResource('DIARIA21794'),
			enumerable: true
		})
		Object.defineProperty(this, 'weekly', {
			get: () => this._fnGetResource('SEMANAL19148'),
			enumerable: true
		})
		Object.defineProperty(this, 'monthly', {
			get: () => this._fnGetResource('MENSAL53343'),
			enumerable: true
		})
		Object.defineProperty(this, 'yearly', {
			get: () => this._fnGetResource('ANUAL55239'),
			enumerable: true
		})
	}
}

class WizardResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'showNextSteps', {
			get: () => this._fnGetResource('CLIQUE_PARA_MOSTRAR_48694'),
			enumerable: true
		})
		Object.defineProperty(this, 'showPrevSteps', {
			get: () => this._fnGetResource('CLIQUE_PARA_MOSTRAR_07566'),
			enumerable: true
		})
	}
}

class FormContainerResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'chooseElement', {
			get: () => this._fnGetResource('ESCOLHA_UM_ELEMENTO_24060'),
			enumerable: true
		})
		Object.defineProperty(this, 'or', {
			get: () => this._fnGetResource('OU11765'),
			enumerable: true
		})
		Object.defineProperty(this, 'insert', {
			get: () => this._fnGetResource('INSERIR43365'),
			enumerable: true
		})
	}
}

class CodeEditorResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'showChanges', {
			get: () => this._fnGetResource('MOSTRAR_ALTERACOES47173'),
			enumerable: true
		})
		Object.defineProperty(this, 'dark', {
			get: () => this._fnGetResource('ESCURO16457'),
			enumerable: true
		})
		Object.defineProperty(this, 'light', {
			get: () => this._fnGetResource('CLARO60841'),
			enumerable: true
		})
		Object.defineProperty(this, 'theme', {
			get: () => this._fnGetResource('TEMA56931'),
			enumerable: true
		})
		Object.defineProperty(this, 'defaultPlaceholder', {
			get: () => this._fnGetResource('ESCREVA_O_SEU_CODIGO16246'),
			enumerable: true
		})
	}
}

class PropertyListResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'emptyMessage', {
			get: () => this._fnGetResource('SELECIONE_UM_CAMPO_P10271'),
			enumerable: true
		})
	}
}

class TabsResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'panels', {
			get: () => this._fnGetResource('PAINEIS13077'),
			enumerable: true
		})
	}
}

class KanbanResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'addItem', {
			get: () => this._fnGetResource('INSERIR43365'),
			enumerable: true
		})
		Object.defineProperty(this, 'columnPlaceholder', {
			get: () => this._fnGetResource('NOME_DA_COLUNA14566'),
			enumerable: true
		})
	}
}

class MarkdownEditorResources extends BaseResources
{
	constructor(fnGetResource)
	{
		super(fnGetResource)

		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'editor', {
			get: () => this._fnGetResource('EDITOR63216'),
			enumerable: true
		})
		Object.defineProperty(this, 'editorAndPreview', {
			get: () => this._fnGetResource('EDITOR_E_PRE_VISUALI39794'),
			enumerable: true
		})
		Object.defineProperty(this, 'preview', {
			get: () => this._fnGetResource('PREVIEW45357'),
			enumerable: true
		})
		Object.defineProperty(this, 'fullScreen', {
			get: () => this._fnGetResource('FULL_SCREEN61499'),
			enumerable: true
		})
		// Toolbar
		Object.defineProperty(this, 'addBoldText', {
			get: () => this._fnGetResource('ADD_BOLD_TEXT22975'),
			enumerable: true
		})
		Object.defineProperty(this, 'addItalicText', {
			get: () => this._fnGetResource('ADD_ITALIC_TEXT40135'),
			enumerable: true
		})
		Object.defineProperty(this, 'addHeadingText', {
			get: () => this._fnGetResource('ADD_HEADING_TEXT32146'),
			enumerable: true
		})
		Object.defineProperty(this, 'addStrikethroughText', {
			get: () => this._fnGetResource('ADD_STRIKETHROUGH_TE17814'),
			enumerable: true
		})
		Object.defineProperty(this, 'insertQuote', {
			get: () => this._fnGetResource('INSERT_A_QUOTE38960'),
			enumerable: true
		})
		Object.defineProperty(this, 'insertCodeBlock', {
			get: () => this._fnGetResource('INSERT_CODE_BLOCK43857'),
			enumerable: true
		})
		Object.defineProperty(this, 'addLink', {
			get: () => this._fnGetResource('ADD_A_LINK01592'),
			enumerable: true
		})
		Object.defineProperty(this, 'addBulletList', {
			get: () => this._fnGetResource('ADD_A_BULLET_LIST23037'),
			enumerable: true
		})
		Object.defineProperty(this, 'addNumberedList', {
			get: () => this._fnGetResource('ADD_A_NUMBERED_LIST63961'),
			enumerable: true
		})
		Object.defineProperty(this, 'addCheckList', {
			get: () => this._fnGetResource('ADD_A_CHECKLIST23651'),
			enumerable: true
		})
		Object.defineProperty(this, 'addHorizontalRule', {
			get: () => this._fnGetResource('ADD_A_HORIZONTAL_RUL24707'),
			enumerable: true
		})
		Object.defineProperty(this, 'addTable', {
			get: () => this._fnGetResource('ADD_A_TABLE25837'),
			enumerable: true
		})
		Object.defineProperty(this, 'addImage', {
			get: () => this._fnGetResource('ADD_AN_IMAGE52382'),
			enumerable: true
		})
	}
}

export default {
	BaseResources,
	TableListMainResources,
	ImportExportResources,
	MultipleValuesExtensionResources,
	LookupResources,
	DateTimeResources,
	DocumentResources,
	ImageResources,
	DashboardResources,
	TimelineResources,
	WizardResources,
	FormContainerResources,
	CodeEditorResources,
	PropertyListResources,
	TabsResources,
	KanbanResources,
	MarkdownEditorResources
}
