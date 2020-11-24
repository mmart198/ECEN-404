import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import VueHtml2pdf from 'vue-html2pdf'

Vue.config.productionTip = false;
Vue.component('vue-html2pdf', VueHtml2pdf);
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
