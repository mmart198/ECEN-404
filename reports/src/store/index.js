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
      console.log('Populating Documents')
      axios.get(`https://localhost:44352/api/search`)
        .then(response => {
          console.log(response.data)
          context.commit('setDocuments', response.data)
        })
    }
  }
});
