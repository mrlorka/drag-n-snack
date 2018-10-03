import Vue from 'vue'
import Vuex from 'vuex'
import uuidv1 from 'uuid'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    projects: [
      { id: uuidv1(), name: 'Projekt 1' },
      { id: uuidv1(), name: 'Projekt 2' },
      { id: uuidv1(), name: 'Projekt C' }
    ]
  },
  mutations: {
    addProject (state) {
      state.projects.push({ id: uuidv1(), name: '' })
    },
    removeProject (state, id) {
      let item = state.projects.find(p=>p.id === id)
      let index = state.projects.indexOf(item)
      state.projects.splice(index, 1)
    }
  },
  actions: {
    addProject (context) {
      context.commit('addProject')
    },
    removeProject (context, id) {
      context.commit('removeProject', id)
    }
  }
})
