<template>
  <div> 
    <div class="lane" v-for="project in projects" :key="project.id">
      <project-lane :project="project" v-on:deleteProject="deleteProject(project)"></project-lane>
    </div>
    <md-divider></md-divider>
    <div class="lane">
      <project-lane :project="bankProject"  :allow-project-deletion="false"></project-lane>
    </div>
  </div>
</template>

<script>
import ProjectLane from './ProjectLane.vue'
import { mapState } from 'vuex'
export default {
  name: 'ProjectBoard',
  components: {
    ProjectLane
  },
  data () {
    return {
    }
  },
  computed: {
    ...mapState ({
      projects: state => state.projects,
      bankProject: state => state.bankProject
    })
  } ,
  methods: {
    deleteProject: function(project) {
      let moveActions = []
      project.members.forEach(member => {
        let payload = { project: project, member: member } 
        moveActions.push(this.$store.dispatch('moveMemberToBank', payload))
      });
      Promise.all(moveActions)
      .then(() => {
        this.$store.commit('removeProject', project.id )
      }).catch((err) => {
        alert(err)
      });
    }
  }
}
</script>
<style scoped>
  .lane {
    padding-top: 10px;
    padding-bottom: 10px;
  }
</style>