import axios from "axios"

const apiClient = axios.create({
    baseURL: 'https://localhost:7026/api/Authentication',
    withCredentials: false,
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    }
})

export default {
    register(){
        return apiClient.post('/register')
    },
      
    login(){
        return apiClient.post('/login')
    }
}