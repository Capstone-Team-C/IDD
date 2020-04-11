<template>
  <v-data-table
    class="elevation-1"
    :headers="headers"
    :items="allEntries"
    @formtable-change="validateFormTable"
  >
    <!-- Table toolbar -->
    <template v-slot:top>
      <v-toolbar flat >
        <v-toolbar-title>
          Service Delivered On:
        </v-toolbar-title>

        <v-spacer></v-spacer>
       

        <!-- Warning dialog upon editing a parsed field -->
        <v-dialog
          max-width="500px"
          v-model="editDialog"
        >
          <v-card>
            <v-card-title class="headline">Do you want to edit this parsed field?</v-card-title>

            <v-card-text>
              This field was parsed by AWS Textrack, based on the IDD
              timesheet that was uploaded. If you edit this field, the
              employer and provider <em>must</em> re-sign this form
              at the bottom before submission.
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>

              <v-btn
                color="red darken-1"
                text
                @click="editDialog = false;"
              >
               Cancel edit
              </v-btn>

              <v-btn
                color="green darken-1"
                text
                @click="editDialog = false; resetParsed()"
              >
                Edit field
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- END warning dialog upon editing a parsed field -->

        <!-- Appears upon adding/editing an item -->
        <v-dialog 
          max-width="500px"
          v-model="dialog" 
        >

          <!-- Display this button, even though parent is hidden -->
          <template 
            class="d-flex flex-row-reverse"  
            v-slot:activator="{ on }"
          > 

            <v-btn-toggle
              dense
              multiple
            >
            <!-- Lock/unlock addding a row to the table -->
            <v-btn
              @click="askTableEdit()"
            >
              <v-icon color="primary" v-if="tableEdited === true">mdi-lock</v-icon>
              <v-icon v-else>mdi-lock-open</v-icon>
            </v-btn>

            <!-- Add a row button -->
            <v-btn 
              color="success"
              v-on="on"
              :disabled="tableEdited"
            >
              <v-icon color="white">mdi-plus</v-icon>
            </v-btn>
            
            <!-- Reset table button -->
            <v-btn
              color="error"
              :disabled="tableEdited"
              @click="initialize"
            >
              <v-icon color="white">mdi-refresh</v-icon>
            </v-btn>

</v-btn-toggle>
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
                  true-value="Yes"
                  false-value="No"
                  v-model="editedItem.group"
                >
                </v-checkbox>
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
    <!-- END table toolbar -->

    <!-- The action column of the table -->
    <template 
      v-slot:item.actions="{ item }"
    >
      <v-row>
        <!-- Unlock editing a row of the table --> 
        <v-simple-checkbox 
          color="primary"
          off-icon="lock_open"
          on-icon="lock"
          v-model="item.disabled"
          :ripple="false"
          @input="askEdit(item)"
        ></v-simple-checkbox>

        <v-icon
          class="mr-2"
          small
          :disabled="item.disabled"
          @click="editItem(item)"
        >
          mdi-pencil
        </v-icon>

        <v-icon
          small
          :disabled="item.disabled"
          @click="deleteItem(item)"
        >
          mdi-delete
        </v-icon>
      </v-row>
    </template>
  </v-data-table>
</template>

<script>
export default {
  name: 'FormTable',
  props: {
    // A .json file that is a section from the parsed uploaded IDD timesheet data
    value: {
      type: Array,
      default: null,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    modified: {
      type: Boolean,
      default: true,
    },
    parsed: {
      type: Boolean,
      default: false,
    },
    // Reset to default props or no
    reset: {
      type: Boolean,
      default: false,
    },
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

      // Record if table added new rows or was reset
      tableEdited: false,
      editTableDialog: false,

      // Hide add/edit entry dialog box
      dialog: false,
      editDialog: false,
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
        timeTotal: '',
        group: "No",
        disabled: false,
        modified: true,
        parsed: false,
      },

      // Holds the new/edited entry
      editedItem: {
        date: new Date().toISOString().substr(0, 10),
        startTime: '',
        endTime: '',
        totalHours: '',
        group: "No",
        disabled: false,
        modified: true,
        parsed: false,
      },
    }
  },
  
  watch: {
    reset () {
      this.initialize()
    }
  },

  created: function () {
    this.initialize()
  },

  methods: {
    initialize () {
      // Reset the current entries in the table
      this.allEntries = [];
      this.tableEdited = false;
      // TODO - Notify parent of reset

      if (this.value !== null) {
        // For each timesheet table entry, create a new set 'obj'
        this.value.forEach(row => {
          let obj = {};
          // If the attributes in the .json are expected, add them to 'obj'
          Object.entries(row).forEach(([key,value]) => {
            if (this.colNames.includes(key)) {
              obj[key] = value;
              if (!('parsedValue' in obj)) {
                obj['parsedValue'] = {}
              }
              obj['parsedValue'][key] = value;
            } else {
              console.log("Unrecognized parsed form field from server: " +
              `${key} - ${value}`);
            }
          });
          // Finally, add obj to an array if it is not empty
          if (Object.keys(obj).length > 0) {
            obj['parsed'] = this.parsed;
            obj['disabled'] = this.disabled;
            this.allEntries.push(obj);

            // Disable the add row and reset table button
            this.tableEdited = true;
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
      this.validateFormTable()
    },

    validateFormTable () { 
      // TODO - this is just here for testing 
      var totalHours = -1
      this.$emit('formtable-change', totalHours)
    },
    
    // Ask before adding a row to the table
    // Or, reset table to parsed vals
    askTableEdit() {
      this.editTableDialog = true
      if (this.tableEdited === true) {
        this.editDialog = true
      } else {
        this.resetParsed()
      }
    }, 
    
    // Ask before editing a single parsed table row
    // Or, reset & lock a parsed table row
    askEdit(item) {
      // v-simple-checkbox is specially made for v-data-table, but it
      // does not have a @click.stop; only a @input
      // Disabling right here mocks the @click.stop behavior
      item.disabled = !item.disabled

      // Globally select this row in the table, so other methods
      // can access it without needing 'item' as an arg
      this.editedIndex = this.allEntries.indexOf(item)
      this.editedItem = Object.assign({}, item)

      // Trying to unlock a parsed row -- show warning
      // Else, reset row to parsed value & lock
      if (item.disabled === true) {
        this.editDialog = true
      } else {
        this.initialize()
      }
    },

    resetParsed () { 
      // Check if entire table/adding row or editing a single row 
      if (this.editTableDialog === true) {
        if (this.tableEdited !== true) {
          this.initialize()
        } else {
          this.tableEdited = !this.tableEdited
        }
        this.editTableDialog = false
      } else {
        // If item was disabled, just enable editing
        // else reset row to parsed value & disable editing 
        if (this.editedItem.disabled !== true) {
          // For each field in this row, reset field to parsed value
          Object.entries(this.editedItem.parsedValue).forEach( ([key, value]) => {
          this.editedItem[key] = value 
          })
        }
        
        // Flip lock/unlock of parsed row & save
        this.editedItem.disabled = !this.editedItem.disabled 
        this.save()
      }
    },

  },
}
</script>
