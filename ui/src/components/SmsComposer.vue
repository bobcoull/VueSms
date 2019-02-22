<template>
  <v-container class="sms-composer">
    <v-layout row wrap>
      <v-flex xs12>
          <v-form v-model="valid" ref="form">
              <v-text-field label="Mobile Number" v-model="mobileNo" :rules="mobileNoValidation"></v-text-field>
              <v-textarea label="Sms Content" :rules="smsMessageValidation" v-model="message"></v-textarea>
              <v-btn v-on:click='smsSend'>Send</v-btn> 
          </v-form>
          <v-card class="resultSuccess" v-if="isSuccess">
            <span style="padding: 0 5px 0 0">{{successMessage}}</span>
          </v-card>
          <v-card class="resultError" v-for="error in errors" :key="error.message" v-if="!isSuccess">
            <div>{{error.message}}</div>
          </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
    export default {
        name: 'SmsComposer',
        data: () => ({
            valid: true,
            mobileNo:'',
            message:'',
            mobileNoValidation: [
              v => !!v || 'Mobile No is required'
            ],
            smsMessageValidation: [
              v => !!v || 'Message is required',
              v => v.length <= 160 || "Maximum length is 160 characters"
            ],
            successMessage : '',
            errors : [],
            isSuccess: null
        }),
        methods: {
          smsSend : function(event){
            if (this.$refs.form.validate())
            {
              console.log('Send); mesage')
              console.log(this.postData);

              var url = 'http://localhost:54457/api/Sms'

              fetch(url, {
                method: "POST",
                cache: "no-cache",
                headers: {
                            "Content-Type": "application/json"
                            // "Content-Type": "application/x-www-form-urlencoded",
                          },
                body: JSON.stringify({message: this.message, mobileNo : this.mobileNo})
                })
                .then(response => response.json())
                .then(data => {              
                  this.isSuccess = data.isSuccess            
                  this.successMessage = data.successMessage
                  this.errors = data.errorMessages
                  console.log('Success')
                  console.log(data) // Prints result from `response.json()` in getRequest // for development only
                })
                .catch(error => {
                    console.log('Error')
                    console.log(error) // Prints result from `response.json()` in getRequest // for development only
                 })
              }
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
</style>
