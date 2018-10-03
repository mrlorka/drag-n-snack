<template>
    <div class="md-layout">
      <div class="md-layout-item md-size-10">
        <md-card>
          <md-card-header>
            <div class="md-headline">{{name}}</div>
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
        <draggable v-model="myArray" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <md-card class="dragcard" v-for="element in myArray" :key="element.id" >
            <md-card-header>
              <div class="md-headline">{{element.name}}</div>
            </md-card-header>
            <md-card-actions>
              <md-button class="md-icon-button" @click="remove(element)">
                <md-icon>event_seat</md-icon>
              </md-button>
            </md-card-actions>
          </md-card>
        </draggable>
      </div>
    </div>
</template>

<script>
import draggable from 'vuedraggable'
import uuidv1 from 'uuid'
export default {
  components: {
      draggable
  },
  props: {
    name: {
      type: String,
      required: true
    }
  },
  data () {
    return {
      myArray: [ 
        { id: uuidv1(), name: 'Robert' }, 
        { id: uuidv1(), name: 'Tomi' },
        { id: uuidv1(), name: 'Sven' },
        { id: uuidv1(), name: 'Sven' },
        { id: uuidv1(), name: 'Robert' }
      ]
    }
  },
  methods: {
    add: function() {
      this.myArray.push( {id: uuidv1(), name: 'Neu' })
    },
    remove: function(element) {
        var index = this.myArray.indexOf(element);
        this.myArray.splice(index, 1);
    }
  }
}
</script>

<style lang="scss" scoped>
  .dragcard {
    display: inline-block;
    width: 150px;
  }

  .search {
    max-width: 500px;
  }
</style>
