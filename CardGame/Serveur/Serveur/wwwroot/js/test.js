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

        this.on("UserSee", (user, guid) => {
            this.UserSee(user, guid);
        })

        this.on("RoomComplete", (guid) => {
            this.RoomComplete(guid);
        })

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
    }

    NewGroupCreated(guid) {
        const li = document.createElement("li");

        const text = document.createElement("h3");
        text.textContent = "The room number " + guid + " has been created !";
        text.classList.add("col-4");

        const buttonPlay = document.createElement("input");
        buttonPlay.type = "button";
        buttonPlay.classList.add("col-4");
        buttonPlay.classList.add("partyButton");
        buttonPlay.classList.add("play");
        buttonPlay.value = "Play";
        buttonPlay.addEventListener("click", event => { this.AskForJoin(guid) });

        const buttonPublic = document.createElement("input");
        buttonPublic.type = "button";
        buttonPublic.classList.add("col-4");
        buttonPublic.classList.add("partyButton");
        buttonPublic.classList.add("public");
        buttonPublic.value = "See";
        buttonPublic.addEventListener("click", event => { this.AskForSee(guid) });

        li.appendChild(text);
        li.appendChild(buttonPlay);
        li.appendChild(buttonPublic);
        document.getElementById("messagesList").appendChild(li);
    }

    AskForJoin(guid) {
        console.log("Envoi demande")
        this.invoke("JoinGroup", guid);
    }

    JoinGroup(guid) {
        //for (var button of document.getElementsByClassName("partyButton")){
        //    button.disabled = true;
        //}
        const li = document.createElement("li");
        li.textContent = "You have joined the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
    }

    UserJoinedGroup(user, guid) {
        const li = document.createElement("li");
        li.textContent = "The user " + user + " has joined the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
    }

    AskForSee(guid) {
        this.invoke("AskForSee", guid);
    }

    UserSee(user, guid) {
        const li = document.createElement("li");
        li.textContent = "The user " + user + " is looking the room number " + guid;
        document.getElementById("messagesList").appendChild(li);
    }

    RoomComplete(guid) {
        const li = document.createElement("li");
        li.textContent = "The Room " + guid + " is complete. The game will begin.";
        document.getElementById("messagesList").appendChild(li);
    }
}

const connection = new ConnectionServer("/main", { logger: signalR.LogLevel.Information });




document.getElementById("newGroupButton").addEventListener("click", event => {
    connection.CreateNewGroup();
    event.preventDefault();
});

connection.start().catch(err => console.error);