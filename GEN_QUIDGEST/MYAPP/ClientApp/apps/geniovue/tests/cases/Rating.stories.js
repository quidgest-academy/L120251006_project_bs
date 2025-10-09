import Rating from '@/components/rendering/QRating.vue'

export default {
	title: 'Inputs/Rating',
	component: Rating,
	tags: ['autodocs']
}

/**
 * Displays the rating's main combobox.
 */
export const Default = {
	args: {
		increment: 1
	}
}
