export default class RatingResources
{
	constructor(fnGetResource)
	{
		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'ratingLabel', {
			get: () => this._fnGetResource('AVALIACAO18442'),
			enumerable: true
		})
		Object.defineProperty(this, 'totalRating', {
			get: () => this._fnGetResource('OF21852'),
			enumerable: true
		})
	}
}
