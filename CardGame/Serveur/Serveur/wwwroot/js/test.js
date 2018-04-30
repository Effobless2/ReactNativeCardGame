const connection = new signalR.HubConnection(
    "/main", { logger: signalR.LogLevel.Information });

connection.on("ReceiveMessage", (message) => {
    const encodedMsg = message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("Connect", (user) => {
    const encodedMsg = user + " s'est connecté.";
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("Disconnected", (user) => {
    const encodedMsg = user + " s'est déconnecté.";
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(err => console.error);
    event.preventDefault();
});

document.getElementById("groupBtn").addEventListener("click", event => {
    connection.invoke("NewGroup").catch(err => console.err);
    event.preventDefault();
});

document.getElementById("sendGroup").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    connection.invoke("sendMessageToTheGroup", message).catch(err => console.error);
    event.preventDefault();
});

connection.start().catch(err => console.error);