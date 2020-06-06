# Vue.js Progressive Web App

Our Progressive Web App (PWA) is built with [Vue.js](https://vuejs.org/) off of [Node.js](https://nodejs.org/en/). 

The packages our PWA uses are managed with NPM.js(https://www.npmjs.com/).

Our PWA uses [Vue CLI](https://cli.vuejs.org/guide/) built on top of wepback for rapid development.

## Project setup
### Windows
#### Install Node.js and NPM
1) Download [Node.js Installer](https://nodejs.org/en/download/)
2) Once the installer finishes, open the downloaded file and run the Node.js Setup Wizard.
3) The installer will prompt your for the installation location and to select components to include or remove from the installation.
4) In **Custom Setup** select **npm package manager** as the recommended package manager for Node.js.
5) Finally, click the Install button to run the installer. When it finishes, click Finish.

#### Verify Installation
Open a command prompt (or PowerShell) and enter the following:
```
node -v
npm -v
```
The system should display the Node.js and NPM version installed.
***
### Linux
#### Install Node.js and NPM
1) Open your terminal.
2) To install Node.js use the following command
```
sudo apt install nodejs
```
3) To install NPM use following command
```
sudo apt install npm
```
4) Once installed, verify it by checking the installed versions:
```
node -v
npm -v
```
The system should display the Node.js and NPM version installed.
***
### Mac
#### Install Node.js and NPM
1) Download [Node.js](https://nodejs.org/en/download/) for macOS.
2) When the file finishes downloading, locate it in **Finder** and double-click on it.
3) Complete the installation process. 
4) Verify instillation by opening a terminal and type
```
node -v
npm -v
```
#### Install Node.js and NPM with Homebrew
If you have Homebrew installed on your device, open a terminal and type:
```
brew update
brew install node
```
***

### Setting up environment variables
This app makes use of [Vue environment variables](https://cli.vuejs.org/guide/mode-and-env.html), an effective method of configuring Node.js applications. You can specify env variables by placing the following files the PWA root directory: 

```sh
.env                # loaded in all cases
.env.local          # loaded in all cases, ignored by git
.env.[mode]         # only loaded in specified mode
.env.[mode].local   # only loaded in specified mode, ignored by git
```
> :warning: **Do not store any secrets (such as private API keys) in your app**: Environment variables are embedded into the build, meaning anyone can view them by inspecting your app's files.



Please note that all env variables in this Vue app should start with `VUE_APP`, a requirement when running `vue-cli-service`.

You can access env variables in your application code:

```js
console.log(process.env.VUE_APP_NOT_SECRET_CODE)
```

We have included an `.env.example` file that example env variables used in the code.  For local development, we recommend creating a `.env` file that includes at least the same variables in the `.env.example` file. You will need to specify these env variables before building the app.

When deploying the app for production, we recommend specifying these env variables in the app settings of the service you are hosting the app on.

See [Configuration Reference](https://cli.vuejs.org/config/).

### Running our Vue.js project locally
1) Move to the PWA root directory in the project directory.
2) Install necessary packages with npm:

```
npm install
```
3) Create a `.env` file with at least the same variables in the `.env.example` file and configure the variables to your needs.

4) Build Vue project:

```
npm run build
```
5) Serve the Vue project:

```
npm run serve
```
56 Open the project in your browser with the designated **localhost** url (ex http://localhost:8080) 

***
### Lints and fixes files
```
npm run lint
```

### Other commands
- npm test
- npm list
- prettify /directory_to_prettify --write


### Deployment
- Build the project via `npm run build`
- Copy the web.manifest file into the /dist folder, as this is where the website will be served
- In VS Code, download the Azure extension
- Press Ctrl+Shift+P
- Type "deploy" and select the first option
- Follow through until it deploys
- Check the Deployments (in the navbar to the left) to ensure that the project built correctly
- Check the output logs if there was an error