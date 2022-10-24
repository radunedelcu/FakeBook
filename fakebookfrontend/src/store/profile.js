import ProfileService from "@/services/ProfileService"

const initialState = {
    profile:[]
}

export const userProfile = {
    namespaced: true,
    state:initialState,
    mutations: {
        GET_PROFILE(state, profile) {
            state.profile = profile;
        }
    },
    actions: {
         async fetchProducts({commit}) {
            return await ProfileService.getProfilePage(id)
            .then((res) => {
                commit("GET_PROFILE", res.data);
            })
            .catch((err) => {
                throw err;
            })
        }
    }
}