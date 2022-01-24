import { createApp } from 'vue';
import './tailwind.css';
import { createPinia } from 'pinia';
import App from './App.vue';
import { routes } from './routes';
import { createRouter, createWebHistory } from 'vue-router';
import api from '@/modules/axios';
import { VueSignalR } from '@/modules/signalR/index';
import { HttpTransportType } from '@microsoft/signalr';
import { useAuth } from '@/state/useAuth';
const app = createApp(App);

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const { token } = useAuth();
    if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (!token) {
            next({ name: 'welcome' });
        } else {
            next();
        }
    } else {
        next();
    }
});
// router.onBeforeRo
app.use(router).use(createPinia());
app.mount('#app');
app.provide('$http', api);

// app.use(VueSignalR, {
//     url: 'https://localhost:7182/',
//     options: {},
//     provider: 'game',
//     withUrlOptions: {
//         skipNegotiation: true,
//         transport: HttpTransportType.WebSockets,
//         withCredentials: false,
//     },
// });

export default app;
