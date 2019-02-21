import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import store from './store'
import './registerServiceWorker'

import DatetimePicker from 'vuetify-datetime-picker'
import 'vuetify-datetime-picker/src/stylus/main.styl'

Vue.config.productionTip = false

new Vue({
  router,
  store,
    render: h => h(App),
    DatetimePicker
}).$mount('#app')
