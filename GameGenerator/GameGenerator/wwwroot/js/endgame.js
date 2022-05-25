"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

////Game start - register connections
connection.start().then(function () {
    var gameName = document.getElementById("gameGroup").innerText;
    
    connection.invoke("EndGame", gameName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

