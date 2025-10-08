<template>
	<div
		ref="logonMenu"
		:class="logonClasses">
		<div class="f-login">
			<div class="f-login__container">
				<div class="f-login__background">
					<div class="f-login__brand">
						<img
							:src="`${$app.resourcesPath}f-login__brand.png?v=${$app.genio.buildVersion}`"
							:alt="texts.enter" />
						<p>{{ texts.appName }}</p>
					</div>

					<p class="q-logon-text">{{ texts.authentication }}</p>

					<div
						id="login-container"
						class="form-flow">
						<div
							v-for="value in model.AuthRedirectMethods"
							:key="value.Id">
							<q-button
								variant="bold"
								block
								class="q-button--login"
								:title="value.Description || 'Authentication'"
								:label="value.Description || value.Id"
								:loading="loading"
								@click="authRedirectButtonClick(value)" />
						</div>


						<template v-if="hasUsernameAuth">
							<hr v-if="!isEmpty(model.AuthRedirectMethods)" />

							<q-input-group
								size="block"
								:prepend-icon="{ icon: 'user' }"
								:class="{ error: userError }">
								<q-text-field
									v-model="currentUser"
									name="username"
									:placeholder="texts.user"
									@keyup.enter="executeLogon"
									@input="hideUserError" />
							</q-input-group>

							<div
								v-if="userError"
								id="user-error"
								class="i-text__error">
								<q-icon icon="exclamation-sign" />
								{{ returnMessage.UserName[0] }}
							</div>

							<q-password-input
								size="block"
								:classes="[{ error: passError }]"
								:model-value="password"
								name="password"
								:placeholder="texts.password"
								@update:model-value="updatePasswordValue"
								@keyup-enter="executeLogon"
								:readonly="!$app.layout.ShowPasswordToggle">
								<template #prepend>
									<span>
										<q-icon icon="lock" />
									</span>
								</template>
							</q-password-input>

							<div
								v-if="errorMessage"
								id="error-message"
								class="i-text__error">
								<q-icon icon="exclamation-sign" />
								{{ errorMessage }}
							</div>

							<div class="q-logon-btns-container">
								<q-button
									id="login-btn"
									block
									borderless
									class="q-button--login"
									:title="texts.enter"
									:label="texts.enter"
									:loading="loading"
									:data-loading="loading"
									@click="executeLogon" />

								<q-register-button
									v-if="allowRegistration && $app.layout.UserRegisterStyle === 'button'"
									:registration-types="$app.userRegistration.registrationTypes"
									:display-style="$app.layout.UserRegisterStyle"
									@navigate-to-register-route="navigateToRegisterRoute" />
							</div>
						</template>

						<div
							class="q-logon-links-container"
							v-if="hasPasswordRecovery || allowRegistration">
							<q-router-link
								v-if="hasPasswordRecovery && !maintenance.isActive"
								id="forgot-password"
								class="f-login__link"
								:link="{
									name: 'password-recovery',
									params: { culture: system.currentLang, system: system.currentSystem, module: system.currentModule }
								}">
								{{ texts.forgotPassword }}
							</q-router-link>

							<q-register-button
								v-if="allowRegistration && $app.layout.UserRegisterStyle === 'hyperlink'"
								:registration-types="$app.userRegistration.registrationTypes"
								:display-style="$app.layout.UserRegisterStyle" />
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { computed } from 'vue'

	import { fetchData, postData } from '@quidgest/clientapp/network'
	import { useUserDataStore } from '@quidgest/clientapp/stores'
	import { displayMessage } from '@quidgest/clientapp/utils/genericFunctions'

	import hardcodedTexts from '@/hardcodedTexts.js'
	import AuthHandlers from '@/mixins/authHandlers.js'
	import LayoutHandlers from '@/mixins/layoutHandlers.js'
	import VueNavigation from '@/mixins/vueNavigation.js'
	import { updateAFToken, updateMainConfig } from '@/utils/system'
	import { resetStoreState } from '@/utils/user'

	import QRouterLink from '@/views/shared/QRouterLink.vue'
	import QRegisterButton from '@/views/shared/RegisterButton.vue'

	export default {
		name: 'LogOn',

		emits: ['set-visibility'],

		components: {
			QRouterLink,
			QRegisterButton
		},

		mixins: [
			VueNavigation,
			LayoutHandlers,
			AuthHandlers
		],

		props: {
		},

		expose: [],

		data()
		{
			return {
				currentUser: '',

				password: '',

				returnMessage: '',

				isPasswordVisible: false,

				loading: false,

				model: {
					AuthRedirectMethods: []
				},

				texts: {
					appName: computed(() => this.Resources[hardcodedTexts.appName]),
					user: computed(() => this.Resources[hardcodedTexts.user]),
					enter: computed(() => this.Resources[hardcodedTexts.enter]),
					authentication: computed(() => this.Resources[hardcodedTexts.authentication]),
					register: computed(() => this.Resources[hardcodedTexts.register]),
					password: computed(() => this.Resources[hardcodedTexts.password]),
					forgotPassword: computed(() => this.Resources[hardcodedTexts.forgotPassword])
				}
			}
		},

		created()
		{
			fetchData(
				'Account',
				'LogOn',
				{},
				(data) => {
					this.loadedContent(data)

					if (
						!this.hasUsernameAuth &&
						this.model.AuthRedirectMethods.length === 1 &&
						this.$app.layout.LoginStyle === 'single_page' &&
						!this.allowRegistration
					)
						this.authRedirectButtonClick(this.model.AuthRedirectMethods[0])
				})
		},

		mounted()
		{
			if (
				this.$app.layout.LoginStyle === 'embeded_page' ||
				(this.isPublicRoute && !this.isFullScreenPage)
			)
				window.addEventListener('mousedown', this.hideLogon)
		},

		beforeUnmount()
		{
			if (
				this.$app.layout.LoginStyle === 'embeded_page' ||
				(this.isPublicRoute && !this.isFullScreenPage)
			)
				window.removeEventListener('mousedown', this.hideLogon)
		},

		computed: {
			passwordFieldType()
			{
				return this.isPasswordVisible ? 'text' : 'password'
			},

			eyeIcon()
			{
				return this.isPasswordVisible ? 'password-hidden' : 'view'
			},

			userError()
			{
				return this.returnMessage && this.returnMessage.UserName
			},

			passError()
			{
				return this.returnMessage && this.returnMessage.Password
			},

			hasError()
			{
				return this.returnMessage && this.returnMessage.Error
			},

			errorMessage()
			{
				if (this.passError)
					return this.returnMessage.Password[0]
				else if (this.hasError)
					return this.returnMessage.Error[0]
				return undefined
			},

			allowRegistration()
			{
				return this.$app.userRegistration.allowRegistration && this.$app.userRegistration.registrationTypes.length > 0
			},

			logonClasses()
			{
				const classes = ['d-block']

				if (
					this.$app.layout.LoginStyle === 'embeded_page' ||
					(this.isPublicRoute && !this.isFullScreenPage)
				)
				{
					classes.push('log-on-container')
					classes.push('dropdown-menu')
					classes.push('dropdown-menu-right')
					classes.push('c-user__dropdown')
				}
				classes.push(...this.authenticationClasses)

				return classes
			}
		},

		methods: {
			async executeLogon()
			{
				if (this.loading)
					return

				this.loading = true

				const params = {
					returnUrl: '',
					userName: this.currentUser,
					password: this.password
				}
				await postData('Account', 'LogOn', params, this.loginSuccess)

				this.loading = false
			},

			loginSuccess(_, response)
			{
				const responseData = response.data
				this.returnMessage = responseData.Errors

				if (responseData.Success)
				{
					if (responseData.Data.Auth2FA && !responseData.Val2FA)
					{
						if (responseData.Data.User.Auth2FATp === 'TOTP')
							this.confirmBox2FA()
						else if (responseData.Data.User.Auth2FATp === 'WebAuth')
							this.handleSignInWebAuth(responseData)
					}
					else
						this.finalizeLogin()
				}
				else if (this.password.length > 0)
					this.password = ''
			},

			/**
			 * Validate if the stored route is valid for the permissions (lang, module and year) that the user has.
			 * @param {object} route Route data
			 */
			checkIfValidRoute(route)
			{
				if (route)
				{
					const locale = route.params?.culture
					const system = route.params?.system
					const module = route.params?.module

					// Check locale.
					if (locale && !this.$app.locale.availableLocales.find((lang) => lang.language === locale))
						return false

					// Check system.
					if (system && !this.system.availableSystems.includes(system))
						return false

					// Check module.
					if (module && module !== 'Public' && !this.system.availableModules[module])
						return false

					return true
				}

				return false
			},

			/**
			 * Retrieve the route for the home page of the first available module and year
			 */
			getMainPageRoute()
			{
				const routeParams = {
					name: 'home',
					params: {
						culture: this.$app.locale.defaultLocale,
						system: this.system.defaultSystem,
						module: this.system.defaultModule
					}
				}
				return routeParams
			},

			finalizeLogin()
			{
				const userDataStore = useUserDataStore()
				const lastAttemptedRoute = userDataStore.routeAfterLogin
				// Remove previously saved
				userDataStore.setRedirectRouteAfterLogin()

				resetStoreState()

				Promise.all([
					updateAFToken(),
					updateMainConfig()
				]).then(() => {
					const userData = {
						Name: this.currentUser
					}
					this.setUserData(userData)

					/**
					 * If the last attempted route is valid, redirect to it; otherwise, go to the default route.
					 * This way, if the user attempted to open a URL that requires authentication,
					 * they will be redirected to it after a successful login.
					 */
					const routeParams = this.checkIfValidRoute(lastAttemptedRoute) ? lastAttemptedRoute : this.getMainPageRoute()
					this.$router.push(routeParams)
				})
			},

			confirmBox2FA()
			{
				const buttons = {
					confirm: {
						label: this.Resources[hardcodedTexts.confirm],
						action: (value) => {
							const params = {
								returnUrl: '',
								userName: this.currentUser,
								password: value
							}

							postData('Account', 'Authentication2FA', params, this.finalizeLogin)
						}
					},
					cancel: {
						label: this.Resources[hardcodedTexts.cancel]
					}
				}

				displayMessage(
					this.Resources[hardcodedTexts.enter6DigitCode],
					'question',
					null,
					buttons, {
						input: {
							type: 'text',
							placeholder: '000000',
							validator: (value) => value?.length !== 6 ? this.Resources[hardcodedTexts.invalidCode] : ''
						}
					})
			},

			handleSignInWebAuth()
			{
				// TODO
			},

			showPassword()
			{
				this.isPasswordVisible = true
			},

			hidePassword()
			{
				this.isPasswordVisible = false
			},

			setLogonVisibility(isVisible)
			{
				this.$emit('set-visibility', isVisible)
			},

			hideUserError()
			{
				delete this.returnMessage.UserName
			},

			hideLogon(event)
			{
				if (!this.isVisible)
					return

				const el = this.$refs.logonMenu
				const target = event.target

				if (el && el !== target && !el.contains(target))
					this.setLogonVisibility(false)
			},

			loadedContent(data)
			{
				if (this.isEmpty(data))
					return

				// Update the store data
				this.setPasswordRecovery(data.HasPasswordRecovery)
				this.setUsernameAuth(data.HasUsernameAuth)
				this.setOpenIdAuth(data.AuthRedirectMethods?.length > 0)

				this.model.AuthRedirectMethods = data.AuthRedirectMethods
			},

			navigateToRegisterRoute()
			{
				this.setLogonVisibility(false)
				
				const params = {
					id: this.$app.userRegistration.registrationTypes[0].id
				}

				this.navigateToRouteName('user-register', params)
			},

			updatePasswordValue(newVal)
			{
				// When the user starts typing hide the error message
				delete this.returnMessage.Password
				this.password = newVal

				if (this.hasError)
					delete this.returnMessage.Error
			}
		}
	}
</script>
