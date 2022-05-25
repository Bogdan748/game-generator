"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Adding connection
connection.start().then(function () {
    var userName = document.getElementById("userName").innerText;
    var gameName = document.getElementById("gameGroup").innerText;
    connection.invoke("AddNewConnection", userName, gameName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});


//Adding cards to view
connection.on("AddCards", function (extractedCards, cardType) {
    
    extractedCards.forEach(el => {

        var elem = createCard(el.id, el.text)
        document.querySelector(`.cards-container-${cardType}`).appendChild(elem);
    });

    document.querySelector('#sendAnswer').disabled = false;
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

document.getElementById("sendAnswer").addEventListener("click", function (event) {

    var cardId = document.querySelector('.answer-option').id;

    var groupName = document.getElementById("gameGroup").innerText;
    var userName = document.getElementById("userName").innerText;

    connection.invoke("SendAnswer", parseInt(cardId), groupName, userName).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById(cardId).remove();
    document.querySelector('.cards-container-black .card').remove()
    document.querySelector('#sendAnswer').disabled = true;
    event.preventDefault();
});

document.querySelector(".cards-container-white").addEventListener("click", function (event) {
    if (event.target.classList.contains("card")) return;

    document.querySelectorAll('.card').forEach(card => card.classList.remove("answer-option"));

    event.target.closest('.card').classList.toggle('answer-option');

});


