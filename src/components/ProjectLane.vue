<template>
    <div class="md-layout">
      <div class="md-layout-item md-size-10">
        <md-card>
          <md-card-header>
            <div class="md-title">Projekt 1</div>
          </md-card-header>
          <md-card-actions>
            <md-button class="md-icon-button" @click="add()">
                <md-icon>person_add</md-icon>
              </md-button>
            <md-button class="md-icon-button">
                <md-icon>delete</md-icon>
              </md-button>
          </md-card-actions>
        </md-card>
      </div>
      <div class="hat-list md-layout-item md-size-80">
        <draggable v-model="myArray" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <md-card class="dragcard" v-for="element in myArray" :key="element.id" >
            <md-card-header>
              <div v-if="element.id !== editId" class="md-title">{{element.name}}</div>
              <div v-if="element.id === editId" class="md-title">
                  <md-input  class="md-mini" v-model="element.name"></md-input>
              </div>
            </md-card-header>
            <md-card-actions>
              <md-button v-if="element.id !== editId" class="md-icon-button" @click="edit(element)">
                <md-icon>build</md-icon>
              </md-button>
              <md-button v-if="element.id === editId" class="md-icon-button" @click="save()">
                <md-icon>check</md-icon>
              </md-button>
              <md-button v-if="element.id === editId" class="md-icon-button" @click="cancel(element)">
                <md-icon>close</md-icon>
              </md-button>
              <md-button class="md-icon-button" @click="remove(element)">
                <md-icon>delete</md-icon>
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
  data () {
    return {
      editId: 0,
      editOld: '',
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
      this.myArray.push( {id: uuidv1(), name: '<Neu>' })
    },
    remove: function(element) {
        var index = this.myArray.indexOf(element);
        this.myArray.splice(index, 1);
    },
    edit: function(element) {
        this.editId = element.id;
        this.editOld = element.name;
    },
    save: function() {
        this.editId = 0;
    },
    cancel: function(element) {
        element.name = this.editOld;
        this.editId = 0;
    }
  }
}
</script>

<style>
  .hat-list {
    text-align: left;
  }
  .dragcard {
    display: inline-block;
    width: 200px;
  }
</style>
