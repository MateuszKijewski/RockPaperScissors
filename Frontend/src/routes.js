import Welcome from '@/views/Welcome.vue';
import NotFound from '@/views/NotFound.vue';
import Game from '@/views/Game.vue';
import ListGames from '@/views/ListGames.vue';
/** @type {import('vue-router').RouterOptions['routes']} */
export const routes = [
    { path: '/', component: Welcome, name: 'Welcome', meta: { title: 'Home' } },
    {
        path: '/games',
        name: 'listGames',
        component: ListGames,
        meta: { title: 'Game' },
    },
    {
        path: '/games/:id',
        name: 'Game',
        component: Game,
        meta: { title: 'Game' },
    },

    { path: '/:path(.*)', component: NotFound },
];
