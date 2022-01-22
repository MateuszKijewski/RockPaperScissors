<template>
    <teleport to="body">
        <transition
            enter-active-class="transition ease-out duration-200 transform"
            enter-from-class="opacity-0"
            enter-to-class="opacity-100"
            leave-active-class="transition ease-in duration-200 transform"
            leave-from-class="opacity-100"
            leave-to-class="opacity-0"
        >
            <div
                ref="modal-backdrop"
                v-show="showModal"
                class="fixed z-10 inset-0 overflow-y-auto bg-light bg-opacity-50"
            >
                <div
                    class="flex items-start justify-center min-h-screen pt-24 text-center"
                >
                    <div
                        class="dark:bg-darken dark:text-light text-dark bg-white rounded-lg text-left overflow-hidden shadow-xl p-8 w-1/2"
                        role="dialog"
                        ref="modal"
                        aria-modal="true"
                        aria-labelledby="modal-headline"
                    >
                        <slot>Siema siema</slot>
                    </div>
                </div>
            </div>
        </transition>
    </teleport>
</template>
<script>
import { onMounted, ref, watch } from 'vue';

export default {
    name: 'modal',
    props: {
        show: {
            type: Boolean,
            default: false,
        },
    },
    setup(props) {
        const showModal = ref(false);
        onMounted(() => (showModal.value = props.show));
        watch(
            () => props.show,
            (show) => {
                console.log(props.show);
                showModal.value = show;
            }
        );

        return {
            showModal,
        };
    },
};
</script>

<style></style>
