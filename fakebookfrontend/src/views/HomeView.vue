<template>
  <div v-for="message in messages">
    <Message :message="message"></Message>
  </div>
</template>

<script>
import Message from '../components/Message.vue'
export default {
  name: 'HomeView',
  computed: {
    messages() {
      return this.$store.state.message.messages;
    }
  },
  components:{
  Message
  },

  created() {
    console.log("asta" + this.$store.state.messages);
    this.$store.dispatch("message/fetchMessages")
  }
}
</script>

<style scoped>
  .home{
    margin-top: 100px;
  }

  .card-body {
  padding: 1.5rem;
  flex: 1 1 auto;
  background-color: white;
}
</style>