<template>
	<div
		class="q-image-preview__modal-main-container">
		<div class="q-image-preview__modal-container">
			<img
				class="q-image-preview__modal-image"
				:src="imageURL"
				:alt="dataTitle" />

			<div class="q-image-preview__modal-buttons">
				<q-button
					id="downloadPreviewBtn"
					variant="ghost"
					color="neutral"
					borderless
					class="q-image-preview__modal-button"
					:title="texts.download"
					@click="download">
					<q-icon icon="download" />
				</q-button>

				<q-button
					id="closePreviewBtn"
					variant="ghost"
					color="neutral"
					borderless
					class="q-image-preview__modal-button"
					:title="texts.close"
					@click="closePreview">
					<q-icon icon="close" />
				</q-button>
			</div>
		</div>
	</div>
</template>

<script>
	import { getImageURL, downloadImage } from '@/utils/image.js'
	import { validateImageFormat, validateTexts } from '@quidgest/clientapp/utils/genericFunctions'

	const DEFAULT_TEXTS = {
		close: 'Close',
		download: 'Download'
	}

	export default {
		name: 'QImagePreview',

		emits: {
			'close-image-preview': () => true
		},

		inheritAttrs: false,

		props: {
			/**
			 * Image title used for the alt attribute.
			 */
			dataTitle: String,

			/**
			 * The image to be displayed
			 */
			image: {
				type: [String, Object],
				validator: (value) => validateImageFormat(value),
				required: true
			},

			/**
			 * Necessary strings to be used in labels and buttons.
			 */
			texts: {
				type: Object,
				validator: (value) => validateTexts(DEFAULT_TEXTS, value),
				default: () => DEFAULT_TEXTS
			}
		},

		expose: [],

		mounted()
		{
			document.addEventListener('keydown', this.onKeyPress)
		},

		beforeUnmount()
		{
			document.removeEventListener('keydown', this.onKeyPress)
		},

		computed: {
			/**
			 * The base64 representation of the image to be displayed.
			 */
			imageURL()
			{
				return getImageURL(this.image)
			}
		},

		methods: {
			/**
			 * Closes the image preview.
			 */
			closePreview()
			{
				this.$emit('close-image-preview')
			},

			/**
			 * Handles the downloading of the image.
			 */
			download()
			{
				downloadImage(this.image)
			},

			/**
			 * Handles key press events.
			 * @param {Object} event - The key press event.
			 */
			onKeyPress(event)
			{
				if (this.image && event.key === 'Escape')
					this.closePreview()
			}
		},

		watch: {
			image(val)
			{
				// We are manipulating the style of the body to prevent overflow when the preview mode is on.
				if (val)
					document.body.style.setProperty('overflow', 'hidden')
				else
					document.body.style.removeProperty('overflow')
			}
		}
	}
</script>
