<template>
    <div class="flex h-screen dark:bg-dark">
        <div class="m-auto">
            <div
                class="p-16 shadow-xl rounded-xl text-center bg-white dark:bg-dark dark:text-light text-dark"
            >
                <h1 class="text-3xl font-bold text-orange-500">Welcome</h1>
                <p>Select your name:</p>
                <div class="text-left p-4">
                    <input
                        v-model="user"
                        type="email"
                        placeholder="name"
                        class="px-6 py-2 rounded-lg dark:bg-dark border-2 dark:border-light shadow-md focus:outline-none focus:border-2"
                        :class="failed ? 'dark:border-cred' : null"
                    />
                </div>
                <button
                    @click="logIn"
                    type="button"
                    class="bg-gradient-to-r from-cred to-[#FF1E38] py-3 pr-8 pl-8 dark:text-light font-semibold rounded-xl focus:ring-2 m-4"
                >
                    Start!
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue';
import { useUser } from '@/state/useUser';
import { useStorage } from '@vueuse/core';
import { useRouter } from 'vue-router';
export default {
    name: 'welcome',
    setup() {
        const user = ref('');
        const router = useRouter();
        onBeforeMount(() => {
            useStorage('rps-app', user);
        });
        const userAuth = useUser();
        const failed = ref(false);
        const logIn = () => {
            failed.value = false;
            console.log(user.value);
            if (user.value === '') {
                failed.value = true;
                return;
            }
            router.push({
                name: 'listGames',
            });
            // userAuth.setUser(user.value);
        };
        return { user, logIn, failed };
    },
};
</script>

<style></style>
