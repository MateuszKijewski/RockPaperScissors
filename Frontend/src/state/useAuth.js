import { defineStore } from 'pinia';
import useToken from '@/composables/useToken';

const { setToken, token } = useToken();

export const useAuth = defineStore({
    id: 'useAuth',
    state: () => ({
        user: null,
        token: token,
        refreshToken: null,
        loading: false,
        isLogged: false,
    }),
    getters: {
        auth: (state) => state.isLogged,
        hasToken: (state) => state.token,
    },
    actions: {
        setUser(data) {
            this.$patch((state) => {
                setToken(data.authToken.token);
                state.refreshToken = data.refreshToken;
                state.isLogged = true;
            });
        },
        setUserData(data) {
            this.$patch((state) => {
                state.isLogged = true;
                state.user = data;
            });
        },
        setIsLogged(flag) {
            this.isLogged = flag;
        },
        logoutUser() {
            setToken(null);
            this.$patch((state) => {
                state.user = null;
                state.isLogged = false;
                state.refreshToken = null;
            });
        },
    },
});
