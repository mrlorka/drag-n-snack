<template>
  <div class="home">
    <ProjectBoard />
  </div>
</template>

<script>
// @ is an alias to /src
import ProjectBoard from '@/components/ProjectBoard.vue'

export default {
  name: 'home',
  components: {
    ProjectBoard
  }
}
</script>
