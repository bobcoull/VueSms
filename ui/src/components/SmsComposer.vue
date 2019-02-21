<template>
  <v-container class="sms-composer">
    <v-layout row wrap>
      <v-flex xs12>
          <v-form v-model="valid">
              <v-text-field label="Mobile Number"></v-text-field>
              <v-textarea label="Sms Content"></v-textarea>
              <v-btn v-on:click='smsSend'>Send</v-btn> 
          </v-form>
          {{info}}
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
    export default {
        name: 'SmsComposer',
        data: () => ({
            valid: true,
            datetime: '2018-10-01 00:00:00',
            info: 'info'
        }),
        methods: {
            smsSend: function (event) {
                // `this` inside methods points to the Vue instance
                fetch('https://localhost:44391/api/Sms?fromDate=20%2F02%2F2019&toDate=22%2F02%2F2019', { mode: 'no-cors' })
                    .then(response => response.json())
                    .then(function (data) {
                        this.info = data
                        alert('bob')
                    })
                // `event` is the native DOM event
                if (event) {
                    alert(event.target.tagName)
                }
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
</style>
