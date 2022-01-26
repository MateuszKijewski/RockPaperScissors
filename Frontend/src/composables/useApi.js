import { computed, ref, inject } from 'vue';
import { useRouter } from 'vue-router';

export const useApi = (endpoint) => {
    const router = useRouter();
    const $http = inject('$http');
    const data = ref();
    const loading = ref(false);
    const error = ref();

    const post = (payload) => {
        loading.value = true;
        error.value = undefined;

        return $http
            .post(endpoint, payload)
            .then((res) => (data.value = res.data))
            .catch((e) => {
                error.value = e;

                throw e;
            })
            .finally(() => (loading.value = false));
    };

    const get = (query, config) => {
        loading.value = true;
        error.value = undefined;

        let queryString = '';

        if (query) {
            queryString =
                '?' +
                Object.entries(query)
                    .map(
                        ([key, value]) =>
                            `${encodeURIComponent(key)}=${encodeURIComponent(
                                value
                            )}`
                    )
                    .join('&');
        }

        return $http
            .get(endpoint + queryString, config)
            .then((res) => (data.value = res.data))
            .catch((e) => {
                error.value = e;
                throw e;
            })
            .finally(() => (loading.value = false));
    };

    // @ts-ignore
    const del = () => {
        loading.value = true;
        error.value = undefined;

        return $http
            .delete(endpoint)
            .then((res) => (data.value = res.data))
            .catch((e) => {
                error.value = e;

                throw e;
            })
            .finally(() => (loading.value = false));
    };

    // @ts-ignore
    const put = (payload) => {
        loading.value = true;
        error.value = undefined;

        return $http
            .put(endpoint, payload)
            .then((res) => (data.value = res.data))
            .catch((e) => {
                error.value = e;

                throw e;
            })
            .finally(() => (loading.value = false));
    };

    const errorMessage = computed(() => {
        console.log('?? compute', error.value);

        if (error.value) {
            return error.value.message;
        }
    });

    const errorDetails = computed(() => {
        if (error.value && error.value.response) {
            return error.value.response.data.message;
        }
    });

    const errorFields = computed(() => {
        if (error.value && Array.isArray(error.value.response.data.message)) {
            return error.value.response.data.message.reduce((acc, msg) => {
                let [field] = msg.split(' ');

                return acc;
            }, {});
        }
    });

    return {
        loading,
        data,
        error,
        get,
        post,
        del,
        put,
        errorMessage,
        errorDetails,
        errorFields,
    };
};
