<template>
  <div id="app">
    <div class="page-container">
      <md-app md-waterfall md-mode="reveal">
        <md-app-toolbar style="z-index:5; position: fixed;">
          <md-button class="md-icon-button" @click="menuVisible = !menuVisible">
            <md-icon>menu</md-icon>
          </md-button>
          <span class="md-title">
          <img alt="Multnomah County logo" src="./assets/icons/logo_short.svg">
          IDD Timesheet Automation
          </span>
        </md-app-toolbar>

        <md-app-drawer :md-active.sync="menuVisible">
          <md-toolbar class="md-transparent" md-elevation="0">Navigation</md-toolbar>

          <md-list>
            <md-list-item>
              <md-icon>home</md-icon>
              <span class="md-list-item-text">Home</span>
            </md-list-item>

            <md-list-item>
              <md-icon>playlist_add</md-icon>
              <span class="md-list-item-text">Submit a Timesheet</span>
            </md-list-item>

            <md-list-item>
              <md-icon>help</md-icon>
              <span class="md-list-item-text">Help</span>
            </md-list-item>

            <md-list-item>
              <md-icon>info</md-icon>
              <span class="md-list-item-text">About</span>
            </md-list-item>
          </md-list>
        </md-app-drawer>

        <md-app-content>
          <HelloWorld msg="IDD Timesheet Happy Path"/>
        </md-app-content>
      </md-app>
    </div>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'

import Vue from 'vue'
import { MdMenu, MdList, MdLayout, MdToolbar, MdIcon, MdButton, MdApp, MdDrawer } from 'vue-material/dist/components'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'

Vue.use(MdMenu)
Vue.use(MdDrawer)
Vue.use(MdApp)
Vue.use(MdToolbar)
Vue.use(MdList)
Vue.use(MdLayout)
Vue.use(MdIcon)
Vue.use(MdButton)


export default {
  name: 'App',
  components: {
    HelloWorld
  },
  data: () => ({
    menuVisible: false 
  })
}
</script>

<style lang="scss" scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.md-app-content {
  padding: 5%;
}

  .md-app {
    max-height: 100vh;
    border: 1px solid rgba(#000, .12);
  }

   // Demo purposes only
  .md-drawer {
    width: 230px;
    max-width: calc(100vw - 125px);
  }
</style>
