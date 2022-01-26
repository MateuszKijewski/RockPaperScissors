import { defineStore } from 'pinia';
import { useStorage } from '@vueuse/core';
import { useApi } from '@/composables/useApi';
import { useRouter } from 'vue-router';
const StartGameApi = useApi('Game/StartGame');

export const useGame = defineStore({
    id: 'useGame',
    state: () => ({
        games: [],
        currentGameId: '',
        currentGame: { guestId: null },
        loading: false,
        toResults: 5,
    }),
    getters: {},
    actions: {
        joinGame() {},
        setCurrentGame(game) {
            this.$patch(() => {
                this.currentGame = game;
            });
        },
        setCurrentGameId(id) {
            this.currentGameId = id;
        },
        makeMove(move) {
            console.log(move);
            const { loading, data, post } = useApi(
                'Game/MakeMove/' + this.currentGameId
            );
            post(move).then(() => {
                data;
            });
        },
        getAllGames() {
            const { loading, data, post } = useApi('Game/StartGame');
        },
        getMyGames() {
            const { loading, data, get } = useApi('Game/GetMyGames');
        },
        createNewGame(id) {
            const router = useRouter();
            // const { loading, data, post } = useApi('Game/StartGame');
            console.log(StartGameApi);
            this.loading = StartGameApi.loading;
            StartGameApi.post(this.toResults).then((e) => {
                this.loading = StartGameApi.loading;
                console.log(router);
                this.currentGame = StartGameApi.data.value;
                router.push({
                    name: 'Game',
                    params: { id: StartGameApi.data.value.id },
                });
            });
        },
    },
});
