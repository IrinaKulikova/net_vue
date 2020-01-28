import config from 'config';
import { httpClient } from '../_helpers';
import { store } from '../_store';

export const userService = {
    login,
    logout,
    getAll
};

function login(username, password) {

    const data = JSON.stringify({ username, password });

    const requestOptions = {
        headers: {
            'Content-Type': 'application/json;charset=UTF-8',
            "Access-Control-Allow-Origin": "*"
        }
    };

    return httpClient.post(`${config.apiUrl}/authentication`, data, requestOptions)
        .then(response => {
            if (response.data.accessToken && response.data.refreshToken) {
                localStorage.setItem('user', JSON.stringify(response.data));
            }
            return user;
        })
        .catch((err) => {
            console.log("AXIOS ERROR: ", err);
        });
}


function logout() {
    localStorage.removeItem('user');
}

function getAll() {

    return httpClient.get(`${config.apiUrl}/admin/users`)
        .then((response) => {

            // var usersJSON = response.data;
            // var a = JSON.parse(usersJSON);
            // console.log("aaa");
            // console.log(response.data);
            // const data = JSON.parse(response.data);

            if (response.status !== 200) {
                if (response.status === 401) {
                    logout();
                    location.reload(true);
                }

                const error = (response.data && response.data.message) || response.statusText;
                return Promise.reject(error);
            }

            return data;
        })
        .catch((err) => {
            console.log("AXIOS ERROR: ", err);
        });
}