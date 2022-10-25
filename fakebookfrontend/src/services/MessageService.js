// import axios from "axios"

// const apiClient = axios.create({
//     baseURL: "https://localhost:7026/api/Message",
//     withCredentials: false,
//     headers: {
//       Accept: "application/json",
//       "Content-Type": "application/json",
//     },
//   });

//   const postApiClient = axios.create({
//     baseURL: "https://localhost:7026/api/Message",
//     withCredentials: false,
//     headers: {
//       Accept: "application/json",
//       'Content-Type': 'multipart/form-data'
//     },
//   })

//   export default {
//     getMessages(id) {
//         return apiClient.get(`/GetMessages/${id}`)
//     },

//     getMessage(id) {
//         return apiClient.get(`/${id}`)
//     },

//     getFriendMessages() {
//         return apiClient.get('/GetFriendMessages')
//     },

//     uploadMessage(message) {
//         return postApiClient.post('/UploadMessage', {
//             Message: message.Message,
//             Image: message.Image 
//         })
//     },

//     editMessage(id) {
//         return apiClient.post(`/EditMessage/${id}`)
//     },



//   }