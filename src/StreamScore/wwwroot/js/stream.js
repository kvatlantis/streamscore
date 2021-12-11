"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/scoreHub").build();

connection.on("ReceiveScore", function (score) {
  document.getElementById("score").textContent = `${(new Date()).toTimeString()} = ${score}`;
});

connection.start().then(function () {
  //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
  return console.error(err.toString());
});

