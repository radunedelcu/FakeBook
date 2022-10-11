<template>


    <div class="auth">
      <form @submit.prevent="handleSubmit" class="box">
        <h1>Login</h1>
        <p class="text-muted"> Please enter your login and password!</p>
        <input type="text" v-model="email" name="" placeholder="Username">
        <input type="password" v-model="password" name="" placeholder="Password">
   
        <input type="submit" name="" value="Login" href="#">
        <div>
        </div>
      </form>
    </div>
    <router-view></router-view>
  </template>

<script>
import axios from 'axios'
export default {
  name: 'LoginView',
  data() {
    return {
      email: '',
      password: ''
    }
  },

  methods:{
     handleSubmit() {
      console.log("intra")
      const response = axios.post('https://localhost:7026/api/Authentication/login', {
        email: this.email,
        password: this.password
      });

      localStorage.SetItem('token', response.data.token)
    }
    
  }
}
</script>

  <style>
    body {
      margin: 0;
      padding: 0;
      font-family: sans-serif;
      background: linear-gradient(to right, #1565c0, #acc5e6)
    }
    
    .card {
      margin-bottom: 20px;
      border: none;
    }
    
    .box {
      width: 500px;
      padding: 40px;
      position: relative;
      background: white;
      text-align: center;
      transition: 0.25s;
    
    }
    
    .box input[type="text"],
    .box input[type="password"] {
      border: 0;
      background: none;
      display: block;
      margin: 20px auto;
      text-align: center;
      border: 2px solid #3498db;
      padding: 10px 10px;
      width: 250px;
      outline: none;
      color: black;
      border-radius: 24px;
      transition: 0.25s
    }
    
    .box h1 {
      color: black;
      text-transform: uppercase;
      font-weight: 500
    }
    
    .box input[type="text"]:focus,
    .box input[type="password"]:focus {
      width: 300px;
      border-color: #2ecc71
    }
    
    .box input[type="submit"] {
      border: 0;
      background: none;
      display: block;
      margin: 20px auto;
      text-align: center;
      border: 2px solid #2ecc71;
      padding: 14px 40px;
      outline: none;
      color: black;
      border-radius: 24px;
      transition: 0.25s;
      cursor: pointer
    }
    
    .box input[type="submit"]:hover {
      background: #2ecc71
    }
    
    .auth {
      margin: auto;
      width: 500px;
      margin-top: 100px;
      padding: 10px;
    }
    
    .forgot {
      text-decoration: underline
    }
    </style>