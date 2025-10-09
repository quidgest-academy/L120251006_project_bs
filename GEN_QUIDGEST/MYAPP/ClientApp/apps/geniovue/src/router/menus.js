// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { updateQueryParams } from './routeUtils.js'

export default function getMenusRoutes()
{
	return [
		{
			path: '/:culture/:system/MOV/menu/MOV_471',
			name: 'menu-MOV_471',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_471/QMenuMov471.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '471',
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
			path: '/:culture/:system/MOV/menu/MOV_451',
			name: 'menu-MOV_451',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_451/QMenuMov451.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '451',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_441',
			name: 'menu-MOV_441',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_441/QMenuMov441.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '441',
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
			path: '/:culture/:system/MOV/menu/MOV_461',
			name: 'menu-MOV_461',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_461/QMenuMov461.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '461',
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
			path: '/:culture/:system/MOV/menu/MOV_431',
			name: 'menu-MOV_431',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_431/QMenuMov431.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '431',
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
	]
}
