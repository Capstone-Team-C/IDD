<template>
  <div>
    <!-- Page Title -->
    <v-row class="mt-9">
      <v-col align="center" justify="center">
        <p class="headline">
          eXPRS Plan of Care - Services Delivered Report Form
        </p>
      </v-col>
    </v-row>

    <!-- Render either file upload or form -->
    <v-row>
      <v-col v-if="fileStatus === FILE_INIT || fileStatus === FILE_FAILURE">
        <FileUploader
          @error="handleError($event)"
          @success="fillForm($event)"
        />
        <v-card v-if="fileStatus === FILE_FAILURE" class="ma-5">
          <v-card-title class="error white--text"
            >FILE UPLOAD ERROR!</v-card-title
          >
          <v-card-text>
            {{ errors }}
          </v-card-text>
        </v-card>
      </v-col>

      <v-col v-else-if="fileStatus === FILE_SUCCESS">
        <IDDForm :parsedFileData="parsedFileData" />
      </v-col>
    </v-row>
  </div>
</template>

<script>
  import FileUploader from "@/components/Timesheet/FileUploader";
  import IDDForm from "@/components/Timesheet/IDDForm";

  export default {
    name: "Timesheet",
    components: {
      FileUploader,
      IDDForm,
    },
    data: function () {
      const FILE_INIT = 1;
      const FILE_SUCCESS = 2;
      const FILE_FAILURE = 3;

      return {
        // The uploaded timesheet, as a .json of parsed values from the backend
        parsedFileData: null,

        // Possible statuses of the uploading the form
        FILE_INIT: FILE_INIT,
        FILE_SUCCESS: FILE_SUCCESS,
        FILE_FAILURE: FILE_FAILURE,
        fileStatus: FILE_INIT,

        // Upload errors
        errors: [],
      };
    },
    methods: {
      // Successfully received parsed .json from the backend
      fillForm(response) {
        // Save the parsed .json
        this.parsedFileData = response;

        // Hide the image upload and display the pre-populated IDD form
        this.fileStatus = this.FILE_SUCCESS;
      },
      handleError(error) {
        this.errors = error;
        this.fileStatus = this.FILE_FAILURE;
      },
    },
  };
</script>
