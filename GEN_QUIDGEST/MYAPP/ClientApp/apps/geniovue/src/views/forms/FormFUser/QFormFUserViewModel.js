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
			name: 'F_USER',
			area: 'USERP',
			actions: {
				recalculateFormulas: 'RecalculateFormulas_F_user',
				updateFilesTickets: 'UpdateFilesTicketsF_user',
				setFile: 'SetFileF_user'
			}
		})

		/** The primary key. */
		this.ValCoduserp = reactive(new modelFieldType.PrimaryKey({
			id: 'ValCoduserp',
			originId: 'ValCoduserp',
			area: 'USERP',
			field: 'CODUSERP',
			description: '',
		}).cloneFrom(values?.ValCoduserp))
		this.stopWatchers.push(watch(() => this.ValCoduserp.value, (newValue, oldValue) => this.onUpdate('userp.coduserp', this.ValCoduserp, newValue, oldValue)))

		/** The remaining form fields. */
		this.ValName = reactive(new modelFieldType.String({
			id: 'ValName',
			originId: 'ValName',
			area: 'USERP',
			field: 'NAME',
			maxLength: 50,
			description: computed(() => this.Resources.NAME31974),
		}).cloneFrom(values?.ValName))
		this.stopWatchers.push(watch(() => this.ValName.value, (newValue, oldValue) => this.onUpdate('userp.name', this.ValName, newValue, oldValue)))

		this.ValEmail = reactive(new modelFieldType.String({
			id: 'ValEmail',
			originId: 'ValEmail',
			area: 'USERP',
			field: 'EMAIL',
			maxLength: 80,
			maskType: 'EM',
			description: computed(() => this.Resources.EMAIL25170),
		}).cloneFrom(values?.ValEmail))
		this.stopWatchers.push(watch(() => this.ValEmail.value, (newValue, oldValue) => this.onUpdate('userp.email', this.ValEmail, newValue, oldValue)))

		this.ValPhoto = reactive(new modelFieldType.Image({
			id: 'ValPhoto',
			originId: 'ValPhoto',
			area: 'USERP',
			field: 'PHOTO',
			description: computed(() => this.Resources.PHOTO51874),
		}).cloneFrom(values?.ValPhoto))
		this.stopWatchers.push(watch(() => this.ValPhoto.value, (newValue, oldValue) => this.onUpdate('userp.photo', this.ValPhoto, newValue, oldValue)))
	}

	/**
	 * Creates a clone of the current QFormFUserViewModel instance.
	 * @returns {QFormFUserViewModel} A new instance of QFormFUserViewModel
	 */
	clone()
	{
		return new ViewModel(this.vueContext, { callbacks: this.externalCallbacks }, this)
	}

	static QPrimaryKeyName = 'ValCoduserp'

	get QPrimaryKey() { return this.ValCoduserp.value }
	set QPrimaryKey(value) { this.ValCoduserp.updateValue(value) }
}
