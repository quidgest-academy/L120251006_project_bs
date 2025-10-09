export default class CarouselResources
{
	constructor(fnGetResource)
	{
		this._fnGetResource = typeof fnGetResource !== 'function' ? resId => resId : fnGetResource
		Object.defineProperty(this, '_fnGetResource', { enumerable: false })

		Object.defineProperty(this, 'previousText', {
			get: () => this._fnGetResource('ANTERIOR34904'),
			enumerable: true
		})
		Object.defineProperty(this, 'nextText', {
			get: () => this._fnGetResource('PROXIMO29858'),
			enumerable: true
		})
		Object.defineProperty(this, 'goToSlide', {
			get: () => this._fnGetResource('IR_PARA_A_DIAPOSITIV34370'),
			enumerable: true
		})
	}
}
