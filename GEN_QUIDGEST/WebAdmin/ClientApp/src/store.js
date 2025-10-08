import { createStore } from 'vuex'

const store = createStore({
	state: {
		currentApp: '',
		currentYear: '',
		multiYearStatus: false, // True if the application has more than one data system, false otherwise
		currentLanguage: '',
		availableYears: []
	},
	mutations: {
		SET_APP(state, newValue) {
			state.currentApp = newValue
		},
		SET_YEAR(state, newValue) {
			state.currentYear = newValue
		},
		SET_YEARS(state, years) {
			state.years = years.map((y) => ({ Text: y, Value: y }))
		},
		SET_MULTIYEARSTATUS(state, newValue) {
			state.multiYearStatus = newValue
		},
		SET_LANGUAGE(state, newValue) {
			state.currentLanguage = newValue
		}
	},
	actions: {
		changeApp(context, newValue) {
			context.commit('SET_APP', newValue)
		},
		changeYear(context, newValue) {
			context.commit('SET_YEAR', newValue)
		},
		changeMultiYearStatus(context, newValue) {
			context.commit('SET_MULTIYEARSTATUS', newValue)
		},
		changeLanguage(context, newValue) {
			context.commit('SET_LANGUAGE', newValue)
		},
		setYears(context, years) {
			context.commit('SET_YEARS', years)
		}
	},
	getters: {
		App: (state) => state.currentApp,
		Year: (state) => state.currentYear,
		MultiYearStatus: (state) => state.multiYearStatus,
		Language: (state) => state.currentLanguage,
		Years: (state) => state.availableYears
	}
})

export default store
