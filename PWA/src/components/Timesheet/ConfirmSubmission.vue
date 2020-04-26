<template>
  <v-row justify="center">
    <v-btn color="success" dark @click.stop="signalParentValidate">
      Submit
    </v-btn>

    <v-dialog v-model="displaySubmit">
      <div v-if="isValid">
        <v-card>
          <div v-if="!loading">
            <v-card-title class="headline" id="confirm">
              Are you sure want to submit the form?
            </v-card-title>

            <v-card-text>
              Some text talking about how this submission is final unless
              something is wrong with it.
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>

              <!-- Confirm if user is ready to submit -->
              <v-btn color="red" text @click="displaySubmit = false">
                Cancel
              </v-btn>
              <v-btn text color="green darken-1" @click="submit">
                Submit
              </v-btn>
            </v-card-actions>
          </div>

          <div v-else>
            <div v-if="!returnHome">
              <!-- Submitting the form -->
              <div class="text-center">
                <v-progress-circular
                  indeterminate
                  color="primary"
                  id="progress"
                  :size="50"
                ></v-progress-circular>
                <p class="text--disabled">Submitting form</p>
              </div>
            </div>

            <div v-else>
              <!-- Display submission status -->
              <div v-if="submissionStatus">
                <v-card-title class="headline text-center" id="submited">
                  Your form has been submitted!
                </v-card-title>

                <v-card-text class="text-center" id="submissionError">
                  Some text on what will come next for the employee.
                </v-card-text>
              </div>
              <div v-else>
                <v-card-title class="headline" id="failure">
                  Something has gone wrong
                </v-card-title>

                <v-card-text>
                  Please try again.
                </v-card-text>
              </div>
            </div>
          </div>
        </v-card>
      </div>

      <!-- The form is not valid -->
      <div v-else>
        <v-card>
          <v-card-title class="headline text-danger" id="invalid">
            Your form is not valid.
          </v-card-title>

          <v-card-text>
            Please fix the invalid parts of the form and then retry submitting
            your form.
            <hr />
            Errors:
            <v-card v-for="(error, index) in errors" :key="index">
              {{ error }}
            </v-card>
          </v-card-text>
        </v-card>
      </div>
    </v-dialog>
  </v-row>
</template>

<style>
  .v-card__text,
  .v-card__title {
    word-break: normal; /* maybe !important  */
  }
  .v-progress-circular {
    margin: 1rem;
  }
  .p {
    margin-bottom: 0 !important;
  }
  .v-application p {
    margin-bottom: 0 !important;
  }
</style>

<script>
  import axios from "axios";
  import time_functions from "@/components/Timesheet/TimeFunctions.js";

  export default {
    name: "ConfirmSubmission",
    props: {
      //If the information is valid.
      valid: {
        type: Boolean,
        default: false,
      },

      // Signal that parent form has completed validation
      validationSignal: {
        type: Boolean,
        default: false,
      },

      //User (edited) information.
      formFields: {
        type: Object,
        default: null,
      },

      // The amount of parsed fields that were edited
      totalEdited: {
        type: Number,
        default: 0,
      },
    },

    data() {
      return {
        // Hide or display the submit pop-up
        displaySubmit: false,

        //Log of POST connection.
        loading: false,

        //Track when the POST completes
        submissionStatus: false,

        //Flag for once POST has been successful/failed
        returnHome: false,

        //Data to be submitted
        submitData: null,

        // All the errors of this form
        errors: [],
        isValid: this.valid,
        waitingOnParent: false,

        //URL for the AppServer
        url: process.env.VUE_APP_SERVER_URL.concat("Submit"),
      };
    },

    watch: {
      valid(newVal) {
        this.isValid = newVal;
      },

      validationSignal() {
        if (this.waitingOnParent === true) {
          this.waitingOnParent = false;
          this.displaySubmit = true;
        }
      },
    },

    methods: {
      // Compute the sum of all serviceDeliveredOn totalHours with the totalHours field
      sumTableHours() {
        var sumHours = 0;
        var sumMinutes = 0;

        // For each row in the array of entries...
        this.formFields["serviceDeliveredOn"]["value"].forEach((entry) => {
          // Check that the totalHours field is valid
          if (entry["errors"]["totalHours"].length == 0) {
            sumHours += parseInt(entry["totalHours"].substr(0, 2));
            sumMinutes += parseInt(entry["totalHours"].substr(3, 2));
          }
        });
        sumHours += (sumMinutes - (sumMinutes % 60)) / 60;
        sumMinutes %= 60;

        return sumHours.toString() + ":" + sumMinutes.toString();
      },

      // Count the number of errors in the serviceDeliveredOn table
      getTableErrors() {
        var numErrors = 0;

        // For each row in the array of entries...
        this.formFields["serviceDeliveredOn"]["value"].forEach(
          (entry, index) => {
            // For each error col in an entry, check the amount of errors
            Object.entries(entry["errors"]).forEach(([col, errors]) => {
              var colErrors = errors.length;
              if (colErrors > 0) {
                this.errors.push(
                  `ERROR: in row ${
                    index + 1
                  } of the serviceDeliveredOn table, '${col}' has the following errors:`,
                  errors
                );
                numErrors += colErrors;
              }
            });
          }
        );
        return numErrors;
      },

      // Validate the form
      validate() {
        // Reset all error messages
        this.errors = [];
        var numErrors = 0;

        // Check parent's response on validity of input fields
        if (!this.valid) {
          numErrors += 1;
          this.errors.push("ERROR: Invalid input in some form fields!");
          this.isValid = false;
        }

        // Check the validity of the serviceDeliveredOn table
        numErrors += this.getTableErrors();

        // Ensure that the serviceDeliveredOn table sum == totalHours field
        if (this.formFields.totalHours.value !== null) {
          var sumHours = this.sumTableHours();
          if (sumHours.localeCompare(this.formFields.totalHours.value) !== 0) {
            numErrors += 1;
            this.errors.push(
              `ERROR: valid rows in the serviceDeliveredOn table sums up to ${sumHours} hours, but the totalHours field reports ${this.formFields.totalHours.value} hours!`
            );
          }
        }

        // If there were no edited fields, ensure that the provider and
        // employer signature date are after the last service date
        if (this.totalEdited <= 0) {
          // Only compare the earlier date
          var comparisonDate = this.formFields.providerSignDate.value;
          if (
            time_functions.dateCompare(
              comparisonDate,
              this.formFields.employerSignDate.value
            ) > 0
          ) {
            comparisonDate = this.formFields.employerSignDate.value;
          }

          // Compare signage dates with the pay period
          // Note, only comparing the YYYY-mm part
          var submissionDate = this.formFields.submissionDate.value;
          if (
            time_functions.dateCompare(
              comparisonDate.substr(0, 7),
              submissionDate
            ) < 0
          ) {
            numErrors += 1;
            this.errors.push(
              `ERROR: the employer or provider sign date is before the pay period.`
            );
          }

          // Get the last date from the serviceDeliveredOn table
          var latestDateIdx = this.formFields.serviceDeliveredOn.value.length;
          if (latestDateIdx > 0) {
            var latestDate = this.formFields.serviceDeliveredOn.value[
              latestDateIdx - 1
            ]["date"];
            if (time_functions.dateCompare(comparisonDate, latestDate) < 0) {
              numErrors += 1;
              this.errors.push(
                `ERROR: the employer or provider sign date is before the latest service delivery date.`
              );
            }
          }
        }
        return numErrors;
      },

      //Formats the data to be posted
      formatData() {
        var submitData = {};
        Object.entries(this.formFields).forEach(([key, value]) => {
          submitData[key] = {};
          submitData[key]["value"] = value["value"];
          submitData[key]["wasEdited"] = !value["disabled"];
        });

        submitData["serviceDeliveredOn"]["value"] = [];
        Object.entries(this.formFields["serviceDeliveredOn"]["value"]).forEach(
          ([key, value]) => {
            key;
            var row = {};

            var cols = ["date", "startTime", "endTime", "totalHours", "group"];
            cols.forEach((col) => {
              row[col] = value[col];
            });
            row["wasEdited"] = !value["disabled"];

            submitData["serviceDeliveredOn"]["value"].push(row);
          }
        );

        return submitData;
      },

      // Send signal to parent component to validate
      signalParentValidate() {
        // Set flag to wait on parent
        this.waitingOnParent = true;
        this.isValid = true;

        // Send signal to parent component to validate input fields
        this.$emit("click");
      },

      //Submits form to AppServer.
      submit() {
        // Validate the form
        var numErrors = this.validate();

        // Display the dialog for submitting the form
        this.displaySubmit = true;

        // If there are errors, do not post timesheet
        if (numErrors > 0) {
          this.errors.push(
            `There were ${numErrors} errors, please fix before submitting.`
          );
          this.isValid = false;
          return false;
        }

        // If there was an edited field, re-acquire provider and employer
        // signatures
        if (this.numEdited > 0) {
          console.log(
            `There were ${this.numEdited} edited fields. Provider and employer must resign the timesheet form.`
          );
        }

        this.isValid = true;

        // Else, post timesheet
        this.loading = true;
        var self = this;

        // Finally, prepare the form data and send to the backend
        this.submitData = this.formatData();

        if (numErrors === 0) {
          axios
            .post(this.url, this.submitData, {
              headers: {
                "content-type": "text/plain",
              },
            })
            .then(function (response) {
              if (response["data"]["response"] == "ok") {
                console.log("Finished posting!");
                self.submissionStatus = true;

                //Return to home here?
                self.returnHome = true;
              }
            })
            .catch(function (error) {
              console.log(error);
            });
        }
      },
    },
  };
</script>
