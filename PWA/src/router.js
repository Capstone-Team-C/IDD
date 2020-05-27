import Vue from "vue";
import VueRouter from "vue-router";
import Home from "./views/Home";

Vue.use(VueRouter);

export default new VueRouter({
  routes: [
    { path: "/", name: "home", component: Home, props: true },
    {
      path: "/timesheet",
      name: "timesheet",
      component: () => import("./views/Timesheet"),
      props: true,
    },
    {
      path: "/about",
      name: "about",
      component: () => import("./views/About"),
      props: true,
    },
    {
      path: "*",
      name: "not_found",
      component: () => import("./views/NotFound"),
      props: true,
    },
  ],

  methods: {
    goBack() {
      window.history.length > 1 ? this.$router.go(-1) : this.$router.push("/");
    },
  },
});
