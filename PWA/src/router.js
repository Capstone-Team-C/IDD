import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from './views/Home';

Vue.use(VueRouter)

export default new VueRouter({
  routes: [
    { path: '/', name: 'home', component: Home },
    { path: '/timesheet', name: 'timesheet', component: () => import('./views/Timesheet') },
    { path: '/about', name: 'about', component: () => import('./views/About') },
    { path: '*', name: 'not_found', component: () => import('./views/NotFound') },
  ],

  methods: {
    goBack() {
      window.history.length > 1 ? this.$router.go(-1) : this.$router.push('/')
    }   
  }
})
