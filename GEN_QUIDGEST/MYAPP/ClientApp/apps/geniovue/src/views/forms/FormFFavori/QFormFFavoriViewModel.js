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
			name: 'F_FAVORI',
			area: 'FAVOR',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_favori',
				updateFilesTickets: 'UpdateFilesTicketsF_favori',
				setFile: 'SetFileF_favori'
			}
		})

		/** The primary key. */
		this.ValCodfavor = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCodfavor',
			originId: 'ValCodfavor',
			area: 'FAVOR',
			field: 'CODFAVOR',
			description: '',
		}).cloneFrom(values?.ValCodfavor))
		this.stopWatchers.push(watch(() => this.ValCodfavor.value, (newValue, oldValue) => this.onUpdate('favor.codfavor', this.ValCodfavor, newValue, oldValue)))

		/** The used foreign keys. */
		this.ValCoduserp = reactive(new modelFieldType.ForeignKey({
			id: 'ValCoduserp',
			originId: 'ValCoduserp',
			area: 'FAVOR',
			field: 'CODUSERP',
			relatedArea: 'USERP',
			description: computed(() => this.Resources.USER_ID19581),
		}).cloneFrom(values?.ValCoduserp))
		this.stopWatchers.push(watch(() => this.ValCoduserp.value, (newValue, oldValue) => this.onUpdate('favor.coduserp', this.ValCoduserp, newValue, oldValue)))

		this.ValMovieid = reactive(new modelFieldType.ForeignKey({
			id: 'ValMovieid',
			originId: 'ValMovieid',
			area: 'FAVOR',
			field: 'MOVIEID',
			relatedArea: 'MOVIE',
			isUnique: true,
			uniquePrefixField: 'CODUSERP',
			description: computed(() => this.Resources.MOVIE_ID54960),
		}).cloneFrom(values?.ValMovieid))
		this.stopWatchers.push(watch(() => this.ValMovieid.value, (newValue, oldValue) => this.onUpdate('favor.movieid', this.ValMovieid, newValue, oldValue)))

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

		this.ValFavorite_at = reactive(new modelFieldType.Date({
			id: 'ValFavorite_at',
			originId: 'ValFavorite_at',
			area: 'FAVOR',
			field: 'FAVDATE',
			description: computed(() => this.Resources.FAVORITE_AT27922),
		}).cloneFrom(values?.ValFavorite_at))
		this.stopWatchers.push(watch(() => this.ValFavorite_at.value, (newValue, oldValue) => this.onUpdate('favor.favorite_at', this.ValFavorite_at, newValue, oldValue)))

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
	}

	/**
	 * Creates a clone of the current QFormFFavoriViewModel instance.
	 * @returns {QFormFFavoriViewModel} A new instance of QFormFFavoriViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCodfavor'

	get QPrimaryKey() { return this.ValCodfavor.value }
	set QPrimaryKey(value) { this.ValCodfavor.updateValue(value) }
}
