<template>
  <div>
    <h1>Hi {{user}}</h1>
    <h3>Users from secure api end point:</h3>
    <em v-if="users.loading">Loading users...</em>
    <span v-if="users.error" class="text-danger">ERROR: {{users.error}}</span>
    <ul v-if="users.items">
      <li v-for="user in users.items" :key="user.id">{{user.userName}}</li>
    </ul>    
  </div>
</template>

<script>

export default {
  computed: {
    user() {
      var user = JSON.parse(localStorage.getItem("user"));
      return user ? user.email : "";
    },
    users() {
      return this.$store.state.users.all;
    }
  },
  created() {
    this.$store.dispatch("users/getUsers");
  }
};
</script>