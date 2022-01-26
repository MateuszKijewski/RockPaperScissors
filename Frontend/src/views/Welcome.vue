<template>
    <div class="flex h-screen dark:bg-dark">
        <div class="m-auto w-3/4 text-center lg:w-1/2">
            <div class="w-100 p-6 bg-slate-300">
                <div
                    class="relative inline-block w-10 mr-2 align-middle select-none transition duration-200 ease-in"
                >
                    <input
                        type="checkbox"
                        name="toggle"
                        id="toggle"
                        v-model="isRegister"
                        class="toggle-checkbox absolute block w-6 h-6 rounded-full bg-dark border-4 appearance-none cursor-pointer"
                    />
                    <label
                        for="toggle"
                        class="toggle-label block overflow-hidden h-6 rounded-full bg-gray-800 cursor-pointer"
                    ></label>
                </div>
                <label for="toggle" class="text-gray-700">{{
                    isRegister ? 'login' : 'register'
                }}</label>
            </div>
            <Register v-if="isRegister" />
            <Login v-else />
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router';
import useVuelidate from '@vuelidate/core';
import { required, email } from '@vuelidate/validators';
import { reactive, ref } from 'vue';
import { useApi } from '@/composables/useApi';
import Register from '@/components/Register.vue';
import Login from '@/components/Login.vue';

export default {
    name: 'welcome',
    components: { Register, Login },
    setup() {
        const user = reactive({
            email: '',
            password: '',
        });
        const isRegister = ref(false);
        const router = useRouter();
        const { post, data } = useApi('Auth/Login');
        const rules = {
            email: { required, email },
            password: { required },
        };
        const failed = ref(false);
        const logIn = () => {
            failed.value = false;

            console.log(v$.$invalid);

            if (!v$.$invalid) {
                post(user).then(() => {
                    console.log(data);
                });
            }
        };
        const v$ = useVuelidate(rules, user);

        return { isRegister, user, logIn, failed, v$ };
    },
};
</script>

<style>
.toggle-checkbox:checked {
    @apply: right-0 border-green-400;
    right: 0;
    border-color: #68d391;
}
.toggle-checkbox:checked + .toggle-label {
    @apply: bg-green-400;
    background-color: #68d391;
}
</style>
