<template>
	<svg
		role="img"
		class="q-rating__shape-svg"
		:height="shapeSize"
		:width="shapeSize"
		:viewBox="viewBox"
		@mousedown.stop.prevent=""
		@click.stop.prevent="selected"
		@mouseover="hovered"
		@mouseout="unhovered">
		<linearGradient
			:id="grad"
			x1="0"
			x2="100%"
			y1="0"
			y2="0">
			<stop
				:offset="shapeFill"
				:stop-color="getColor(activeColor)"
				:stop-opacity="getOpacity(activeColor)" />
			<stop
				:offset="shapeFill"
				:stop-color="getColor(inactiveColor)"
				:stop-opacity="getOpacity(inactiveColor)" />
		</linearGradient>

		<polygon
			:points="shapePointsAsString"
			:fill="gradId"
			:stroke="getBorderColor"
			:stroke-width="border"
			:stroke-linejoin="roundedCorners ? 'round' : 'miter'" />
	</svg>
</template>

<script>
	export default {
		name: 'QRatingShape',

		emits: ['shape-selected', 'shape-hovered', 'shape-unhovered'],

		props: {
			/**
			 * Array of points to define the shape polygon.
			 */
			points: {
				type: Array,
				default: () => [[19.8, 2.2], [6.6, 43.56], [39.6, 17.16], [0, 17.16], [33, 43.56]]
			},

			/**
			 * The fill percentage of the shape used in linear gradient calculation.
			 */
			fill: {
				type: Number,
				default: 0
			},

			/**
			 * Size of the shape.
			 */
			size: {
				type: Number,
				default: 40
			},

			/**
			 * Numerical identifier for the shape.
			 */
			shapeId: {
				type: Number,
				required: true
			},

			/**
			 * Fill color when the shape is active.
			 */
			activeColor: {
				type: String,
				required: true
			},

			/**
			 * Fill color when the shape is inactive.
			 */
			inactiveColor: {
				type: String,
				required: true
			},

			/**
			 * Color of the border for the shape.
			 */
			borderColor: {
				type: String,
				default: '#000'
			},

			/**
			 * Width of the border for the shape.
			 */
			borderWidth: {
				type: Number,
				default: 0
			},

			/**
			 * If true, the corners of the stroke are rounded.
			 */
			roundedCorners: {
				type: Boolean,
				default: false
			}
		},

		expose: [],

		computed: {
			/**
			 * Scaled points for the shape accounting for size and border.
			 */
			shapePoints()
			{
				let xCoords = this.points.map(p => p[0]);
				let yCoords = this.points.map(p => p[1]);
				let xMin = Math.min(...xCoords) - this.border;
				let xMax = Math.max(...xCoords) + this.border;
				let yMin = Math.min(...yCoords) - this.border;
				let yMax = Math.max(...yCoords) + this.border;

				// Calculate the width and height of the shape's bounding box
				let shapeWidth = xMax - xMin;
				let shapeHeight = yMax - yMin;

				let scale = Math.min(this.size / shapeWidth, this.size / shapeHeight);

				let scaledPoints = this.points.map(([x, y]) => [
					(x - xMin) * scale,  // normalize to (0,0) and scale
					(y - yMin) * scale   // normalize to (0,0) and scale
				]);

				let newShapeWidth = shapeWidth * scale;
				let newShapeHeight = shapeHeight * scale;

				let offsetX = (this.size - newShapeWidth) / 2;
				let offsetY = (this.size - newShapeHeight) / 2;

				let centeredPoints = scaledPoints.map(([x, y]) => [
					x + offsetX,  // shift x to center
					y + offsetY   // shift y to center
				]);

				return centeredPoints;
			},

			/**
			 * Converts the shape points array into a string for the SVG polygon attribute.
			 */
			shapePointsAsString()
			{
				return this.shapePoints.join(',')
			},

			/**
			 * Randomly generated ID for the linear gradient.
			 */
			grad()
			{
				return Math.random().toString(36).substring(7)
			},

			/**
			 * Full URL for the linear gradient id reference.
			 */
			gradId()
			{
				return 'url(#' + this.grad + ')'
			},

			/**
			 * Calculated size of the shape including border adjustments.
			 */
			shapeSize()
			{
				// Adjust shape size when rounded corners are set with no border, to account for the 'hidden' border.
				const size =
					this.roundedCorners && this.borderWidth <= 0
						? parseInt(this.size) - parseInt(this.border)
						: this.size
				return parseInt(size) + parseInt(this.border)
			},

			/**
			 * Converts the fill property to a percentage string for gradient offset.
			 */
			shapeFill()
			{
				return this.fill + '%'
			},

			/**
			 * The border width to be used in the SVG, calculated for rounded corner support.
			 */
			border()
			{
				return this.roundedCorners && this.borderWidth <= 0 ? 6 : this.borderWidth
			},

			/**
			 * The border color to be used in the SVG, possibly altered for rounded corners.
			 */
			getBorderColor()
			{
				if (this.roundedCorners && this.borderWidth <= 0)
					return this.fill <= 0 ? this.inactiveColor : this.activeColor
				return this.borderColor
			},

			/**
			 * Determines the view box attribute for the SVG based on the shape size.
			 */
			viewBox()
			{
				return `0 0 ${this.shapeSize} ${this.shapeSize}`
			}
		},

		methods: {
			/**
			 * Calculates the position within the shape in percentage based on a click event offset.
			 * @param {MouseEvent} event - The mouse event object from the click event.
			 * @returns {number} The position in percentage where the shape was clicked.
			 */
			getPosition(event)
			{
				const shapeWidth = (92 / 100) * this.size
				const offset = Math.max(event.offsetX, 1)
				const position = Math.round((100 / shapeWidth) * offset)
				return Math.min(position, 100)
			},

			/**
			 * Emits the selected event with shape id and position when the shape is clicked.
			 * @param {MouseEvent} event - The mouse event object from the click event.
			 */
			selected(event)
			{
				this.$emit('shape-selected', { id: this.shapeId, position: this.getPosition(event) })
			},

			/**
			 * Emits the hovered event with shape id and position when the shape is hovered.
			 * @param {MouseEvent} event - The mouse event object from the hover event.
			 */
			hovered(event) {
				this.$emit('shape-hovered', { id: this.shapeId, position: this.getPosition(event)})
			},

			/**
			 * Emits the unhovered event with shape id and position when the shape is unhovered.
			 */
			unhovered() {
				this.$emit('shape-unhovered')
			},

			/**
			 * Parses a color input and separates alpha if present. Supports rgba, hsla, and hex with alpha values.
			 * @param {string} inputColor - The input color in rgba, hsla, alphahex, or keyword format.
			 * @returns {object} An object with the color and opacity parsed.
			 */
			parseAlphaColor(inputColor)
			{
				const patterns = [
					{ // rgba
						pattern: /^rgba\((\d{1,3}%?\s*,\s*){3}(\d*(?:\.\d+)?)\)$/,
						getColor: (color) => color.replace(/,(?!.*,).*(?=\))|a/g, ''),
						getOpacity: (color) => color.match(/\.\d+|[01](?=\))/)[0]
					},
					{ // hsla
						pattern: /^hsla\(\d+\s*,\s*([\d.]+%\s*,\s*){2}(\d*(?:\.\d+)?)\)$/,
						getColor: (color) => color.replace(/,(?!.*,).*(?=\))|a/g, ''),
						getOpacity: (color) => color.match(/\.\d+|[01](?=\))/)[0]
					},
					{ // alphahex
						pattern: /^#([0-9A-Fa-f]{4}|[0-9A-Fa-f]{8})$/,
						getColor: (color) => color.length === 5 ? color.substring(0, 4) : color.substring(0, 7),
						getOpacity: (color) => {
							if (color.length === 5)
								return (parseInt(color.substring(4, 5) + color.substring(4, 5), 16) / 255).toFixed(2)
							else
								return (parseInt(color.substring(7, 9), 16) / 255).toFixed(2)
						}
					},
					{ // transparent
						pattern: /^transparent$/,
						getColor: () => '#fff',
						getOpacity: () => '0'
					}
				]

				for (let i = 0; i < patterns.length; i++)
				{
					if (patterns[i].pattern.test(inputColor))
					{
						return {
							color: patterns[i].getColor(inputColor),
							opacity: patterns[i].getOpacity(inputColor)
						}
					}
				}

				return {
					color: inputColor,
					opacity: '1'
				}
			},

			/**
			 * Returns the color part of an input color, stripping out the alpha value if present.
			 * @param {string} color - The input color in rgba, hsla, alphahex, or keyword format.
			 * @returns {string} The color part as a valid CSS color string.
			 */
			getColor(color)
			{
				return this.parseAlphaColor(color).color
			},

			/**
			 * Returns the opacity part of an input color.
			 * @param {string} color - The input color in rgba, hsla, alphahex, or keyword format.
			 * @returns {string} The opacity part as a string.
			 */
			getOpacity(color)
			{
				return this.parseAlphaColor(color).opacity
			}
		}
	}
</script>
