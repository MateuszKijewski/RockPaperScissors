import { useStorage } from '@vueuse/core';
import { readonly } from 'vue';

export default () => {
    const token = useStorage('rps-game', '');

    const getToken = () => {
        return token.value;
    };
    const setToken = (data) => {
        token.value = data;
    };
    return {
        token: readonly(token),
        getToken,
        setToken,
    };
};
