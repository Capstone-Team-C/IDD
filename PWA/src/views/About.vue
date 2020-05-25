<template>
    <v-col>
        <v-card>
            <v-card-title class="headline blue white--text" primary-title>
                Learn more
            </v-card-title>

            <div class="text-center">
                <v-card-text>
                    To learn more about the Multnomah County Intellectual and
                    Developmental Disabilites department, you can visit our website.
                </v-card-text>
                <v-card-actions>
                    <v-btn text
                           color="deep-purple accent-4"
                           href="https://multco.us/dd"
                           target="_blank">
                        Learn More
                    </v-btn>
                </v-card-actions>
            </div>
        </v-card>

        <v-card>
            <v-card-title class="headline blue white--text" primary-title>
                How to Install
            </v-card-title>

            <div class="text-center">
                <v-card-text class="text-center">
                    To install this app onto your mobile device,
                </v-card-text>
                    <ol class="text-left d-inline-block">
                        <li>Open the settings of the browser</li>
                        <li>Click 'Add to Home screen'</li>
                        <li>Navigate to the home screen of your phone</li>
                        <li>Open the 'IDD App'</li>
                    </ol>
            </div>
        </v-card>

        <v-card>
            <v-card-title class="headline blue white--text" primary-title>
                Contributors
            </v-card-title>

            <div class="text-center">
                <v-card-text>
                    Built in Collaboration with Portland State University Capstone Program and Multnomah County.
                </v-card-text>
                <v-card-text>
                    <ul>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/larrychiem/">Larry Chiem</a>
                        </li>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/johnbrusaw/">John Brusaw</a>
                        </li>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/keaton-kraiger/">Keaton Kraiger</a>
                        </li>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/yves-wienecke/">Yves Wienecke</a>
                        </li>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/ohyonghao/">David Post</a>
                        </li>
                        <li class="d-inline-block mx-5">
                            <a href="https://www.linkedin.com/in/brian-lee-pdx/">Brian Lee</a>
                        </li>
                        <li class="d-inline-block">
                            <a href="https://www.linkedin.com/in/curtis-james-lewis/">Curtis Lewis</a>
                        </li>
                    </ul>
                </v-card-text>
            </div>
        </v-card>
    </v-col>
</template>
<script>
  export default {
    name: "about"
  };
</script>
