/* eslint-disable @typescript-eslint/no-unused-vars */
import { computed, reactive, watch } from 'vue'
import _merge from 'lodash-es/merge'

import FormViewModelBase from '@/mixins/formViewModelBase.js'
import genericFunctions from '@quidgest/clientapp/utils/genericFunctions'
import modelFieldType from '@quidgest/clientapp/models/fields'

import hardcodedTexts from '@/hardcodedTexts.js'
import netAPI from '@quidgest/clientapp/network'
import qApi from '@/api/genio/quidgestFunctions.js'
import qFunctions from '@/api/genio/projectFunctions.js'
import qProjArrays from '@/api/genio/projectArrays.js'
/* eslint-enable @typescript-eslint/no-unused-vars */

/**
 * Represents a ViewModel class.
 * @extends FormViewModelBase
 */
export default class ViewModel extends FormViewModelBase
{
	/**
	 * Creates a new instance of the ViewModel.
	 * @param {object} vueContext - The Vue context
	 * @param {object} options - The options for the ViewModel
	 * @param {object} values - A ViewModel instance to copy values from
	 */
	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	constructor(vueContext, options, values)
	{
		super(vueContext, options)
		// eslint-disable-next-line @typescript-eslint/no-unused-vars
		const vm = this.vueContext

		// The view model metadata
		_merge(this.modelInfo, {
			name: 'F_RATTIN',
			area: 'RATTI',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_rattin',
				updateFilesTickets: 'UpdateFilesTicketsF_rattin',
				setFile: 'SetFileF_rattin'
			}
		})

		/** The primary key. */
		this.ValCodratti = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCodratti',
			originId: 'ValCodratti',
			area: 'RATTI',
			field: 'CODRATTI',
			description: '',
		}).cloneFrom(values?.ValCodratti))
		this.stopWatchers.push(watch(() => this.ValCodratti.value, (newValue, oldValue) => this.onUpdate('ratti.codratti', this.ValCodratti, newValue, oldValue)))

		/** The used foreign keys. */
		this.ValCodmovie = reactive(new modelFieldType.ForeignKey({
			id: 'ValCodmovie',
			originId: 'ValCodmovie',
			area: 'RATTI',
			field: 'CODMOVIE',
			relatedArea: 'MOVIE',
			description: computed(() => this.Resources.MOVIE_ID24048),
		}).cloneFrom(values?.ValCodmovie))
		this.stopWatchers.push(watch(() => this.ValCodmovie.value, (newValue, oldValue) => this.onUpdate('ratti.codmovie', this.ValCodmovie, newValue, oldValue)))

		this.ValCoduserp = reactive(new modelFieldType.ForeignKey({
			id: 'ValCoduserp',
			originId: 'ValCoduserp',
			area: 'RATTI',
			field: 'CODUSERP',
			relatedArea: 'USERP',
			description: computed(() => this.Resources.USER_ID19581),
		}).cloneFrom(values?.ValCoduserp))
		this.stopWatchers.push(watch(() => this.ValCoduserp.value, (newValue, oldValue) => this.onUpdate('ratti.coduserp', this.ValCoduserp, newValue, oldValue)))

		/** The remaining form fields. */
		this.TableMovieTitle = reactive(new modelFieldType.String({
			type: 'Lookup',
			id: 'TableMovieTitle',
			originId: 'ValTitle',
			area: 'MOVIE',
			field: 'TITLE',
			maxLength: 80,
			description: computed(() => this.Resources.TITLE21885),
		}).cloneFrom(values?.TableMovieTitle))
		this.stopWatchers.push(watch(() => this.TableMovieTitle.value, (newValue, oldValue) => this.onUpdate('movie.title', this.TableMovieTitle, newValue, oldValue)))

		this.TableUserpName = reactive(new modelFieldType.String({
			type: 'Lookup',
			id: 'TableUserpName',
			originId: 'ValName',
			area: 'USERP',
			field: 'NAME',
			maxLength: 50,
			description: computed(() => this.Resources.NAME31974),
		}).cloneFrom(values?.TableUserpName))
		this.stopWatchers.push(watch(() => this.TableUserpName.value, (newValue, oldValue) => this.onUpdate('userp.name', this.TableUserpName, newValue, oldValue)))

		this.ValRate = reactive(new modelFieldType.Number({
			id: 'ValRate',
			originId: 'ValRate',
			area: 'RATTI',
			field: 'RATE',
			maxDigits: 1,
			decimalDigits: 0,
			description: computed(() => this.Resources.RATE50728),
		}).cloneFrom(values?.ValRate))
		this.stopWatchers.push(watch(() => this.ValRate.value, (newValue, oldValue) => this.onUpdate('ratti.rate', this.ValRate, newValue, oldValue)))

		this.ValRatedat = reactive(new modelFieldType.DateTime({
			id: 'ValRatedat',
			originId: 'ValRatedat',
			area: 'RATTI',
			field: 'RATEDAT',
			description: computed(() => this.Resources.RATE_AT01141),
		}).cloneFrom(values?.ValRatedat))
		this.stopWatchers.push(watch(() => this.ValRatedat.value, (newValue, oldValue) => this.onUpdate('ratti.ratedat', this.ValRatedat, newValue, oldValue)))
	}

	/**
	 * Creates a clone of the current QFormFRattinViewModel instance.
	 * @returns {QFormFRattinViewModel} A new instance of QFormFRattinViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCodratti'

	get QPrimaryKey() { return this.ValCodratti.value }
	set QPrimaryKey(value) { this.ValCodratti.updateValue(value) }
}
