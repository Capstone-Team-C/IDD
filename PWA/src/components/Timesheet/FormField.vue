<template>
  <v-row>
    <v-col 
      cols="1"
      v-if="this.parsed_value !== null"
    >
      <!-- Lock/Unlock icon to enable editing or reset & lock a parsed field -->
      <v-checkbox 
        off-icon="lock_open"
        on-icon="lock"
        v-model="fieldDisabled"
        @click.stop="askEdit()"
      ></v-checkbox>
      
      <!-- Warning dialog upon editing a parsed field -->
      <v-dialog
        v-model="editDialog"
          max-width="500px"
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
              @click="editDialog = false"
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
    </v-col>

    <!-- Form Field -->
    <!-- Check if field is a checkbox -->
    <v-col v-if="this.field_type === 1">
      <v-checkbox
        v-model="fieldValue"
        :disabled="fieldDisabled"
        :label="label"
        :rules="rules"
      ></v-checkbox>
    </v-col>

    <!-- Is not a checkbox, check if this field is a textarea -->
    <v-col v-else-if="this.rows > 1 || this.auto_grow">
      <v-textarea
        clearable
        outlined
        v-model="fieldValue"
        :auto-grow="auto_grow"
        :counter="counter"
        :disabled="fieldDisabled"
        :hint="hint"
        :label="label"
        :rows="rows"
        :rules="rules"
      ></v-textarea>
    </v-col>

    <!-- Is not a textarea, so it is a text-field -->
    <v-col v-else>
      <v-text-field
        clearable
        outlined
        v-model="fieldValue"
        :counter="counter"
        :disabled="fieldDisabled"
        :hint="hint"
        :label="label"
        :rules="rules"
      ></v-text-field>
    </v-col>
    <!-- End form Field -->
  </v-row>
</template>

<script>
export default {
  name: 'FormField',
  props: {
    // does the text field grow in size as more text is entered
    auto_grow: {
      type: Boolean,
      Default: false,
    },
    // max strlen for text field
    counter: {
      default: 25,
    },
    // is field blocked from being edited?
    disabled: {
      type: Boolean,
      default: false,
    },
    // 0 - text-field/textarea
    // 1 - checkbox
    field_type: {
      type: Number,
      default: 0,
    },
    // appears below text field
    hint: {
      type: String,
      default: "",
    },
    // appears above text field
    label: {
      type: String,
      default: "",
    },
    // was field parsed from .json?
    parsed: {
      type: Boolean,
      default: false,  
    },
    // value parsed from .json
    parsed_value: {
      default: null,
    },
    // Reset to default props or no
    reset: {
      type: Boolean,
      default: false,
    },
    // size of this text field
    rows: {
      type: Number,
      Default: 1,
    },
    // validation rules for text field
    rules: {
    },
    // value entered in text field
    value: {
      type: [String, Boolean, Number],
      Default: "",
    },
  },

  // Initialize component upon being first loaded onto the page
  created: function () {
    this.fieldDisabled = this.disabled;
    this.fieldValue = this.value;
  },
  
  // Manage fields that change on this page
  data: function () {
    return {
      fieldDisabled: false,
      fieldValue: '',
      editDialog: false,
    }
  },

  watch: {
    reset () {
      if (this.parsed_value !== null) {
        this.fieldDisabled = true 
        this.fieldValue = this.parsed_value
      } else {
        this.fieldValue = null
      }
    }
  },

  // Do an action or communicate info to parent component upon a certain
  // event
  methods: {
    // Display warning message before unlocking field for editing 
    // Will not display warning if re-locking field
    askEdit() {
      if (this.fieldDisabled === true) {
        this.editDialog = true
      } else {
        this.resetParsed()
      }
    },

    // For a parsed field, send a warning if being edited
    // Else, reset value to parsed value & disable field
    resetParsed () {
      // Unlock field and notify parent
      this.fieldDisabled = !this.fieldDisabled
      if (this.fieldDisabled === true) {
        this.fieldValue = this.parsed_value
        this.$emit('field-change', -1)
      } else {
        this.$emit('field-change', 1)
      }
    },
    
  }
}
</script>
