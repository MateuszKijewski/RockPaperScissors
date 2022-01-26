<template>
  <div
    class="w-full"
    :class="[
      {
        relative: type === 'password',
      },
    ]"
  >
    <div class="flex justify-content-between">
      <label
        class="font-semiBold mb-2 text-sm font-bold text-gray-600"
        :for="nameId"
        >{{ label }}
      </label>
      <!-- <template v-if="forgotPassword">
        <a class="text--dark-pink" href="/forgot-password"> forgot passord?</a>
      </template> -->
    </div>

    <input
      :id="nameId"
      ref="inputUI"
      :value="inputValue"
      :type="type"
      placeholder=""
      class="w-full py-2 px-4 bg-white border border-gray-200 rounded-md placeholder-gray-400 text-gray-500 focus:border-orange outline-none transition placeholder-font-light font-weight-normal"
      :class="[
        {
          'border-valid': meta.valid,
          'border-error': errorMessage,
        },
        size,
      ]"
      @input="handleChange"
      @blur="handleBlur"
    />
    <!-- <template v-if="type === 'password'">
      <span class="icon__eye" @click="togglePassword"
        ><img src="/icons/eye.svg" alt=""
      /></span>
    </template> -->
    <div
      v-show="errorMessage || meta.valid"
      class="text-xs mt-1 pl-3"
      :class="[{ 'text-valid': meta.valid, 'text-error': errorMessage }]"
    >
      {{ errorMessage || successMessage }}
    </div>
  </div>
</template>

<script lang="ts">
  import { useField } from 'vee-validate'
  import { PropType } from 'vue'

  type FDInputSizeType = PropType<'small |default | large'>
  export default {
    props: {
      nameId: {
        type: String,
        default: 'id',
      },
      type: {
        type: String,
        default: 'text',
      },
      size: {
        type: String as FDInputSizeType,
        default: 'default',
      },
      value: {
        type: String,
        default: '',
      },
      name: {
        type: String,
        required: true,
      },
      label: {
        type: String,
        required: true,
      },
      successMessage: {
        type: String,
        default: '',
      },
      placeholder: {
        type: String,
        default: '',
      },
    },
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    setup(props: any) {
      // @ts-ignore

      const {
        value: inputValue,
        errorMessage,
        handleBlur,
        handleChange,
        meta,
      } = useField(props.name, undefined, {
        initialValue: props.value,
      })

      return {
        handleChange,
        handleBlur,
        errorMessage,
        inputValue,
        meta,
      }
    },
  }
</script>

<style scoped>
  .small {
    @apply py-1 px-2 text-sm;
  }
  .large {
    @apply py-3 px-6 text-xl;
  }
</style>
