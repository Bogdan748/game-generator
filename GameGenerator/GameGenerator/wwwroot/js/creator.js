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



///Start Round 
//1. get one black card
//2. get 5 white for each
//3. Send response
//4. choose the winner

//1. get ona black card
//2. get 1 for each
//3. Send response
//4. choose the winner


document.getElementById("startRound").addEventListener("click", function (event) {
    var gameName = document.getElementById("gameGroup").innerText;
    var gameRound = document.getElementById("gameRound").innerText;

    connection.invoke("StartRound", gameName, parseInt(gameRound)).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});



connection.on("AddCards", function (extractedCards, cardType) {
    
    extractedCards.forEach(el => {

        var elem = createCard(el.id, el.text)
        document.querySelector(`.cards-container-${cardType}`).appendChild(elem);
    });
        
    
});

connection.on("AddAnswer", function (sentdCard, usernamme) {
        var label;
    
        label = document.createElement("div");
        label.className = 'answer-label';
        var elem = createCard(sentdCard.id, sentdCard.text)

        label.innerText = `${usernamme} answered:`
        document.querySelector(`.cards-container-white`).appendChild(label)
        document.querySelector(`.cards-container-white`).appendChild(elem);

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

    return element;

}


