import Vue from "vue";
import Vuex from "vuex";
import VueResource from "vue-resource";

Vue.use(Vuex);
Vue.use(VueResource);
const api = "https://dragsnackapi.azurewebsites.net/api";

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
  actions: {
    loadProjects(context) {
      Vue.http
        .get(api + "/projects")
        .then(result => {
          context.commit("setProjects", result.data);
        })
        .catch(err => {
          alert("Fehler beim Laden der Projekte: " + err);
        });
    },
    loadBoard(context) {
      Vue.http
        .get(api + "/board")
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
        .get(api + "/teammembers")
        .then(result => {
          context.commit("setMembers", result.data);
        })
        .catch(err => {
          alert("Fehler beim Laden der Teammitglieder: " + err);
        });
    },
    moveMemberToBank(context, payload) {
      //TODO move member in database
      //??
      context.commit("removeProjectMember", payload);
      let payloadTo = {
        project: context.state.bankProject,
        member: payload.member
      };
      context.commit("addProjectMember", payloadTo);
    },
    setMembers(context, members) {
      //TODO update members in database
      //put members
      Vue.http
        .put(api + "/teammembers", members)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
      context.commit("setMembers", members);
    },
    addMember(context) {
      //TODO write new member without name and capacity of 4 to database
      // db method returns new member
      //post members
      let member = {
        id: "cd122fd8-7afd-4a20-be56-e0292af640da",
        name: "Neuer Dude",
        capacity: 4
      };
      Vue.http
        .post(api + "/teammembers", member)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
      context.commit("addMember", member);
    },
    updateMember(context, member) {
      Vue.http
        .put(api + "/teammembers/" + member.id, member)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
    },
    removeMember(context, id) {
      //delete members
      Vue.http
        .delete(api + "/teammembers/" + id)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
      context.commit("removeMember", id);
    },
    setProjects(context, projects) {
      //put projects
      context.commit("setProjects", projects);
    },
    addProject(context) {
      //TODO write new project without name to database
      // db method returns new project
      //post projects
      let project = {
        id: "8c111c2f-775f-459a-9a8e-69a2aa8c9cd2",
        name: "Tesla ablösen",
        members: []
      };
      Vue.http
        .post(api + "/projects", project)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
      context.commit("addProject", project);
    },
    updateProject(context, project) {
      Vue.http
        .put(api + "/projects/" + project.id, project)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
    },
    removeProject(context, id) {
      //TODO move members to bank project in database
      //TODO remove project from database
      //delete project
      Vue.http
        .delete(api + "/projects/" + id)
        .then(result => {
          // eslint-disable-next-line
          console.log(result);
        })
        .catch(err => {
          alert("error handling database: " + err);
        });
      context.commit("removeProject", id);
    },
    updateProjectMembers(context, payload) {
      if (context.state.bankProject.id === payload.project.id) {
        //TODO update Bank project members in database
        //put projectmembers
        context.commit("updateBankProjectMembers", payload);
      } else {
        // TODO update project members in database
        //put projectmembers
        context.commit("updateProjectMembers", payload);
      }
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
    addProject(state, project) {
      state.projects.push(project);
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
    updateBankProjectMembers(state, payload) {
      state.bankProject.members = payload.newMembers;
    },
    updateProjectMembers(state, payload) {
      state.projectsWithMembers.find(p => p.id === payload.project.id).members =
        payload.newMembers;
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
  }
});
