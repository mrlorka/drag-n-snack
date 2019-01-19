<template>
  <div> 
    <div class="lane" v-for="project in projects" :key="project.id">
      <project-lane :project="project" v-on:deleteProject="deleteProject(project)"></project-lane>
    </div>
    <md-divider></md-divider>
    <div class="lane">
      <project-lane :project="bankProject"></project-lane>
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
      //TODO move members to bank
      let deleteCandidate = this.projects.find(element => project.id === element.id)
      var index = this.projects.indexOf(deleteCandidate);
      this.projects.splice(index, 1);
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