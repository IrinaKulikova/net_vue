import Vue from 'vue';
import { store } from './_store';
import { router } from './_helpers';
import { httpClient } from './_helpers';
import App from './App';
import Footer from './components/Footer';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.component('my-footer',Footer);
Vue.use

new Vue({
    el: '#app',
    router,
    store,
    httpClient,
    render: h => h(App)
});
