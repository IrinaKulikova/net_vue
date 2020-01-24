import config from 'config';
import { authHeader } from '../_helpers';

export const userService = {
    login,
    refreshToken,
    logout,
    getAll
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(`${config.apiUrl}/authentication`, requestOptions)
        .then(handleResponse)
        .then(user => {
            if (user.accessToken && user.refreshToken) {
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
        });
}

function refreshToken(){
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, refreshToken })
    };

    return fetch(`${config.apiUrl}/authentication/refresh`, requestOptions)
        .then(handleResponse)
        .then(user => {
            if (user.accessToken && user.refreshToken) {
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
        });
}

function logout() {
    localStorage.removeItem('user');
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/admin/users`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}