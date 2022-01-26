<template>
    <div
        class="shadow-xl rounded-xl text-center bg-white dark:bg-dark dark:text-light text-dark dark-border-light border"
    >
        <h1 class="text-3xl font-bold text-orange-500">Welcome</h1>
        <div class="text-left flex flex-col p-4">
            <label>First Name:</label>

            <input
                v-model="user.firstName"
                type="text"
                class="px-6 py-2 rounded-lg dark:bg-dark border-2 dark:border-light shadow-md focus:outline-none focus:border-2"
                :class="failed ? 'dark:border-cred' : null"
            />
        </div>

        <div class="text-left flex flex-col p-4">
            <label>Last Name:</label>

            <input
                v-model="user.lastName"
                type="text"
                class="px-6 py-2 rounded-lg dark:bg-dark border-2 dark:border-light shadow-md focus:outline-none focus:border-2"
                :class="failed ? 'dark:border-cred' : null"
            />
        </div>
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
        <h2 v-if="success" class="text-cgreen m-5 text-3xl">
            Your account was created! go to Login!
        </h2>
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
import { useUser } from '@/state/useUser';
import { useRouter } from 'vue-router';
import useVuelidate from '@vuelidate/core';
import { required, email } from '@vuelidate/validators';
import { reactive, ref } from 'vue';
import { useApi } from '@/composables/useApi';
export default {
    name: 'Register',
    setup() {
        const user = reactive({
            firstName: '',
            lastName: '',
            email: '',
            password: '',
        });
        const rules = {
            firstName: { required },
            lastName: { required },
            email: { required, email },
            password: { required },
        };
        const v$ = useVuelidate(rules, user);

        const router = useRouter();
        const { post, data } = useApi('Auth/Register');
        const success = ref(false);
        const failed = ref(false);

        const logIn = () => {
            console.log(v$.$invalid);
            success.value = false;
            if (!v$.$invalid) {
                post(user).then(() => {
                    console.log(data);
                    success.value = true;
                });
            } else {
                failed.value = false;
            }
        };

        return { user, logIn, failed, v$, success };
    },
};
</script>

<style></style>
