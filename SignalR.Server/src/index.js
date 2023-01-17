"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var signalR = require("@microsoft/signalr");
require("./css/main.css");
var divMessages = document.querySelector("#divMessages");
var tbMessage = document.querySelector("#tbMessage");
var btnSend = document.querySelector("#btnSend");
var tbUsername = document.querySelector("#tbUsername");
function appendChat(username, message, receiver) {
    var m = document.createElement("div");
    m.innerHTML = "<div class=\"message-author\">".concat(username, " to ").concat(receiver, "</div><div>").concat(message, "</div>");
    divMessages.appendChild(m);
    divMessages.scrollTop = divMessages.scrollHeight;
}
function printChatHelp() {
    var text = "\n    Type '{message}' to send a message to everyone.<br />\n    Type 'JoinGroup {groupName}' to enter a group.<br />\n    Type 'LeaveGroup {groupName}' to remove a group.<br />\n    Type 'GroupMessage {groupName} {message}' to send a message to every member of the specified group.<br />\n    ";
    var m = document.createElement("div");
    m.innerHTML = "<div>".concat(text, "</div>");
    divMessages.appendChild(m);
    divMessages.scrollTop = divMessages.scrollHeight;
}
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();
connection.on("BroadcastMessage", function (command) {
    appendChat(command.username, command.message, 'all');
});
connection.on("GroupMessage", function (command) {
    appendChat(command.username, command.message, command.group);
});
connection.on("JoinGroup", function (command) {
    appendChat(command.username, 'User joined', command.group);
});
connection.on("LeaveGroup", function (command) {
    appendChat(command.username, 'User left', command.group);
});
connection.start().catch(function (err) { return document.write(err); });
printChatHelp();
tbMessage.addEventListener("keyup", function (e) {
    if (e.key === "Enter") {
        send();
    }
});
btnSend.addEventListener("click", send);
function send() {
    if (tbMessage.value.includes('JoinGroup')) {
        var group = tbMessage.value.substring(tbMessage.value.lastIndexOf(' ') + 1);
        var command = {
            methodName: 'JoinGroup',
            username: tbUsername.value,
            description: '',
            group: group
        };
        connection.send(command.methodName, command)
            .then(function () { return (tbMessage.value = ""); });
    }
    else if (tbMessage.value.includes('LeaveGroup')) {
        var group = tbMessage.value.substring(tbMessage.value.lastIndexOf(' ') + 1);
        var command = {
            methodName: 'LeaveGroup',
            username: tbUsername.value,
            description: '',
            group: group
        };
        connection.send(command.methodName, command)
            .then(function () { return (tbMessage.value = ""); });
    }
    else if (tbMessage.value.includes('GroupMessage')) {
        var firstSpaceIndex = tbMessage.value.indexOf(' ') + 1;
        var group = tbMessage.value.substring(firstSpaceIndex, tbMessage.value.indexOf(' ', firstSpaceIndex));
        var command = {
            message: tbMessage.value,
            methodName: 'BroadcastMessage',
            username: tbUsername.value,
            description: '',
            group: group
        };
        connection.send(command.methodName, command)
            .then(function () { return (tbMessage.value = ""); });
    }
    else {
        var command = {
            message: tbMessage.value,
            methodName: 'BroadcastMessage',
            username: tbUsername.value,
            description: ''
        };
        connection.send(command.methodName, command)
            .then(function () {
            tbMessage.value = "";
        });
    }
}
