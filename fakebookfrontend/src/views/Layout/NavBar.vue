<template>
    <nav v-if="currentUser" class="navbar navbar-expand-lg bg-primary">
        <div class="container-fluid">
            <router-link to="/home" class="navbar-brand text-white">fakebook</router-link>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false"
                aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <form class="justify-content-start d-flex" role="search" style="margin-left: 0px">
                    <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                    <button class="btn btn-outline-light" type="submit">Search</button>
                </form>
                <ul class="navbar-nav me-auto mb-2 mb-lg-0 d-flex" style="margin-right: 0px !important;">

                    <li class="nav-item dropdown">
                        <router-link to="/profile" class="nav-link dropdown-toggle text-white" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false" style="margin-left: 100px">
                            Profile
                        </router-link>
                        <ul class="dropdown-menu " style="margin: 0 !important;
                        ">
                            <li v-if="currentUser">
                                <router-link to="/logout" @click.prevent="logOut" class="dropdown-item">Logout</router-link>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li v-if="currentUser">
                                <router-link v-bind:to="'/profile/' + currentUser.id" class="dropdown-item"> Hi {{currentUser.name}} !</router-link>
                            </li>
                        </ul>
                    </li>
                </ul>

            </div>
        </div>
    </nav>
    <router-view></router-view>
</template>
  
<script>
export default {
    name: "NavBar",
    computed: {
        currentUser() {
            return this.$store.state.auth.user;
         
        },
    },
    methods: {
    logOut() {
      this.$store.dispatch('auth/logout');
      this.$router.push('/login');
    }
  }
}
</script>
  
<style>
#app {

    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}

nav {
    padding: 30px;
}

nav a {
    font-weight: bold;
    color: #2c3e50;
}

nav a.router-link-exact-active {
    color: #42b983;
}

.d-flex {
    margin: auto;
}

.dropdown-menu[data-bs-popper] {
    top: 115% !important;
    right: -12px !important;
    margin-top: var(--bs-dropdown-spacer);
}

</style>
  