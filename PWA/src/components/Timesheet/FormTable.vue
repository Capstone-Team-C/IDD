<template>
  <v-data-table
    class="elevation-1"
    :headers="headers"
    :items="allEntries"
  >
    <!-- The header of the table -->
    <template v-slot:top>
      <v-toolbar flat >
        <v-toolbar-title>
          Service Delivered On:
        </v-toolbar-title>

        <v-spacer></v-spacer>
        
        <!-- Appears upon adding/editing an item -->
        <v-dialog 
          max-width="500px"
          v-model="dialog" 
        >
          <!-- Display this button, even though parent is hidden -->
          <template v-slot:activator="{ on }">
            <v-btn 
              color="primary" 
              v-on="on"
            >
              Add a Row
            </v-btn>
          </template>

          <!-- The dialog box title --> 
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
          
            <!-- The form area -->
            <v-card-text>
              <v-container>

                <!-- Date selector -->
                <v-menu
                  min-width="290px"
                  offset-y
                  transition="scale-transition"
                  v-model="openDate"
                  :close-on-content-click="false"
                  :nudge-right="40"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="editedItem.date"
                      label="Date"
                      prepend-icon="event"
                      readonly
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker 
                    v-model="editedItem.date" 
                    @input="openDate = false"
                  ></v-date-picker>
                </v-menu>
                <!-- END Date selector -->
                
                <!-- Time IN selector -->
                <v-menu
                  ref="refStartTime"
                  v-model="openStartTime"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  :return-value.sync="editedItem.startTime"
                  transition="scale-transition"
                  offset-y
                  max-width="290px"
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      label="Start/Time IN"
                      prepend-icon="access_time"
                      readonly
                      v-model="editedItem.startTime"
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-time-picker
                    full-width
                    v-if="openStartTime"
                    v-model="editedItem.startTime"
                  >

                    <!-- Cancel/Save panel -->
                    <v-spacer></v-spacer>
                    <v-btn 
                      text 
                      color="primary" 
                      @click="openStartTime = false"
                    >
                      Cancel
                    </v-btn>
                    <v-btn 
                      text 
                      color="primary" 
                      @click="$refs.refStartTime.save(editedItem.startTime)"
                    >
                      OK
                    </v-btn>
                  </v-time-picker>
                </v-menu>
                <!-- END Time IN selector -->
                
                <!-- Time OUT selector -->
                <v-menu
                  max-width="290px"
                  min-width="290px"
                  offset-y
                  ref="refEndTime"
                  transition="scale-transition"
                  v-model="openEndTime"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  :return-value.sync="editedItem.endTime"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      label="End/Time OUT"
                      prepend-icon="access_time"
                      readonly
                      v-model="editedItem.endTime"
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-time-picker
                    v-if="openEndTime"
                    v-model="editedItem.endTime"
                    full-width
                  >

                    <!-- Cancel/Save Panel -->
                    <v-spacer></v-spacer>
                    <v-btn 
                      text 
                      color="primary" 
                      @click="openEndTime = false"
                    >
                      Cancel
                    </v-btn>
                    <v-btn 
                      text 
                      color="primary" 
                      @click="$refs.refEndTime.save(editedItem.endTime)"
                    >
                      OK
                    </v-btn>
                  </v-time-picker>
                </v-menu>
                <!-- END Time OUT selector -->
                
                <v-text-field 
                  v-model="editedItem.totalHours" 
                    label="Total Hours"
                ></v-text-field>

                <v-checkbox
                  label="Group? (y/n)"
                  v-model="editedItem.group"
                ></v-checkbox>
              </v-container>
            </v-card-text>
            <!-- END form area -->
          
            <!-- Cancel/Save edited item panel -->
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn 
                color="red darken-1" 
                text 
                @click="close"
              >
                Cancel
              </v-btn>
              <v-btn 
                color="green darken-1" 
                text 
                @click="save"
              >
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- END Appears upon adding/editing an item -->
      </v-toolbar>
    </template>
    
    <!-- The data area of the table -->
    <template 
      v-slot:item.actions="{ item }"
    >
      <v-icon
        class="mr-2"
        small
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

    <template 
      v-slot:no-data
    >
      <v-btn 
        color="red" 
        @click="initialize"
        >
          Reset
        </v-btn>
    </template>
  </v-data-table>
</template>

<script>
export default {
  name: 'FormTable',
  props: {
    // A .json file that is a section from the parsed uploaded IDD timesheet data
    entries: {
      type: Array,
      default: null
    }
  },
  data: function () {
    return {
      colNames: [
        'date', 'startTime', 'endTime', 'totalHours', 'group'
      ],
      headers: [
        { text: 'Date', align: 'start', value: 'date', sortable: false },
        { text: 'Start/Time IN', value: 'startTime', sortable: false  },
        { text: 'Start/Time OUT', value: 'endTime', sortable: false  },
        { text: 'Total Hours', value: 'totalHours', sortable: false  },
        { text: 'Group?', value: 'group', sortable: false  },
        { text: 'Actions', value: 'actions', sortable: false },
      ],

      // Hide add/edit entry dialog box
      dialog: false,
      formTitle: 'Edit Row',
      
      // Date, Time IN, Time OUT
      openDate: false,
      refStartTime: false,
      openStartTime: false,
      refEndTime: false,
      openEndTime: false,
      
      // All entries and entry currently being added/edited
      allEntries: [],
      editedIndex: -1,
 
      defaultItem: {
        date: '',
        timeIn: '',
        timeOut: '',
        timeTotal: 0,
        group: false,
      },

      // Holds the new/edited entry
      editedItem: {
        date: new Date().toISOString().substr(0, 10),
        startTime: '',
        endTime: '',
        totalHours: 0,
        group: false,
      },
    }
  },
  // Parent -> Child communication
  watch: {
    // If the props passed from parent changes, add any new entries
    // to the data table
    entries (newVal) {
      this.entries = newVal
      this.initialize()
    },
    dialog (val) {
      val || this.close()
    },
  },
  methods: {
    initialize () {
      if (this.entries !== null) {
        // For each timesheet table entry, create a new set 'obj'
        this.entries.forEach(row => {
          let obj = {};
          // If the attributes in the .json are expected, add them to 'obj'
          Object.entries(row).forEach(([key,value]) => {
            if (this.colNames.includes(key)) {
              obj[key] = value;
            } else {
              console.log("Unrecognized parsed form field from server: " +
              `${key} - ${value}`);
            }
          });
          // Finally, add obj to an array if it is not empty
          if (Object.keys(obj).length > 0) {
            this.allEntries.push(obj);
          }
        });
      }
    },

    editItem (item) {
      this.editedIndex = this.allEntries.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
      const index = this.allEntries.indexOf(item)
      confirm('Are you sure you want to delete this item?') && this.allEntries.splice(index, 1)
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
        Object.assign(this.allEntries[this.editedIndex], this.editedItem)
      } else {
        this.allEntries.push(this.editedItem)
      }
      this.close()
    },
  },
}
</script>
