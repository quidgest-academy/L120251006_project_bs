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
			name: 'F_MOVADM',
			area: 'MOVIE',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_movadm',
				updateFilesTickets: 'UpdateFilesTicketsF_movadm',
				setFile: 'SetFileF_movadm'
			}
		})

		/** The primary key. */
		this.ValCodmovie = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCodmovie',
			originId: 'ValCodmovie',
			area: 'MOVIE',
			field: 'CODMOVIE',
			description: '',
		}).cloneFrom(values?.ValCodmovie))
		this.stopWatchers.push(watch(() => this.ValCodmovie.value, (newValue, oldValue) => this.onUpdate('movie.codmovie', this.ValCodmovie, newValue, oldValue)))

		/** The remaining form fields. */
		this.ValPoster = reactive(new modelFieldType.Image({
			id: 'ValPoster',
			originId: 'ValPoster',
			area: 'MOVIE',
			field: 'POSTER',
			description: computed(() => this.Resources.POSTER52933),
		}).cloneFrom(values?.ValPoster))
		this.stopWatchers.push(watch(() => this.ValPoster.value, (newValue, oldValue) => this.onUpdate('movie.poster', this.ValPoster, newValue, oldValue)))

		this.ValBackdrop = reactive(new modelFieldType.Image({
			id: 'ValBackdrop',
			originId: 'ValBackdrop',
			area: 'MOVIE',
			field: 'BACKDROP',
			description: computed(() => this.Resources.BACKDROP05167),
		}).cloneFrom(values?.ValBackdrop))
		this.stopWatchers.push(watch(() => this.ValBackdrop.value, (newValue, oldValue) => this.onUpdate('movie.backdrop', this.ValBackdrop, newValue, oldValue)))

		this.ValTitle = reactive(new modelFieldType.String({
			id: 'ValTitle',
			originId: 'ValTitle',
			area: 'MOVIE',
			field: 'TITLE',
			maxLength: 80,
			description: computed(() => this.Resources.TITLE21885),
		}).cloneFrom(values?.ValTitle))
		this.stopWatchers.push(watch(() => this.ValTitle.value, (newValue, oldValue) => this.onUpdate('movie.title', this.ValTitle, newValue, oldValue)))

		this.ValRealease_date = reactive(new modelFieldType.Date({
			id: 'ValRealease_date',
			originId: 'ValRealease_date',
			area: 'MOVIE',
			field: 'RELDATE',
			description: computed(() => this.Resources.REALEASE_DATE49316),
		}).cloneFrom(values?.ValRealease_date))
		this.stopWatchers.push(watch(() => this.ValRealease_date.value, (newValue, oldValue) => this.onUpdate('movie.realease_date', this.ValRealease_date, newValue, oldValue)))

		this.ValCreateat = reactive(new modelFieldType.Date({
			id: 'ValCreateat',
			originId: 'ValCreateat',
			area: 'MOVIE',
			field: 'CREATEAT',
			valueFormula: {
				stopRecalcCondition() { return false },
				// eslint-disable-next-line @typescript-eslint/no-unused-vars
				fnFormula(params)
				{
					// Formula: [Today]
					return qApi.Today()
				},
				dependencyEvents: [],
				isServerRecalc: false,
				isEmpty: qApi.emptyD,
			},
			description: computed(() => this.Resources.CREATE_AT36393),
		}).cloneFrom(values?.ValCreateat))
		this.stopWatchers.push(watch(() => this.ValCreateat.value, (newValue, oldValue) => this.onUpdate('movie.createat', this.ValCreateat, newValue, oldValue)))

		this.ValMoviesgenre = reactive(new modelFieldType.String({
			id: 'ValMoviesgenre',
			originId: 'ValMoviesgenre',
			area: 'MOVIE',
			field: 'MOVIEGEN',
			maxLength: 15,
			arrayOptions: computed(() => new qProjArrays.QArrayMoviegenre(vm.$getResource).elements),
			description: computed(() => this.Resources.MOVIES_GENRE22042),
		}).cloneFrom(values?.ValMoviesgenre))
		this.stopWatchers.push(watch(() => this.ValMoviesgenre.value, (newValue, oldValue) => this.onUpdate('movie.moviesgenre', this.ValMoviesgenre, newValue, oldValue)))

		this.ValDescription = reactive(new modelFieldType.MultiLineString({
			id: 'ValDescription',
			originId: 'ValDescription',
			area: 'MOVIE',
			field: 'DISCRIPT',
			description: computed(() => this.Resources.DISCRIPTION02169),
		}).cloneFrom(values?.ValDescription))
		this.stopWatchers.push(watch(() => this.ValDescription.value, (newValue, oldValue) => this.onUpdate('movie.description', this.ValDescription, newValue, oldValue)))

		this.ValNumberoflikes = reactive(new modelFieldType.Number({
			id: 'ValNumberoflikes',
			originId: 'ValNumberoflikes',
			area: 'MOVIE',
			field: 'NUMBEROF',
			maxDigits: 9,
			decimalDigits: 0,
			isFixed: true,
			description: '',
		}).cloneFrom(values?.ValNumberoflikes))
		this.stopWatchers.push(watch(() => this.ValNumberoflikes.value, (newValue, oldValue) => this.onUpdate('movie.numberoflikes', this.ValNumberoflikes, newValue, oldValue)))
	}

	/**
	 * Creates a clone of the current QFormFMovadmViewModel instance.
	 * @returns {QFormFMovadmViewModel} A new instance of QFormFMovadmViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCodmovie'

	get QPrimaryKey() { return this.ValCodmovie.value }
	set QPrimaryKey(value) { this.ValCodmovie.updateValue(value) }
}
