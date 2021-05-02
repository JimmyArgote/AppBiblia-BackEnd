import Vue from 'vue'
import VueRouter from 'vue-router';

//vue router
Vue.use(VueRouter)

const routes = [
    { path: '/', name: 'home', component: require('./components/Home/Home.vue').default },
    { path: '/about', name: 'about', component: require('./components/About.vue').default },
    { path: '/contact', name: 'contact', component: require('./components/Contact.vue').default },
    { path: '/livros', name: 'livros', component: require('./components/Livros.vue').default },
    { path: '/capitulos', name: 'capitulos', component: require('./components/Capitulos.vue').default },
    { path: '/versiculos', name: 'versiculos', component: require('./components/Versiculos.vue').default },
];

const router = new VueRouter({
    mode: 'history', //removes # (hashtag) from url
    base: '/',
    fallback: true, //router should fallback to hash (#) mode when the browser does not support history.pushState
    routes // short for `routes: routes`
});

export default router;