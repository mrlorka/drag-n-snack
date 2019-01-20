<template>
  <div class="projects">
    <md-button class="md-primary md-raised" @click="addProject()">
      <md-icon>add</md-icon>
      Neues Projekt
    </md-button>
    <md-list>
      <md-list-item v-for="project in projects" :key="project.id">
        <md-button class="md-icon-button" @click="removeProject(project.id)">
          <md-icon>delete</md-icon>
        </md-button>
        <md-button class="md-icon-button">
          <md-icon>arrow_upward</md-icon>
        </md-button>
        <md-button class="md-icon-button">
          <md-icon>arrow_downward</md-icon>
        </md-button>
        <md-field md-inline>
          <label>Neues Projekt</label>
          <md-input v-model="project.name"></md-input>
        </md-field>
      </md-list-item>
    </md-list>
  </div>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
    computed: mapState({
        projects: state => state.projects
    }),
    methods: {
        ...mapActions([
            'addProject', 
            'removeProject'
        ])
    },
    mounted: function() {
      this.$store.dispatch('loadProjects')
    }
}
</script>
<style scoped>
    .md-list {
        width: 300px;
    }
</style>

