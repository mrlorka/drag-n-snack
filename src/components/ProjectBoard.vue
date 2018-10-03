<template>
  <div> 
    <md-button class="md-primary" @click="add()">
      <md-icon>add</md-icon>
      Neues Projekt
    </md-button>
    <div class="lane" v-for="project in projects" :key="project.id">
      <project-lane :name="project.name" v-on:deleteProject="deleteProject(project)"></project-lane>
    </div>
    <md-divider></md-divider>
    <div class="lane">
      <project-lane name="Bank"></project-lane>
    </div>
  </div>
</template>

<script>
import ProjectLane from './ProjectLane.vue'
import uuidv1 from 'uuid'
export default {
  name: 'ProjectBoard',
  components: {
    ProjectLane
  },
  data () {
    return {
      projects: [
        { name: 'Projekt 1' },
        { name: 'Projekt 2' },
        { name: 'Projekt 3' }
      ]
    }
  },
  methods: {
    add: function() {
      this.projects.push({ id: uuidv1(), name: 'Projekt 4 '})
    },
    deleteProject: function(project) {
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