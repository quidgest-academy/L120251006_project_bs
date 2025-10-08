<template>
	<div class="container profile-form">
		<q-validation-summary :messages="validationErrors" />

		<div
			id="change-2fa"
			class="nested-form">
			<fieldset v-if="componentOnLoadProc.loaded">
				<div class="form-flow profile-form">
					<div
						id="c-sticky-header"
						class="form-header">
						<h1>{{ texts.twoFactorAuth }}</h1>
					</div>

					<div class="row mb-4">
						<div class="col">
							<b>{{ texts.byApp }}</b>

							<div class="float-right">
								<q-switch
									v-bind="controls.HasTotp.props"
									v-on="controls.HasTotp.handlers" />
							</div>

							<template v-if="!model.ShowTotp.value">
								<p>{{ texts.totpUsage }}</p>

								<q-button
									:title="totpLabel"
									:label="totpLabel"
									@click="createTOTP">
									<q-icon :icon="totpIcon" />
								</q-button>
							</template>
							<template v-else>
								<div
									id="panelTOTP"
									class="row-group">
									<div class="zone-field">
										<p>{{ texts.totpStep1 }}</p>

										<ul>
											<li>
												<a
													href="https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2"
													target="_blank"
													rel="noopener noreferrer">
													Google authenticator
												</a>
											</li>

											<li>
												<a
													href="https://www.microsoft.com/pt-pt/security/mobile-authenticator-app"
													target="_blank"
													rel="noopener noreferrer">
													Microsoft authenticator
												</a>
											</li>

											<li>
												<a
													href="https://authy.com/download/"
													target="_blank"
													rel="noopener noreferrer">
													Authy
												</a>
											</li>
										</ul>

										<p>{{ texts.totpStep2 }}</p>

										<div class="i-text__error mb-2">{{ texts.saveInSecurePlace }}</div>

										<qrcode-vue
											v-if="!isEmpty(model.TotpUrl.value)"
											id="qrcode"
											:value="model.TotpUrl.value"
											render-as="svg"
											:size="150" />

										<br />

										<b>{{ texts.code }}</b>

										{{ model.TotpDisplayCode.value }}

										<p>{{ texts.totpStep3 }}</p>

										<q-row-container>
											<q-control-wrapper class="control-join-group">
												<base-input-structure
													class="i-text"
													v-bind="controls.Totp6Code">
													<q-text-field
														v-bind="controls.Totp6Code.props"
														v-on="controls.Totp6Code.handlers" />
												</base-input-structure>
											</q-control-wrapper>
										</q-row-container>
									</div>
								</div>
							</template>
						</div>

						<div class="col">
							<b>{{ texts.securityKey }}</b>

							<div class="float-right">
								<q-switch
									v-bind="controls.HasWebAuthN.props"
									v-on="controls.HasWebAuthN.handlers" />
							</div>

							<template v-if="!model.ShowWebAuthN.value">
								<p>{{ texts.securityKeyHelp }}</p>

								<q-button
									:label="texts.addSecurityKey"
									:title="texts.addSecurityKey"
									@click="createWebAuthN">
									<q-icon icon="add" />
								</q-button>
							</template>
							<template v-else>
								<p>{{ texts.addSecurityKey }}</p>

								<p>
									{{ texts.securityKeyInfo }}

									<a
										href="https://www.google.com/search?q=fido+u2f+security+key"
										target="_blank"
										rel="noopener noreferrer">
										{{ texts.securityKeyOrder }}
									</a>
								</p>

								<q-button
									:label="texts.setup"
									:title="texts.setup"
									@click="createRegisterWebAuth">
									<q-icon icon="reset-password" />
								</q-button>
							</template>
						</div>
					</div>

					<div id="footer-action-btns">
						<q-button
							variant="bold"
							:label="texts.save"
							:title="texts.save"
							@click="save">
							<q-icon icon="save" />
						</q-button>

						<q-button
							:label="texts.cancel"
							:title="texts.cancel"
							@click="cancel">
							<q-icon icon="cancel" />
						</q-button>
					</div>
				</div>
			</fieldset>
		</div>
	</div>
</template>

<script>
	import { computed } from 'vue'
	import { mapActions } from 'pinia'
	import _forEach from 'lodash-es/forEach'
	import _isEmpty from 'lodash-es/isEmpty'

	import QrcodeVue from 'qrcode.vue'

	import { useGenericDataStore } from '@quidgest/clientapp/stores'
	import asyncProcM from '@quidgest/clientapp/composables/async'
	import { messageTypes } from '@quidgest/clientapp/constants/enums'
	import { displayMessage } from '@quidgest/clientapp/utils/genericFunctions'
	import NavHandlers from '@/mixins/navHandlers.js'
	import VueNavigation from '@/mixins/vueNavigation.js'
	import modelFieldType from '@quidgest/clientapp/models/fields'
	import fieldControlClass from '@/mixins/fieldControl.js'
	import FormViewModelBase from '@/mixins/formViewModelBase.js'
	import hardcodedTexts from '@/hardcodedTexts.js'

	import { createRegisterWebAuth } from '@/plugins/quidgest.webauth.js'

	class ViewModel extends FormViewModelBase
	{
		constructor(vueContext)
		{
			super(vueContext)

			this.Totp6Code = new modelFieldType.String({
				id: 'Totp6Code',
				originId: 'Totp6Code',
				area: 'PSEUD',
				field: 'Totp6Code',
				required: true
			})

			this.HasTotp = new modelFieldType.Number({
				id: 'HasTotp',
				originId: 'HasTotp',
				area: 'PSEUD',
				field: 'HasTotp',
				maxDigits: 1
			})

			this.HasWebAuthN = new modelFieldType.Number({
				id: 'HasWebAuthN',
				originId: 'HasWebAuthN',
				area: 'PSEUD',
				field: 'HasWebAuthN',
				maxDigits: 1
			})

			this.TotpUrl = new modelFieldType.String({
				id: 'TotpUrl',
				originId: 'TotpUrl',
				area: 'PSEUD',
				field: 'TotpUrl'
			})

			this.TotpDisplayCode = new modelFieldType.String({
				id: 'TotpDisplayCode',
				originId: 'TotpDisplayCode',
				area: 'PSEUD',
				field: 'TotpDisplayCode'
			})

			this.ShowTotp = new modelFieldType.Boolean({
				id: 'ShowTotp',
				originId: 'ShowTotp',
				area: 'PSEUD',
				field: 'ShowTotp'
			})

			this.ShowWebAuthN = new modelFieldType.Boolean({
				id: 'ShowWebAuthN',
				originId: 'ShowWebAuthN',
				area: 'PSEUD',
				field: 'ShowWebAuthN'
			})
		}
	}

	export default {
		name: 'Change2FA',

		components: {
			QrcodeVue
		},

		mixins: [
			NavHandlers,
			VueNavigation
		],

		expose: [
			'navigationId'
		],

		data()
		{
			return {
				componentOnLoadProc: asyncProcM.getProcListMonitor('Profile', false),

				validationErrors: {},

				model: new ViewModel(this),

				controls: {
					HasTotp: new fieldControlClass.ArrayBooleanControl({
						id: 'HasTotp',
						modelField: 'HasTotp',
						valueChangeEvent: 'fieldChange:pseud.HasTotp',
						name: 'HasTotp',
						hasLabel: false
					}, this),

					HasWebAuthN: new fieldControlClass.ArrayBooleanControl({
						id: 'HasWebAuthN',
						modelField: 'HasWebAuthN',
						valueChangeEvent: 'fieldChange:pseud.HasWebAuthN',
						name: 'HasWebAuthN',
						hasLabel: false
					}, this),

					Totp6Code: new fieldControlClass.StringControl({
						id: 'Totp6Code',
						modelField: 'Totp6Code',
						valueChangeEvent: 'fieldChange:pseud.Totp6Code',
						name: 'Totp6Code',
						hasLabel: false,
						maxLength: 6
					}, this)
				},

				texts: {
					twoFactorAuth: computed(() => this.Resources[hardcodedTexts.twoFactorAuth]),
					byApp: computed(() => this.Resources[hardcodedTexts.byApp]),
					totpUsage: computed(() => this.Resources[hardcodedTexts.totpUsage]),
					totpStep1: computed(() => this.Resources[hardcodedTexts.totpStep1]),
					totpStep2: computed(() => this.Resources[hardcodedTexts.totpStep2]),
					totpStep3: computed(() => this.Resources[hardcodedTexts.totpStep3]),
					saveInSecurePlace: computed(() => this.Resources[hardcodedTexts.saveInSecurePlace]),
					code: computed(() => this.Resources[hardcodedTexts.code]),
					securityKey: computed(() => this.Resources[hardcodedTexts.securityKey]),
					securityKeyHelp: computed(() => this.Resources[hardcodedTexts.securityKeyHelp]),
					addSecurityKey: computed(() => this.Resources[hardcodedTexts.addSecurityKey]),
					securityKeyInfo: computed(() => this.Resources[hardcodedTexts.securityKeyInfo]),
					securityKeyOrder: computed(() => this.Resources[hardcodedTexts.securityKeyOrder]),
					setup: computed(() => this.Resources[hardcodedTexts.setup]),
					save: computed(() => this.Resources[hardcodedTexts.save]),
					cancel: computed(() => this.Resources[hardcodedTexts.cancel]),
					create: computed(() => this.Resources[hardcodedTexts.create]),
					change: computed(() => this.Resources[hardcodedTexts.change])
				}
			}
		},

		created()
		{
			this.clearHistory()

			// Load data
			this.componentOnLoadProc.addBusy(this.fetchData('Change2FA'))
			// Only after the data is loaded from the server, init captch control
			this.componentOnLoadProc.once(() => this.initFormControls(), this)
		},

		beforeUnmount()
		{
			this.destroyFormControls()
			this.componentOnLoadProc.destroy()
		},

		computed: {
			totpIcon()
			{
				return this.model.HasTotp.value === 1 ? 'reset-password' : 'add'
			},

			totpLabel()
			{
				return this.model.HasTotp.value === 1 ? this.texts.change : this.texts.create
			}
		},

		methods: {
			...mapActions(useGenericDataStore, [
				'setInfoMessage',
				'clearInfoMessages'
			]),

			isEmpty: _isEmpty,

			initFormControls()
			{
				const blockedCtrls = [
					'HasTotp',
					'HasWebAuthN'
				]

				_forEach(this.controls, (ctrl) => ctrl.init(!blockedCtrls.includes(ctrl.name)))
			},

			destroyFormControls()
			{
				_forEach(this.controls, (ctrl) => ctrl.destroy())
			},

			cancel()
			{
				this.$router.push({ name: 'main' })
			},

			save()
			{
				return this.netAPI.postData(
					'Home',
					'Change2FA',
					this.model.serverObjModel,
					async (data, response) => {
						this.showResponseUserMsg(response.data.Errors, (data ?? {}).Warnings, response.data.Message, response.data.Success)

						if (response.data.Success)
						{
							this.clearHistory()
							this.navigateToRouteName('main')
						}
						else
							this.setData(data)
					})
			},

			showResponseUserMsg(errors, warnings, message, success, keepAlert)
			{
				this.validationErrors = {}
				if (!keepAlert)
					this.clearInfoMessages()

				if (!_isEmpty(errors))
					this.validationErrors = errors
				if (typeof message === 'string')
					displayMessage(message, success === true ? 'success' : 'error')

				// If there are any warning messages, they will be displayed.
				if (typeof warnings === 'object' && Array.isArray(warnings))
				{
					warnings.forEach((w) => {
						const warningProps = {
							type: messageTypes.W,
							message: w,
							icon: 'error',
							pinned: true
						}
						this.setInfoMessage(warningProps)
					})
				}
			},

			setData(modelValue)
			{
				for (const fld in this.model)
				{
					if (this.model[fld] instanceof modelFieldType.Base)
						this.model[fld].updateValue(modelValue[fld])
					else
						this.model[fld] = modelValue[fld]
				}
			},

			fetchData(actionName)
			{
				return this.netAPI.fetchData(
					'Home',
					actionName,
					null,
					async (data, response) => {
						this.showResponseUserMsg(response.data.Errors, (data ?? {}).Warnings, response.data.Message)
						this.setData(data)
					})
			},

			createTOTP()
			{
				this.fetchData('CreateTOTP')
			},

			createWebAuthN()
			{
				this.fetchData('CreateWebAuthN')
			},

			async createRegisterWebAuth()
			{
				createRegisterWebAuth()
			}
		}
	}
</script>
