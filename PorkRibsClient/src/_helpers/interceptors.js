import axios from 'axios';
import { Promise } from "es6-promise";
import { router } from '.';
import { store } from '../_store';
import config from 'config';

export const httpClient = axios.create();

httpClient.interceptors.request.use(
  config => {

    const user = JSON.parse(localStorage.getItem('user'));
    if (user && user.accessToken) {
      config.headers['Authorization'] = 'Bearer ' + user.accessToken;
    }
    config.headers['Content-Type'] = 'application/json';
    return config;
  },
  error => {
    Promise.reject(error)
  });


httpClient.interceptors.response.use((response) => {
  return response;
}, (error) => {

  if (error.response.status !== 401) {
    return new Promise((resolve, reject) => {
      reject(error);
    });
  }

  const originalRequest = error.config;

  if (error.response.status === 401) {

    const requestOptions = {
      headers: {
        'Content-Type': 'application/json',
        'DataType': 'json',
        'Access-Control-Allow-Origin': '*'
      }
    };

    const user = JSON.parse(localStorage.getItem('user'));

    const data = JSON.stringify({ 'refreshToken':user.refreshToken, 'userName': user.email});

    httpClient.post(`${config.apiUrl}/authentication/refresh`, data, requestOptions)
      .then((responseRefresh) => {
        if (responseRefresh.status == 400) {
          return new Promise((resolve, reject) => {
            store.clear();
            router.push("/login");
          });
        }

        if (responseRefresh && responseRefresh.status === 201) {
          const data = JSON.stringify(responseRefresh.data);

          localStorage.setItem('user', data);
          store.state.authentication.user = responseRefresh.data;
          store.state.authentication.loggedIn = true;

          httpClient.defaults.headers.common['Authorization'] = 'Bearer ' + store.state.authentication.user.accessToken;
          return httpClient(originalRequest);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }
});