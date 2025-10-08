<template>
	<div
		id="cav-save-query-modal"
		class="modal-fbody">
		<div class="container-fluid">
			<q-row-container>
				<q-control-wrapper class="control-join-group">
					<base-input-structure
						id="cav-save-query-name"
						:class="['i-text', { 'i-text--disabled': false }]"
						:label="texts.queryName"
						:label-attrs="{ class: 'i-text__label' }">
						<q-text-field
							id="cav-save-query-name"
							size="small"
							v-model="queryName"
							:max-length="15" />
					</base-input-structure>
				</q-control-wrapper>
			</q-row-container>

			<q-row-container>
				<q-control-wrapper class="control-join-group">
					<base-input-structure
						id="cav-save-query-access"
						:class="['i-text', { 'i-text--disabled': false }]"
						:label="texts.queryAccess"
						:label-attrs="{ class: 'i-text__label' }">
						<q-radio-group
							id="cav-save-query-access"
							v-model="accessType"
							:value="accessTypes">
							<q-radio-button
								v-for="radio in accessTypes"
								:key="radio.key"
								:value="radio.key"
								:label="radio.label" />
						</q-radio-group>
					</base-input-structure>
				</q-control-wrapper>
			</q-row-container>

			<q-row-container v-if="overrideVisible">
				<q-control-wrapper class="control-join-group">
					<q-checkbox
						id="cav-save-query-is-override"
						v-model="isOverride"
						:label="texts.overlapQuery" />
				</q-control-wrapper>
			</q-row-container>

			<q-row-container>
				<q-control-wrapper class="control-join-group">
					<q-button
						variant="bold"
						:label="texts.save"
						:title="texts.save"
						@click="saveQuery">
						<q-icon icon="save" />
					</q-button>
				</q-control-wrapper>
			</q-row-container>
		</div>
	</div>
</template>

<script>
	import { computed } from 'vue'

	import hardcodedTexts from '@/hardcodedTexts.js'
	import cavArrays from '@/api/genio/cavArrays.js'

	export default {
		name: 'QCavSaveQuery',

		emits: ['save-query'],

		props: {
			/**
			 * Holds the unique query identifier.
			 */
			queryId: {
				type: String,
				default: 'new'
			},

			/**
			 * The title or name to be given to the query when saved.
			 */
			title: {
				type: String,
				default: ''
			}
		},

		expose: [],

		data()
		{
			return {
				accessType: 'PUB',

				queryName: this.title || '',

				isOverride: false,

				accessTypes: cavArrays.accessType.setResources(this.$getResource).elements,

				texts: {
					queryName: computed(() => this.Resources[hardcodedTexts.queryName]),
					queryAccess: computed(() => this.Resources[hardcodedTexts.queryAccess]),
					overlapQuery: computed(() => this.Resources[hardcodedTexts.overlapQuery]),
					save: computed(() => this.Resources[hardcodedTexts.save])
				}
			}
		},

		computed: {
			/**
			 * Computes the currentQueryId, defaulting to 'new' if the 'queryId' prop is not provided.
			 */
			currentQueryId()
			{
				return this.queryId || 'new'
			},

			/**
			 * Determines whether the override option should be visible based on currentQueryId.
			 * If the queryId is 'new', the option to override is hidden, else it's shown.
			 */
			overrideVisible()
			{
				return this.currentQueryId !== 'new'
			}
		},

		methods: {
			/**
			 * Emits a 'save-query' event with the query data when the save button is clicked.
			 */
			saveQuery()
			{
				const data = {
					id: this.currentQueryId,
					title: this.queryName,
					queryOverride: this.isOverride,
					accessType: this.accessType
				}

				this.$emit('save-query', data)
			}
		}
	}
</script>
