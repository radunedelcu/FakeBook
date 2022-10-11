import { createStore } from "vuex";
import { auth } from "./authentication";

const store = createStore({
  modules: {
    auth,
  },
});

export default store;