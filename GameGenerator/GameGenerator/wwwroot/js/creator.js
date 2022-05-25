"use strict";
document.querySelector('#sendWinner').disabled = true;
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

//Start Round
document.getElementById("startRound").addEventListener("click", function (event) {
    var gameName = document.getElementById("gameGroup").innerText;
    var gameRound = document.getElementById("gameRound").innerText;

    connection.invoke("StartRound", gameName, parseInt(gameRound)).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    document.querySelector('#sendWinner').disabled = false;
    document.querySelector('#startRound').disabled = true;
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


//Send Winner
document.getElementById("sendWinner").addEventListener("click", function (event) {

    var cardId = document.querySelector('.answer-option').id;
    var groupName = document.getElementById("gameGroup").innerText;
    

    connection.invoke("SendWinner", parseInt(cardId), groupName).catch(function (err) {
        return console.error(err.toString());
    });

    document.querySelectorAll('.card').forEach(el => el.remove());
    document.querySelectorAll('.answer-label').forEach(el => el.remove());


    document.querySelector('#startRound').disabled = false;
    document.querySelector('#sendWinner').disabled = true;
    event.preventDefault();
});

document.querySelector(".cards-container-white").addEventListener("click", function (event) {
    if (event.target.classList.contains("card")) return;

    document.querySelectorAll('.card').forEach(card => card.classList.remove("answer-option"));

    event.target.closest('.card').classList.toggle('answer-option');

});

//////Update point
connection.on("UpdatePoint", function (UserName, Points) {

    var row = document.getElementById(UserName);

    row.childNodes[3].innerText = Points;

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


