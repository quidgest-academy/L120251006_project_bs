<template>
	<div
		:class="['layout-container', ...layoutClasses, ...customClasses]"
		:data-loading="loading">
		<slot name="layout-loading-effect"></slot>

		<div
			v-if="this.maintenance.isScheduled || this.maintenance.isActive"
			class="q-maintenance-container">
			<span>
				<q-icon icon="alert" />
				{{ maintenanceMessage }}
			</span>
		</div>

		<div
			v-if="showContent"
			id="header-container">
<!-- eslint-disable indent, vue/html-indent, vue/script-indent -->
<!-- USE /[MANUAL MOV LAYOUT_HEADER]/ -->
<!-- eslint-disable-next-line -->
<!-- eslint-enable indent, vue/html-indent, vue/script-indent -->
			<navigational-bar :loading-menus="loadingMenus" />

			<slot name="layout-header"></slot>
		</div>

<!-- eslint-disable indent, vue/html-indent, vue/script-indent -->
<!-- USE /[MANUAL MOV LAYOUT_CONTENT]/ -->
<!-- eslint-disable-next-line -->
<!-- eslint-enable indent, vue/html-indent, vue/script-indent -->
		<slot name="layout-content"></slot>
	</div>
</template>

<script>
	import { computed } from 'vue'
	import NavigationalBar from './NavigationalBar.vue'
	import LayoutHandlers from '@/mixins/layoutHandlers.js'
	import hardcodedTexts from '@/hardcodedTexts.js'

/* eslint-disable indent, vue/html-indent, vue/script-indent */
// USE /[MANUAL MOV LAYOUT_INCLUDEJS LAYOUT]/
// eslint-disable-next-line
/* eslint-enable indent, vue/html-indent, vue/script-indent */

	export default {
		name: 'QLayout',

		components: {
			NavigationalBar
		},

		mixins: [
			LayoutHandlers
		],

		inheritAttrs: false,

		data() {
			const vm = this
			return {
				texts: {
					maintenanceActive: computed(() => this.Resources[hardcodedTexts.maintenanceActive]),
					maintenanceScheduled: computed(() => this.Resources[hardcodedTexts.maintenanceScheduled].replace('{0}', vm.maintenanceDate)),
				},
			}
		},

		props: {
			/**
			 * Custom classes to apply to the layout container.
			 */
			customClasses: {
				type: Array,
				default: () => []
			},

			/**
			 * Whether there's any asynchronous process running.
			 */
			loading: {
				type: Boolean,
				default: false
			},

			/**
			 * Whether the menu structure is loading.
			 */
			loadingMenus: {
				type: Boolean,
				default: false
			}
		},

		expose: [],

		computed: {
			/**
			 * List of classes to control the behavior of the sidebar.
			 */
			layoutClasses()
			{
				const classes = ['sidebar-mini', 'layout-fixed']

				if (!this.showContent)
					classes.push('login-page')

				if (!this.sidebarIsVisible ||
					this.isFullScreenPage ||
					!this.userIsLoggedIn && !this.isPublicRoute && this.$app.layout.LoginStyle === 'single_page' ||
					Object.keys(this.system.availableModules).length === 0)
					classes.push('sidebar-closed')

				if (this.sidebarIsCollapsed)
					classes.push('sidebar-collapse')

				if (this.rightSidebarIsCollapsed)
					classes.push('right-sidebar-collapse')

				if (this.maintenance.isScheduled || this.maintenance.isActive)
					classes.push('maintenance')

				return classes
			},

			/**
			 * True if the layout content should be visible, false otherwise.
			 */
			showContent()
			{
				return !this.isFullScreenPage && (this.userIsLoggedIn || this.isPublicRoute || this.$app.layout.LoginStyle !== 'single_page')
			},

			maintenanceMessage() {
				return this.maintenance.isScheduled ? this.texts.maintenanceScheduled : this.texts.maintenanceActive
			},
		},

		watch: {
			hasMenus: {
				handler(val)
				{
					this.setHeaderHeight(50)

					if (val)
						this.expandSidebar()
					else
					{
						this.setSidebarVisibility(false)
						this.setSidebarCollapseState(true)
					}
				},
				immediate: true
			}
		}
	}
</script>
