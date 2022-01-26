<script lang="ts">
  // import { defineComponent, computed } from 'vue';
  import { FDButtonTagType } from './FDButtonTagType'
  import { FDButtonType } from './FDButtonType'

  export default {
    name: 'FDButton',
    props: {
      tag: {
        type: String as FDButtonTagType,
        default: 'button',
      },
      type: {
        type: String as FDButtonType,
        default: 'default',
      },
    },
  }
</script>

<template>
  <div>
    <component :is="tag" class="btn"><slot /> </component>
  </div>
</template>

<style>
  .btn {
    @apply border border-solid cursor-pointer transition px-4 py-2 h-12 font-bold  rounded font-medium 
  focus:outline-none bg-orange text-white w-full;
  }
</style>
