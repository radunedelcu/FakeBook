<template>


  <div class="auth">
    <!-- <Form @submit.prevent="handleRegister" class="box">
      <h1>Register</h1>
      <div class="form-group">
      </div>
      <input type="text" name="" v-model="name" placeholder="Username">
      <input type="text" name="" v-model="email" placeholder="Email">
      <input type="password" v-model="password" name="" placeholder="Password">
      <input type="password" v-model="confirmPassword" name="" placeholder="Confirm Password">
      <input type="submit" name="" value="Register" href="/">
      <div>
      </div>
    </Form> -->
    <Form class="box" @submit="handleRegister" :validation-schema="schema">
      <h1>Register</h1>
      <div v-if="!successful">
        <div class="form-group">
          <Field name="name" placeholder="Name" type="text" class="form-control" />
          <ErrorMessage name="username" class="error-feedback" />
        </div>
        <div class="form-group">
          <Field name="email" placeholder="Email" type="text" class="form-control" />
          <ErrorMessage name="email" class="error-feedback" />
        </div>
        <div class="form-group">
          <Field name="password" placeholder="Password" type="password" class="form-control" />
          <ErrorMessage name="password" class="error-feedback" />
        </div>
        <div class="form-group">
          <Field name="password" placeholder="Confirm Password" type="password" class="form-control" />
          <ErrorMessage name="password" class="error-feedback" />
        </div>

        <div class="form-group">
          <button class="btn btn-primary btn-block" :disabled="loading">
            <span
              v-show="loading"
              class="spinner-border spinner-border-sm"
            ></span>
            Register
          </button>
        </div>
        <br/>
        <br/>
        <a href="/login">Already have an account? Login</a>
      </div>
    </Form>

  </div>
  <router-view></router-view>
</template>

<script>
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
  name: "RegisterView",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  data() {
    const schema = yup.object().shape({
      username: yup
        .string()
        .required("Name is required!")
        .min(3, "Must be at least 3 characters!")
        .max(20, "Must be maximum 20 characters!"),
      email: yup
        .string()
        .required("Email is required!")
        .email("Email is invalid!")
        .max(50, "Must be maximum 50 characters!"),
      password: yup
        .string()
        .required("Password is required!")
        .min(6, "Must be at least 6 characters!")
        .max(40, "Must be maximum 40 characters!"),
    });

    return {
      successful: false,
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
  mounted() {
    if (this.loggedIn) {
      this.$router.push("/");
    }
  },
  methods: {
    handleRegister(user) {
      this.message = "";
      this.successful = false;
      this.loading = true;

      this.$store.dispatch("auth/register", user).then(
        (data) => {
          this.message = data.message;
          this.successful = true;
          this.loading = false;
        },
        (error) => {
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
          this.successful = false;
          this.loading = false;
        }
      );
    },
  },
};
</script>

<style scoped>
body {
  margin: 0;
  padding: 0;
  font-family: sans-serif;
  background: #f0f2f5
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
</style>