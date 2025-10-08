<template>
	<q-action-list
		:actions="exportActions"
		:action-groups="exportActionGroups"
		:dropdown-options="dropdownOptions"
		:texts="texts"
		@click:action="$emit('export-data', $event.id)" />
</template>

<script>
	import { validateTexts } from '@quidgest/clientapp/utils/genericFunctions'

	// The texts needed by the component.
	const DEFAULT_TEXTS = {
		exportButtonTitle: 'Export',
		actionMenuTitle: 'Actions'
	}

	export default {
		name: 'QTableExport',

		emits: ['export-data'],

		props: {
			/**
			 * An object containing localized texts for the component, allowing customization of displayed strings.
			 */
			texts: {
				type: Object,
				validator: (value) => validateTexts(DEFAULT_TEXTS, value),
				default: () => DEFAULT_TEXTS
			},

			/**
			 * An array of options available for export. Each option represents a format or export type.
			 */
			options: {
				type: Array,
				default: () => []
			}
		},

		expose: [],

		computed: {
			/**
			 * Computes the default options for the dropdown
			 */
			dropdownOptions() {
				return {
					size: 'normal',
					label: this.texts.exportButtonTitle,
					icon: 'file-export',
					borderless: true,
					placement: 'bottom-start',
					class: 'q-dropdown-toggle'
				}
			},

			/**
			 * Computes the options to the dropdown
			 */
			exportActions() {
				return this.options.map((act) => ({
					id: act.id,
					title: act.text,
					group: 'export'
				}))
			},

			/**
			 * Computes the options groups for the dropdown
			 */
			exportActionGroups() {
				return [{
					id: 'export',
					display: 'dropdown',
				}]
			}
		}
	}
</script>
