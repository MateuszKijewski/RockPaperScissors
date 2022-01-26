<template>
  <select
    v-model="selected"
    class="form-select appearance-none block w-full px-3 py-1.5 text-base font-normal text-gray-700 bg-white bg-clip-padding bg-no-repeat border border-solid border-gray-300 rounded transition ease-in-out m-0 focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
    aria-label="Default select example"
  >
    <option v-for="l in availableLocales" :key="l" :value="l">{{ l }}</option>
  </select>
</template>

<script lang="ts">
  export default {
    name: 'LanguageSwitcher',
    setup() {
      const { t, availableLocales, locale } = useI18n()
      const selected = ref('')
      selected.value = locale.value
      const toggleLocales = () => {
        const locales = availableLocales
        locale.value =
          locales[(locales.indexOf(locale.value) + 1) % locales.length]
      }
      watchEffect(() => {
        locale.value = selected.value
      })
      return { availableLocales, selected }
    },
  }
</script>
