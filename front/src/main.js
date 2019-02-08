import Vue from 'vue'
import App from './App.vue'
import draggable from 'vuedraggable'
import VueMaterial from 'vue-material'

import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import store from './store'
import router from './router'

Vue.use(draggable)
Vue.use(VueMaterial)
Vue.config.productionTip = false

new Vue({
  store,
  router,
  render: h => h(App)
}).$mount('#app')
