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
			name: 'F_CHARAC',
			area: 'CHARA',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_charac',
				updateFilesTickets: 'UpdateFilesTicketsF_charac',
				setFile: 'SetFileF_charac'
			}
		})

		/** The primary key. */
		this.ValCodchara = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCodchara',
			originId: 'ValCodchara',
			area: 'CHARA',
			field: 'CODCHARA',
			description: '',
		}).cloneFrom(values?.ValCodchara))
		this.stopWatchers.push(watch(() => this.ValCodchara.value, (newValue, oldValue) => this.onUpdate('chara.codchara', this.ValCodchara, newValue, oldValue)))

		/** The used foreign keys. */
		this.ValMovieid = reactive(new modelFieldType.ForeignKey({
			id: 'ValMovieid',
			originId: 'ValMovieid',
			area: 'CHARA',
			field: 'MOVIEID',
			relatedArea: 'MOVIE',
			description: computed(() => this.Resources.MOVIE_ID54960),
		}).cloneFrom(values?.ValMovieid))
		this.stopWatchers.push(watch(() => this.ValMovieid.value, (newValue, oldValue) => this.onUpdate('chara.movieid', this.ValMovieid, newValue, oldValue)))

		/** The remaining form fields. */
		this.ValPhoto = reactive(new modelFieldType.Image({
			id: 'ValPhoto',
			originId: 'ValPhoto',
			area: 'CHARA',
			field: 'PHOTO',
			description: computed(() => this.Resources.PHOTO51874),
		}).cloneFrom(values?.ValPhoto))
		this.stopWatchers.push(watch(() => this.ValPhoto.value, (newValue, oldValue) => this.onUpdate('chara.photo', this.ValPhoto, newValue, oldValue)))

		this.ValName = reactive(new modelFieldType.String({
			id: 'ValName',
			originId: 'ValName',
			area: 'CHARA',
			field: 'NAME',
			maxLength: 80,
			description: computed(() => this.Resources.NAME31974),
		}).cloneFrom(values?.ValName))
		this.stopWatchers.push(watch(() => this.ValName.value, (newValue, oldValue) => this.onUpdate('chara.name', this.ValName, newValue, oldValue)))

		this.ValActorname = reactive(new modelFieldType.String({
			id: 'ValActorname',
			originId: 'ValActorname',
			area: 'CHARA',
			field: 'ACTORNAM',
			maxLength: 80,
			description: computed(() => this.Resources.ACTOR_NAME48089),
		}).cloneFrom(values?.ValActorname))
		this.stopWatchers.push(watch(() => this.ValActorname.value, (newValue, oldValue) => this.onUpdate('chara.actorname', this.ValActorname, newValue, oldValue)))

		this.ValCreateat = reactive(new modelFieldType.Date({
			id: 'ValCreateat',
			originId: 'ValCreateat',
			area: 'CHARA',
			field: 'CREATEAT',
			description: computed(() => this.Resources.CREATE_AT36393),
		}).cloneFrom(values?.ValCreateat))
		this.stopWatchers.push(watch(() => this.ValCreateat.value, (newValue, oldValue) => this.onUpdate('chara.createat', this.ValCreateat, newValue, oldValue)))

		this.ValGenre = reactive(new modelFieldType.String({
			id: 'ValGenre',
			originId: 'ValGenre',
			area: 'CHARA',
			field: 'GENRE',
			maxLength: 1,
			arrayOptions: computed(() => new qProjArrays.QArrayGenre(vm.$getResource).elements),
			description: computed(() => this.Resources.GENRE63303),
		}).cloneFrom(values?.ValGenre))
		this.stopWatchers.push(watch(() => this.ValGenre.value, (newValue, oldValue) => this.onUpdate('chara.genre', this.ValGenre, newValue, oldValue)))

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
	 * Creates a clone of the current QFormFCharacViewModel instance.
	 * @returns {QFormFCharacViewModel} A new instance of QFormFCharacViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCodchara'

	get QPrimaryKey() { return this.ValCodchara.value }
	set QPrimaryKey(value) { this.ValCodchara.updateValue(value) }
}
