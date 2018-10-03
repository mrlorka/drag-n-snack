<template>
  <div class="page-container">
    <md-app md-mode="reveal">
      <md-app-toolbar class="md-primary">
        <md-button class="md-icon-button" @click="menuVisible = !menuVisible">
          <md-icon>menu</md-icon>
        </md-button>
        <span class="md-title">Drag'n'Snack</span>
      </md-app-toolbar>

      <md-app-drawer :md-active.sync="menuVisible">
        <md-toolbar class="md-transparent" md-elevation="0">
          Navigation
        </md-toolbar>

        <md-list>
          <md-list-item to="/" @click="menuVisible = false">
            <md-icon>dashboard</md-icon>
            <span class="md-list-item-text">Board</span>
          </md-list-item>

          <md-list-item to="/projects" @click="menuVisible = false">
              <md-icon>work</md-icon>
              <span class="md-list-item-text">Projekte</span>
          </md-list-item>
          
          <md-list-item to="/about" @click="menuVisible = false">
              <md-icon>info</md-icon>
              <span class="md-list-item-text">Info</span>
          </md-list-item>
        </md-list>
      </md-app-drawer>

      <md-app-content>
        <router-view></router-view>
      </md-app-content>
    </md-app>
  </div>
</template>
<script>
export default {
  data () {
    return {
      menuVisible: false
    }
  }
}
</script>

<style>
.md-app {
    min-height: 100vh;
    border: 1px solid rgba(#000, .12);
  }

  .md-drawer {
    width: 230px;
    max-width: calc(100vw - 125px);
  }
</style>
