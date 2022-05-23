"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

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


document.getElementById("getCards").addEventListener("click", function (event) {
    var gameName = document.getElementById("gameGroup").innerText;

    connection.invoke("GetCardsForAll", gameName, "black", 1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


connection.on("AddCards", function (extractedCards, cardType) {
    console.log(extractedCards);
    console.log(`cards-container-${cardType}`);
    extractedCards.forEach(el => {

        var elem = createCard(el.id, el.text)
        document.querySelector(`.cards-container-${cardType}`).appendChild(elem);
    });
        
    
});


function createCard(cardId, cardText) {
    var element = document.createElement("div");
    element.className = "card";
    element.id = cardId;

    var container = document.createElement("div");
    container.className = "container";

    var text = document.createElement("h4");
    text.innerText = cardText;

    container.appendChild(text);
    element.appendChild(container);


    console.log(element);
    return element;

    
}


