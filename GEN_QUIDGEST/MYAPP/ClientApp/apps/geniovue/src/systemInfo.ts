// @ts-expect-error -- types still WIP
import { getLayoutVariables } from '@quidgest/clientapp/utils/genericFunctions'

import layoutConfigJson from './assets/config/Layoutconfig.json'

export const systemInfo = {
	applicationName: 'My application',

	genio: {
		buildVersion: 83,
		dbIdxVersion: 25,
		dbVersion: '2525',
		genioVersion: '374,11',
		trackChangesVersion: '0',
		assemblyVersion: '374,11.2525.0.83',
		generationDate: {
			year: 2025,
			month: 10,
			day: 10
		}
	},

	system: {
		acronym: 'QUIDGEST',
		name: 'Quidgest',
		baseCurrency: {
			symbol: '€',
			code: 'EUR',
			precision: 2
		}
	},

	locale: {
		defaultLocale: 'en-US',
		availableLocales: [
			{
				language: 'en-US',
				acronym: 'EN',
				displayName: 'English'
			},
		]
	},

	// FIXME: This should be the generator's responsibility, not the client app.
	layout: getLayoutVariables(layoutConfigJson),

	authConfig: {
		useCertificate: false,
		maxUsrSize: 100,
		maxPswSize: 150
	},

	cookies: {
		cookieText: '',
		cookieActive: false,
		filePath: ''
	},

	isCavAvailable: false,

	isChatBotAvailable: false,

	isSuggestionsAvailable: true,

	appAlerts: [
	],

	userRegistration: {
		allowRegistration: false,
		registrationTypes: [
		]
	},

	resourcesPath: 'Content/img/'
}
