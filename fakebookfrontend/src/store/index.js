import { createStore } from "vuex";
import { auth } from "./authentication";
import {profile} from "./profile";
import {message} from "./message";
import createPersistedState from "vuex-persistedstate";
const store = createStore({
  modules: {
    auth,
    profile,
    message,
  },
  plugins: [createPersistedState()]
});

export default store;