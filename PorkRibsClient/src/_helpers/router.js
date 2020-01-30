import Vue from 'vue';
import Router from 'vue-router';

import HomePage from '../components/HomePage';
import LoginPage from '../components/LoginPage';
import Item1 from '../components/Item1';
import Item2 from '../components/Item2';
import Profile from '../components/Profile';
import Admin from '../components/Admin';

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    { path: '/', component: HomePage },
    { path: '/login', component: LoginPage },
    { path: '/item1', component: Item1 },
    { path: '/item2', component: Item2 },
    { path: '/admin', component: Admin },
    { path: '/profile/', component: Profile },
    { path: '/admin/', component: Admin },
    { path: '*', redirect: '/' }
  ]
});

router.beforeEach((to, from, next) => {

  const publicPages = ['/','/login', '/home', '/item1', '/item2'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if(to.path=='/profile' && !loggedIn){
    return next('/login');
  }

  next();
})