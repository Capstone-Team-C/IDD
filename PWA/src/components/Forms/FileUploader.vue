<template>
  <div class="example-drag">
    <template v-if="onlineStatus">
      <!-- Pop-up at the bottom of the page -->
      <v-snackbar
        v-model="snackbar"
        :timeout=9000
      >
        <a 
          class="white--text"
          target="_blank"
          title="Link to How to Print/Download eXPRS Timesheet"
          :href="xPRSLink"
        >
          {{ $t("components_Forms_FileUploader_xPRS") }}
        </a>
        <v-btn color="red" dark text @click="snackbar = false">
          {{ $t("close") }}
        </v-btn>
      </v-snackbar>
      <!-- END Pop-up at the bottom of the page -->

      <!-- Uploader Section -->
      <div class="upload">
        <!-- File Drop Area -->
        <v-container v-if="files.length < 2" class="pa-0 mx-0" fluid>
          <v-row class="pa-0 ma-0">
            <v-col class="text-center pa-0 ma-0">
              <v-alert
                class="text-center title px-3 py-5 ma-0"
                width="80vw"
                color="info"
                outlined
                dense
                text
              >
                <div
                  v-html="$t('components_Forms_FileUploader_dropfiles')"
                ></div>
                <label for="file" class="btn btn-primary">
                  {{ $t("components_Forms_FileUploader_selectfiles") }}
                </label>
              </v-alert>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              v-show="$refs.upload && $refs.upload.dropActive"
              class="drop-active"
            >
              <h3>
                {{ $t("components_Forms_FileUploader_drop") }}
              </h3>
            </v-col>
          </v-row>
        </v-container>
        <!-- END File Drop Area -->
        
        <v-container>
          <v-row>
            <v-col>
              <div v-if="!submitted">
                <!-- File Upload Buttons -->
                <div class="example-btn">
                  <file-upload
                    class="btn btn-primary"
                    :custom-action="uploadToServer"
                    :multiple="true"
                    :drop="true"
                    :drop-directory="true"
                    :maximum="2"
                    :size="1024 * 1024 * 10"
                    accept="image/*, application/pdf, image/heic"
                    @input-file="inputFile"
                    v-model="files"
                    ref="upload"
                  >
                    <i class="fa fa-plus"></i>
                    {{ $t("components_Forms_FileUploader_select") }}
                  </file-upload>

                  <button
                    type="button"
                    class="btn btn-success"
                    v-if="!$refs.upload || !$refs.upload.active"
                    @click.prevent="$refs.upload.active = true"
                  >
                    <i class="fa fa-arrow-up" aria-hidden="true"></i>
                    {{ $t("components_Forms_FileUploader_startupload") }}
                  </button>

                  <button
                    type="button"
                    class="btn btn-danger"
                    v-else
                    @click.prevent="$refs.upload.active = false"
                  >
                    <i class="fa fa-stop" aria-hidden="true"></i>
                    {{ $t("components_Forms_FileUploader_stopupload") }}
                  </button>
                  
                  <button
                    type="button"
                    class="mx-5 btn btn-danger"
                    ref="files"
                    @click="reset"
                  >
                    {{ $t("components_Forms_FileUploader_resetfiles") }}
                  </button>
                </div>
              </div>
              <!-- END File Upload Buttons -->


              <!-- List of Files and their details -->
              <div v-if="files.length">
                <ul class="file-list">
                  <li v-for="file in files" :key="file.id">
                    <span data-testid="name">{{ file.name }}</span> -
                    <!--span>{{ file.size | formatSize }}</span-->
                    <span v-if="file.error">{{ file.error }}</span>
                    <span v-else-if="file.success">{{
                      $t("components_Forms_FileUploader_success")
                    }}</span>
                    <span v-else-if="file.active">

                      <!-- Files Processing Dialog Box -->
                      <v-dialog
                        value="true"
                        hide-overlay
                        persistent
                        width="300"
                      >
                        <v-card color="primary" dark>
                          <v-card-text>
                            {{ $t("components_Forms_FileUploader_processing") }}
                            <v-progress-linear
                              indeterminate
                              color="white"
                              class="mb-0"
                            ></v-progress-linear>
                          </v-card-text>
                        </v-card>
                      </v-dialog>
                      <!-- END Files Processing Dialog Box -->
                    
                    </span>
                  </li>
                </ul>
              </div>
            <!-- END List of Files and their details -->
            </v-col>
          </v-row>
            
          <!-- Files successfully uploaded, but not yet parsed -->
          <!-- This button kicks off parsing the uploaded files -->
          <div class="continue" v-if="check()">
            <v-row>
              <v-col class="text-center">
                  <v-btn
                    :loading="loading"
                    :disabled="loading"
                    color="success"
                    class="ma-0 white--text"
                    @click="completeForm()"
                  >
                    {{ $t("components_Forms_FileUploader_continue") }}
                    <v-icon right dark>mdi-cloud-upload</v-icon>
                  </v-btn>

                  <button
                    type="button"
                    class="mx-5 btn btn-danger"
                    ref="files"
                    @click="reset"
                  >
                    {{ $t("components_Forms_FileUploader_resetfiles") }}
                  </button>
              </v-col>
            </v-row>
          </div>
        </v-container>
      </div>
      <!-- END Uploader Section -->

    </template>
    <template v-else>
      {{ $t("components_Forms_FileUploader_offline") }}
    </template>
  </div>
</template>

<style lang="scss" scoped>
  .example-drag.drop-space {
    padding-inline-start: 0;
    background: #000;
  }
  .file-list {
    margin-top: 1em;
  }
  .btn-success {
    margin-left: 1rem;
  }
  .example-drag.upload {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-flow: column;
    padding-top: 20em;
  }
  .example-drag {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-flow: column;
    padding-top: 1em;
  }
  .example-drag.btn {
    margin-bottom: 0;
    margin-right: 1rem;
  }
  .example-drag.drop-active {
    top: 5em;
    bottom: 0;
    right: 0;
    left: 0;
    position: fixed;
    z-index: 9999;
    opacity: 0.6;
    text-align: center;
    color: red;
  }
  .example-drag.drop-active h3 {
    margin: -0.5em 0 0;
    position: absolute;
    top: 10em;
    left: 5em;
    right: 0;
    -webkit-transform: translateY(-50%);
    -ms-transform: translateY(-50%);
    transform: translateY(-50%);
    font-size: 40px;
    color: red;
    padding: 0;
  }
  .custom-loader {
    animation: loader 1s infinite;
    display: flex;
  }
  @-moz-keyframes loader {
    from {
      transform: rotate(0);
    }
    to {
      transform: rotate(360deg);
    }
  }
  @-webkit-keyframes loader {
    from {
      transform: rotate(0);
    }
    to {
      transform: rotate(360deg);
    }
  }
  @-o-keyframes loader {
    from {
      transform: rotate(0);
    }
    to {
      transform: rotate(360deg);
    }
  }
  @keyframes loader {
    from {
      transform: rotate(0);
    }
    to {
      transform: rotate(360deg);
    }
  }
</style>

<script>
  import axios from "axios";
  import FileUpload from "vue-upload-component";
  import { FORM } from "@/components/Utility/Enums.js";
  import { mapFields } from "vuex-map-fields";
  
  export default {
    name: "file_uploader",
    components: {
      FileUpload,
    },
    props: {
      uploadFiles: {
        type: Array,
        defaut: () => [],
        validator: (value) =>
          Array.isArray(value) &&
          value.every((file) => file.id && file.name && file.type),
      },
    },
    data() {
      return {
        // More info about printing the IDD timesheet
        xPRSLink: 'https://apps.state.or.us/exprsDocs/HowToPrintPSWTimesheetsFromeXPRS.pdf',
        // Controls whether the 'more info' box is displayed or hidden
        snackbar: true,
        // User uploaded files 
        files: [],
        // Have the files been uploaded yet?
        submitted: false,
        // Did the user already hit the upload files button?
        getUpdated: false,
        //Calls our form retrieval and displays loading progress
        loader: null, 
        // Are the uploaded files being parsed right now?
        loading: false,
        // URL, where files are uploaded to the Appserver
        urlPost: process.env.VUE_APP_SERVER_URL.concat("ImageUpload/DocAsForm"),
        // URL, where uploaded files are parsed and a .json is returned
        urlGet: process.env.VUE_APP_SERVER_URL.concat(
          "Timesheet/Ready?id="
        ),
      };
    },

    computed: {
      ...mapFields(["formChoice", "formId", "guid", "onlineStatus"]),
    },

    methods: {
      // Upload the files to the server (no parsing yet!) 
      uploadToServer() {
                let formData = new FormData();
        for (let i = 0; i < this.files.length; i++) {
          let file = this.files[i].file;
          formData.append("files[" + i + "]", file);
        }
        formData.append("formChoice", FORM[this.formChoice]);
        let self = this;
        axios
          .post(this.urlPost, formData, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then(function (response) {
            console.log("Response from Appserver, FileUploader", response);
            if (response["data"]["response"] == "too blurry") {
              for (let i = 0; i < self.files.length; i++) {
                self.files[i].active = false;
                self.files[i].success = false;
                self.files[i].error = true;
              }
              self.$emit("success", response["data"]);
            } else {
              for (let i = 0; i < self.files.length; i++) {
                self.files[i].active = false;
                self.files[i].success = true;
              }
              self.files.active = false;
              self.files.success = true;
              self.formId = response["data"]["id"];
              self.guid = response["data"]["guid"];
              self.submitted = true;
            }
          })
          .catch(function (error) {
            console.log("FileUploader", error);
            for (let i = 0; i < self.files.length; i++) {
              self.files[i].active = false;
              self.files[i].success = false;
              self.files[i].error = true;
            }
            self.$emit("error", error);
          });
        return;
      },

      // Parse the uploaded files and get the response as .json
      completeForm() {
        this.loading = true;
        const l = this.loader;
        this[l] = !this[l];
        let self = this;

        // Retrieves a .json response from timesheet
        if (!this.getUpdated) {
          this.urlGet = this.urlGet
            .concat(this.formId)
            .concat("&guid=")
            .concat(this.guid);
          this.getUpdated = true;
          axios
            .get(this.urlGet)
            .then(function (response) {
              self.$emit("success", response["data"]);
            })
            .catch(function (error) {
              console.log("FileUploader", error);
              for (let i = 0; i < self.files.length; i++) {
                self.files[i].active = false;
                self.files[i].success = false;
                self.files[i].error = true;
              }
              self.$emit("error", error);
            });
        }
        setTimeout(() => (this[l] = false), 30000);
        this.loader = null;
      },
      
      //Checks if all of the files are ready to be submitted.
      check() {
        if (!this.files.length) return false;
        let count = 0;
        let x;
        for (x in this.files) {
          if (this.files[x].success == true) count += 1;
        }
        if (count == this.files.length) {
          return true;
        } else {
          return false;
        }
      },

      reset() {
        this.files = [];
        this.submitted = false;
        this.loader = null;
        this.loading = false;
        this.getUpdated = false;
        this.$emit("reset");
      },

      inputFile: function (newFile, oldFile) {
        let jsonResponse;
        if (newFile.xhr) {
          if (!newFile.active) {
            jsonResponse = JSON.parse(newFile.xhr.response);
            this.formId = jsonResponse["id"];
            this.guid = jsonResponse["guid"];
          }
        }
        if (newFile && oldFile && !newFile.active && oldFile.active) {
          if (newFile.xhr) {
            if (!newFile.active) {
              jsonResponse = JSON.parse(newFile.xhr.response);
              this.formId = jsonResponse["id"];
              this.guid = jsonResponse["guid"];
            }
          }
        }
      },
    },
   };
</script>
