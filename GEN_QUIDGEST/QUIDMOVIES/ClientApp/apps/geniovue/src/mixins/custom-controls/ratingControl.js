import CustomControl from './baseControl.js'
import qApi from '@/api/genio/quidgestFunctions.js'
import RatingResources from './resources/ratingResources.js'


/**
 * Rating control
 */
export default class RatingControl extends CustomControl {
	constructor(controlContext, controlOrder) {
		super(controlContext, controlOrder)

		this.texts = new RatingResources(controlContext.vueContext.$getResource)
	}
	
	getProps(viewMode) { // props - QRating.vue
		return {
			id: this.id,
			name: this.controlContext.label,
			readonly: this.controlContext.readonly,
			maxRating: this.maxRating(viewMode),
			increment: this.increment(viewMode),
			shapeSize: this.shapeSize(viewMode),
			showRating: this.showRating(viewMode),
			activeColor: this.activeColor(viewMode),
			inactiveColor: this.inactiveColor(viewMode),
			padding: this.padding(viewMode),
			modelValue: viewMode.mappedValues[0].rating[0].value,
		}
	}

	updateValue(value) {
		this.controlContext.modelFieldRef.updateValue(value)
	}

	maxRating(viewMode) {
		return viewMode.styleVariables.maxRating?.value ?? 5
	}

	increment(viewMode) {
		return viewMode.styleVariables.increment?.value ?? 1
	}

	activeColor(viewMode) {
		let activecolor = viewMode.styleVariables.activeColor?.value ?? qApi.GetAppThemeVariable('$primary')

		if(activecolor === "$primary")
			activecolor = qApi.GetAppThemeVariable('$primary')

		return activecolor
	}

	inactiveColor(viewMode) {
		let inactivecolor = viewMode.styleVariables.inactiveColor?.value ?? qApi.GetAppThemeVariable('$gray-light')

		if(inactivecolor === "$gray-light")
			inactivecolor = qApi.GetAppThemeVariable('$gray-light')

		return inactivecolor
	}

	shapeSize(viewMode) {
		return viewMode.styleVariables.shapeSize?.value
	}

	showRating(viewMode) {
		return viewMode.styleVariables.showRating?.value ?? true
	}

	padding(viewMode) {
		return viewMode.styleVariables.padding?.value ?? 0
	}
}
