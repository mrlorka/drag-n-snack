import Vue from 'vue'
import App from './App.vue'
import draggable from 'vuedraggable'
import VueMaterial from 'vue-material'
import * as Sentry from '@sentry/browser';
import * as Integrations from '@sentry/integrations';

import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import store from './store'
import router from './router'

Sentry.init({
  dsn: 'http://59230b0c287b4e55be353ab83f6c4d33@localhost:9000/2',
  integrations: [new Integrations.Vue({Vue, attachProps: true})],
});

Vue.use(draggable)
Vue.use(VueMaterial)
Vue.config.productionTip = false

new Vue({
  store,
  router,
  render: h => h(App)
}).$mount('#app')
