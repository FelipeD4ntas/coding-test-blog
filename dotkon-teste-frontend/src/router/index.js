import registerRoutes from '@/modules/cadastro/router'
import dashboardRoutes from '@/modules/dashboard/router'
import authRoutes from '@/modules/login/router'
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  ...authRoutes,
  ...registerRoutes,
  ...dashboardRoutes,
  { path: '', redirect: '/dashboard' },
  { path: '/:catchAll(.*)', redirect: '/dashboard' }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach(async (to, from, next) => {
  const token = window.localStorage.getItem('AUTH_TOKEN')
  if (to.matched.some(route => route.meta.requiredAuth)) {
    if (!token) {
      return next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    }

    try {
      return next()
    } catch (error) {
      return next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    }
  }

  next();
});

export default router