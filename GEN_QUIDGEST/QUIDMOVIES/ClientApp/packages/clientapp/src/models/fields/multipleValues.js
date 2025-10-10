import _assignIn from 'lodash-es/assignIn'

import { Base } from './base'

export class MultipleValues extends Base {
	constructor(options) {
		super(
			_assignIn(
				{
					type: 'MultipleValues',
					_value: []
				},
				options
			)
		)
	}

	/**
	 * @override
	 */
	clearValue() {
		super.clearValue([])
	}

	/**
	 * @override
	 */
	isValidType(value) {
		return typeof value === 'object'
	}
}
