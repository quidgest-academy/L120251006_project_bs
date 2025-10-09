import { computed } from 'vue'

import CustomControl from './baseControl.js'
import CarouselResources from './resources/carouselResources.js'

/**
 * Carousel control
 * @extends CustomControl
 */
export default class CarouselControl extends CustomControl {
	/**
	 * Creates an instance of CarouselControl.
	 * @param {object} controlContext - The context of the control.
	 * @param {number} controlOrder - The order of the control.
	 */
	constructor(controlContext, controlOrder) {
		super(controlContext, controlOrder)

		this.currentSlide = 0
		this.texts = new CarouselResources(controlContext.vueContext.$getResource)
		this.usesFullSizeImg = true

		// Carousel-specific handlers
		this.handlers = {
			'update:current-slide': (newIndex) => this.handleCurrentSlideUpdate(newIndex),
			'slide:before-change': (rowKey) => this.handleSlideBeforeChange(rowKey),
			'slide:click': (rowKey) => this.handleSlideClick(rowKey)
		}
	}

	/**
	 * Get the properties for configuring the carousel component.
	 * @param {object} viewMode - The current view mode of the carousel.
	 * @returns {object} - An object containing carousel properties.
	 */
	getProps(viewMode) {
		const slides = computed(() => {
			return (viewMode.mappedValues ?? []).map((mappedValue) => ({
				id: mappedValue.rowKey,
				title: mappedValue.slideTitle?.value,
				subtitle: mappedValue.slideSubtitle?.value,
				image: mappedValue.slideImage?.previewData ?? mappedValue.slideImage?.value,
				colorPlaceholder: mappedValue.slideImage?.dominantColor
			}))
		})

		// Immediately fetch image of the current slide
		const currentSlideRowKey = slides.value[this.currentSlide]?.id
		if (currentSlideRowKey) this.fetchImage(currentSlideRowKey, 'slideImage')

		return {
			id: viewMode.containerId,
			currentSlide: this.currentSlide,
			slides: slides,
			showIndicators: viewMode.styleVariables.showIndicators.value,
			showControls: viewMode.styleVariables.showControls.value,
			keyboardControllable: viewMode.styleVariables.keyboardControllable.value,
			autoCycleInterval: viewMode.styleVariables.autoCycleInterval.value,
			autoCyclePause: viewMode.styleVariables.autoCyclePause.value,
			ride: viewMode.styleVariables.ride.value,
			wrap: viewMode.styleVariables.wrap.value,
			loading: !this.controlContext.loaded,
			texts: this.texts
		}
	}

	/**
	 * Handles the `update:current-slide` event.
	 * Updates the current slide index.
	 * @param {number} newIndex - The index of the new current slide.
	 */
	handleCurrentSlideUpdate(newIndex) {
		this.currentSlide = newIndex
	}

	/**
	 * Handles the `slide:before-change` event.
	 * Ensures the upcoming slide's image is preloaded.
	 * @param {string} rowKey - The key of the slide that will become visible.
	 */
	handleSlideBeforeChange(rowKey) {
		this.fetchImage(rowKey, 'slideImage')
	}

	/**
	 * Handles the `slide:click` event.
	 * Executes the configured row click action, if any.
	 * @param {string} rowKey - The key of the clicked slide.
	 */
	handleSlideClick(rowKey) {
		const action = this.controlContext.config.rowClickAction

		if (!action) return

		const viewMode = this.controlContext.viewModes[this.controlOrder - 1]
		const handler = viewMode.handlers.rowAction

		if (typeof handler === 'function') {
			handler({ id: action.id, rowKey })
		}
	}
}
