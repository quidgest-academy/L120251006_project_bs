// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { updateQueryParams } from './routeUtils.js'

export default function getMenusRoutes()
{
	return [
		{
			path: '/:culture/:system/MOV/menu/MOV_651',
			name: 'menu-MOV_651',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_651/QMenuMov651.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '651',
				baseArea: 'RATTI',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_21',
			name: 'menu-MOV_21',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_21/QMenuMov21.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '21',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_631',
			name: 'menu-MOV_631',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_631/QMenuMov631.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '631',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_31',
			name: 'menu-MOV_31',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_31/QMenuMov31.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '31',
				baseArea: 'USERP',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_611',
			name: 'menu-MOV_611',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_611/QMenuMov611.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '611',
				baseArea: 'USERP',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_511',
			name: 'menu-MOV_511',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_511/QMenuMov511.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '511',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_521',
			name: 'menu-MOV_521',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_521/QMenuMov521.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '521',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_641',
			name: 'menu-MOV_641',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_641/QMenuMov641.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '641',
				baseArea: 'COMME',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_41',
			name: 'menu-MOV_41',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_41/QMenuMov41.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '41',
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_5211',
			name: 'menu-MOV_5211',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_5211/QMenuMov5211.vue'),
			beforeEnter: [updateQueryParams],
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '5211',
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
				limitations: ['movie' /* DB */]
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_621',
			name: 'menu-MOV_621',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_621/QMenuMov621.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '621',
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
	]
}
