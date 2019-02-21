<template>
  <v-container class="sms-composer">
    <v-layout row wrap>
      <v-flex xs12>
          <v-form v-model="valid">
              <v-text-field label="Mobile Number"></v-text-field>
              <v-textarea label="Sms Content"></v-textarea>
              <v-btn v-on:click='smsSend'>Send</v-btn> 
          </v-form>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
    export default {
        name: 'SmsComposer',
        data: () => ({
            valid: true
        }),
        methods: {
            smsSend: function (event) {
                // `this` inside methods points to the Vue instance
                alert('Hello ' + this.name + '!')
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
