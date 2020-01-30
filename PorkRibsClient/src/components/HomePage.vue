<template>
  <div>
    <h1>HomePage</h1>
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