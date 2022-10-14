<template>
<NavBar/>
</template>

<script>
import NavBar from './views/Layout/NavBar.vue'
import store from './store/index';


export default {

  created() {

     
      
      if (localStorage.getItem('userToken') && !store.state.auth?.user) {
          store.dispatch('auth/getAuth');
      }
  
  },

  components: {
    NavBar
  }
}
</script>

