import config from 'config';
import { httpClient } from '../_helpers';


export const userService = {
    login,     
    logout,
    getUsers
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
        })
        .catch((err) => {
            console.log("AXIOS ERROR: ", err);
        });
}


function logout() {
    localStorage.removeItem('user');
}

function getUsers() {

    return httpClient.get(`${config.apiUrl}/admin/users`)
        .then((response) => {

            if (response) {
                if (response && response.status !== 200) {
                    if (response.status === 401) {
                        logout();
                        location.reload(true);
                    }

                    const error = (response.data && response.data.message) || response.statusText;
                    return Promise.reject(error);
                }
                
                return response.data;
            }
            return null;
        })
        .catch((err) => {
            console.log("AXIOS ERROR: ", err);
        });
}
