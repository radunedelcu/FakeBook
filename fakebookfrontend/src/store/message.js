import MessageService from "../services/MessageService.js"

const initialState = {
    messages:[],
    message:{}
}

export const message = {
    namespaced: true,
    state:initialState,
    mutations: {
        GET_FRIEND_MESSAGES(state, messages) {
            state.messages = messages;
        }
    },
    actions: {
        async fetchMessages({commit}) {
            return await MessageService.getFriendMessages()
            .then((res) => {
                commit("GET_FRIEND_MESSAGES", res.data );
            })
            .catch((err) => {
                throw err;
            })
        }
    }
}