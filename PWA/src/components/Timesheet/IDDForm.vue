<template>
  <v-form
    class="mx-9"
    lazy-validation
    ref="form"
    v-model="valid"
  >
    <p class="title">
      Front side of the form
    </p>

    <!-- Customer Name -->
    <FormField 
      v-bind="formFields.customerName"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    
    <!-- Prime -->
    <FormField 
      v-bind="formFields.prime"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    
    <!-- submissionDate -->
    <FormField 
      v-bind="formFields.submissionDate"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
          
    <!-- providerName -->
    <FormField 
      v-bind="formFields.providerName"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <!-- providerNumber-->
    <FormField 
      v-bind="formFields.providerNumber"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    
    <!-- SC/PA Name -->
    <FormField 
      v-bind="formFields['SC/PA Name']"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <!-- CMOrg -->
    <FormField 
      v-bind="formFields.CMOrg"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    
    <!-- service -->
    <FormField 
      v-bind="formFields.service"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
  
    <!-- Table containing timesheet  -->
    <v-card-text>
      <FormTable
        v-bind="formFields.serviceDeliveredOn"
        :reset="resetChild"
        @formtable-change="validateFormTable"
      />
    </v-card-text>

    <!-- totalHours -->
    <FormField 
      v-bind="formFields.totalHours"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <hr />

    <p class="title">
      Back side of the form
    </p>
    
    <!-- serviceGoals -->
    <FormField 
      v-bind="formFields.serviceGoal"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <!-- progressNotes -->
    <FormField 
      v-bind="formFields.progressNotes"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    
    <hr />
    
    <!-- Employer Verification Section -->
    <p class="subtitle-1">
      <strong>RECIPIENT/EMPLOYER VERIFICATION:</strong><br/>
      <em>
        I affirm that the data reported on this form is for actual
        dates/time worked by the provider delivering the service/supports
        listed to the recipient, that it does not exceed the total amount
        of service authorized and was delivered according to the 
        recipient's service plan and provider/recipient service agreement.
      </em>
    </p>
    
    <!-- employerSignature -->
    <FormField 
      v-bind="formFields.employerSignature"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <!-- employerSignDate-->
    <FormField 
      v-bind="formFields.employerSignDate"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    <!-- End Employer Verification Section -->
  
    <hr />

    <!-- Provider Verification Section -->
    <p class="subtitle-1">
      <strong>PROVIDER/EMPLOYEE VERIFICATION:</strong><br/>
      <em>
        I affirm that the data reported on this form is for actual
        dates/time I worked delivering the service/supports
        listed to the recipient, that it does not exceed the total amount
        of service authorized and was delivered according to the 
        recipient's service plan and provider/recipient service 
        agreement. I further acknowledge that reporting dates/tim worked
        in excess of the amount of service authorized or not consistent
        with the recipient's service plan may be considered Medicaid
        Fraud.
      </em>
    </p>
    
    <!-- providerSignature -->
    <FormField 
      v-bind="formFields.providerSignature"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />

    <!-- providerSignDate-->
    <FormField 
      v-bind="formFields.providerSignDate"
      :reset="resetChild"
      @field-change="handleFieldChange"
    />
    <!-- END Provider Verification Section -->

    <hr />
    
    <strong class="subtitle-1">
      <!-- authorization-->
      <FormField 
        v-bind="formFields.authorization"
        :reset="resetChild"
        @field-change="handleFieldChange"
      />

      <!-- providerInitials -->
      <FormField 
        v-bind="formFields.providerInitials"
        :reset="resetChild"
        @field-change="handleFieldChange"
      />

      Providers submit this completed/signed form to the CDDP, Brokerage
      or CIIS Program that authorized the service delivered.
    </strong>
  
    <hr />

    <v-btn
      color="success"
      class="mr-4"
      :disabled="!valid"
      @click="submit"
    >
      Submit 
    </v-btn>

    <v-btn
      color="error"
      class="mr-4"
      @click="reset"
    >
      Reset Form
    </v-btn>
  </v-form>
</template>

<script>
import FormTable from '@/components/Timesheet/FormTable'
import FormField from '@/components/Timesheet/FormField'

export default {
  name: 'IDDForm',
  components: {
    FormTable,
    FormField,
  },
  props: {
    // A .json file that is the parsed uploaded IDD timesheet data
    parsedFileData: {
      type: Object,
      default: null
    }
  },

  // Upon first loading on the page, bind parsed form data to each 
  // IDD Timesheet form field
  created: function () {
    this.initialize()
  },
  data: function () {
    // Generic form validation rules 
    let nameRules = [
      v => !!v || 'This field is required',
      v => (v && v.length <= 25) || 'This field must be less than 25 characters',
    ];

    let alphanumericRules = [
      v => !!v || 'This field is required',
      v => /^[a-zA-Z0-9]+$/.test(v) || 'This field must be letters or numbers',
    ];
    
    let numericRules = [
      v => !!v || 'This field is required',
      v => /^[0-9]+$/.test(v) || 'This field must be a number',
    ];

    let monthyearRules = [
      v => !!v || 'This field is required',
      v => /^[0-9]{4}-[01][0-9]$/.test(v) || 'This field must be in format YYYY-MM',
    ];

    let dateRules = [
      v => !!v || 'This field is required',
      v => /^[0-9]{4}-[01][0-9]-[0123][0-9]$/.test(v) || 'This field must be in format YYYY-MM-DD',
    ];

    /*
    let emailRules = [
      v => !!v || 'E-mail is required',
      v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
    ];
    */

    return {
      // Reset form of arbitrary value
      resetChildField: false,

      // Default form field values and props
      formFields: {
        // Front side of form
        customerName: {
          counter: 25,              // max strlen for text field
          disabled: false,          // is field blocked from being edited?
          hint: "Full name",        // appears below text field
          label: "Customer Name",   // appears above text field
          parsed: false,            // was field parsed from .json?
          parsed_value: null,          // value parsed from .json
          rules: nameRules,         // validation rules for text field
          value: null,                // value entered in text field
        },
        prime: { 
          counter: 8,
          disabled: false,
          label: "Prime",
          parsed: false,
          parsed_value: null,
          rules: alphanumericRules,
          value: null,
        },
        submissionDate: { 
          disabled: false,
          hint: "YYYY-MM",
          label: "Pay Period Month and Year",
          parsed: false,
          parsed_value: null,
          rules: monthyearRules,
          value: null,
        },
        providerName: { 
          counter: 25,
          disabled: false,
          hint: "Full name",
          label: "Provider Name",
          parsed: false,
          parsed_value: null,
          rules: nameRules,
          value: null,
        },
        providerNumber: { 
          counter: 6,
          disabled: false,
          label: "Provider Number",
          parsed: false,
          parsed_value: null,
          rules: numericRules,
          value: null,
        },
        'SC/PA Name': { 
          counter: 25,
          disabled: false,
          label: "SC/PA Name",
          parsed: false,
          parsed_value: null,
          rules: nameRules,
          value: null,
        },
        CMOrg: { 
          counter: 50,
          disabled: false,
          label: "CM Organization",
          parsed: false,
          parsed_value: null,
          rules: nameRules,
          value: null,
        },
        service: {
          counter: 100,
          disabled: false,
          label: "Service",
          parsed: false,
          parsed_value: null,
          rules: nameRules,
          value: null,
        },
        serviceDeliveredOn: {
          disabled: false,
          parsed: false,
          parsed_value: null,
          value: null,
        },
        totalHours: { 
          counter: 5,
          disabled: false,
          // HH:mm
          hint: "HH:mm",
          label: "Total Hours",
          parsed: false,
          parsed_value: null,
          //mask: "##:##", // I dont know why, but this doesn't work
          value: "",
        },
        
        // Back side of form
        serviceGoal: { 
          auto_grow: true,
          counter: 250,
          disabled: false,
          label: "Service Goal",
          parsed: false,
          parsed_value: null,
          rows: 1,
          rules: nameRules,
          value: "",
        },
        progressNotes: { 
          auto_grow: true,
          counter: 500,
          disabled: false,
          label: "Progress Notes",
          parsed: false,
          parsed_value: null,
          rows: 5,
          rules: nameRules,
          value: "",
        },
        employerSignDate: { 
          disabled: false,
          hint: "YYYY-MM-DD",
          label: "Date",
          parsed: false,
          parsed_value: null,
          rules: dateRules,
          value: null,
        },
        employerSignature: { 
          disabled: false,
          field_type: 1,
          label: "Customer Employer or Employer Rep Signature",
          parsed: false,
          parsed_value: null,
          value: false,
        },
        providerSignDate: { 
          disabled: false,
          label: "Date",
          modified: true,
          parsed: false,
          parsed_value: null,
          rules: dateRules,
          value: null,
        },
        providerSignature: { 
          disabled: false,
          field_type: 1,
          label: "Provider/Employee Signature",
          parsed: false,
          parsed_value: null,
          value: false,
        },
        authorization: { 
          disabled: false,
          field_type: 1,
          label: "I authorize the CDDP/Brokerage/CIIS staff to enter the\
          data reported on this form into eXPRS on my behalf for claims\
          creation and payment.",
          parsed: false,
          parsed_value: null,
          value: false,
        },
        providerInitials: { 
          disabled: false,
          label: "Provider Initials",
          parsed: false,
          parsed_value: null,
          rules: nameRules,
          value: null,
        },
      },
      
      // The amount of parsed fields that were edited
      amtEdited: 0,
      
      // Hide form validation error messages by default
      valid: true,
    }
  },
  computed: {
      resetChild () {
        return this.resetChildField
      }
  },
  methods: {
    initialize() {
      // Bind data from a .json IDD timesheet to form fields
      if (this.entries !== null) {
        Object.entries(this.parsedFileData).forEach(([key, value]) => {
          if (key in this.formFields) {
            this.formFields[key]['parsed_value'] = value;
            this.formFields[key]['value'] = value;
            this.formFields[key]['disabled'] = true;
            this.formFields[key]['parsed'] = true;
          } else {
            console.log("Unrecognized parsed form field from server: " +
            `${key} - ${value}`);
          }
        });
      }
    },

    validateFormTable (hours) {
      // TODO - Compare form table total hours with parsed total hours
      hours;
    },

    submit () {
      this.$refs.form.validate()
    },

    reset () {
      this.initialize()
      this.resetChildField = !this.resetChildField
      this.$refs.form.resetValidation()
    },

    resetValidation () {
      this.$refs.form.resetValidation()
    },
    
    // For a parsed field, send a warning if being edited
    // Else, reset value to parsed value & disable field
    resetParsed (field) {
      if (this.formFields[field].parsed === true) {
        if (this.formFields[field].disabled === true) {
          this.amtEdited -= 1
        } else {
          this.amtEdited += 1 
          this.formFields[field].value = this.formFields[field].parsed_value
        }
        this.formFields[field].disabled = !this.formFields[field].disabled
      }
    },
    // Update the amount of parsed fields edited
    handleFieldChange (amt) {
      this.amtEdited += amt
    }
  },
}
</script>
