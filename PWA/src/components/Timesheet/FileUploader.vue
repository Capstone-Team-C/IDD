<template>
  <div class="example-drag">
    <div class="upload">
      <!--div v-if="files.length">
      <ul v-if="files.length">
			<v-container>
				<v-row>
					<v-col>
						<ul>
							<li v-for="(file) in files" :key="file.id">
								<span data-testid="name">{{file.name}}</span> -
								<span>{{file.size | formatSize}}</span> -
								<span v-if="file.error">{{file.error}}</span>
								<span v-else-if="file.success">success</span>
								<span v-else-if="file.active">active</span>
								<span v-else></span>
							</li>
					</ul>
				</v-col>
					<div class="continue" v-if="check()">
						<v-col>
							<div class="text-center">
								<v-btn
									:loading="loading"
									:disabled="loading"
									color="blue-grey"
									class="ma-2 white-text"
									@click="loader = 'loading'"
								>
									Complete Form
									<v-icon right dark>mdi-cloud-upload</v-icon>
								</v-btn>
							</div>
						</v-col>
				</div>
				</v-row>
			</v-container>
			</div-->

      <!--ul v-else-->
      <ul v-if="!files.length">
        <td colspan="7">
          <div class="text-center p-5">
            <h4>Drop files anywhere to upload<br />or</h4>
            <label for="file" class="btn btn-lg btn-primary"
              >Select Files</label
            >
          </div>
        </td>
      </ul>

      <div v-show="$refs.upload && $refs.upload.dropActive" class="drop-active">
        <h3>Drop files to upload</h3>
      </div>

      <v-container>
        <v-row>
          <v-col>
            <div class="example-btn">
              <file-upload
                class="btn btn-primary"
                :post-action="url"
                :multiple="true"
                :drop="true"
                :drop-directory="true"
                :maximum="2"
                :size="1024 * 1024 * 10"
                accept="image/*, application/pdf"
                v-model="files"
                ref="upload"
              >
                <i class="fa fa-plus"></i>
                Select files
              </file-upload>

              <button
                type="button"
                class="btn btn-success"
                v-if="!$refs.upload || !$refs.upload.active"
                @click.prevent="$refs.upload.active = true"
              >
                <i class="fa fa-arrow-up" aria-hidden="true"></i>
                Start Upload
              </button>

              <button
                type="button"
                class="btn btn-danger"
                v-else
                @click.prevent="$refs.upload.active = false"
              >
                <i class="fa fa-stop" aria-hidden="true"></i>
                Stop Upload
              </button>
            </div>

            <div v-if="files.length">
              <!--ul v-if="files.length"-->
              <ul class="file-list">
                <li v-for="file in files" :key="file.id">
                  <span data-testid="name">{{ file.name }}</span> -
                  <span>{{ file.size | formatSize }}</span> -
                  <span v-if="file.error">{{ file.error }}</span>
                  <span v-else-if="file.success">success</span>
                  <span v-else-if="file.active">active</span>
                  <span v-else></span>
                </li>
              </ul>
            </div>
          </v-col>

          <div class="continue" v-if="check()">
            <v-col>
              <div class="text-center">
                <v-btn
                  :loading="loading"
                  :disabled="loading"
                  color="blue-grey"
                  class="ma-2 white-text"
                  @click="loader = 'loading'"
                >
                  Complete Form
                  <v-icon right dark>mdi-cloud-upload</v-icon>
                </v-btn>
              </div>
            </v-col>
          </div>
        </v-row>
      </v-container>
    </div>
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
    background: #000;
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
    color: #fff;
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
  import FileUpload from "vue-upload-component";
  import axios from "axios"
	//import IDDForm from "@/components/Timesheet/IDDForm"
	
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
    methods: {
      types() {
        this.uploadStatus = 1;
        return 1;
      },
      check() {
        if (!this.files.length) return false;

        var count = 0;
        var x;
        for (x in this.files) {
          if (this.files[x].success == true) count += 1;
        }
        if (count == this.files.length) {
          return true;
        } else {
          return false;
        }
      },
    },
    data() {
      return {
        files: [],
        uploadStatus: 0,
        url: "https://localhost:5004/ImageUpload",
        urlGet: "https://localhost:5004/Timesheet/Ready",
        loader: null, //Calls our form retrieval and displays loading progress
        loading: false, //Is form retrieval loading
				fieldData: null
        //url: process.env.VUE_APP_SERVER_URL.concat('ImageUpload/DocAsForm'),
      };
    },
    watch: {
      loader() {
        const l = this.loader;
        this[l] = !this[l];
				var self = this

				axios.get(this.urlGet)
					.then( function(response) {
						console.log(response) 
						self.fieldData = response
					})
					.catch(function (error) {
						console.log(error)
					})

					

        setTimeout(() => (this[l] = false), 3000);

        this.loader = null;
      },
    },
  };
</script>
