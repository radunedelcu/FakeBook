import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import RegisterView from '../views/Authentication/RegisterView.vue'
import LoginView from '../views/Authentication/LoginView.vue'
import ProfilePage from '../views/Authentication/ProfilePage.vue'

const routes = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/home',
    name: 'home',
    component: HomeView,
    beforeEnter: checkAuth
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/profile/:id',
    name: 'profile',
    component: ProfilePage,
    props: true,
    beforeEnter: checkAuth,
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

function checkAuth(to, from, next) 
{
    if (localStorage.getItem('userToken')) next();
    else next("/login");
}


export default router
