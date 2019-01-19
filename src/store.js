import Vue from 'vue'
import Vuex from 'vuex'
import uuidv1 from 'uuid'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    projects: [
      { id: uuidv1(), name: 'Projekt 1', members: [{ id: uuidv1(), name: 'Robert' }] },
      { id: uuidv1(), name: 'Projekt 2', members: [] },
      { id: uuidv1(), name: 'Projekt C', members: [] }
    ],
    bankProject: {
      id: uuidv1(),
      name: 'Bank',
      members: [
        { id: uuidv1(), name: 'Robert' },
        { id: uuidv1(), name: 'Robert' },
        { id: uuidv1(), name: 'Robert' },
        { id: uuidv1(), name: 'Robert' },
        { id: uuidv1(), name: 'Tomi' },
        { id: uuidv1(), name: 'Tomi' },
        { id: uuidv1(), name: 'Tomi' },
        { id: uuidv1(), name: 'Sven' },
        { id: uuidv1(), name: 'Sven' }
      ]
    }
  },
  getters: {
    boardProjects: state =>  {
      return state.projects.concat([state.bankProject])
    }
  },
  mutations: {
    addProject (state) {
      state.projects.push({ id: uuidv1(), name: '' })
    },
    removeProject (state, id) {
      let item = state.projects.find(p=>p.id === id)
      let index = state.projects.indexOf(item)
      state.projects.splice(index, 1)
    },
    updateMembers(state, payload) {
      if (state.bankProject.id === payload.project.id) {
        state.bankProject.members = payload.newMembers
      }else {
        state.projects.find(p=>p.id===payload.project.id).members = payload.newMembers
      }
    },
    removeProjectMember(state, payload) {
      let project = state.bankProject
      if (state.bankProject.id !== payload.project.id) {
        project = state.projects.find(p=>p.id===payload.project.id)
      }
      let memberItem = project.members.find(m=>m.id === payload.member.Id)
      let memberIndex = project.members.indexOf(memberItem)
      project.members.splice(memberIndex, 1)
    },
    addProjectMember(state, payload) {
      let project = state.bankProject
      if (state.bankProject.id !== payload.project.id) {
        project = state.projects.find(p=>p.id===payload.project.id)
      }
      project.members.push(payload.member)
    }
  },
  actions: {
    addProject (context) {
      context.commit('addProject')
    },
    removeProject (context, id) {
      context.commit('removeProject', id)
    },
    moveMemberToBank(context, payload) {
      context.commit('removeProjectMember', payload)
      let payloadTo = { project: context.state.bankProject, member: payload.member }
      context.commit('addProjectMember', payloadTo)
    }
  }
})
