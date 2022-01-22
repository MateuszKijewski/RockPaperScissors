import { defineStore } from 'pinia';
import { useStorage } from '@vueuse/core';

export const useUser = defineStore({
    id: 'useUser',
    state: () => ({
        user: '',
    }),
    getters: {},
    actions: {},
});
