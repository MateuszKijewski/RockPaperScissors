<template>
  <component
    :is="import(`../../../../assets/svg/${name}.svg`)"
    role="img"
    class="inline-block fill-current"
    style="height: 1em; width: 1em"
  />
</template>

<script lang="ts">
  export default {
    props: {
      name: {
        type: String,
        required: true,
      },
    },
  }
</script>
