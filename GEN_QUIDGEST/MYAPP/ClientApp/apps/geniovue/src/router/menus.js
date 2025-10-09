// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { updateQueryParams } from './routeUtils.js'

export default function getMenusRoutes()
{
	return [
		{
			path: '/:culture/:system/MOV/menu/MOV_61',
			name: 'menu-MOV_61',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_61/QMenuMov61.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '61',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_71',
			name: 'menu-MOV_71',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_71/QMenuMov71.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '71',
				baseArea: 'USERP',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_211',
			name: 'menu-MOV_211',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_211/QMenuMov211.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '211',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_81',
			name: 'menu-MOV_81',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_81/QMenuMov81.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '81',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_51',
			name: 'menu-MOV_51',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_51/QMenuMov51.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '51',
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
	]
}
