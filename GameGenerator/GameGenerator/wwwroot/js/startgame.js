"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

////Game start - register connections
connection.start().then(function () {
    var gameName = document.getElementById("gameGroup").innerText;
    var userName = document.getElementById("userName").innerText;
    connection.invoke("AddNewConnection", userName, gameName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("AddConnectedUser", function (name) {
    var li = document.createElement("li");
    document.getElementById("users-list").appendChild(li);
    li.textContent = `${name} has connected`;
});

