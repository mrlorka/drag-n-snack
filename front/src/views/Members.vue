<template>
  <div>
    <md-button class="md-primary md-raised" @click="addMember()">
      <md-icon>add</md-icon>Neues Teammitglied
    </md-button>
    <md-list>
      <md-list-item v-for="member in members" :key="member.id">
        <md-button class="md-icon-button" @click="removeMember(member.id)">
          <md-icon>delete</md-icon>
        </md-button>
        <md-field md-inline>
          <label>Neues Teammitglied</label>
          <md-input v-model="member.name" @change="updateMember(member)"></md-input>
        </md-field>
        <md-field md-inline>
          <md-input v-model="member.capacity" type="number" @change="updateMember(member)"></md-input>
        </md-field>
      </md-list-item>
    </md-list>
  </div>
</template>
<script>
import { mapActions } from "vuex";
export default {
  computed: {
    members: {
      get() {
        return this.$store.state.members;
      },
      set(value) {
        this.$store.dispatch("setMembers", value);
      }
    }
  },
  methods: {
    ...mapActions(["addMember", "removeMember"]),
    updateMember(member) {
      this.$store.dispatch("updateMember", member);
    }
  },
  mounted: function() {
    this.$store.dispatch("loadMembers");
  }
};
</script>
<style scoped>
.md-list {
  width: 300px;
}
</style>

