<template>
  <v-container class="sms-list">
    <v-layout row wrap>
      <v-flex xs12>
        <v-menu bottom left>
          <v-text-field slot="activator" label="From Date" prepend-icon="date_range" :value="formattedFromDate" disabled></v-text-field>
          <v-date-picker v-model="fromDate"></v-date-picker>
        </v-menu>
        <v-spacer></v-spacer>
        <v-menu bottom left>
          <v-text-field slot="activator" label="To Date" prepend-icon="date_range" :value="formattedToDate" disabled></v-text-field>
          <v-date-picker v-model="toDate"></v-date-picker>
        </v-menu>
		<v-spacer></v-spacer>
        <v-btn v-on:click='smsList' >List</v-btn> 
        <v-card class="resultSuccess" v-for="message in messages" :key="message.message" v-if="isSuccess">
          <span style="padding: 0 5px 0 0">{{message.dateSentFormatted}}</span>
		  <span>{{message.messageBody}}</span>
        </v-card>
        <v-card class="resultError" v-for="error in errors" :key="error.message" v-if="!isSuccess">
          <div>{{error.message}}</div>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
  import format from 'date-fns/format'

  export default {
    name: 'SmsList',
    data: () => ({
      valid: true,
      fromDate : null,
      toDate : null,
      messages : [],
      errors : [],
      isSuccess: true
    }),
    methods: {
      // `this` inside methods points to the Vue instance
      smsList: function (event) {
        var url = 'http://localhost:54457/api/Sms?fromDate=' + this.fromDate + '&toDate=' + this.toDate 
        fetch(url)
          .then(response => response.json())
          .then(data => {              
              this.isSuccess = data.isSuccess            
              this.messages = data.smsMessages
              this.errors = data.errorMessages
              console.log('Success')
              console.log(data) // Prints result from `response.json()` in getRequest // for development only
            })
          .catch(error => {
            console.log('Error')
            console.log(error) // Prints result from `response.json()` in getRequest // for development only
          })
		}
    },
    computed: {
      formattedFromDate() {
        return this.fromDate ? format(this.fromDate, 'DD/MM/YYYY') : ''
      },
      formattedToDate() {
        return this.toDate ? format(this.toDate, 'DD/MM/YYYY') : ''
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
  .resultError {background-color:#ff0000; color:#ffffff;}
  .resultSuccess {background-color:#ffffff; color:#000000; margin:3px 3px 3px 3px; border: 3px 3px 3px 3px;}
</style>
