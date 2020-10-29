import Vue from "vue";
import VueRouter from "vue-router";
import SearchPage from '../views/Search.vue'
import Results from '../views/Results.vue'

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Landing",
    component: SearchPage
  },
  {
    path: "/results/:sampleid",
    name: "Results",
    component: Results
  }
];

const router = new VueRouter({
  routes
});

export default router;
