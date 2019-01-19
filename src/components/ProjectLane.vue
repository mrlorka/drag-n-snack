<template>
    <div class="md-layout">
      <div class="md-layout-item md-size-10">
        <md-card>
          <md-card-header>
            <div class="md-headline">{{project.name}}</div>
          </md-card-header>
          <md-card-actions>
            <md-button v-if="allowProjectDeletion" class="md-icon-button" @click="$emit('deleteProject')">
              <md-icon>delete</md-icon>
            </md-button>
          </md-card-actions>
        </md-card>
      </div>
      <div class="md-layout-item md-size-80">
        <draggable class="draglist" v-model="members" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <md-card class="dragcard md-with-hover" v-for="member in members" :key="member.id" >
            <md-card-header>
              <div class="md-headline">{{member.name}}</div>
            </md-card-header>
            <md-card-actions>
              <md-button class="md-icon-button" @click="toBank(member)">
                <md-icon>event_seat</md-icon>
              </md-button>
            </md-card-actions>
          </md-card>
        </draggable>
      </div>
    </div>
</template>

<script>
import draggable from "vuedraggable";
export default {
  components: {
    draggable
  },
  props: {
    project: {
      type: Object,
      required: true
    },
    allowProjectDeletion: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  data() {
    return {
    };
  },
  methods: {
    toBank: function(member) {
      let payload = { project: this.project, member: member } 
      this.$store.dispatch('moveMemberToBank', payload)
    }
  }, 
  computed: {
    members: {
      get() {
        return this.$store.getters.boardProjects.find(p=>p.id === this.project.id).members
      },
      set(value) {
        let payload = { project: this.project, newMembers: value }
        this.$store.commit('updateMembers', payload )
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.draglist {
  // min-height ist nötig, damit es eine dropzone gibt, auch wenn die Liste leer ist
  min-height: 150px;
}
.dragcard {
  display: inline-block;
  width: 150px;
}

.search {
  max-width: 500px;
}

.dragcard .md-icon-button {
  opacity: 0;
  animation-name: fadeOutOpacity;
  animation-iteration-count: 1;
  animation-timing-function: ease-in;
  animation-duration: 0.3;
}

.dragcard:hover .md-icon-button {
  opacity: 1;
  animation-name: fadeInOpacity;
  animation-iteration-count: 1;
  animation-timing-function: ease-in;
  animation-duration: 0.3s;
}

@keyframes fadeInOpacity {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}

@keyframes fadeOutOpacity {
  0% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}
</style>
