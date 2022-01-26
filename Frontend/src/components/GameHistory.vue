<template>
    <div class="">
        <h2 class="text-xl font-semibold mb-2 text-center">History</h2>
        <div
            v-for="item in data"
            class="flex w-full justify-between border-2 text-center p-2 rounded-md mb-2"
        >
            <div class="flex-1 space-x-4">
                <p class="text-lg">{{ item.host.firstName }}</p>
                <div class="w-6"></div>
            </div>
            <div class="flex-1 w-100">
                {{ item.hostScore }} : {{ item.guestScore }}
            </div>
            <div class="flex-1 space-x-4">
                <p class="text-lg">{{ item.guest.firstName }}</p>
                <div class="w-6"></div>
            </div>
        </div>
    </div>
</template>

<script>
import { useApi } from '@/composables/useApi';
import { onMounted } from 'vue';

export default {
    name: 'GameHistory',
    setup() {
        const { data, get } = useApi('Game/GetMyGames');

        onMounted(() => {
            get().then(() => {
                console.log(data.value);
            });
        });

        return { data };
    },
};
</script>

<style></style>
