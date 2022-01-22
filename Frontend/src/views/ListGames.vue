<template>
    <div
        class="min-h-screen flex-1 bg-gray-200 dark:bg-dark p-4 dark:text-light text-dark flex justify-center items-center"
    >
        <div
            class="dark:bg-darken bg-white w-full md:max-w-4xl rounded-lg shadow-xl"
        >
            <div class="h-12 flex justify-between items-center m-4">
                <div>
                    <h1 class="text-3xl font-bold">Lobbies</h1>
                </div>
                <div>
                    <div
                        class="flex items-center justify-center w-full shadow-md rounded-full"
                    ></div>
                </div>
            </div>
            <ListGameItem v-for="game in games" :key="game.id" :game="game" />
            <div class="p-6">
                <button
                    class="p-2 bg-cred hover:bg-red-600 w-full rounded-lg shadow text-xl font-medium uppercase text-white"
                >
                    Create new Game
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import ListGameItem from '@/components/ListGameItem.vue';
import { ref } from 'vue';
export default {
    name: 'Games',

    components: { ListGameItem },
    setup() {
        const games = ref([
            {
                id: '3334-3343-2233',
                nick: 'Jarek',
            },
        ]);
        return {
            games,
        };
    },
};
</script>

<style></style>
