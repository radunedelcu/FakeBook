import axios from "axios"

const API_URL = 'https://localhost:7026/api/Authentication/';

class AuthService {

   getAuth(){
     return  axios
      .get(API_URL + 'auth')
      .then(response => response.data);
   }

  login(user) {
    return axios
    .post(API_URL + 'login', {
      email: user.email,
      password: user.password
    })
    .then(response => {
      if (response.data.token) {
        localStorage.setItem('userName', JSON.stringify(response.data.name))
        localStorage.setItem('userToken', JSON.stringify(response.data.token));
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