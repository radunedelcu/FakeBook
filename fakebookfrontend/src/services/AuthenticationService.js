import axios from "axios"

const API_URL = 'https://localhost:7026/api/Authentication/';

class AuthService {



  login(user) {
    return axios
    .post(API_URL + 'login', {
      email: user.email,
      password: user.password
    })
    .then(response => {
      if (response.data.token) {
        localStorage.setItem('userName', response.data.name)
        localStorage.setItem('userToken', response.data.token);
        }

        return response.data;
      });
  } 

  logout() {
    localStorage.removeItem('userToken')
    localStorage.removeItem('userName')
  }

  register(user) {
    return axios.post(API_URL + 'register', {
      name: user.name,
      email: user.email,
      password: user.password,
      confirmPassword: user.confirmPassword
    });
  }
}

export default new AuthService();