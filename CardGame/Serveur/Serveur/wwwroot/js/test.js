class ConnectionServer extends signalR.HubConnection {
    constructor(url, log) {
        super(url, log);

        this.on("Connect", (user) => {
            this.Connect(user);
        });

        this.on("Disconnected", (user) => {
            this.Disconnect(user);
        });

        this.on("ReceiveNewGroup", (guid) => {
            this.ReceiveNewGroup(guid);
        });

        this.on("NewGroupCreated", (guid) => {
            this.NewGroupCreated(guid);
        });

        this.on("UserJoinedGroup", (user, guid) => {
            this.UserJoinedGroup(user, guid);
        });

        this.on("JoinGroup", (guid) => {
            this.JoinGroup(guid);
        });

    }

    Connect(user) {
        const li = document.createElement("li");
        li.textContent = user + " is connected.";
        document.getElementById("messagesList").appendChild(li);
    }

    Disconnect(user) {
        const li = document.createElement("li");
        li.textContent = user + " is disconnected."
        document.getElementById("messagesList").appendChild(li);
    }

    CreateNewGroup() {
        this.invoke("NewGroup");
    }

    ReceiveNewGroup(guid) {
        const li = document.createElement("li");
        li.textContent = "You have created the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
        this.JoinGroup(guid);
    }

    NewGroupCreated(guid) {
        const li = document.createElement("li");
        const button = document.createElement("input");
        button.type = "button";
        button.classList.add("partyButton");
        button.value = "The room number " + guid + " has been created ! </br>" +
            " Click here to join it !";
        button.addEventListener("click", event => { this.AskForJoin(guid) });
        li.appendChild(button);
        document.getElementById("messagesList").appendChild(li);
    }

    AskForJoin(guid) {
        this.invoke("JoinGroup", guid);
    }

    JoinGroup(guid) {
        for (var button of document.getElementsByClassName("partyButton")){
            button.disabled = true;
        }
        const li = document.createElement("li");
        li.textContent = "You have joined the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
    }

    UserJoinedGroup(user, guid) {
        const li = document.createElement("li");
        li.textContent = "The user " + user + " has joined the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
    }
}

const connection = new ConnectionServer("/main", { logger: signalR.LogLevel.Information });




document.getElementById("newGroupButton").addEventListener("click", event => {
    connection.CreateNewGroup();
    event.preventDefault();
});

connection.start().catch(err => console.error);