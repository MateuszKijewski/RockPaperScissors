<template>
    <div
        class="min-h-screen flex-1 bg-gray-200 dark:bg-dark p-4 dark:text-light text-dark flex flex-col justify-center items-center"
    >
        <div class="h-12 flex justify-between items-center m-4">
            <div>
                <h1 v-if="user" class="text-3xl font-bold">
                    Hello {{ user.firstName }} create game or join to lobby with
                    key
                </h1>
            </div>
        </div>

        <div
            class="dark:bg-darken bg-white w-full md:max-w-4xl rounded-lg shadow-xl"
        >
            <div class="p-6">
                <button
                    @click="startGame"
                    type="button"
                    class="p-2 bg-cred hover:bg-red-600 w-full rounded-lg shadow text-xl font-medium uppercase text-white"
                >
                    Create new Game
                </button>
            </div>
        </div>
        <div
            class="dark:bg-darken bg-white w-full md:max-w-4xl rounded-lg shadow-xl mt-12"
        >
            <div class="p-6">
                <input
                    v-model="key"
                    type="text"
                    pattern="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
                    placeholder="key"
                    class="px-6 py-2 rounded-lg w-full dark:bg-dark border-2 dark:border-light shadow-md focus:outline-none focus:border-2"
                />
            </div>
            <div class="p-6">
                <button
                    @click="joinToGame"
                    type="button"
                    class="p-2 bg-cred hover:bg-red-600 w-full rounded-lg shadow text-xl font-medium uppercase text-white"
                >
                    Join to game
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import ListGameItem from '@/components/ListGameItem.vue';
import { onMounted, ref, toRefs } from 'vue';
import { useAuth } from '@/state/useAuth';
import { useApi } from '@/composables/useApi';
import { useGame } from '@/state/useGame';
import { useRouter } from 'vue-router';

export default {
    name: 'Games',

    components: { ListGameItem },
    setup() {
        const router = useRouter();
        const authStore = useAuth();
        const gameStore = useGame();
        const key = ref('');
        const { loading, get, data } = useApi('Auth/GetCurrentUser');
        const { user } = toRefs(authStore);
        onMounted(() => {
            get()
                .then(() => {
                    authStore.setUserData(data.value);
                    return;
                })
                .catch(() => {
                    router.push({
                        name: 'Welcome',
                    });
                    authStore.setIsLogged(false);
                });
        });

        const startGame = useApi('Game/StartGame');
        const joinToGame = () => {
            router.push({
                name: 'Game',
                params: { id: key.value },
                query: { type: 'join' },
            });
        };
        const CreateNewGame = () => {
            startGame.post(5).then((e) => {
                gameStore.setCurrentGame(startGame.data.value);
                router.push({
                    name: 'Game',
                    params: { id: startGame.data.value.id },
                });
            });
        };
        return {
            user,
            key,
            startGame: CreateNewGame,
            joinToGame,
        };
    },
};
</script>

<style></style>
