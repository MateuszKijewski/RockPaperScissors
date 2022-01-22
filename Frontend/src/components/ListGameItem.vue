<template>
    <div class="px-6">
        <div
            class="flex justify-between items-center h-16 p-4 my-6 rounded-lg border border-gray-100 shadow-md"
        >
            <div class="flex items-center">
                <div class="ml-2">
                    <div class="text-sm font-semibold">{{ game.nick }}</div>
                </div>
            </div>
            <div>
                <router-link
                    :to="{ name: 'Game', params: { id: game.id } }"
                    class="bg-cgreen hover:bg-green-600 dark:text-light p-2 rounded-full shadow-md flex justify-center items-center"
                >
                    <FightIcon class="w-8 h-8 fill-light" />
                </router-link>
            </div>
        </div>
    </div>
</template>

<script>
import FightIcon from '@/assets/icons/fight.svg';

export default {
    name: 'ListGameItem',
    props: {
        game: {
            type: Object,
            required: true,
        },
    },
    components: { FightIcon },
    setup() {},
};
</script>

<style></style>
