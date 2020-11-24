import Vue from "vue";
import Vuex from "vuex";
import axios from 'axios'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    documents: []
  },
  mutations: {
    setDocuments (state, data) {
      state.documents = data
    }
  },
  actions: {
    async populateDocuments (context) {
      axios.get(`https://localhost:44352/api/search`)
        .then(response => {
          context.commit('setDocuments', response.data)
        })
    }
  }
});
