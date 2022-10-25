import { createStore } from "vuex";
import { auth } from "./authentication";
import {profile} from "./profile";
import createPersistedState from "vuex-persistedstate";
const store = createStore({
  modules: {
    auth,
    profile,
  },
  plugins: [createPersistedState()]
});

export default store;