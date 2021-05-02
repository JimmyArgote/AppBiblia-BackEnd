window._ = require('lodash');
window.Vue = require('vue')

import Vue from 'vue';
import App from './App.vue';
import router from './router';
import Vuetify from 'vuetify';
//import '@mdi/font/css/materialdesignicons.css' // Ensure you are using css-loader
//import "vuetify/dist/vuetify.min.css";

Vue.config.productionTip = false;
//vuetify
Vue.use(Vuetify);
//vue components
Vue.component('App', require('./App.vue').default);
var livros = null;
new Vue({
    el: '#app',
    router,
    data: {
        json: function(){
            return {
                json: this.getLivros(),
            }
        }
    },
    created: function () {
        var _this = this;
        livros = this.getLivros()
    },
    methods: {
        getLivros () {
            var _this = this;
            jQuery.getJSON('/Livros/4/16', function (json) {
                _this.json = json;
                console.log("VUE JS");
                console.log(json);
            });
            return json;
        },
    },
    template: '<App/>',
    components: { App },
    vuetify: new Vuetify(),
});

