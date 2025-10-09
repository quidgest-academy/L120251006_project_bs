<template>
	<teleport
		v-if="menuModalIsReady"
		:to="`#${uiContainersId.body}`"
		:disabled="!menuInfo.isPopup">
		<form
			class="form-horizontal"
			@submit.prevent>
			<q-row-container>
				<q-table
					v-bind="controls.menu"
					v-on="controls.menu.handlers">
				</q-table>

				<q-table-extra-extension
					:list-ctrl="controls.menu"
					:filter-operators="controls.menu.filterOperators"
					v-on="controls.menu.handlers" />
			</q-row-container>
		</form>
	</teleport>

	<teleport
		v-if="menuModalIsReady && hasButtons"
		:to="`#${uiContainersId.footer}`"
		:disabled="!menuInfo.isPopup">
		<q-row-container>
			<div id="footer-action-btns">
				<template
					v-for="btn in menuButtons"
					:key="btn.id">
					<q-button
						v-if="btn.isVisible"
						:id="btn.id"
						:label="btn.text"
						:variant="btn.variant"
						:disabled="btn.disabled"
						:icon-pos="btn.iconPos"
						:class="btn.classes"
						@click="btn.action">
						<q-icon
							v-if="btn.icon"
							v-bind="btn.icon" />
					</q-button>
				</template>
			</div>
		</q-row-container>
	</teleport>
</template>

<script>
	/* eslint-disable @typescript-eslint/no-unused-vars */
	import asyncProcM from '@quidgest/clientapp/composables/async'
	import qEnums from '@quidgest/clientapp/constants/enums'
	import netAPI from '@quidgest/clientapp/network'
	import openQSign from '@quidgest/clientapp/plugins/qSign'
	import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'
	import { computed, readonly } from 'vue'

	import MenuHandlers from '@/mixins/menuHandlers.js'
	import controlClass from '@/mixins/fieldControl.js'
	import listFunctions from '@/mixins/listFunctions.js'
	import listColumnTypes from '@/mixins/listColumnTypes.js'
	import { resetProgressBar, setProgressBar } from '@/utils/layout.js'

	import { loadResources } from '@/plugins/i18n.js'

	import hardcodedTexts from '@/hardcodedTexts'
	import qApi from '@/api/genio/quidgestFunctions.js'
	import qFunctions from '@/api/genio/projectFunctions.js'
	import qProjArrays from '@/api/genio/projectArrays.js'
	/* eslint-enable @typescript-eslint/no-unused-vars */

	import MenuViewModel from './QMenuMOV_11ViewModel.js'

	const requiredTextResources = ['QMenuMOV_11', 'hardcoded', 'messages']

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_INCLUDEJS MOV_MENU_11]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

	export default {
		name: 'QMenuMov11',

		mixins: [
			MenuHandlers
		],

		inheritAttrs: false,

		props: {
			/**
			 * Whether or not the menu is used as a homepage.
			 */
			isHomePage: {
				type: Boolean,
				default: false
			}
		},

		expose: [
			'navigationId',
			'onBeforeRouteLeave',
			'updateMenuNavigation'
		],

		data()
		{
			// eslint-disable-next-line
			const vm = this
			return {
				componentOnLoadProc: asyncProcM.getProcListMonitor('QMenuMOV_11', false),

				interfaceMetadata: {
					id: 'QMenuMOV_11', // Used for resources
					requiredTextResources
				},

				menuInfo: {
					id: '11',
					isMenuList: true,
					designation: computed(() => this.Resources.MOVIES48538),
					acronym: 'MOV_11',
					name: 'MOVIE',
					route: 'menu-MOV_11',
					order: '11',
					controller: 'MOVIE',
					action: 'MOV_Menu_11',
					isPopup: false
				},

				model: new MenuViewModel(this),

				controls: {
					menu: new controlClass.TableSpecialRenderingControl({
						fnHydrateViewModel: (data) => vm.model.hydrate(data),
						id: 'MOV_Menu_11',
						controller: 'MOVIE',
						action: 'MOV_Menu_11',
						hasDependencies: false,
						isInCollapsible: false,
						tableModeClasses: [
							'q-table--full-height',
							'page-full-height'
						],
						columnsOriginal: [
							new listColumnTypes.TextColumn({
								order: 1,
								name: 'ValDescription',
								area: 'MOVIE',
								field: 'DESCRIPTION',
								label: computed(() => this.Resources.DISCRIPTION02169),
								scrollData: 30,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.DateColumn({
								order: 2,
								name: 'ValRealease_date',
								area: 'MOVIE',
								field: 'REALEASE_DATE',
								label: computed(() => this.Resources.REALEASE_DATE49316),
								scrollData: 8,
								dateTimeType: 'date',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.ArrayColumn({
								order: 3,
								name: 'ValMoviesgenre',
								area: 'MOVIE',
								field: 'MOVIESGENRE',
								label: computed(() => this.Resources.MOVIES_GENRE22042),
								dataLength: 15,
								scrollData: 15,
								array: computed(() => new qProjArrays.QArrayMoviegenre(vm.$getResource).elements),
								arrayType: qProjArrays.QArrayMoviegenre.type,
								arrayDisplayMode: 'D',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 4,
								name: 'ValTitle',
								area: 'MOVIE',
								field: 'TITLE',
								label: computed(() => this.Resources.TITLE21885),
								dataLength: 80,
								scrollData: 30,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.ImageColumn({
								order: 5,
								name: 'ValPoster',
								area: 'MOVIE',
								field: 'POSTER',
								label: computed(() => this.Resources.POSTER52933),
								dataTitle: computed(() => genericFunctions.formatString(vm.Resources.IMAGEM_UTILIZADA_PAR58591, vm.Resources.POSTER52933)),
								scrollData: 3,
								sortable: false,
								searchable: false,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.DateColumn({
								order: 6,
								name: 'ValCreateat',
								area: 'MOVIE',
								field: 'CREATEAT',
								label: computed(() => this.Resources.CREATE_AT36393),
								scrollData: 8,
								dateTimeType: 'date',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
						],
						config: {
							name: 'MOV_Menu_11',
							serverMode: true,
							pkColumn: 'ValCodmovie',
							tableAlias: 'MOVIE',
							tableNamePlural: computed(() => this.Resources.MOVIES48538),
							viewManagement: '',
							showLimitsInfo: true,
							tableTitle: computed(() => this.Resources.MOVIES48538),
							showAlternatePagination: true,
							permissions: {
							},
							searchBarConfig: {
								visibility: true,
								searchOnPressEnter: true
							},
							filtersVisible: true,
							allowColumnFilters: true,
							allowColumnSort: true,
							crudActions: [
								{
									id: 'show',
									name: 'show',
									title: computed(() => this.Resources.CONSULTAR57388),
									icon: {
										icon: 'view'
									},
									isInReadOnly: true,
									params: {
										action: vm.openFormAction,
										type: 'form',
										formName: 'F_MOVIES',
										mode: 'SHOW',
										isControlled: true
									}
								},
								{
									id: 'edit',
									name: 'edit',
									title: computed(() => this.Resources.EDITAR11616),
									icon: {
										icon: 'pencil'
									},
									isInReadOnly: true,
									params: {
										action: vm.openFormAction,
										type: 'form',
										formName: 'F_MOVIES',
										mode: 'EDIT',
										isControlled: true
									}
								},
								{
									id: 'duplicate',
									name: 'duplicate',
									title: computed(() => this.Resources.DUPLICAR09748),
									icon: {
										icon: 'duplicate'
									},
									isInReadOnly: true,
									params: {
										action: vm.openFormAction,
										type: 'form',
										formName: 'F_MOVIES',
										mode: 'DUPLICATE',
										isControlled: true
									}
								},
								{
									id: 'delete',
									name: 'delete',
									title: computed(() => this.Resources.ELIMINAR21155),
									icon: {
										icon: 'delete'
									},
									isInReadOnly: true,
									params: {
										action: vm.openFormAction,
										type: 'form',
										formName: 'F_MOVIES',
										mode: 'DELETE',
										isControlled: true
									}
								}
							],
							generalActions: [
								{
									id: 'insert',
									name: 'insert',
									title: computed(() => this.Resources.INSERIR43365),
									icon: {
										icon: 'add'
									},
									isInReadOnly: true,
									params: {
										action: vm.openFormAction,
										type: 'form',
										formName: 'F_MOVIES',
										mode: 'NEW',
										repeatInsertion: false,
										isControlled: true
									}
								},
							],
							generalCustomActions: [
							],
							groupActions: [
							],
							customActions: [
							],
							MCActions: [
							],
							rowClickAction: {
								id: 'RCA_MOV_111',
								name: 'form-F_MOVIES',
								params: {
									isRoute: true,
									limits: [
										{
											identifier: 'id',
											fnValueSelector: (row) => row.ValCodmovie
										},
									],
									isControlled: true,
									action: vm.openFormAction, type: 'form', mode: 'SHOW', formName: 'F_MOVIES'
								}
							},
							formsDefinition: {
								'F_MOVIES': {
									fnKeySelector: (row) => row.Fields.ValCodmovie,
									isPopup: false
								},
							},
							insertCondition: {
								// eslint-disable-next-line @typescript-eslint/no-unused-vars
								fnFormula(params)
								{
									return netAPI.postData(
										'Movie',
										'F_MOVIES_InsertCondition',
										this.serverObjModel,
										undefined,
										undefined,
										undefined,
										this.navigationId)
								},
								dependencyEvents: [],
								isServerRecalc: false,
							},
							defaultSearchColumnName: 'ValTitle',
							defaultSearchColumnNameOriginal: 'ValTitle',
							defaultColumnSorting: {
								columnName: 'ValRealease_date',
								sortOrder: 'asc'
							}
						},
						globalEvents: ['changed-MOVIE'],
						uuid: 'fe04e135-1c37-44a2-a675-d67681e46f67',
						allSelectedRows: 'false',
						viewModes: [
							{
								id: 'CARDS',
								type: 'cards',
								subtype: 'card-img-top',
								label: computed(() => this.Resources.CARTOES27587),
								order: 1,
								mappingVariables: readonly({
									title: {
										allowsMultiple: false,
										sources: [
											'MOVIE.TITLE',
										]
									},
									subtitle: {
										allowsMultiple: false,
										sources: [
											'MOVIE.REALEASE_DATE',
										]
									},
									image: {
										allowsMultiple: false,
										sources: [
											'MOVIE.POSTER',
										]
									},
								}),
								styleVariables: {
									actionsAlignment: {
										rawValue: 'left',
										isMapped: false
									},
									actionsStyle: {
										rawValue: 'dropdown',
										isMapped: false
									},
									backgroundColor: {
										rawValue: 'auto',
										isMapped: false
									},
									contentAlignment: {
										rawValue: 'left',
										isMapped: false
									},
									customFollowupDefaultTarget: {
										rawValue: 'blank',
										isMapped: false
									},
									customInsertCard: {
										rawValue: false,
										isMapped: false
									},
									customInsertCardStyle: {
										rawValue: 'secondary',
										isMapped: false
									},
									displayMode: {
										rawValue: 'grid',
										isMapped: false
									},
									gridMode: {
										rawValue: 'fixed',
										isMapped: false
									},
									containerAlignment: {
										rawValue: 'left',
										isMapped: false
									},
									hoverScaleAmount: {
										rawValue: '1.00',
										isMapped: false
									},
									imageShape: {
										rawValue: 'rectangular',
										isMapped: false
									},
									showColumnTitles: {
										rawValue: false,
										isMapped: false
									},
									showEmptyColumnTitles: {
										rawValue: true,
										isMapped: false
									},
									size: {
										rawValue: 'regular',
										isMapped: false
									},
								},
								groups: {
								}
							},
						],
						headerLevel: 1,
					}, this),
				}
			}
		},

		beforeRouteEnter(to, _, next)
		{
			// called before the route that renders this component is confirmed.
			// does NOT have access to `this` component instance,
			// because it has not been created yet when this guard is called!

			next((vm) => vm.updateMenuNavigation(to))
		},

		beforeRouteLeave(to, _, next)
		{
			this.onBeforeRouteLeave(next)
		},

		mounted()
		{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_CODEJS MOV_MENU_11]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		beforeUnmount()
		{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV COMPONENT_BEFORE_UNMOUNT MOV_MENU_11]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		methods: {
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FUNCTIONS_JS MOV_11]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV LISTING_CODEJS MOV_MENU_11]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		}
	}
</script>
