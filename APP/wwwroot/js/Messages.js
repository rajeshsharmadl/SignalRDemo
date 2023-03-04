"use strict"

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7243/messages")
    .build();

connection.on('WELCOME', (m) => console.log(m));

connection.on("NEW_MSG", (message) => {
    var el = document.createElement('li');
    el.textContent = message;
    var list = document.getElementById('lstmessages');
    list.appendChild(el);
});

connection.start({ withCredentials: false }).catch(() => console.log("error while connecting"));

document.getElementById("btnsendmsg").addEventListener('click', function () {
    var msg = document.getElementById("txtmsg").value;
    connection.invoke("SendMessageToAll", msg);
});