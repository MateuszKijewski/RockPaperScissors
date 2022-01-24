<template>
    <div
        class="shadow-xl rounded-xl text-center bg-white dark:bg-dark dark:text-light text-dark dark-border-light border"
    >
        <h1 class="text-3xl font-bold text-orange-500">Welcome Back</h1>

        <div class="text-left flex flex-col p-4">
            <label>Email</label>

            <input
                v-model="user.email"
                type="email"
                placeholder="email..."
                class="px-6 py-2 rounded-lg dark:bg-dark border-2 dark:border-light shadow-md focus:outline-none focus:border-2"
                :class="failed ? 'dark:border-cred' : null"
            />
        </div>
        <div class="text-left flex flex-col p-4">
            <label>Password</label>

            <input
                v-model="user.password"
                type="password"
                placeholder="pasword..."
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
</template>

<script>
import { useRouter } from 'vue-router';
import useVuelidate from '@vuelidate/core';
import { required, email } from '@vuelidate/validators';
import { reactive, ref } from 'vue';
import { useApi } from '@/composables/useApi';
import { useAuth } from '@/state/useAuth';
export default {
    name: 'Register',
    setup() {
        const user = reactive({
            email: '',
            password: '',
        });
        const router = useRouter();
        const authStore = useAuth();
        const { post, data } = useApi('Auth/Login');
        const rules = {
            email: { required, email },
            password: { required },
        };
        const failed = ref(false);
        const logIn = () => {
            failed.value = false;

            console.log(v$);

            if (!v$.$invalid) {
                post(user).then(() => {
                    authStore.setUser(data.value);
                    router.push({ name: 'listGames' });
                });
            }
        };
        const v$ = useVuelidate(rules, user);

        return { user, logIn, failed, v$ };
    },
};
</script>

<style></style>
