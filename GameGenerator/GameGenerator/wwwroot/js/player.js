"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    var userName = document.getElementById("userName").innerText;
    var gameName = document.getElementById("gameGroup").innerText;
    connection.invoke("AddNewConnection", userName, gameName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});


