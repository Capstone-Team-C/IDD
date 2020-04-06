<template>
  <v-data-table
    :headers="headers"
    :items="services"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar flat color="white">
        <v-toolbar-title>Service Delivered On:</v-toolbar-title>
        <v-divider
          class="mx-4"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on">Add a Row</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
        

        <v-menu
        v-model="menu3"
        :close-on-content-click="false"
        :nudge-right="40"
        transition="scale-transition"
        offset-y
        min-width="290px"
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="editedItem.date"
            label="Picker without buttons"
            prepend-icon="event"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker v-model="editedItem.date" @input="menu3 = false"></v-date-picker>
      </v-menu>

<v-menu
        ref="menu9"
        v-model="menu4"
        :close-on-content-click="false"
        :nudge-right="40"
        :return-value.sync="editedItem.timeIn"
        transition="scale-transition"
        offset-y
        max-width="290px"
        min-width="290px"
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="editedItem.timeIn"
            label="Start/Time IN"
            prepend-icon="access_time"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-time-picker
          v-if="menu4"
          v-model="editedItem.timeIn"
          full-width
        >
 <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menu4 = false">Cancel</v-btn>
          <v-btn text color="primary" @click="$refs.menu9.save(editedItem.timeIn)">OK</v-btn>
        </v-time-picker>
      </v-menu>

<v-menu
        ref="menu"
        v-model="menu5"
        :close-on-content-click="false"
        :nudge-right="40"
        :return-value.sync="editedItem.timeOut"
        transition="scale-transition"
        offset-y
        max-width="290px"
        min-width="290px"
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="editedItem.timeOut"
            label="Start/Time OUT"
            prepend-icon="access_time"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-time-picker
          v-if="menu5"
          v-model="editedItem.timeOut"
          full-width
        >
 <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menu5 = false">Cancel</v-btn>
          <v-btn text color="primary" @click="$refs.menu.save(editedItem.timeOut)">OK</v-btn>
        </v-time-picker>
      </v-menu>

                    <v-text-field v-model="editedItem.timeTotal" label="Total Hours"></v-text-field>
                    <v-text-field v-model="editedItem.clients" label="# of Clients"></v-text-field>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="close">Cancel</v-btn>
              <v-btn color="blue darken-1" text @click="save">Save</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon
        small
        class="mr-2"
        @click="editItem(item)"
      >
        mdi-pencil
      </v-icon>
      <v-icon
        small
        @click="deleteItem(item)"
      >
        mdi-delete
      </v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize">Reset</v-btn>
    </template>
  </v-data-table>
</template>

<script>
  export default {
    data: () => ({ 
        time: null,
        menu3: false,
        menu4: false,
        modal2: false,
date: new Date().toISOString().substr(0, 10),
      menu: false,
      menu9: false,
      modal: false,
      menu5: false,


      dialog: false,
      headers: [
        { text: 'Date', align: 'start', value: 'date', sortable: false },
        { text: 'Start/Time IN', value: 'timeIn', sortable: false  },
        { text: 'Start/Time OUT', value: 'timeOut', sortable: false  },
        { text: 'Total Hours', value: 'timeTotal', sortable: false  },
        { text: '# of Clients', value: 'clients', sortable: false  },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      services: [],
      editedIndex: -1,
      editedItem: {
        date: new Date().toISOString().substr(0, 10),
        timeIn: '',
        timeOut: '',
        timeTotal: 0,
        clients: 0,
      },
      defaultItem: {
        date: '',
        timeIn: '',
        timeOut: '',
        timeTotal: 0,
        clients: 0,
      },
    }),

    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
      },
    },

    watch: {
      dialog (val) {
        val || this.close()
      },
    },

    created () {
      this.initialize()
    },

    methods: {
      initialize () {
      },

      editItem (item) {
        this.editedIndex = this.services.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },

      deleteItem (item) {
        const index = this.services.indexOf(item)
        confirm('Are you sure you want to delete this item?') && this.services.splice(index, 1)
      },

      close () {
        this.dialog = false
        setTimeout(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        }, 300)
      },

      save () {
        if (this.editedIndex > -1) {
          Object.assign(this.services[this.editedIndex], this.editedItem)
        } else {
          this.services.push(this.editedItem)
        }
        this.close()
      },
    },
  }
</script>
