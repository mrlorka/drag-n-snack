<template>
  <div class="page-container">
    <md-app>
      <md-app-toolbar class="md-primary">
        <span class="md-title">Drag'n'Snack</span>
      </md-app-toolbar>
      <md-app-content>
        <ProjectBoard/>
      </md-app-content>
    </md-app>
  </div>
</template>

<script>
import ProjectBoard from './components/ProjectBoard.vue'

export default {
  name: 'app',
  components: {
    ProjectBoard
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
