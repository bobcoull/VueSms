<template>
  <div class="sms-send">
    <h1>sms send</h1>
    <SmsComposer />
  </div>
</template>

<script>
// @ is an alias to /src
import SmsComposer from '@/components/SmsComposer.vue'

export default {
  name: 'Send',
  components: {
      SmsComposer
  }
}
</script>
