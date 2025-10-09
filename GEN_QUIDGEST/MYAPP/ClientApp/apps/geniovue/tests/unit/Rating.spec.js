import '@testing-library/jest-dom'
import { fireEvent, render } from '@testing-library/vue'

import QRating from '@/components/rendering/QRating.vue'

describe('QRating.vue', () => {
	it('Default rating with 5 shapes', async () => {
		const wrapper = render(QRating)

		let shapes = await wrapper.findAllByRole('img')
		expect(shapes).toHaveLength(5)
	})

	it('Rating with maxRating 10', async () => {
		const wrapper = render(QRating, {
			props: {
				maxRating: 10
			}
		})

		let shapes = await wrapper.findAllByRole('img')
		expect(shapes).toHaveLength(10)
	})

	it('By default enabled show rating value', async () => {
		const wrapper = render(QRating, {
			props: {
				maxRating: 10
			}
		})

		let ratingValue = await wrapper.findByTestId('numericValue')
		expect(ratingValue.innerHTML).toBe('0')
	})

	it('Disabled numeric value for rating', async () => {
		const wrapper = render(QRating, {
			props: {
				showRating: false
			}
		})

		// Assert that numeric value doesn't exist
		expect(wrapper.findByTestId('numericValue')).toMatchObject({})
	})

	it('Emit update:modelValue on shape click', async () => {
		const wrapper = render(QRating, {
			props: {
				maxRating: 5
			}
		})

		let shapes = await wrapper.findAllByTestId('shape')
		await fireEvent.click(shapes[0])
		expect(wrapper.emitted()).toHaveProperty('update:modelValue')
	})
})
