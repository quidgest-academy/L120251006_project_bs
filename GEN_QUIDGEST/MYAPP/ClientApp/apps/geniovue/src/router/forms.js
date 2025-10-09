import { propsConverter } from './routeUtils.js'

export default function getFormsRoutes()
{
	return [
		{
			path: '/:culture/:system/:module/form/F_CHARAC/:mode/:id?',
			name: 'form-F_CHARAC',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormFCharac/QFormFCharac.vue'),
			meta: {
				routeType: 'form',
				baseArea: 'CHARA',
				humanKeyFields: ['ValName']
			}
		},
		{
			path: '/:culture/:system/:module/form/F_COMMEN/:mode/:id?',
			name: 'form-F_COMMEN',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormFCommen/QFormFCommen.vue'),
			meta: {
				routeType: 'form',
				baseArea: 'COMME',
				humanKeyFields: []
			}
		},
		{
			path: '/:culture/:system/:module/form/F_FAVORI/:mode/:id?',
			name: 'form-F_FAVORI',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormFFavori/QFormFFavori.vue'),
			meta: {
				routeType: 'form',
				baseArea: 'FAVOR',
				humanKeyFields: []
			}
		},
		{
			path: '/:culture/:system/:module/form/F_MOVIES/:mode/:id?',
			name: 'form-F_MOVIES',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormFMovies/QFormFMovies.vue'),
			meta: {
				routeType: 'form',
				baseArea: 'MOVIE',
				humanKeyFields: ['ValTitle']
			}
		},
		{
			path: '/:culture/:system/:module/form/F_USER/:mode/:id?',
			name: 'form-F_USER',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormFUser/QFormFUser.vue'),
			meta: {
				routeType: 'form',
				baseArea: 'USERP',
				humanKeyFields: ['ValName']
			}
		},
		{
			path: '/:culture/:system/:module/form/HOMP/:mode/:id?',
			name: 'form-HOMP',
			props: route => propsConverter(route),
			component: () => import('@/views/forms/FormHomp/QFormHomp.vue'),
			meta: {
				routeType: 'form',
				baseArea: '',
				humanKeyFields: []
			}
		},
	]
}
