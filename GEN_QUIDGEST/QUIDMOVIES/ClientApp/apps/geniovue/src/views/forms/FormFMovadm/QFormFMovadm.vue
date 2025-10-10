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
			data-key="F_MOVADM"
			:data-loading="!formInitialDataLoaded">
			<template v-if="formControl.initialized && showFormBody">
				<q-row-container
					v-if="controls.F_MOVADMPSEUDNEWGRP01.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVADMPSEUDNEWGRP01.isVisible"
						class="row-line-group">
						<q-group-box-container
							v-if="controls.F_MOVADMPSEUDNEWGRP01.isVisible"
							id="F_MOVADMPSEUDNEWGRP01"
							v-bind="controls.F_MOVADMPSEUDNEWGRP01"
							:is-visible="controls.F_MOVADMPSEUDNEWGRP01.isVisible">
							<!-- Start F_MOVADMPSEUDNEWGRP01 -->
							<q-row-container v-if="controls.F_MOVADMMOVIEPOSTER__.isVisible || controls.F_MOVADMMOVIEBACKDROP.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIEPOSTER__.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIEPOSTER__.isVisible"
										class="q-image"
										v-bind="controls.F_MOVADMMOVIEPOSTER__"
										v-on="controls.F_MOVADMMOVIEPOSTER__.handlers"
										:loading="controls.F_MOVADMMOVIEPOSTER__.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-image
											v-if="controls.F_MOVADMMOVIEPOSTER__.isVisible"
											v-bind="controls.F_MOVADMMOVIEPOSTER__.props"
											v-on="controls.F_MOVADMMOVIEPOSTER__.handlers" />
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIEBACKDROP.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIEBACKDROP.isVisible"
										class="q-image"
										v-bind="controls.F_MOVADMMOVIEBACKDROP"
										v-on="controls.F_MOVADMMOVIEBACKDROP.handlers"
										:loading="controls.F_MOVADMMOVIEBACKDROP.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-image
											v-if="controls.F_MOVADMMOVIEBACKDROP.isVisible"
											v-bind="controls.F_MOVADMMOVIEBACKDROP.props"
											v-on="controls.F_MOVADMMOVIEBACKDROP.handlers" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVADMMOVIETITLE___.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIETITLE___.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIETITLE___.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIETITLE___"
										v-on="controls.F_MOVADMMOVIETITLE___.handlers"
										:loading="controls.F_MOVADMMOVIETITLE___.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-text-field
											v-bind="controls.F_MOVADMMOVIETITLE___.props"
											@blur="onBlur(controls.F_MOVADMMOVIETITLE___, model.ValTitle.value)"
											@change="model.ValTitle.fnUpdateValueOnChange" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVADMMOVIERELDATE_.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIERELDATE_.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIERELDATE_.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIERELDATE_"
										v-on="controls.F_MOVADMMOVIERELDATE_.handlers"
										:loading="controls.F_MOVADMMOVIERELDATE_.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-date-time-picker
											v-if="controls.F_MOVADMMOVIERELDATE_.isVisible"
											v-bind="controls.F_MOVADMMOVIERELDATE_.props"
											:model-value="model.ValRealease_date.value"
											@reset-icon-click="model.ValRealease_date.fnUpdateValue(model.ValRealease_date.originalValue ?? new Date())"
											@update:model-value="model.ValRealease_date.fnUpdateValue($event ?? '')" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVADMMOVIECREATEAT.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIECREATEAT.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIECREATEAT.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIECREATEAT"
										v-on="controls.F_MOVADMMOVIECREATEAT.handlers"
										:loading="controls.F_MOVADMMOVIECREATEAT.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-date-time-picker
											v-if="controls.F_MOVADMMOVIECREATEAT.isVisible"
											v-bind="controls.F_MOVADMMOVIECREATEAT.props"
											:model-value="model.ValCreateat.value"
											@reset-icon-click="model.ValCreateat.fnUpdateValue(model.ValCreateat.originalValue ?? new Date())"
											@update:model-value="model.ValCreateat.fnUpdateValue($event ?? '')" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVADMMOVIEMOVIEGEN.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIEMOVIEGEN.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIEMOVIEGEN.isVisible"
										class="i-radio-container"
										v-bind="controls.F_MOVADMMOVIEMOVIEGEN"
										v-on="controls.F_MOVADMMOVIEMOVIEGEN.handlers"
										:label-position="labelAlignment.topleft"
										:loading="controls.F_MOVADMMOVIEMOVIEGEN.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-radio-group
											v-if="controls.F_MOVADMMOVIEMOVIEGEN.isVisible"
											v-bind="controls.F_MOVADMMOVIEMOVIEGEN.props"
											v-on="controls.F_MOVADMMOVIEMOVIEGEN.handlers">
											<q-radio-button
												v-for="radio in controls.F_MOVADMMOVIEMOVIEGEN.items"
												:key="radio.key"
												:label="radio.value"
												:value="radio.key" />
										</q-radio-group>
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<q-row-container v-if="controls.F_MOVADMMOVIEDISCRIPT.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIEDISCRIPT.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIEDISCRIPT.isVisible"
										class="i-textarea"
										v-bind="controls.F_MOVADMMOVIEDISCRIPT"
										v-on="controls.F_MOVADMMOVIEDISCRIPT.handlers"
										:loading="controls.F_MOVADMMOVIEDISCRIPT.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-text-area
											v-if="controls.F_MOVADMMOVIEDISCRIPT.isVisible"
											v-bind="controls.F_MOVADMMOVIEDISCRIPT.props"
											v-on="controls.F_MOVADMMOVIEDISCRIPT.handlers" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVADMPSEUDNEWGRP01 -->
						</q-group-box-container>
					</q-control-wrapper>
				</q-row-container>
				<q-row-container
					v-if="controls.F_MOVADMPSEUDNEWGRP02.isVisible"
					is-large>
					<q-control-wrapper
						v-if="controls.F_MOVADMPSEUDNEWGRP02.isVisible"
						class="row-line-group">
						<q-group-collapsible
							v-if="controls.F_MOVADMPSEUDNEWGRP02.isVisible"
							id="F_MOVADMPSEUDNEWGRP02"
							v-bind="controls.F_MOVADMPSEUDNEWGRP02"
							v-on="controls.F_MOVADMPSEUDNEWGRP02.handlers">
							<!-- Start F_MOVADMPSEUDNEWGRP02 -->
							<q-row-container v-if="controls.F_MOVADMMOVIENUMBEROF.isVisible || controls.F_MOVADMMOVIELASTRATE.isVisible || controls.F_MOVADMMOVIEARATE___.isVisible">
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIENUMBEROF.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIENUMBEROF.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIENUMBEROF"
										v-on="controls.F_MOVADMMOVIENUMBEROF.handlers"
										:loading="controls.F_MOVADMMOVIENUMBEROF.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVADMMOVIENUMBEROF.isVisible"
											v-bind="controls.F_MOVADMMOVIENUMBEROF.props"
											@update:model-value="model.ValNumberoflikes.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIELASTRATE.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIELASTRATE.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIELASTRATE"
										v-on="controls.F_MOVADMMOVIELASTRATE.handlers"
										:loading="controls.F_MOVADMMOVIELASTRATE.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVADMMOVIELASTRATE.isVisible"
											v-bind="controls.F_MOVADMMOVIELASTRATE.props"
											@update:model-value="model.ValLastrate.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
								<q-control-wrapper
									v-if="controls.F_MOVADMMOVIEARATE___.isVisible"
									class="control-join-group">
									<base-input-structure
										v-if="controls.F_MOVADMMOVIEARATE___.isVisible"
										class="i-text"
										v-bind="controls.F_MOVADMMOVIEARATE___"
										v-on="controls.F_MOVADMMOVIEARATE___.handlers"
										:loading="controls.F_MOVADMMOVIEARATE___.props.loading"
										:reporting-mode-on="reportingModeCAV"
										:suggestion-mode-on="suggestionModeOn">
										<q-numeric-input
											v-if="controls.F_MOVADMMOVIEARATE___.isVisible"
											v-bind="controls.F_MOVADMMOVIEARATE___.props"
											@update:model-value="model.ValAveragerate.fnUpdateValue" />
									</base-input-structure>
								</q-control-wrapper>
							</q-row-container>
							<!-- End F_MOVADMPSEUDNEWGRP02 -->
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

	import FormViewModel from './QFormFMovadmViewModel.js'

	const requiredTextResources = ['QFormFMovadm', 'hardcoded', 'messages']

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FORM_INCLUDEJS F_MOVADM]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

	export default {
		name: 'QFormFMovadm',

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
					name: 'F_MOVADM',
					location: 'form-F_MOVADM',
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
				componentOnLoadProc: asyncProcM.getProcListMonitor('QFormFMovadm', false),

				interfaceMetadata: {
					id: 'QFormFMovadm', // Used for resources
					requiredTextResources
				},

				formInfo: {
					type: 'normal',
					name: 'F_MOVADM',
					route: 'form-F_MOVADM',
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
					F_MOVADMPSEUDNEWGRP01: new fieldControlClass.GroupControl({
						id: 'F_MOVADMPSEUDNEWGRP01',
						name: 'NEWGRP01',
						size: 'block',
						label: computed(() => this.Resources.CONTENT24069),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: false,
						anchored: false,
						directChildren: ['F_MOVADMMOVIEPOSTER__', 'F_MOVADMMOVIEBACKDROP', 'F_MOVADMMOVIETITLE___', 'F_MOVADMMOVIERELDATE_', 'F_MOVADMMOVIECREATEAT', 'F_MOVADMMOVIEMOVIEGEN', 'F_MOVADMMOVIEDISCRIPT'],
						mustBeFilled: true,
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIEPOSTER__: new fieldControlClass.ImageControl({
						modelField: 'ValPoster',
						valueChangeEvent: 'fieldChange:movie.poster',
						id: 'F_MOVADMMOVIEPOSTER__',
						name: 'POSTER',
						size: 'mini',
						label: computed(() => this.Resources.POSTER52933),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						height: 50,
						width: 30,
						dataTitle: computed(() => genericFunctions.formatString(vm.Resources.IMAGEM_UTILIZADA_PAR17299, vm.Resources.POSTER52933)),
						maxFileSize: 10485760, // In bytes.
						maxFileSizeLabel: '10 MB',
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIEBACKDROP: new fieldControlClass.ImageControl({
						modelField: 'ValBackdrop',
						valueChangeEvent: 'fieldChange:movie.backdrop',
						id: 'F_MOVADMMOVIEBACKDROP',
						name: 'BACKDROP',
						size: 'mini',
						label: computed(() => this.Resources.BACKDROP05167),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						height: 50,
						width: 30,
						dataTitle: computed(() => genericFunctions.formatString(vm.Resources.IMAGEM_UTILIZADA_PAR17299, vm.Resources.BACKDROP05167)),
						maxFileSize: 10485760, // In bytes.
						maxFileSizeLabel: '10 MB',
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIETITLE___: new fieldControlClass.StringControl({
						modelField: 'ValTitle',
						valueChangeEvent: 'fieldChange:movie.title',
						id: 'F_MOVADMMOVIETITLE___',
						name: 'TITLE',
						size: 'xxlarge',
						label: computed(() => this.Resources.TITLE21885),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						maxLength: 80,
						labelId: 'label_F_MOVADMMOVIETITLE___',
						mustBeFilled: true,
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIERELDATE_: new fieldControlClass.DateControl({
						modelField: 'ValRealease_date',
						valueChangeEvent: 'fieldChange:movie.realease_date',
						id: 'F_MOVADMMOVIERELDATE_',
						name: 'RELDATE',
						size: 'medium',
						label: computed(() => this.Resources.REALEASE_DATE49316),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						dateTimeType: 'date',
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIECREATEAT: new fieldControlClass.DateControl({
						modelField: 'ValCreateat',
						valueChangeEvent: 'fieldChange:movie.createat',
						id: 'F_MOVADMMOVIECREATEAT',
						name: 'CREATEAT',
						size: 'small',
						label: computed(() => this.Resources.CREATE_AT36393),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						dateTimeType: 'date',
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIEMOVIEGEN: new fieldControlClass.RadioGroupControl({
						modelField: 'ValMoviesgenre',
						valueChangeEvent: 'fieldChange:movie.moviesgenre',
						id: 'F_MOVADMMOVIEMOVIEGEN',
						name: 'MOVIEGEN',
						label: computed(() => this.Resources.MOVIES_GENRE22042),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						maxLength: 15,
						labelId: 'label_F_MOVADMMOVIEMOVIEGEN',
						mustBeFilled: true,
						arrayName: 'MOVIEGENRE',
						columns: 0,
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIEDISCRIPT: new fieldControlClass.MultilineStringControl({
						modelField: 'ValDescription',
						valueChangeEvent: 'fieldChange:movie.description',
						id: 'F_MOVADMMOVIEDISCRIPT',
						name: 'DISCRIPT',
						size: 'xxlarge',
						label: computed(() => this.Resources.DISCRIPTION02169),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP01',
						rows: 3,
						cols: 200,
						controlLimits: [
						],
					}, this),
					F_MOVADMPSEUDNEWGRP02: new fieldControlClass.GroupControl({
						id: 'F_MOVADMPSEUDNEWGRP02',
						name: 'NEWGRP02',
						size: 'block',
						label: computed(() => this.Resources.FEEDBACK_ZONE21601),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						isCollapsible: true,
						anchored: false,
						directChildren: ['F_MOVADMMOVIENUMBEROF', 'F_MOVADMMOVIELASTRATE', 'F_MOVADMMOVIEARATE___'],
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIENUMBEROF: new fieldControlClass.NumberControl({
						modelField: 'ValNumberoflikes',
						valueChangeEvent: 'fieldChange:movie.numberoflikes',
						id: 'F_MOVADMMOVIENUMBEROF',
						name: 'NUMBEROF',
						size: 'medium',
						label: computed(() => this.Resources.NUMBEROFLIKES65014),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP02',
						isFormulaBlocked: true,
						maxIntegers: 9,
						maxDecimals: 0,
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIELASTRATE: new fieldControlClass.NumberControl({
						modelField: 'ValLastrate',
						valueChangeEvent: 'fieldChange:movie.lastrate',
						id: 'F_MOVADMMOVIELASTRATE',
						name: 'LASTRATE',
						size: 'small',
						label: computed(() => this.Resources.LASTRATE08537),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP02',
						isFormulaBlocked: true,
						maxIntegers: 1,
						maxDecimals: 0,
						controlLimits: [
						],
					}, this),
					F_MOVADMMOVIEARATE___: new fieldControlClass.NumberControl({
						modelField: 'ValAveragerate',
						valueChangeEvent: 'fieldChange:movie.averagerate',
						id: 'F_MOVADMMOVIEARATE___',
						name: 'ARATE',
						size: 'medium',
						label: computed(() => this.Resources.AVERAGE_RATE24140),
						placeholder: '',
						labelPosition: computed(() => this.labelAlignment.topleft),
						container: 'F_MOVADMPSEUDNEWGRP02',
						isFormulaBlocked: true,
						maxIntegers: 6,
						maxDecimals: 2,
						controlLimits: [
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
					'F_MOVADMPSEUDNEWGRP01',
					'F_MOVADMPSEUDNEWGRP02',
				]),

				tableFields: readonly([
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
// USE /[MANUAL MOV FORM_CODEJS F_MOVADM]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		beforeUnmount()
		{
/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV COMPONENT_BEFORE_UNMOUNT F_MOVADM]/
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
// USE /[MANUAL MOV BEFORE_LOAD_JS F_MOVADM]/
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
// USE /[MANUAL MOV FORM_LOADED_JS F_MOVADM]/
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
// USE /[MANUAL MOV BEFORE_APPLY_JS F_MOVADM]/
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
// USE /[MANUAL MOV AFTER_APPLY_JS F_MOVADM]/
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
// USE /[MANUAL MOV BEFORE_SAVE_JS F_MOVADM]/
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
// USE /[MANUAL MOV AFTER_SAVE_JS F_MOVADM]/
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
// USE /[MANUAL MOV BEFORE_DEL_JS F_MOVADM]/
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
// USE /[MANUAL MOV AFTER_DEL_JS F_MOVADM]/
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
// USE /[MANUAL MOV BEFORE_EXIT_JS F_MOVADM]/
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
// USE /[MANUAL MOV AFTER_EXIT_JS F_MOVADM]/
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
// USE /[MANUAL MOV DLGUPDT F_MOVADM]/
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
// USE /[MANUAL MOV CTRLBLR F_MOVADM]/
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
// USE /[MANUAL MOV CTRLUPD F_MOVADM]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

				this.afterControlUpdate(controlField, fieldValue)
			},

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV FUNCTIONS_JS F_MOVADM]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */
		},

		watch: {
		}
	}
</script>
