<template>
  <v-form
    class="mx-9"
    lazy-validation
    ref="form"
    v-model="valid"
  >
    <v-text-field
      v-model="email"
      :rules="emailRules"
      label="E-mail"
      required
    ></v-text-field>
    
    <v-text-field
      filled
      label="Client Name"
      required
      v-model="clientName"
      :counter="10"
      :rules="nameRules"
    ></v-text-field>
      
        <v-text-field
            v-model="prime"
            filled
            label="Prime"
        ></v-text-field>
      
      <v-menu
        v-model="menu2"
        :close-on-content-click="false"
        :nudge-right="40"
        transition="scale-transition"
        offset-y
        min-width="290px"
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="submissionDate"
            label="Pay Period Month and Year"
            prepend-icon="event"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker type="month" v-model="submissionDate" @input="menu2 = false"></v-date-picker>
      </v-menu>
          
      <v-text-field
        v-model="providerName"
        :counter="10"
        :rules="nameRules"
        label="Provider Name"
        filled
        required
      ></v-text-field>

        <v-text-field
            v-model="providerNum"
            filled
            label="Provider Num"
        ></v-text-field>

        <v-text-field
          v-model="cddp_brokerage"
          :counter="10"
          :rules="nameRules"
          label="CDDP/Brokerage"
          filled
          required
        ></v-text-field>

        <v-text-field
            v-model="sc_paName"
            filled
            label="SC/PA Name"
        ></v-text-field>

      <v-text-field
        v-model="serviceAuthorized"
        :counter="10"
        :rules="nameRules"
        label="Service Authorized"
        filled
        required
      ></v-text-field>

      <v-text-field
          v-model="units"
          filled
          label="Units"
      ></v-text-field>

      <v-text-field
          v-model="type"
          filled
          label="Type"
      ></v-text-field>

      <v-text-field
          v-model="frequency"
          filled
          label="Frequency"
      ></v-text-field>
    
    <FormTable />
   

    <v-btn
      :disabled="!valid"
      color="success"
      class="mr-4"
      @click="validate"
    >
      Validate
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

export default {
  name: 'IDDForm',
  components: {
    FormTable  
  },
  props: [
    
  ],
  data: function () {
    return {
      date: new Date().toISOString().substr(0, 10),
      menu: false,
      modal: false,
      menu2: false,
      valid: true,
      clientName: '',
      prime: 0,
      submissionDate: '',
      providerName: '',
      providerNum: 0,
      cddp_brokerage: 0,
      sc_paName: 0,
      serviceAuthorized: '',
      units: 0,
      type: '',
      frequency: 0,
      nameRules: [
        v => !!v || 'Name is required',
        v => (v && v.length <= 10) || 'Name must be less than 10 characters',
      ],
      email: '',
      emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
      ]
    }
  },
  methods: {
    validate () {
      this.$refs.form.validate()
    },
    reset () {
      this.$refs.form.reset()
    },
    resetValidation () {
      this.$refs.form.resetValidation()
    },
  },
}
</script>
