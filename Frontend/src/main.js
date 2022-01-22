import { createApp } from 'vue';
import './tailwind.css';
import { createPinia } from 'pinia';
import App from './App.vue';
import { routes } from './routes';
import { createRouter, createWebHistory } from 'vue-router';

const app = createApp(App);

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// router.onBeforeRo
app.use(router).use(createPinia());
app.mount('#app');
