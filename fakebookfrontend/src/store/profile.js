import ProfileService from "@/services/ProfileService"

const initialState = {
    profile:[]
}

export const profile = {
    namespaced: true,
    state:initialState,
    mutations: {
        GET_PROFILE(state, profile) {
            console.log("api" + JSON.stringify(profile))
            state.profile = profile;

        }
    },
    actions: {
         async fetchProfile({commit}, id) {
            return await ProfileService.getProfilePage(id)
            .then((res) => {
                commit("GET_PROFILE", res.data );
                console.log("intra")
            })
            .catch((err) => {
                throw err;
            })
        }
    }
}