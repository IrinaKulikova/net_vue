<template>
<div>

<div>
  <b-navbar toggleable="lg" type="dark" variant="info">
    <b-navbar-brand href="/">RND</b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item href="/item1">Item1</b-nav-item>
        <b-nav-item href="/item2">Item2</b-nav-item>
        <b-nav-item href="/login">login</b-nav-item>
      </b-navbar-nav>

      <b-navbar-nav class="ml-auto">
        
        <b-nav-item-dropdown right>
          <template v-slot:button-content>
            <em>{{user}}</em>
          </template>
          <b-dropdown-item href="/profile">Profile</b-dropdown-item>
          <b-dropdown-item href="/login"><router-link to="/login">Logout</router-link></b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</div>
</div>
</template>

<script>
export default {
  computed: {
    user() {
      var user = JSON.parse(localStorage.getItem("user"));
      return user ? user.email : "";
    }
  }
};
</script>