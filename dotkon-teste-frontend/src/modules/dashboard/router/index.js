import Dashboard from '@/modules/dashboard/views/Dashboard'

export default [
  {
    path: '/dashboard',
    component: Dashboard,
    meta: { requiredAuth: false }
  }
]