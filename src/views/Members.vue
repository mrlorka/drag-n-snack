<template>
  <div>
    <md-button class="md-primary md-raised" @click="addMember()">
      <md-icon>add</md-icon>
      Neues Teammitglied
    </md-button>
    <md-list>
      <md-list-item v-for="member in members" :key="member.id">
        <md-button class="md-icon-button" @click="removeMember(member.id)">
          <md-icon>delete</md-icon>
        </md-button>
        <md-field md-inline>
          <label>Neues Teammitglied</label>
          <md-input v-model="member.name"></md-input>
        </md-field>
      </md-list-item>
    </md-list>
  </div>
</template>
<script>
import { mapState, mapMutations } from 'vuex'
export default {
    computed: mapState({
        members: state => state.members
    }),
    methods: {
        ...mapMutations([
            'addMember', 
            'removeMember'
        ])
    }
}
</script>
<style scoped>
    .md-list {
        width: 300px;
    }
</style>

