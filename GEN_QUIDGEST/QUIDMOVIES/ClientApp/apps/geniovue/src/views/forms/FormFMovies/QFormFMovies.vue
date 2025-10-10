<template>
	<teleport
		v-if="formModalIsReady && showFormHeader"
		:to="`#${uiContainersId.header}`"
		:disabled="!isPopup || isNested">
		<div
			ref="formHeader"
			:class="{ 'c-sticky-header': isStickyHeader, 'sticky-top': isStickyTop }">
			<div
				v-if="showFormHeader"
				class="c-action-bar">
				<h1
					v-if="formControl.uiComponents.header && formInfo.designation"
					:id="formTitleId"
					class="form-header">
					{{ formInfo.designation }}
				</h1>

				<div class="c-action-bar__menu">
					<template
						v-for="(section, sectionId) in formButtonSections"
						:key="sectionId">
						<span
							v-if="showHeadingSep(sectionId)"
							class="main-title-sep" />

						<q-toggle-group
							v-if="formControl.uiComponents.headerButtons"
							borderless>
							<template
								v-for="btn in section"
								:key="btn.id">
								<q-toggle-group-item
									v-if="showFormHeaderButton(btn)"
									:model-value="btn.isSelected"
									:id="`top-${btn.id}`"
									:title="btn.text"
									:label="btn.label"
									:disabled="btn.disabled"
									@click="btn.action">
									<q-icon
										v-if="btn.icon"
										v-bind="btn.icon" />
								</q-toggle-group-item>
							</template>
						</q-toggle-group>
					</template>
				</div>
			</div>

			<q-anchor-container-horizontal
				v-if="$app.layout.FormAnchorsPosition === 'form-header' && visibleGroups.length > 0"
				:anchors="anchorGroups"
				:controls="visibleControls"
				@focus-control="focusControl" />
		</div>
	</teleport>

	<teleport
		v-if="formModalIsReady && showFormBody"
		:to="`#${uiContainersId.body}`"
		:disabled="!isPopup || isNested">
		<q-validation-summary
			:messages="validationErrors"
			@error-clicked="focusField" />

		<div :class="[`float-${actionsPlacement}`, 'c-action-bar']">
			<q-button-group borderless>
				<template
					v-for="btn in formButtons"
					:key="btn.id">
					<q-button
						v-if="btn.isActive && btn.isVisible && btn.showInHeading"
						:id="`heading-${btn.id}`"
						:label="btn.text"
						:variant="btn.variant"
						:disabled="btn.disabled"
						:icon-pos="btn.iconPos"
						:class="btn.classes"
						@click="btn.action(); btn.emitAction ? $emit(btn.emitAction.name, btn.emitAction.params) : null">
						<q-icon
							v-if="btn.icon"
							v-bind="btn.icon" />
					</q-button>
				</template>
			</q-button-group>
		</div>

		<div
			class="form-flow"
			data-key="F_MOVIES"
			:data-loading="!formInitialDataLoaded">
			<template v-if="formControl.initialized && showFormBody">
				<q-row-container
					v-if="controls.F_MOVIESPSEUDNEWGRP02.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVIESPSEUDNEWGRP02.isVisible"
						class="row-line-group">
						<q-group-box-container
							v-if="controls.F_MOVIESPSEUDNEWGRP02.isVisible"
							id="F_MOVIESPSEUDNEWGRP02"
							v-bind="controls.F_MOVIESPSEUDNEWGRP02"
							:is-visible="controls.F_MOVIESPSEUDNEWGRP02.isVisible">
							<!-- Start F_MOVIESPSEUDNEWGRP02 -->
							<q-row-container v-if="controls.F_MOVIESMOVIEPOSTER__.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIEPOSTER__.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIEPOSTER__.isVisible"
										class="q-image"
										v-bind="controls.F_MOVIESMOVIEPOSTER__"
										v-on="controls.F_MOVIESMOVIEPOSTER__.handlers"
										:loading="controls.F_MOVIESMOVIEPOSTER__.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-image
											v-if="controls.F_MOVIESMOVIEPOSTER__.isVisible"
											v-bind="controls.F_MOVIESMOVIEPOSTER__.props"
											v-on="controls.F_MOVIESMOVIEPOSTER__.handlers" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVIESMOVIETITLE___.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIETITLE___.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIETITLE___.isVisible"
										class="i-text"
										v-bind="controls.F_MOVIESMOVIETITLE___"
										v-on="controls.F_MOVIESMOVIETITLE___.handlers"
										:loading="controls.F_MOVIESMOVIETITLE___.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-text-field
											v-bind="controls.F_MOVIESMOVIETITLE___.props"
											@blur="onBlur(controls.F_MOVIESMOVIETITLE___, model.ValTitle.value)"
											@change="model.ValTitle.fnUpdateValueOnChange" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVIESMOVIEDISCRIPT.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIEDISCRIPT.isVisible"
									class="control-join-group control-dynamic-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIEDISCRIPT.isVisible"
										class="i-textarea"
										v-bind="controls.F_MOVIESMOVIEDISCRIPT"
										v-on="controls.F_MOVIESMOVIEDISCRIPT.handlers"
										:loading="controls.F_MOVIESMOVIEDISCRIPT.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-text-area
											v-if="controls.F_MOVIESMOVIEDISCRIPT.isVisible"
											v-bind="controls.F_MOVIESMOVIEDISCRIPT.props"
											v-on="controls.F_MOVIESMOVIEDISCRIPT.handlers" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVIESMOVIEMOVIEGEN.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIEMOVIEGEN.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIEMOVIEGEN.isVisible"
										class="i-radio-container"
										v-bind="controls.F_MOVIESMOVIEMOVIEGEN"
										v-on="controls.F_MOVIESMOVIEMOVIEGEN.handlers"
										:label-position="labelAlignment.topleft"
										:loading="controls.F_MOVIESMOVIEMOVIEGEN.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-radio-group
											v-if="controls.F_MOVIESMOVIEMOVIEGEN.isVisible"
											v-bind="controls.F_MOVIESMOVIEMOVIEGEN.props"
											v-on="controls.F_MOVIESMOVIEMOVIEGEN.handlers">
											<q-radio-button
												v-for="radio in controls.F_MOVIESMOVIEMOVIEGEN.items"
												:key="radio.key"
												:label="radio.value"
												:value="radio.key" />
										</q-radio-group>
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVIESMOVIERELDATE_.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIERELDATE_.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIERELDATE_.isVisible"
										class="i-text"
										v-bind="controls.F_MOVIESMOVIERELDATE_"
										v-on="controls.F_MOVIESMOVIERELDATE_.handlers"
										:loading="controls.F_MOVIESMOVIERELDATE_.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-date-time-picker
											v-if="controls.F_MOVIESMOVIERELDATE_.isVisible"
											v-bind="controls.F_MOVIESMOVIERELDATE_.props"
											:model-value="model.ValRealease_date.value"
											@reset-icon-click="model.ValRealease_date.fnUpdateValue(model.ValRealease_date.originalValue ?? new Date())"
											@update:model-value="model.ValRealease_date.fnUpdateValue($event ?? '')" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVIESPSEUDNEWGRP02 -->
						</q-group-box-container>
					</q-control-wrapper>
				</q-row-container>
				<q-row-container
					v-if="controls.F_MOVIESPSEUDNEWGRP03.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVIESPSEUDNEWGRP03.isVisible"
						class="row-line-group">
						<q-group-box-container
							v-if="controls.F_MOVIESPSEUDNEWGRP03.isVisible"
							id="F_MOVIESPSEUDNEWGRP03"
							v-bind="controls.F_MOVIESPSEUDNEWGRP03"
							:is-visible="controls.F_MOVIESPSEUDNEWGRP03.isVisible">
							<!-- Start F_MOVIESPSEUDNEWGRP03 -->
							<q-row-container v-if="controls.F_MOVIESMOVIENUMBEROF.isVisible || controls.F_MOVIESPSEUDFIELD001.isVisible || controls.F_MOVIESMOVIEARATE___.isVisible || controls.F_MOVIESPSEUDFIELD003.isVisible || controls.F_MOVIESMOVIELASTRATE.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIENUMBEROF.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIENUMBEROF.isVisible"
										class="i-text"
										v-bind="controls.F_MOVIESMOVIENUMBEROF"
										v-on="controls.F_MOVIESMOVIENUMBEROF.handlers"
										:loading="controls.F_MOVIESMOVIENUMBEROF.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVIESMOVIENUMBEROF.isVisible"
											v-bind="controls.F_MOVIESMOVIENUMBEROF.props"
											@update:model-value="model.ValNumberoflikes.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVIESPSEUDFIELD001.isVisible || controls.F_MOVIESMOVIEARATE___.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESPSEUDFIELD001.isVisible"
										class="i-button"
										v-bind="controls.F_MOVIESPSEUDFIELD001"
										v-on="controls.F_MOVIESPSEUDFIELD001.handlers"
										:loading="controls.F_MOVIESPSEUDFIELD001.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-button
											v-if="controls.F_MOVIESPSEUDFIELD001.isVisible"
											v-bind="controls.F_MOVIESPSEUDFIELD001.props"
											@click="controls.F_MOVIESPSEUDFIELD001.action($event)">
										</q-button>
									</base-input-structure>
									<base-input-structure
										v-if="controls.F_MOVIESMOVIEARATE___.isVisible"
										class="i-text"
										v-bind="controls.F_MOVIESMOVIEARATE___"
										v-on="controls.F_MOVIESMOVIEARATE___.handlers"
										:loading="controls.F_MOVIESMOVIEARATE___.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVIESMOVIEARATE___.isVisible"
											v-bind="controls.F_MOVIESMOVIEARATE___.props"
											@update:model-value="model.ValAveragerate.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVIESPSEUDFIELD003.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESPSEUDFIELD003.isVisible"
										class="i-button"
										v-bind="controls.F_MOVIESPSEUDFIELD003"
										v-on="controls.F_MOVIESPSEUDFIELD003.handlers"
										:loading="controls.F_MOVIESPSEUDFIELD003.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-button
											v-if="controls.F_MOVIESPSEUDFIELD003.isVisible"
											v-bind="controls.F_MOVIESPSEUDFIELD003.props"
											@click="controls.F_MOVIESPSEUDFIELD003.action($event)">
										</q-button>
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVIESMOVIELASTRATE.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESMOVIELASTRATE.isVisible"
										class="i-text"
										v-bind="controls.F_MOVIESMOVIELASTRATE"
										v-on="controls.F_MOVIESMOVIELASTRATE.handlers"
										:loading="controls.F_MOVIESMOVIELASTRATE.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVIESMOVIELASTRATE.isVisible"
											v-bind="controls.F_MOVIESMOVIELASTRATE.props"
											@update:model-value="model.ValLastrate.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVIESPSEUDNEWGRP03 -->
						</q-group-box-container>
					</q-control-wrapper>
				</q-row-container>
				<q-row-container
					v-if="controls.F_MOVIESPSEUDNEWGRP04.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVIESPSEUDNEWGRP04.isVisible"
						class="row-line-group">
						<q-group-collapsible
							v-if="controls.F_MOVIESPSEUDNEWGRP04.isVisible"
							id="F_MOVIESPSEUDNEWGRP04"
							v-bind="controls.F_MOVIESPSEUDNEWGRP04"
							v-on="controls.F_MOVIESPSEUDNEWGRP04.handlers">
							<!-- Start F_MOVIESPSEUDNEWGRP04 -->
							<q-row-container v-if="controls.F_MOVIESPSEUDDATACOME.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESPSEUDDATACOME.isVisible"
									class="control-join-group">
									<q-table
										v-if="controls.F_MOVIESPSEUDDATACOME.isVisible"
										v-bind="controls.F_MOVIESPSEUDDATACOME"
										v-on="controls.F_MOVIESPSEUDDATACOME.handlers" />
									<q-table-extra-extension
										v-if="controls.F_MOVIESPSEUDDATACOME.isVisible"
										:list-ctrl="controls.F_MOVIESPSEUDDATACOME"
										:filter-operators="controls.F_MOVIESPSEUDDATACOME.filterOperators"
										v-on="controls.F_MOVIESPSEUDDATACOME.handlers" />
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVIESPSEUDFIELD002.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESPSEUDFIELD002.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVIESPSEUDFIELD002.isVisible"
										class="i-button"
										v-bind="controls.F_MOVIESPSEUDFIELD002"
										v-on="controls.F_MOVIESPSEUDFIELD002.handlers"
										:loading="controls.F_MOVIESPSEUDFIELD002.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-button
											v-if="controls.F_MOVIESPSEUDFIELD002.isVisible"
											v-bind="controls.F_MOVIESPSEUDFIELD002.props"
											@click="controls.F_MOVIESPSEUDFIELD002.action($event)">
										</q-button>
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVIESPSEUDNEWGRP04 -->
						</q-group-collapsible>
					</q-control-wrapper>
				</q-row-container>
				<q-row-container
					v-if="controls.F_MOVIESPSEUDNEWGRP01.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVIESPSEUDNEWGRP01.isVisible"
						class="row-line-group">
						<q-group-collapsible
							v-if="controls.F_MOVIESPSEUDNEWGRP01.isVisible"
							id="F_MOVIESPSEUDNEWGRP01"
							v-bind="controls.F_MOVIESPSEUDNEWGRP01"
							v-on="controls.F_MOVIESPSEUDNEWGRP01.handlers">
							<!-- Start F_MOVIESPSEUDNEWGRP01 -->
							<q-row-container v-if="controls.F_MOVIESPSEUDDATACHAR.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVIESPSEUDDATACHAR.isVisible"
									class="control-join-group control-dynamic-group">
									<q-table
										v-if="controls.F_MOVIESPSEUDDATACHAR.isVisible"
										v-bind="controls.F_MOVIESPSEUDDATACHAR"
										v-on="controls.F_MOVIESPSEUDDATACHAR.handlers" />
									<q-table-extra-extension
										v-if="controls.F_MOVIESPSEUDDATACHAR.isVisible"
										:list-ctrl="controls.F_MOVIESPSEUDDATACHAR"
										:filter-operators="controls.F_MOVIESPSEUDDATACHAR.filterOperators"
										v-on="controls.F_MOVIESPSEUDDATACHAR.handlers" />
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVIESPSEUDNEWGRP01 -->
						</q-group-collapsible>
					</q-control-wrapper>
				</q-row-container>
			</template>
		</div>
	</teleport>

	<hr v-if="!isPopup && showFormFooter" />

	<teleport
		v-if="formModalIsReady && showFormFooter"
		:to="`#${uiContainersId.footer}`"
		:disabled="!isPopup || isNested">
		<q-row-container v-if="showFormFooter">
			<div id="footer-action-btns">
				<template
					v-for="btn in formButtons"
					:key="btn.id">
					<q-button
						v-if="btn.isActive && btn.isVisible && btn.showInFooter"
						:id="`bottom-${btn.id}`"
						:label="btn.text"
						:variant="btn.variant"
						:disabled="btn.disabled"
						:icon-pos="btn.iconPos"
						:class="btn.classes"
						@click="btn.action(); btn.emitAction ? $emit(btn.emitAction.name, btn.emitAction.params) : null">
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
	import { computed, defineAsyncComponent, readonly } from 'vue'
	import { useRoute } from 'vue-router'

	import FormHandlers from '@/mixins/formHandlers.js'
	import formFunctions from '@/mixins/formFunctions.js'
	import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'
	import listFunctions from '@/mixins/listFunctions.js'
	import listColumnTypes from '@/mixins/listColumnTypes.js'
	import modelFieldType from '@quidgest/clientapp/models/fields'
	import fieldControlClass from '@/mixins/fieldControl.js'
	import qEnums from '@quidgest/clientapp/constants/enums'
	import { resetProgressBar, setProgressBar } from '@/utils/layout.js'

	import hardcodedTexts from '@/hardcodedTexts.js'
	import netAPI from '@quidgest/clientapp/network'
	import asyncProcM from '@quidgest/clientapp/composables/async'
	import qApi from '@/api/genio/quidgestFunctions.js'
	import qFunctions from '@/api/genio/projectFunctions.js'
	import qProjArrays from '@/api/genio/projectArrays.js'
	/* eslint-enable @typescript-eslint/no-unused-vars */

	import FormViewModel from './QFormFMoviesViewModel.js'

	const requiredTextResources = ['QFormFMovies', 'hardcoded', 'messages']

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_INCLUDEJS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

	export default {
		name: 'QFormFMovies',

		components: {
		},

		mixins: [
			FormHandlers
		],

		props: {
			/**
			 * Parameters passed in case the form is nested.
			 */
			nestedRouteParams: {
				type: Object,
				default: () => ({
					name: 'F_MOVIES',
					location: 'form-F_MOVIES',
					params: {
						isNested: true
					}
				})
			}
		},

		expose: [
			'cancel',
			'initFormProperties',
			'navigationId'
		],

		setup(props)
		{
			const route = useRoute()

			return {
				/*
				 * As properties are reactive, when using $route.params, then when we exit it updates cached components.
				 * Properties have no value and this creates an error in new versions of vue-router.
				 * That's why the value has to be copied to a local property to be used in the router-link tag.
				 */
				currentRouteParams: props.isNested ? {} : route.params
			}
		},

		data()
		{
			// eslint-disable-next-line
			const vm = this
			return {
				componentOnLoadProc: asyncProcM.getProcListMonitor('QFormFMovies', false),

				interfaceMetadata: {
					id: 'QFormFMovies', // Used for resources
					requiredTextResources
				},

				formInfo: {
					type: 'normal',
					name: 'F_MOVIES',
					route: 'form-F_MOVIES',
					area: 'MOVIE',
					primaryKey: 'ValCodmovie',
					designation: computed(() => this.Resources.MOVIES48538),
					identifier: '', // Unique identifier received by route (when it's nested).
					mode: '',
					availableAgents: [],
				},

				formButtons: {
					changeToShow: {
						id: 'change-to-show-btn',
						icon: {
							icon: 'view',
							type: 'svg'
						},
						type: 'form-mode',
						text: computed(() => vm.Resources[hardcodedTexts.view]),
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isSelected: computed(() => vm.formModes.show === vm.formInfo.mode),
						isVisible: computed(() => vm.authData.isAllowed && [vm.formModes.show, vm.formModes.edit, vm.formModes.delete].includes(vm.formInfo.mode)),
						action: vm.changeToShowMode
					},
					changeToEdit: {
						id: 'change-to-edit-btn',
						icon: {
							icon: 'pencil',
							type: 'svg'
						},
						type: 'form-mode',
						text: computed(() => vm.Resources[hardcodedTexts.edit]),
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isSelected: computed(() => vm.formModes.edit === vm.formInfo.mode),
						isVisible: computed(() => vm.authData.isAllowed && [vm.formModes.show, vm.formModes.edit, vm.formModes.delete].includes(vm.formInfo.mode)),
						action: vm.changeToEditMode
					},
					changeToDuplicate: {
						id: 'change-to-dup-btn',
						icon: {
							icon: 'duplicate',
							type: 'svg'
						},
						type: 'form-mode',
						text: computed(() => vm.Resources[hardcodedTexts.duplicate]),
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isSelected: computed(() => vm.formModes.duplicate === vm.formInfo.mode),
						isVisible: computed(() => vm.authData.isAllowed && vm.formModes.new !== vm.formInfo.mode),
						action: vm.changeToDupMode
					},
					changeToDelete: {
						id: 'change-to-delete-btn',
						icon: {
							icon: 'delete',
							type: 'svg'
						},
						type: 'form-mode',
						text: computed(() => vm.Resources[hardcodedTexts.delete]),
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isSelected: computed(() => vm.formModes.delete === vm.formInfo.mode),
						isVisible: computed(() => vm.authData.isAllowed && [vm.formModes.show, vm.formModes.edit, vm.formModes.delete].includes(vm.formInfo.mode)),
						action: vm.changeToDeleteMode
					},
					changeToInsert: {
						id: 'change-to-insert-btn',
						icon: {
							icon: 'add',
							type: 'svg'
						},
						type: 'form-insert',
						text: computed(() => vm.Resources[hardcodedTexts.insert]),
						label: computed(() => vm.Resources[hardcodedTexts.insert]),
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isSelected: computed(() => vm.formModes.new === vm.formInfo.mode),
						isVisible: computed(() => vm.authData.isAllowed && vm.formModes.duplicate !== vm.formInfo.mode),
						action: vm.changeToInsertMode
					},
					repeatInsertBtn: {
						id: 'repeat-insert-btn',
						icon: {
							icon: 'save-new',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources[hardcodedTexts.repeatInsert]),
						variant: 'bold',
						showInHeader: true,
						showInFooter: true,
						isActive: false,
						isVisible: computed(() => vm.authData.isAllowed && vm.formInfo.mode === vm.formModes.new),
						action: () => vm.saveForm(true)
					},
					saveBtn: {
						id: 'save-btn',
						icon: {
							icon: 'save',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources.GRAVAR45301),
						variant: 'bold',
						showInHeader: true,
						showInFooter: true,
						isActive: true,
						isVisible: computed(() => vm.authData.isAllowed && vm.isEditable),
						action: vm.saveForm
					},
					confirmBtn: {
						id: 'confirm-btn',
						icon: {
							icon: 'check',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources[vm.isNested ? hardcodedTexts.delete : hardcodedTexts.confirm]),
						variant: 'bold',
						showInHeader: true,
						showInFooter: true,
						isActive: true,
						isVisible: computed(() => vm.authData.isAllowed && (vm.formInfo.mode === vm.formModes.delete || vm.isNested)),
						action: vm.deleteRecord
					},
					cancelBtn: {
						id: 'cancel-btn',
						icon: {
							icon: 'cancel',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources.CANCELAR49513),
						showInHeader: true,
						showInFooter: true,
						isActive: true,
						isVisible: computed(() => vm.authData.isAllowed && vm.isEditable),
						action: vm.leaveForm
					},
					resetCancelBtn: {
						id: 'reset-cancel-btn',
						icon: {
							icon: 'cancel',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources[hardcodedTexts.cancel]),
						showInHeader: true,
						showInFooter: true,
						isActive: false,
						isVisible: computed(() => vm.authData.isAllowed && vm.isEditable),
						action: () => vm.model.resetValues(),
						emitAction: {
							name: 'deselect',
							params: {}
						}
					},
					editBtn: {
						id: 'edit-btn',
						icon: {
							icon: 'pencil',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources[hardcodedTexts.edit]),
						variant: 'bold',
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isVisible: computed(() => vm.authData.isAllowed && vm.parentFormMode !== vm.formModes.show && vm.parentFormMode !== vm.formModes.delete),
						action: () => {},
						emitAction: {
							name: 'edit',
							params: {}
						}
					},
					deleteQuickBtn: {
						id: 'delete-btn',
						icon: {
							icon: 'bin',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.Resources[hardcodedTexts.delete]),
						variant: 'bold',
						showInHeader: true,
						showInFooter: false,
						isActive: false,
						isVisible: computed(() => vm.authData.isAllowed && vm.parentFormMode !== vm.formModes.show && (typeof vm.permissions.canDelete === 'boolean' ? vm.permissions.canDelete : true)),
						action: vm.deleteRecord
					},
					backBtn: {
						id: 'back-btn',
						icon: {
							icon: 'back',
							type: 'svg'
						},
						type: 'form-action',
						text: computed(() => vm.isPopup ? vm.Resources[hardcodedTexts.close] : vm.Resources[hardcodedTexts.goBack]),
						showInHeader: true,
						showInFooter: true,
						isActive: true,
						isVisible: computed(() => !vm.authData.isAllowed || !vm.isEditable),
						action: vm.leaveForm
					}
				},

				controls: {
					F_MOVIESPSEUDNEWGRP02: new fieldControlClass.GroupControl({
						id: 'F_MOVIESPSEUDNEWGRP02',
						name: 'NEWGRP02',
						size: 'block',
						label: computed(() => this.Resources.DETAILS19591),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: false,
						anchored: false,
						directChildren: ['F_MOVIESMOVIEPOSTER__', 'F_MOVIESMOVIETITLE___', 'F_MOVIESMOVIEDISCRIPT', 'F_MOVIESMOVIEMOVIEGEN', 'F_MOVIESMOVIERELDATE_'],
						mustBeFilled: true,
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIEPOSTER__: new fieldControlClass.ImageControl({
						modelField: 'ValPoster',
						valueChangeEvent: 'fieldChange:movie.poster',
						id: 'F_MOVIESMOVIEPOSTER__',
						name: 'POSTER',
						size: 'mini',
						label: computed(() => this.Resources.POSTER52933),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP02',
						height: 500,
						width: 30,
						dataTitle: computed(() => genericFunctions.formatString(vm.Resources.IMAGEM_UTILIZADA_PAR17299, vm.Resources.POSTER52933)),
						maxFileSize: 10485760, // In bytes.
						maxFileSizeLabel: '10 MB',
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIETITLE___: new fieldControlClass.StringControl({
						modelField: 'ValTitle',
						valueChangeEvent: 'fieldChange:movie.title',
						id: 'F_MOVIESMOVIETITLE___',
						name: 'TITLE',
						size: 'xxlarge',
						label: computed(() => this.Resources.TITLE21885),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP02',
						maxLength: 80,
						labelId: 'label_F_MOVIESMOVIETITLE___',
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIEDISCRIPT: new fieldControlClass.MultilineStringControl({
						modelField: 'ValDescription',
						valueChangeEvent: 'fieldChange:movie.description',
						id: 'F_MOVIESMOVIEDISCRIPT',
						name: 'DISCRIPT',
						size: 'block',
						label: computed(() => this.Resources.DISCRIPTION02169),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP02',
						rows: 3,
						cols: 200,
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIEMOVIEGEN: new fieldControlClass.RadioGroupControl({
						modelField: 'ValMoviesgenre',
						valueChangeEvent: 'fieldChange:movie.moviesgenre',
						id: 'F_MOVIESMOVIEMOVIEGEN',
						name: 'MOVIEGEN',
						label: computed(() => this.Resources.MOVIES_GENRE22042),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP02',
						maxLength: 15,
						labelId: 'label_F_MOVIESMOVIEMOVIEGEN',
						mustBeFilled: true,
						arrayName: 'MOVIEGENRE',
						columns: 0,
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIERELDATE_: new fieldControlClass.DateControl({
						modelField: 'ValRealease_date',
						valueChangeEvent: 'fieldChange:movie.realease_date',
						id: 'F_MOVIESMOVIERELDATE_',
						name: 'RELDATE',
						size: 'medium',
						label: computed(() => this.Resources.REALEASE_DATE49316),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP02',
						dateTimeType: 'date',
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDNEWGRP03: new fieldControlClass.GroupControl({
						id: 'F_MOVIESPSEUDNEWGRP03',
						name: 'NEWGRP03',
						size: 'block',
						label: computed(() => this.Resources.FEEDBACK49013),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: false,
						anchored: false,
						directChildren: ['F_MOVIESMOVIENUMBEROF', 'F_MOVIESPSEUDFIELD001', 'F_MOVIESMOVIEARATE___', 'F_MOVIESPSEUDFIELD003', 'F_MOVIESMOVIELASTRATE'],
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIENUMBEROF: new fieldControlClass.NumberControl({
						modelField: 'ValNumberoflikes',
						valueChangeEvent: 'fieldChange:movie.numberoflikes',
						id: 'F_MOVIESMOVIENUMBEROF',
						name: 'NUMBEROF',
						size: 'small',
						label: computed(() => this.Resources.NUM_LIKES09507),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP03',
						isFormulaBlocked: true,
						maxIntegers: 9,
						maxDecimals: 0,
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDFIELD001: new fieldControlClass.ButtonControl({
						id: 'F_MOVIESPSEUDFIELD001',
						name: 'FIELD001',
						hasLabel: false,
						label: computed(() => this.Resources.LIKE38722),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP03',
						// eslint-disable-next-line
						action: (event) => {
							const btnAction = () => {
								// Button to open the form "F_FAVORI" in "INS" mode.
								const params = {
									mode: vm.formModes.new,
									modes: 'vedai',
									isControlled: true,
									extraData: JSON.stringify(event)
								}

								vm.navigateToForm('F_FAVORI', vm.formModes.new, null, params)
							}
							const options = {
								form: 'F_MOVIES',
								callback: btnAction
							}
							vm.$eventHub.emit('form-apply', options)
						},
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIEARATE___: new fieldControlClass.NumberControl({
						modelField: 'ValAveragerate',
						valueChangeEvent: 'fieldChange:movie.averagerate',
						id: 'F_MOVIESMOVIEARATE___',
						name: 'ARATE',
						size: 'medium',
						label: computed(() => this.Resources.AVERAGE_RATE24140),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP03',
						isFormulaBlocked: true,
						maxIntegers: 9,
						maxDecimals: 0,
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDFIELD003: new fieldControlClass.ButtonControl({
						id: 'F_MOVIESPSEUDFIELD003',
						name: 'FIELD003',
						hasLabel: false,
						label: computed(() => this.Resources.RATE50728),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP03',
						// eslint-disable-next-line
						action: (event) => {
							const btnAction = () => {
								// Button to open the form "F_RATTIN" in "INS" mode.
								const params = {
									mode: vm.formModes.new,
									modes: 'vedai',
									isControlled: true,
									extraData: JSON.stringify(event)
								}

								vm.navigateToForm('F_RATTIN', vm.formModes.new, null, params)
							}
							const options = {
								form: 'F_MOVIES',
								callback: btnAction
							}
							vm.$eventHub.emit('form-apply', options)
						},
						controlLimits: [
						],
					}, this),
					F_MOVIESMOVIELASTRATE: new fieldControlClass.NumberControl({
						modelField: 'ValLastrate',
						valueChangeEvent: 'fieldChange:movie.lastrate',
						id: 'F_MOVIESMOVIELASTRATE',
						name: 'LASTRATE',
						size: 'small',
						label: computed(() => this.Resources.LASTRATE08537),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP03',
						isFormulaBlocked: true,
						maxIntegers: 1,
						maxDecimals: 0,
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDNEWGRP04: new fieldControlClass.GroupControl({
						id: 'F_MOVIESPSEUDNEWGRP04',
						name: 'NEWGRP04',
						size: 'block',
						label: computed(() => this.Resources.COMMENTS30895),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: true,
						anchored: false,
						directChildren: ['F_MOVIESPSEUDDATACOME', 'F_MOVIESPSEUDFIELD002'],
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDDATACOME: new fieldControlClass.TableListControl({
						id: 'F_MOVIESPSEUDDATACOME',
						name: 'DATACOME',
						size: '',
						label: computed(() => this.Resources.COMMENTS30895),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP04',
						controller: 'MOVIE',
						action: 'F_movies_ValDatacome',
						hasDependencies: false,
						isInCollapsible: true,
						columnsOriginal: [
							new listColumnTypes.TextColumn({
								order: 1,
								name: 'ValPost',
								area: 'COMME',
								field: 'POST',
								label: computed(() => this.Resources.POST24992),
								scrollData: 30,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 2,
								name: 'Userp.ValName',
								area: 'USERP',
								field: 'NAME',
								label: computed(() => this.Resources.NAME31974),
								dataLength: 50,
								scrollData: 30,
								pkColumn: 'ValCoduserp',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.DateColumn({
								order: 3,
								name: 'ValCreateat',
								area: 'COMME',
								field: 'CREATEAT',
								label: computed(() => this.Resources.CREATE_AT36393),
								scrollData: 8,
								dateTimeType: 'date',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 4,
								name: 'Movie.ValTitle',
								area: 'MOVIE',
								field: 'TITLE',
								label: computed(() => this.Resources.TITLE21885),
								dataLength: 80,
								scrollData: 30,
								pkColumn: 'ValCodmovie',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
						],
						config: {
							name: 'ValDatacome',
							serverMode: true,
							pkColumn: 'ValCodcomme',
							tableAlias: 'COMME',
							tableNamePlural: computed(() => this.Resources.COMMENTS30895),
							viewManagement: '',
							showLimitsInfo: true,
							tableTitle: computed(() => this.Resources.COMMENTS30895),
							showAlternatePagination: true,
							permissions: {
							},
							searchBarConfig: {
								visibility: false,
								searchOnPressEnter: true
							},
							filtersVisible: false,
							allowColumnFilters: false,
							allowColumnSort: true,
							generalCustomActions: [
							],
							groupActions: [
							],
							customActions: [
							],
							MCActions: [
							],
							rowClickAction: {
							},
							formsDefinition: {
							},
							defaultSearchColumnName: 'Movie.ValTitle',
							defaultSearchColumnNameOriginal: 'Movie.ValTitle',
							defaultColumnSorting: {
								columnName: '',
								sortOrder: 'asc'
							}
						},
						globalEvents: ['changed-COMME', 'changed-USERP', 'changed-MOVIE'],
						uuid: 'F_movies_ValDatacome',
						allSelectedRows: 'false',
						controlLimits: [
							{
								identifier: ['id', 'movie'],
								dependencyEvents: ['fieldChange:movie.codmovie'],
								dependencyField: 'MOVIE.CODMOVIE',
								fnValueSelector: (model) => model.ValCodmovie.value
							},
						],
					}, this),
					F_MOVIESPSEUDFIELD002: new fieldControlClass.ButtonControl({
						id: 'F_MOVIESPSEUDFIELD002',
						name: 'FIELD002',
						hasLabel: false,
						label: computed(() => this.Resources.COMMENT01969),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP04',
						// eslint-disable-next-line
						action: (event) => {
							const btnAction = () => {
								// Button to open the form "F_COMMEN" in "INS" mode.
								const params = {
									mode: vm.formModes.new,
									modes: 'vedai',
									isControlled: true,
									extraData: JSON.stringify(event)
								}

								vm.navigateToForm('F_COMMEN', vm.formModes.new, null, params)
							}
							const options = {
								form: 'F_MOVIES',
								callback: btnAction
							}
							vm.$eventHub.emit('form-apply', options)
						},
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDNEWGRP01: new fieldControlClass.GroupControl({
						id: 'F_MOVIESPSEUDNEWGRP01',
						name: 'NEWGRP01',
						size: 'block',
						label: computed(() => this.Resources.CHARACTERS03735),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: true,
						anchored: false,
						directChildren: ['F_MOVIESPSEUDDATACHAR'],
						controlLimits: [
						],
					}, this),
					F_MOVIESPSEUDDATACHAR: new fieldControlClass.TableSpecialRenderingControl({
						id: 'F_MOVIESPSEUDDATACHAR',
						name: 'DATACHAR',
						size: 'block',
						label: computed(() => this.Resources.CHARACTERS03735),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVIESPSEUDNEWGRP01',
						controller: 'MOVIE',
						action: 'F_movies_ValDatachar',
						hasDependencies: false,
						isInCollapsible: true,
						columnsOriginal: [
							new listColumnTypes.ArrayColumn({
								order: 1,
								name: 'ValGenre',
								area: 'CHARA',
								field: 'GENRE',
								label: computed(() => this.Resources.GENRE63303),
								dataLength: 1,
								scrollData: 1,
								array: computed(() => new qProjArrays.QArrayGenre(vm.$getResource).elements),
								arrayType: qProjArrays.QArrayGenre.type,
								arrayDisplayMode: 'D',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 2,
								name: 'Movie.ValTitle',
								area: 'MOVIE',
								field: 'TITLE',
								label: computed(() => this.Resources.TITLE21885),
								dataLength: 80,
								scrollData: 30,
								pkColumn: 'ValCodmovie',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.DateColumn({
								order: 3,
								name: 'ValCreateat',
								area: 'CHARA',
								field: 'CREATEAT',
								label: computed(() => this.Resources.CREATE_AT36393),
								scrollData: 8,
								dateTimeType: 'date',
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 4,
								name: 'ValName',
								area: 'CHARA',
								field: 'NAME',
								label: computed(() => this.Resources.NAME31974),
								dataLength: 80,
								scrollData: 30,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.TextColumn({
								order: 5,
								name: 'ValActorname',
								area: 'CHARA',
								field: 'ACTORNAME',
								label: computed(() => this.Resources.ACTOR_NAME48089),
								dataLength: 80,
								scrollData: 30,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
							new listColumnTypes.ImageColumn({
								order: 6,
								name: 'ValPhoto',
								area: 'CHARA',
								field: 'PHOTO',
								label: computed(() => this.Resources.PHOTO51874),
								dataTitle: computed(() => genericFunctions.formatString(vm.Resources.IMAGEM_UTILIZADA_PAR58591, vm.Resources.PHOTO51874)),
								scrollData: 3,
								isVisible: false,
								sortable: false,
								searchable: false,
							}, computed(() => vm.model), computed(() => vm.internalEvents)),
						],
						config: {
							name: 'ValDatachar',
							serverMode: true,
							pkColumn: 'ValCodchara',
							tableAlias: 'CHARA',
							tableNamePlural: computed(() => this.Resources.CHARACTERS03735),
							viewManagement: '',
							showLimitsInfo: true,
							tableTitle: computed(() => this.Resources.CHARACTERS03735),
							showAlternatePagination: true,
							permissions: {
							},
							searchBarConfig: {
								visibility: false,
								searchOnPressEnter: true
							},
							filtersVisible: false,
							allowColumnFilters: false,
							allowColumnSort: true,
							generalCustomActions: [
							],
							groupActions: [
							],
							customActions: [
							],
							MCActions: [
							],
							rowClickAction: {
							},
							formsDefinition: {
							},
							defaultSearchColumnName: 'ValName',
							defaultSearchColumnNameOriginal: 'ValName',
							defaultColumnSorting: {
								columnName: '',
								sortOrder: 'asc'
							}
						},
						globalEvents: ['changed-MOVIE', 'changed-CHARA'],
						uuid: 'F_movies_ValDatachar',
						allSelectedRows: 'false',
						viewModes: [
							{
								id: 'CARDS',
								type: 'cards',
								subtype: 'card-horizontal',
								label: computed(() => this.Resources.CARTOES27587),
								order: 1,
								mappingVariables: readonly({
									title: {
										allowsMultiple: false,
										sources: [
											'CHARA.NAME',
										]
									},
									subtitle: {
										allowsMultiple: false,
										sources: [
											'CHARA.GENRE',
										]
									},
									image: {
										allowsMultiple: false,
										sources: [
											'CHARA.PHOTO',
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
						controlLimits: [
							{
								identifier: ['id', 'movie'],
								dependencyEvents: ['fieldChange:movie.codmovie'],
								dependencyField: 'MOVIE.CODMOVIE',
								fnValueSelector: (model) => model.ValCodmovie.value
							},
						],
					}, this),
				},

				model: new FormViewModel(this, {
					callbacks: {
						onUpdate: this.onUpdate,
						setFormKey: this.setFormKey
					}
				}),

				groupFields: readonly([
					'F_MOVIESPSEUDNEWGRP02',
					'F_MOVIESPSEUDNEWGRP03',
					'F_MOVIESPSEUDNEWGRP04',
					'F_MOVIESPSEUDNEWGRP01',
				]),

				tableFields: readonly([
					'F_MOVIESPSEUDDATACOME',
					'F_MOVIESPSEUDDATACHAR',
				]),

				timelineFields: readonly([
				]),

				/**
				 * The Data API for easy access to model variables.
				 */
				dataApi: {
					Movie: {
						get ValAveragerate() { return vm.model.ValAveragerate.value },
						set ValAveragerate(value) { vm.model.ValAveragerate.updateValue(value) },
						get ValBackdrop() { return vm.model.ValBackdrop.value },
						set ValBackdrop(value) { vm.model.ValBackdrop.updateValue(value) },
						get ValCreateat() { return vm.model.ValCreateat.value },
						set ValCreateat(value) { vm.model.ValCreateat.updateValue(value) },
						get ValDescription() { return vm.model.ValDescription.value },
						set ValDescription(value) { vm.model.ValDescription.updateValue(value) },
						get ValLastrate() { return vm.model.ValLastrate.value },
						set ValLastrate(value) { vm.model.ValLastrate.updateValue(value) },
						get ValMoviesgenre() { return vm.model.ValMoviesgenre.value },
						set ValMoviesgenre(value) { vm.model.ValMoviesgenre.updateValue(value) },
						get ValNumberoflikes() { return vm.model.ValNumberoflikes.value },
						set ValNumberoflikes(value) { vm.model.ValNumberoflikes.updateValue(value) },
						get ValPoster() { return vm.model.ValPoster.value },
						set ValPoster(value) { vm.model.ValPoster.updateValue(value) },
						get ValRealease_date() { return vm.model.ValRealease_date.value },
						set ValRealease_date(value) { vm.model.ValRealease_date.updateValue(value) },
						get ValSumavg() { return vm.model.ValSumavg.value },
						set ValSumavg(value) { vm.model.ValSumavg.updateValue(value) },
						get ValTitle() { return vm.model.ValTitle.value },
						set ValTitle(value) { vm.model.ValTitle.updateValue(value) },
						get ValTotalrate() { return vm.model.ValTotalrate.value },
						set ValTotalrate(value) { vm.model.ValTotalrate.updateValue(value) },
					},
					keys: {
						/** The primary key of the MOVIE table */
						get movie() { return vm.model.ValCodmovie },
					},
					get extraProperties() { return vm.model.extraProperties },
				},
			}
		},

		beforeRouteEnter(to, _, next)
		{
			// Called before the route that renders this component is confirmed.
			// Does NOT have access to `this` component instance, because
			// it has not been created yet when this guard is called!

			next((vm) => {
				vm.initFormProperties(to)
			})
		},

		beforeRouteLeave(to, _, next)
		{
			if (to.params.isControlled === 'true')
			{
				genericFunctions.setNavigationState(false)
				next()
			}
			else
				this.cancel(next)
		},

		beforeRouteUpdate(to, _, next)
		{
			if (to.params.isControlled === 'true')
				next()
			else
				this.cancel(next)
		},

		mounted()
		{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_CODEJS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		beforeUnmount()
		{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV COMPONENT_BEFORE_UNMOUNT F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		methods: {
			/**
			 * Called before form init.
			 */
			async beforeLoad()
			{
				// Execute the "Before init" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.beforeInit)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('before-load-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV BEFORE_LOAD_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return true
			},

			/**
			 * Called after form init.
			 */
			async afterLoad()
			{
				// Execute the "After init" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.afterInit)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('after-load-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_LOADED_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
			},

			/**
			 * Called before an apply action is performed.
			 */
			async beforeApply()
			{
				let applyForm = true // Set to 'false' to cancel form apply.

				// Execute the "Before apply" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.beforeApply)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				const canSetDocums = await this.model.updateFilesTickets(true)

				if (canSetDocums)
				{
					applyForm = await this.model.setDocumentChanges()

					if (applyForm)
					{
						const results = await this.model.saveDocuments()
						applyForm = results.every((e) => e === true)
					}
				}

				this.emitEvent('before-apply-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV BEFORE_APPLY_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return applyForm
			},

			/**
			 * Called after an apply action is performed.
			 */
			async afterApply()
			{
				// Execute the "After apply" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.afterApply)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('after-apply-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV AFTER_APPLY_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
			},

			/**
			 * Called before the record is saved.
			 */
			async beforeSave()
			{
				let saveForm = true // Set to 'false' to cancel form saving.

				// Execute the "Before save" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.beforeSave)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				const canSetDocums = await this.model.updateFilesTickets()

				if (canSetDocums)
				{
					saveForm = await this.model.setDocumentChanges()

					if (saveForm)
					{
						const results = await this.model.saveDocuments()
						saveForm = results.every((e) => e === true)
					}
				}

				this.emitEvent('before-save-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV BEFORE_SAVE_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return saveForm
			},

			/**
			 * Called after the record is saved.
			 */
			async afterSave()
			{
				// Execute the "After save" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.afterSave)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('after-save-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV AFTER_SAVE_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return true
			},

			/**
			 * Called before the record is deleted.
			 */
			async beforeDel()
			{
				this.emitEvent('before-delete-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV BEFORE_DEL_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return true
			},

			/**
			 * Called after the record is deleted.
			 */
			async afterDel()
			{
				this.emitEvent('after-delete-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV AFTER_DEL_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return true
			},

			/**
			 * Called before leaving the form.
			 */
			async beforeExit()
			{
				// Execute the "Before exit" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.beforeExit)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('before-exit-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV BEFORE_EXIT_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				return true
			},

			/**
			 * Called after leaving the form.
			 */
			async afterExit()
			{
				// Execute the "After exit" triggers.
				const triggers = this.getTriggers(qEnums.triggerEvents.afterExit)
				for (const trigger of triggers)
					await formFunctions.executeTriggerAction(trigger)

				this.emitEvent('after-exit-form')

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV AFTER_EXIT_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
			},

			/**
			 * Called whenever a field's value is updated.
			 * @param {string} fieldName The name of the field in the format [table].[field] (ex: 'person.name')
			 * @param {object} fieldObject The object representing the field in the model
			 * @param {any} fieldValue The value of the field
			 * @param {any} oldFieldValue The previous value of the field
			 */
			// eslint-disable-next-line
			onUpdate(fieldName, fieldObject, fieldValue, oldFieldValue)
			{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV DLGUPDT F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				this.afterFieldUpdate(fieldName, fieldObject)
			},

			/**
			 * Called whenever a field is unfocused.
			 * @param {*} fieldObject The object representing the field in the model
			 * @param {*} fieldValue The value of the field
			 */
			// eslint-disable-next-line
			onBlur(fieldObject, fieldValue)
			{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV CTRLBLR F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				this.afterFieldUnfocus(fieldObject, fieldValue)
			},

			/**
			 * Called whenever a control's value is updated.
			 * @param {string} controlField The name of the field in the controls that will be updated
			 * @param {object} control The object representing the field in the controls
			 * @param {any} fieldValue The value of the field
			 */
			// eslint-disable-next-line
			onControlUpdate(controlField, control, fieldValue)
			{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV CTRLUPD F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				this.afterControlUpdate(controlField, fieldValue)
			},

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FUNCTIONS_JS F_MOVIES]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		watch: {
		}
	}
</script>
