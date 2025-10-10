<template>
	<q-checkbox
		:model-value="model"
		:id="rowKey !== undefined ? `${tableName}_${rowKey}` : `${tableName}_all`"
		:disabled="disabled"
		:readonly="readonly"
		:title="title"
		:data-table-action-selected="rowKey ? false : null"
		tabindex="-1"
		@mousedown.stop.prevent
		@click="onSelect"
		@update:model-value="onValueChanged" />
</template>

<script setup lang="ts">
	import { ref, watch } from 'vue'

	/**
	 * Typed props for QTableChecklistCheckbox
	 */
	type QTableChecklistCheckboxProps = {
		/** The current value or state of the checkbox, indicating whether it's checked (true) or unchecked (false). */
		value?: boolean

		/** A unique identifier or name associated with the parent table of the checkbox. */
		tableName: string

		/** The key or identifier for the specific row in the table. If not provided, the checkbox is placed on the header. */
		rowKey?: string | number

		/** Indicates whether the table is in a read-only state, which will affect the checkbox's interactivity. */
		readonly?: boolean

		/** A flag indicating whether the checkbox should be manually disabled, independent of the table's read-only state. */
		disabled?: boolean

		/** Text for the title attribute. */
		title?: string
	}

	const props = defineProps<QTableChecklistCheckboxProps>()

	const emit = defineEmits<{
		(e: 'toggle-row-selected'): void
	}>()

	const model = ref<boolean>(props.value ?? false)

	function onSelect() {
		if (props.rowKey !== undefined) {
			emit('toggle-row-selected')
		}
	}

	/**
	 * Handler for value change.
	 * Ensures header "select all" checkbox stays false after click.
	 */
	function onValueChanged(value: boolean) {
		model.value = props.rowKey !== undefined ? value : false
	}

	watch(
		() => props.value,
		newVal => {
			if (newVal !== undefined) {
				onValueChanged(newVal)
			}
		}
	)
</script>
