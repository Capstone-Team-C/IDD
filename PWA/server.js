const express = require('express');
const serveStatic = require("serve-static")
const path = require('path');
let app = express();
app.use(serveStatic(path.join(__dirname, 'dist')));
const port = process.env.PORT || 80;
let listener = app.listen(port, function(){
    console.log('Listening on port ' + listener.address().port); 
});
