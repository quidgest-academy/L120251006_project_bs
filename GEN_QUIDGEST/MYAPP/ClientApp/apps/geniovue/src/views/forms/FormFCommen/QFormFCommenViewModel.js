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
			name: 'F_COMMEN',
			area: 'COMME',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_commen',
				updateFilesTickets: 'UpdateFilesTicketsF_commen',
				setFile: 'SetFileF_commen'
			}
		})

		/** The primary key. */
		this.ValCodcomme = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCodcomme',
			originId: 'ValCodcomme',
			area: 'COMME',
			field: 'CODCOMME',
			description: '',
		}).cloneFrom(values?.ValCodcomme))
		this.stopWatchers.push(watch(() => this.ValCodcomme.value, (newValue, oldValue) => this.onUpdate('comme.codcomme', this.ValCodcomme, newValue, oldValue)))

		/** The used foreign keys. */
		this.ValCoduserp = reactive(new modelFieldType.ForeignKey({
			id: 'ValCoduserp',
			originId: 'ValCoduserp',
			area: 'COMME',
			field: 'CODUSERP',
			relatedArea: 'USERP',
			description: computed(() => this.Resources.USER_ID19581),
		}).cloneFrom(values?.ValCoduserp))
		this.stopWatchers.push(watch(() => this.ValCoduserp.value, (newValue, oldValue) => this.onUpdate('comme.coduserp', this.ValCoduserp, newValue, oldValue)))

		this.ValCodmovie = reactive(new modelFieldType.ForeignKey({
			id: 'ValCodmovie',
			originId: 'ValCodmovie',
			area: 'COMME',
			field: 'CODMOVIE',
			relatedArea: 'MOVIE',
			description: computed(() => this.Resources.MOVIE_ID24048),
		}).cloneFrom(values?.ValCodmovie))
		this.stopWatchers.push(watch(() => this.ValCodmovie.value, (newValue, oldValue) => this.onUpdate('comme.codmovie', this.ValCodmovie, newValue, oldValue)))

		/** The remaining form fields. */
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

		this.ValPost = reactive(new modelFieldType.MultiLineString({
			id: 'ValPost',
			originId: 'ValPost',
			area: 'COMME',
			field: 'POST',
			description: computed(() => this.Resources.POST24992),
		}).cloneFrom(values?.ValPost))
		this.stopWatchers.push(watch(() => this.ValPost.value, (newValue, oldValue) => this.onUpdate('comme.post', this.ValPost, newValue, oldValue)))

		this.ValCreateat = reactive(new modelFieldType.Date({
			id: 'ValCreateat',
			originId: 'ValCreateat',
			area: 'COMME',
			field: 'CREATEAT',
			description: computed(() => this.Resources.CREATE_AT36393),
		}).cloneFrom(values?.ValCreateat))
		this.stopWatchers.push(watch(() => this.ValCreateat.value, (newValue, oldValue) => this.onUpdate('comme.createat', this.ValCreateat, newValue, oldValue)))
	}

	/**
	 * Creates a clone of the current QFormFCommenViewModel instance.
	 * @returns {QFormFCommenViewModel} A new instance of QFormFCommenViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCodcomme'

	get QPrimaryKey() { return this.ValCodcomme.value }
	set QPrimaryKey(value) { this.ValCodcomme.updateValue(value) }
}
