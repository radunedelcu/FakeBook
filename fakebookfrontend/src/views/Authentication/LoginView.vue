<template>


    <div class="auth">
      <!-- <Form @submit.prevent="handleLogin" class="box">
        <h1>Login</h1>
        <p class="text-muted"> Please enter your login and password!</p>
        <div class="form-group">
          <label for="username">Username</label>
          <Field name="username" type="text" class="form-control" />
          <ErrorMessage name="username" class="error-feedback" />
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <Field name="password" type="password" class="form-control" />
          <ErrorMessage name="password" class="error-feedback" />
        </div>
   
        <input type="submit" name="" value="Login" href="#">
        <div>
        </div>
      </Form> -->
      <Form class="box" @submit="handleLogin" :validation-schema="schema">
        <h1>Login</h1>
        <div class="form-group">
          <Field name="email" placeholder="Email" type="text" class="form-control" />
          <ErrorMessage name="username" class="error-feedback" />
        </div>
        <div class="form-group">
          <Field name="password" placeholder="Password" type="password" class="form-control"/>
          <ErrorMessage name="password" class="error-feedback" />
        </div>

        <div class="form-group">
          <button class="btn btn-primary btn-block" :disabled="loading">
            <span
              v-show="loading"
              class="spinner-border spinner-border-sm"
            ></span>
            <span>Login</span>
          </button>
        </div>

        <div class="form-group">
          <div v-if="message" class="alert alert-danger" role="alert">
            {{ message }}
          </div>
        </div>
      </Form>
    </div>
    <router-view></router-view>
  </template>

<script>
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
  name: "LoginView",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  data() {
    const schema = yup.object().shape({
      email: yup.string().required("Email is required!"),
      password: yup.string().required("Password is required!"),
    });

    return {
      loading: false,
      message: "",
      schema,
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  },
  created() {
    if (this.loggedIn) {
      this.$router.push("/");
    }
  },
  methods: {
    handleLogin(user) {
      this.loading = true;

      this.$store.dispatch("auth/login", user).then(
        () => {
          this.$router.push("/");
        },
        (error) => {
          this.loading = false;
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
        }
      );
    },
  },
};
</script>

  <style>
    body {
      margin: 0;
      padding: 0;
      font-family: sans-serif;
      background-color: #f0f2f5 !important
    }
    
    .card {
      margin-bottom: 20px;
      border: none;
    }
    
    .box {
      box-shadow: 0 2px 4px rgb(0 0 0 / 10%), 0 8px 16px rgb(0 0 0 / 10%);;
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

    .error-feedback {
       color: red;
    }

    
    </style>