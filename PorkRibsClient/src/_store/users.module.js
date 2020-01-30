import { userService } from '../_services';

export const users = {
    namespaced: true,
    state: {
        all: {}
    },
    actions: {
        getUsers({ commit }) {
            commit('getAllRequest');

            userService.getUsers()
                .then(
                    users => commit('getAllSuccess', users),
                    error => commit('getAllFailure', error)
                );
        }
    },
    mutations: {
        getAllRequest(state) {
            state.all = { loading: true };
        },
        getAllSuccess(state, users) {
            state.all = { items: users };
        },
        getAllFailure(state, error) {
            state.all = { error };
        }
    }
}