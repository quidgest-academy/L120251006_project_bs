// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { updateQueryParams } from './routeUtils.js'

export default function getMenusRoutes()
{
	return [
		{
			path: '/:culture/:system/MOV/menu/MOV_551',
			name: 'menu-MOV_551',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_551/QMenuMov551.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '551',
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
			path: '/:culture/:system/MOV/menu/MOV_531',
			name: 'menu-MOV_531',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_531/QMenuMov531.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '531',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
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
				baseArea: 'USERP',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_411',
			name: 'menu-MOV_411',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_411/QMenuMov411.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '411',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_421',
			name: 'menu-MOV_421',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_421/QMenuMov421.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '421',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_541',
			name: 'menu-MOV_541',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_541/QMenuMov541.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '541',
				baseArea: 'COMME',
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
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_4211',
			name: 'menu-MOV_4211',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_4211/QMenuMov4211.vue'),
			beforeEnter: [updateQueryParams],
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '4211',
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
				limitations: ['movie' /* DB */]
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
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
	]
}
