<template>
	<div
		:id="id"
		:class="['q-rating', { 'q-rating--inline': inline }]"
		role="slider"
		tabindex="0"
		@keydown.stop="changeRating"
		ref="ratingControl"
		:aria-label="ratingName"
		aria-valuemin="0"
		:aria-valuemax="maxRating"
		:aria-valuenow="selectedRating"
		:aria-valuetext="`${texts.ratingLabel} ${formattedRating} ${texts.totalRating} ${maxRating}`">
		<span
			v-for="n in maxRating"
			:key="getShapeId(n)"
			:class="[{ 'q-rating--read-only': readonly }, 'q-rating__shape']"
			:style="{ 'margin-right': `${margin}px` }">

			<q-rating-shape
				data-testid="shape"
				:fill="fillLevel[n - 1]"
				:size="shapeSize"
				:shape-id="n"
				:step="step"
				:active-color="activeColor"
				:inactive-color="inactiveColor"
				:border-color="borderColor"
				:border-width="borderWidth"
				:rounded-corners="roundedCorners"
				:aria-labelledby="id"
				@shape-selected="setRating($event, true)"
				@shape-hovered="setRating($event, false)"
				@shape-unhovered="resetRating()" />
		</span>

		<span
			v-if="showRating"
			data-testid="numericValue"
			:class="['q-rating__value', textClass]"
			:data-rating="formattedRating">
			{{ formattedRating }}
		</span>
	</div>
</template>

<script>
	import { validateTexts } from '@quidgest/clientapp/utils/genericFunctions'

	import QRatingShape from './QRatingShape.vue'

	// The texts needed by the component.
	const DEFAULT_TEXTS = {
		ratingLabel: 'Rating',
		totalRating: 'of'
	}

	/**
	 * Rating component which provides an alternative interaction with a numeric field for use cases where the business interpretation of that number is a quality rating.
	 */
	export default {
		name: 'QRating',

		emits: [
			'update:modelValue'
		],

		components: {
			QRatingShape
		},

		inheritAttrs: false,

		props: {
			/**
			 * Unique identifier for the control.
			 */
			id: {
				type: String,
				required: true
			},

			/**
			 * Unique identifier for the control.
			 */
			name: {
				type: String
			},

			/**
			 * Configures the increment of rating.
			 */
			increment: {
				type: Number,
				default: 1
			},

			/**
			 * The current rating value.
			 */
			modelValue: {
				type: Number,
				default: 0
			},

			/**
			 * Configures the active color of rating.
			 */
			activeColor: {
				type: String
			},

			/**
			 * Configures the Inactive color of rating.
			 */
			inactiveColor: {
				type: String
			},

			/**
			 * Configures the max rating.
			 */
			maxRating: {
				type: Number,
				default: 5
			},

			/**
			 * Configures the size of the rating shape.
			 */
			shapeSize: {
				type: Number
			},

			/**
			 * Configures if the rating is shown to the user.
			 */
			showRating: {
				type: Boolean,
				default: true
			},

			/**
			 * Configures if the rating is read only.
			 */
			readonly: {
				type: Boolean,
				default: false
			},

			/**
			 * Necessary strings to be used in labels and headers.
			 */
			texts: {
				type: Object,
				validator: (value) => validateTexts(DEFAULT_TEXTS, value),
				default: () => DEFAULT_TEXTS
			},

			/**
			 * The class used for texts.
			 */
			textClass: {
				type: String,
				default: ''
			},

			/**
			 * Whether or not it's an inline rating.
			 */
			inline: {
				type: Boolean,
				default: false
			},

			/**
			 * Configures the border color of the shape.
			 */
			borderColor: {
				type: String,
				default: '#999'
			},

			/**
			 * Configures if the rating border is rounded or not.
			 */
			roundedCorners: {
				type: Boolean,
				default: false
			},

			/**
			 * Configures the padding of rating.
			 */
			padding: {
				type: Number,
				default: 0
			},

			/**
			 * Configures the border-width of the shape.
			 */
			borderWidth: {
				type: Number,
				default: 0
			},

			/**
			 * Configures up to how much decimal point rating value should be displayed.
			 */
			fixedPoints: {
				type: Number,
				default: -1,
				validator: (value) => {
					return value >= -1
				}
			},

			/**
			 * Whether or not the component is clearable.
			 */
			clearable: {
				type: Boolean,
				default: false
			}
		},

		expose: [],

		data() {
			return {
				step: this.increment * 100,
				currentRating: this.modelValue,
				selectedRating: this.modelValue,
				ratingSelected: false,
				fillLevel: []
			}
		},

		mounted() {

			this.createShapes(this.shouldRound)
		},

		computed: {
			/**
			 * The rating value in the right format.
			 */
			formattedRating() {
				return Number(this.fixedPoints < 0 ? this.selectedRating : this.selectedRating.toFixed(this.fixedPoints))
			},

			/**
			 * Whether or not the rating value should be rounded.
			 */
			shouldRound() {
				return this.ratingSelected
			},

			/**
			 * The margin of the component.
			 */
			margin() {
				return this.padding + this.borderWidth
			},

			/**
			 * The name of the component
			 */
			ratingName() {
				return this.name ?? this.texts.ratingLabel
			}
		},

		methods: {
			/**
			 * Sets and emits the current rating value.
			 * @param {object} event The triggered change event
			 */
			changeRating(event) {
				if (this.readonly)
					return

				if (event.key === 'ArrowRight' || event.key === 'ArrowUp') {
					this.currentRating += this.increment
					this.createShapes()
					this.selectedRating = this.currentRating
				}
				else if (event.key === 'ArrowLeft' || event.key === 'ArrowDown') {
					if (this.currentRating - this.increment >= 0)
						this.currentRating -= this.increment
					this.createShapes()
					this.selectedRating = this.currentRating
				}

				this.$emit('update:modelValue', this.currentRating)
				this.ratingSelected = true
			},

			/**
			 * Sets and emits the current rating value.
			 * @param {object} event The triggered change event
			 * @param {boolean} persist Whether or not the value change should be persisted
			 */
			setRating(event, persist) {
				if (this.readonly)
					return

				const position = event.position / 100
				this.currentRating = (event.id + position - 1).toFixed(2)
				this.currentRating = this.currentRating > this.maxRating ? this.maxRating : this.currentRating
				this.createShapes()
				if (persist) {
					if (this.clearable && this.currentRating === this.selectedRating)
						this.selectedRating = 0
					else
						this.selectedRating = this.currentRating

					this.changeRating(event)
				}
			},

			/**
			 * Resets the rating value.
			 */
			resetRating() {
				if (!this.readonly) {
					this.currentRating = this.selectedRating
					this.createShapes(this.shouldRound)
				}
			},

			/**
			 * Creates the shapes to be displayed.
			 * @param {boolean} round Whether or not to round the rating value
			 */
			createShapes(round = true) {
				if (round)
					this.round()

				for (let i = 0; i < this.maxRating; i++) {
					let level = 0
					if (i < this.currentRating)
						level = this.currentRating - i > 1 ? 100 : (this.currentRating - i) * 100
					this.fillLevel[i] = Math.round(level)
				}
			},

			/**
			 * Rounds the current rating value.
			 */
			round() {
				var inv = 1.0 / this.increment

				// I did the division by 3 so that if the user wants to select 0 stars, 
				// he has to press 1/3 of the increment 
				// (ex: if the increment is 0.2 and the user clicks in 1/3 of 0.2,
				// he selects 0 stars, and if you clicks again it selects the increment to 0.2).
				if (this.currentRating < (this.increment / 3) && this.selectedRating > 0)
					this.currentRating = 0
				else
					this.currentRating = Math.min(this.maxRating, Math.ceil(this.currentRating * inv) / inv)
			},

			/**
			 * Gets a unique identifier for each shape of this control.
			 */
			getShapeId(n) {
				return this.id + "_" + n;
			}
		},

		watch: {
			modelValue(val) {
				this.currentRating = val
				this.selectedRating = val
				this.createShapes(this.shouldRound)
				//Focusing needs to happen only after the model value has changed.
				//Otherwise, screen readers will read the previous value first.
				this.$refs.ratingControl.focus()
			},

			maxRating(val) {
				this.currentRating = val < this.selectedRating ? val : this.selectedRating
			}
		},
	}
</script>