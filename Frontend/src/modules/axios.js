import useToken from '@/composables/useToken';
import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7182/api/',
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    },
});

api.interceptors.request.use(
    (config) => {
        console.log(`Request:[${config.url}]`, config);

        const { token } = useToken();
        if (config.headers === undefined) return;
        if (token.value.length > 0) {
            config.headers.common['Authorization'] = `Bearer ${token.value}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);
api.interceptors.response.use(
    (response) => {
        console.log(`Respons:[]`, response);
        if (response === undefined) return;
        if (response.status === 200 || response.status === 201) {
            return Promise.resolve(response);
        } else {
            return Promise.reject(response);
        }
    },
    (error) => {
        if (error.response === undefined) return;
    }
);

export default api;
