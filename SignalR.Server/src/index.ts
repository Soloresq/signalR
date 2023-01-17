import * as signalR from "@microsoft/signalr";
import "./css/main.css";

const divMessages: HTMLDivElement = document.querySelector("#divMessages");
const tbMessage: HTMLInputElement = document.querySelector("#tbMessage");
const btnSend: HTMLButtonElement = document.querySelector("#btnSend");
const tbUsername: HTMLInputElement = document.querySelector("#tbUsername");

type BroadcastMessageCommand = {
  methodName: string;
  description: string;
  username: string;
  message: string;
}

type GroupMessageCommand = {
  methodName: string;
  description: string;
  username: string;
  message: string;
  group: string;
}

type JoinGroupCommand = {
  methodName: string;
  description: string;
  username: string;
  group: string;
}

type LeaveGroupCommand = {
  methodName: string;
  description: string;
  username: string;
  group: string;
}

function appendChat(username: string, message: string, receiver: string) {
    const m = document.createElement("div");
    m.innerHTML = `<div class="message-author">${username} to ${receiver}</div><div>${message}</div>`;
    divMessages.appendChild(m);
    divMessages.scrollTop = divMessages.scrollHeight;
}

function printChatHelp() {
  const text = 
    `
    Type '{message}' to send a message to everyone.<br />
    Type 'JoinGroup {groupName}' to enter a group.<br />
    Type 'LeaveGroup {groupName}' to remove a group.<br />
    Type 'GroupMessage {groupName} {message}' to send a message to every member of the specified group.<br />
    `;
  
    const m = document.createElement("div");
    m.innerHTML = `<div>${text}</div>`;
    divMessages.appendChild(m);
    divMessages.scrollTop = divMessages.scrollHeight;
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

connection.on("BroadcastMessage", (command: BroadcastMessageCommand) => {
  appendChat(command.username, command.message, 'all');
});

connection.on("GroupMessage", (command: GroupMessageCommand) => {
  appendChat(command.username, command.message, command.group);
});

connection.on("JoinGroup", (command: JoinGroupCommand) => {
  appendChat(command.username, 'User joined', command.group);
});

connection.on("LeaveGroup", (command: LeaveGroupCommand) => {
  appendChat(command.username, 'User left', command.group);
});

connection.start().catch((err) => document.write(err));

printChatHelp();

tbMessage.addEventListener("keyup", (e: KeyboardEvent) => {
  if (e.key === "Enter") {
    send();
  }
});

btnSend.addEventListener("click", send);

function send() {
  if(tbMessage.value.includes('JoinGroup')) {
    let group = tbMessage.value.substring(tbMessage.value.lastIndexOf(' ') + 1);
    
    const command: JoinGroupCommand = {
      methodName: 'JoinGroup',
      username: tbUsername.value,
      description: '',
      group: group
    };
    
    connection.send(command.methodName, command)
    .then(() => (tbMessage.value = ""));
  }
  else if(tbMessage.value.includes('LeaveGroup')) {
    let group = tbMessage.value.substring(tbMessage.value.lastIndexOf(' ') + 1);
    
    const command: LeaveGroupCommand = {
      methodName: 'LeaveGroup',
      username: tbUsername.value,
      description: '',
      group: group
    };
    
    connection.send(command.methodName, command)
    .then(() => (tbMessage.value = ""));
  }
  else if(tbMessage.value.includes('GroupMessage')){
    const firstSpaceIndex = tbMessage.value.indexOf(' ') + 1;
    let group = tbMessage.value.substring(firstSpaceIndex, tbMessage.value.indexOf(' ', firstSpaceIndex));
    
    const command: GroupMessageCommand = {
      message: tbMessage.value,
      methodName: 'BroadcastMessage',
      username: tbUsername.value,
      description: '',
      group: group
    };
    
     connection.send(command.methodName, command)
    .then(() => (tbMessage.value = "")); 
  }
  else {
    const command: BroadcastMessageCommand = {
      message: tbMessage.value,
      methodName: 'BroadcastMessage',
      username: tbUsername.value,
      description: ''
    };
    connection.send(command.methodName, command)
    .then(() => {
      tbMessage.value = "";
    }); 
  }
}
