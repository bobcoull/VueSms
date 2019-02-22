<template>
  <div class="sms-list">
    <h1>sms list</h1>
    <SmsList/>
  </div>
</template>

<script>
  // @ is an alias to /src
  import SmsList from '@/components/SmsList.vue'

  export default {
  name: 'List',
  components: {
    SmsList
    }
  }
</script>
