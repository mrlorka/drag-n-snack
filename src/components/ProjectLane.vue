<template>
    <div class="md-layout">
      <div class="md-layout-item md-size-10">
        <md-card>
          <md-card-header>
            <div class="md-headline">{{project.name}}</div>
          </md-card-header>
          <md-card-actions>
            <md-button class="md-icon-button" @click="$emit('deleteProject')">
              <md-icon>delete</md-icon>
            </md-button>
            <md-button class="md-icon-button md-primary" @click="add()">
              <md-icon>person_add</md-icon>
            </md-button>
          </md-card-actions>
        </md-card>
      </div>
      <div class="md-layout-item md-size-80">
        <draggable v-model="project.members" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <md-card class="dragcard md-with-hover" v-for="member in project.members" :key="member.id" >
            <md-card-header>
              <div class="md-headline">{{member.name}}</div>
            </md-card-header>
            <md-card-actions>
              <md-button class="md-icon-button" @click="remove(member)">
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
import uuidv1 from "uuid";
export default {
  components: {
    draggable
  },
  props: {
    project: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
    };
  },
  methods: {
    add: function() {
      this.project.members.push({ id: uuidv1(), name: "Neu" });
    },
    remove: function(element) {
      var index = this.project.members.indexOf(element);
      this.project.members.splice(index, 1);
    }
  }
};
</script>

<style lang="scss" scoped>
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
