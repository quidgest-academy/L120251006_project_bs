import _assignIn from 'lodash-es/assignIn'

import { Base } from './base'

export class Boolean extends Base {
	constructor(options) {
		super(
			_assignIn(
				{
					type: 'Boolean'
				},
				options
			)
		)
	}

	/**
	 * @override
	 */
	get serverValue() {
		return this.value ?? false
	}

	/**
	 * @override
	 */
	sanitizeValue(value) {
		const sanitizedVal = super.sanitizeValue(value)

		if (typeof sanitizedVal === 'number') return sanitizedVal === 1

		return sanitizedVal
	}

	/**
	 * @override
	 */
	clearValue() {
		super.clearValue(false)
	}

	/**
	 * @override
	 */
	isValidType(value) {
		return (
			typeof value === 'boolean' ||
			value === this.constructor.EMPTY_VALUE ||
			[0, 1].includes(value)
		)
	}
}
