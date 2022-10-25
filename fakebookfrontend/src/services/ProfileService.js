import axios from "axios"

const apiClient = axios.create({
    baseURL: "https://localhost:7026/api/Profile",
    withCredentials: false,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

  export default {
    getProfilePage(id) {
        return apiClient.get(`/${id}/GetProfilePage`)
    } 
  };
