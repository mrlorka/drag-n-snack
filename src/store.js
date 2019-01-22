import Vue from "vue";
import Vuex from "vuex";
import uuidv1 from "uuid";
import VueResource from "vue-resource";

Vue.use(Vuex);
Vue.use(VueResource);

export default new Vuex.Store({
  state: {
    projects: [],
    members: [],
    projectsWithMembers: [],
    bankProject: {}
  },
  getters: {
    boardProjects: state => {
      return state.projectsWithMembers.concat([state.bankProject]);
    }
  },
  mutations: {
    setProjects(state, projects) {
      state.projects.splice(0, state.projects.length);
      projects.forEach(project => {
        state.projects.push(project);
      });
    },
    setProjectsWithMembers(state, projects) {
      state.projectsWithMembers.splice(0, state.projectsWithMembers.length);
      projects.forEach(project => {
        state.projectsWithMembers.push(project);
      });
    },
    setBankProject(state, bankProject) {
      state.bankProject = bankProject;
    },
    setMembers(state, members) {
      state.members.splice(0, state.members.length);
      members.forEach(member => {
        state.members.push(member);
      });
    },
    addProject(state) {
      state.projects.push({ id: uuidv1(), name: "" });
    },
    removeProject(state, id) {
      let item = state.projects.find(p => p.id === id);
      let index = state.projects.indexOf(item);
      state.projects.splice(index, 1);
    },
    removeProjectFromBoard(state, id) {
      let item = state.projectsWithMembers.find(p => p.id === id);
      let index = state.projectsWithMembers.indexOf(item);
      state.projectsWithMembers.splice(index, 1);
    },
    addMember(state, member) {
      state.members.push(member);
    },
    removeMember(state, id) {
      let item = state.members.find(p => p.id === id);
      let index = state.members.indexOf(item);
      state.members.splice(index, 1);
    },
    updateMembers(state, payload) {
      if (state.bankProject.id === payload.project.id) {
        state.bankProject.members = payload.newMembers;
      } else {
        state.projectsWithMembers.find(
          p => p.id === payload.project.id
        ).members = payload.newMembers;
      }
    },
    addProjectMember(state, payload) {
      let project = state.bankProject;
      if (state.bankProject.id !== payload.project.id) {
        project = state.projectsWithMembers.find(
          p => p.id === payload.project.id
        );
      }
      project.members.push(payload.member);
    },
    removeProjectMember(state, payload) {
      let project = state.bankProject;
      if (state.bankProject.id !== payload.project.id) {
        project = state.projectsWithMembers.find(
          p => p.id === payload.project.id
        );
      }
      let memberItem = project.members.find(m => m.id === payload.member.Id);
      let memberIndex = project.members.indexOf(memberItem);
      project.members.splice(memberIndex, 1);
    }
  },
  actions: {
    loadProjects(context) {
      Vue.http
        .get("http://localhost:3000/projects")
        .then(result => {
          context.commit("setProjects", result.data);
        })
        .catch(err => {
          alert("Fehler beim Laden der Projekte: " + err);
        });
    },
    loadBoard(context) {
      Vue.http
        .get("http://localhost:3000/board")
        .then(result => {
          context.commit("setProjectsWithMembers", result.data.projects);
          context.commit("setBankProject", result.data.bankProject);
        })
        .catch(err => {
          alert("Fehler beim Laden des Boards: " + err);
        });
    },
    loadMembers(context) {
      Vue.http
        .get("http://localhost:3000/members")
        .then(result => {
          context.commit("setMembers", result.data);
        })
        .catch(err => {
          alert("Fehler beim Laden der Teammitglieder: " + err);
        });
    },
    moveMemberToBank(context, payload) {
      context.commit("removeProjectMember", payload);
      let payloadTo = {
        project: context.state.bankProject,
        member: payload.member
      };
      context.commit("addProjectMember", payloadTo);
    },
    addMember(context) {
      //TODO write new member without name and capacity of 4 to database
      // db method returns new member
      let member = {
        id: "cd122fd8-7afd-4a20-be56-e0292af640da",
        name: "Neuer Dude",
        capacity: 4
      };
      context.commit("addMember", member);
    },
    removeMember(context, id) {
      //TODO remove member from database
      context.commit("removeMember", id);
    }
  }
});
