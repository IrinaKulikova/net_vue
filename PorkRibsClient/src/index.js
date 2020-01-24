import Vue from 'vue';
import axios from 'axios';
import { Promise } from "es6-promise";

import { store } from './_store';
import { router } from './_helpers';
import App from './app/App';



export const httpClient = axios.create({
    baseURL: "https://localhost:4445/api/v1/",
    timeout: 3000,
    headers: {
        "Content-Type": "application/json",
    }
});

Vue.prototype.$http = httpClient;
Vue.prototype.axios = axios;

httpClient.interceptors.response.use((response) => {
    return response;
  }, (error) => {
    if (error.response.status !== 401) {
      return new Promise((resolve, reject) => {
        reject(error);
      });
    }
});
// const myInterceptor = axios.create();

// myInterceptor.interceptors.response.use((response) => {
//     return response;
//   }, (error) => {
//     if (error.response.status !== 401) {
//       return new Promise((resolve, reject) => {
//         console.log("error");
//       });
//     }
// });

// Vue.http.interceptors.push(function(request) {
//     console.log("aaa");
//   });

//Vue.prototype.$http = myInterceptor;

var vue = new Vue({
    el: '#app',
    router,
    store,  
    render: h => h(App)
});
