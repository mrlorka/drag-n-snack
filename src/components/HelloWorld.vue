<template>
  <div> 
    <md-button class="md-icon-button" @click="add()">
      <md-icon>fiber_new</md-icon>
    </md-button>
    <project-lane v-for="project in projects" :key="project.name" :name="project.name" v-on:deleteProject="deleteProject(project)"></project-lane>
  </div>
</template>

<script>
import ProjectLane from './ProjectLane.vue'
export default {
  name: 'HelloWorld',
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
      this.projects.push({ name: 'Projekt 4 '})
    },
    deleteProject: function(project) {
      let deleteCandidate = this.projects.find(element => project.name === element.name)
      var index = this.projects.indexOf(deleteCandidate);
      this.projects.splice(index, 1);
    }
  }
}
</script>
