import { defineAsyncComponent } from 'vue'

export default {
	install: (app) => {
		app.component('QFormAccountInfo', defineAsyncComponent(() => import('@/views/shared/AccountInfo.vue')))
		app.component('QFormFCharac', defineAsyncComponent(() => import('@/views/forms/FormFCharac/QFormFCharac.vue')))
		app.component('QFormFCommen', defineAsyncComponent(() => import('@/views/forms/FormFCommen/QFormFCommen.vue')))
		app.component('QFormFFavori', defineAsyncComponent(() => import('@/views/forms/FormFFavori/QFormFFavori.vue')))
		app.component('QFormFMovies', defineAsyncComponent(() => import('@/views/forms/FormFMovies/QFormFMovies.vue')))
		app.component('QFormFUser', defineAsyncComponent(() => import('@/views/forms/FormFUser/QFormFUser.vue')))
	}
}
