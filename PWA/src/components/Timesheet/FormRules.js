// Generic form validation rules
let rules = {
  nameRules: [
    (v) => !!v || "This field is required",
    (v) =>
      (v && v.length <= 25) || "This field must be less than 25 characters",
  ],
  alphanumericRules: [
    (v) => !!v || "This field is required",
    (v) => /^[a-zA-Z0-9]+$/.test(v) || "This field must be letters or numbers",
  ],
  numericRules: [
    (v) => !!v || "This field is required",
    (v) => /^[0-9]+$/.test(v) || "This field must be a number",
  ],
  timeRules: [
    (v) => !!v || "This field is required",
    (v) =>
      /^[0-1][0-9]:[0-6][0-9] [AaPp][Mm]$/.test(v) ||
      "This field must be in format HH:mm AM/PM",
  ],
  monthyearRules: [
    (v) => !!v || "This field is required",
    (v) =>
      /^[0-9]{4}-[01][0-9]$/.test(v) || "This field must be in format YYYY-MM",
  ],
  dateRules: [
    (v) => !!v || "This field is required",
    (v) =>
      /^[0-9]{4}-[01][0-9]-[0123][0-9]$/.test(v) ||
      "This field must be in format YYYY-MM-DD",
  ],
};
/*
let emailRules = [
  v => !!v || 'E-mail is required',
  v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
];
*/

export default rules;
