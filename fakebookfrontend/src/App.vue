<template>
  <nav class="navbar navbar-expand-lg bg-primary">
    <div class="container-fluid">
      <a class="navbar-brand text-white" href="/">fakebook</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <form class="justify-content-start d-flex" role="search" style="margin-left: 0px">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-light" type="submit">Search</button>
        </form>
        <ul class="navbar-nav me-auto mb-2 mb-lg-0 d-flex" style="margin-right: 0px !important;">

          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" style="margin-left: 100px">
              Profile
            </a>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="/login">Login</a></li>
              <li><a class="dropdown-item" href="/register">Register</a></li>
              <li><a class="dropdown-item" href="#">Logout</a></li>
              <li>
                <hr class="dropdown-divider">
              </li>
              <li v-if="this.$store.state.auth.status.loggedIn">
                <a class="dropdown-item" href="#">Hi </a>
              </li>
            </ul>
          </li>
        </ul>

      </div>
    </div>
  </nav>
  <router-view></router-view>
</template>



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
