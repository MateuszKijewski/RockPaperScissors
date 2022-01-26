<template>
  <div class="card mb-8 w-full">
    <div v-if="noHeader" class="py-8 px-4 border-b border-gray-200">
      <slot name="header" />
    </div>
    <div class="p-8">
      <slot name="body" />
    </div>
  </div>
</template>

<script lang="ts">
  export default {
    name: 'Card',
    props: {
      noHeader: {
        type: Boolean,
        default: false,
      },
    },
    setup(props) {
      return {}
    },
  }
</script>

<style></style>
