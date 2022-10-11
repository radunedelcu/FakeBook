<template>
  <div class="home">
 <h1>Home</h1>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'HomeView',
  async created() {
    const response = await axios.get('user');

    console.log(response)
  }
}
</script>
<style>
  .home{
    margin-top: 100px;
  }
</style>