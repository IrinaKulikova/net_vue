import axios from 'axios';
import { Promise } from "es6-promise";
import { router } from '.';

export const authInterceptor = {
  httpInterceptor
};

function httpInterceptor(){

  axios.interceptors.response.use((response) => {
    return response;
  }, (error) => {
    if (error.response.status !== 401) {
      return new Promise((resolve, reject) => {
        reject(error);
      });
    }

    // Logout user if token refresh didn't work or user is disabled
    if (error.config.url == '/authentication/refresh' || error.response.message == 'Refresh token not found') {
      
      store.clear();
      router.push("/login");

      return new Promise((resolve, reject) => {
        reject(error);
      });
    }

    // Try request again with new token
    return TokenStorage.getNewToken()
      .then((token) => {

        // New request with new token
        const config = error.config;
        config.headers['Authorization'] = `Bearer ${token}`;

        return new Promise((resolve, reject) => {
          axios.request(config).then(response => {
            resolve(response);
          }).catch((error) => {
            reject(error);
          })
        });

      })
      .catch((error) => {
      	Promise.reject(error);
      });
  });
}