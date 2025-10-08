// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { updateQueryParams } from './routeUtils.js'

export default function getMenusRoutes()
{
	return [
		{
			path: '/:culture/:system/MOV/menu/MOV_41',
			name: 'menu-MOV_41',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_41/QMenuMov41.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '41',
				baseArea: 'FAVOR',
				hasInitialPHE: false,
				humanKeyFields: [],
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
				baseArea: 'USERP',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
		{
			path: '/:culture/:system/MOV/menu/MOV_11',
			name: 'menu-MOV_11',
			component: () => import('@/views/menus/ModuleMOV/MenuMOV_11/QMenuMov11.vue'),
			meta: {
				routeType: 'menu',
				module: 'MOV',
				order: '11',
				baseArea: 'MOVIE',
				hasInitialPHE: false,
				humanKeyFields: ['ValTitle'],
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
				baseArea: 'COMME',
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
				baseArea: 'CHARA',
				hasInitialPHE: false,
				humanKeyFields: ['ValName'],
			}
		},
	]
}
