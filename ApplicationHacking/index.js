"use strict";

const express = require("express");
const fs = require("fs");
const bodyParser = require("body-parser");

const app = express();
const port = 5007;
app.listen(port);
console.log("Server on port " + port);

app.use(function(req, res, next) {
    res.header("Cache-Control", "no-cache, no-store, must-revalidate");
    next();
});

app.use(bodyParser.json({
    limit: '500mb',
}));

app.get("/winton/app", (request, response) => {
    const path = __dirname + "/" + "mainPage.html";
    return response.sendFile(path);
});

app.get("/winton/scripts/:name", (request, response) => {
    const name = request.params.name;
    const path = __dirname + "/scripts/" + name + ".js";
    return response.sendFile(path);
});

app.get("/winton/style", (request, response) => {
    const path = __dirname + "/css/style.css";
    return response.sendFile(path);
});

app.get("/winton/images/:name", (request, response) => {
    const name = request.params.name;
    const path = __dirname + "/images/" + name;
    return response.sendFile(path);
});

app.get("/winton/fonts/:name", (request, response) => {
    const name = request.params.name;
    const path = __dirname + "/fonts/" + name;
    return response.sendFile(path);
});

