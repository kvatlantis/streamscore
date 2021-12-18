"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("/scoreHub")
  .configureLogging(signalR.LogLevel.Debug)
  .build();


function tryUpdate(id, value) {
  var element = document.getElementById(id);
  if (!element)
    return;

  element.textContent = value;
}

connection.on("ReceiveScore", function (score) {
  document.getElementById("score").textContent = `${score}`;
});


connection.on("ScreenUpdate", function (info) {

  tryUpdate("team1", info.team1);
  tryUpdate("score", `${info.score1}-${info.score2}`);
  tryUpdate("team2", info.team2);
  tryUpdate("tijd", info.time);
  tryUpdate("w1", info.team1Name);
  tryUpdate("wissel", info.status);
  tryUpdate("w2", info.team1Name);
  tryUpdate("helft", info.helft);

});



connection.start().then(function () {
  //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
  return console.error(err.toString());
});



function setupClick(id, name, arg1, arg2, arg3, arg4) {
  var element = document.getElementById(id);
  if (!element)
    return;

  element.addEventListener("click", function (event) {
    connection.invoke(name).catch(function (err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });
}

function setupClick4(id, name) {
  var element = document.getElementById(id);
  if (!element)
    return;

  element.addEventListener("click", function (event) {
    var data = {
      team1: document.getElementById("t1").value,
      team1Name: document.getElementById("t1n").value,
      team2: document.getElementById("t2").value,
      team2Name: document.getElementById("t2n").value,
      status: document.getElementById("s").value,
      helft: document.getElementById("h").value,
    };

    connection.invoke(name, data).catch(function (err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });
}

function setupClickKlok(id, name, eln) {
  var element = document.getElementById(id);
  if (!element)
    return;

  element.addEventListener("click", function (event) {
    var data = document.getElementById(eln).value;
    connection.invoke(name, data).catch(function (err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });
}

setupClick("team1Plus", "Team1Plus");
setupClick("team1Min", "Team1Min");
setupClick("team2Plus", "Team2Plus");
setupClick("team2Min", "Team2Min");
setupClick("stateRun", "StateRun");
setupClick("stateStop", "StateStop");
setupClick("reset", "Reset");
setupClick4("setData", "SetData");
setupClickKlok("klok", "Klok", "klokmin");

