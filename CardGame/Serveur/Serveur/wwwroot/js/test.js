class ConnectionServer extends signalR.HubConnection {
    constructor(url, log) {
        super(url, log);

        this.on("Connect", (user) => {
            this.Connect(user);
        });

        this.on("Disconnect", (user) => {
            this.Disconnect(user);
        });

        this.on("ReceiveNewRoom", (room) => {
            this.ReceiveNewRoom(room);
        });

        this.on("NewRoomCreated", (room) => {
            this.NewRoomCreated(room);
        });

        this.on("NewPlayer", (user, room) => {
            this.NewPlayer(user, room);
        });

        this.on("JoinPlayers", (room) => {
            this.JoinPlayers(room);
        });

        this.on("NewPublic", (user, room) => {
            this.NewPublic(user, room);
        });

        this.on("RoomComplete", (room) => {
            this.RoomComplete(room);
        });

        this.on("GameIsLeft", (room) => {
            this.GameIsLeft(room);
        });

        this.on("LeftTheGame", (room, user) => {
            this.LeftTheGame(room, user);
        });

        this.on("YourRoomIsDestroyed", (room) => {
            this.YourRoomIsDestroyed(room);
        });

        this.on("RoomDestroyed", (room) => {
            this.RoomDestroyed(room);
        });

        this.on("JoinPublic", (room) => {
            this.JoinPublic(room);
        });

        this.on("AlreadyInRoom", (room) => {
            this.AlreadyInRoom(room);
        });

        this.on("RoomFulfill", (room) => {
            this.RoomFulfill(room);
        });

        this.on("NotInThisRoom", (room) => {
            this.NotInThisRoom(room);
        });

        this.on("RoomIsUndefined", (roomId) => {
            this.RoomIsUndefined(roomId);
        });

        this.on("UserIsUndefined", (userId) => {
            this.UserIsUndefined(userId);
        });

    }

    Connect(user) {
        console.log(user);
        const li = document.createElement("li");
        li.textContent = user.userName + " is connected.";
        document.getElementById("messagesList").appendChild(li);
    }

    Disconnect(user) {
        console.log(user);
        const li = document.createElement("li");
        li.textContent = user.userName + " is disconnected.";
        document.getElementById("messagesList").appendChild(li);
    }

    CreatingRoom() {
        this.invoke("CreatingRoom");
    }

    ReceiveNewRoom(room) {
        console.log(room);
        const li = document.createElement("li");
        li.textContent = "You have created the room number " + room.roomId;
        document.getElementById("messagesList").appendChild(li);
    }

    NewRoomCreated(room) {
        const li = document.createElement("li");
        console.log(room);

        const text = document.createElement("h3");
        text.textContent = "The room number " + room.roomId + " has been created !";
        text.classList.add("col-4");

        const buttonPlay = document.createElement("input");
        buttonPlay.type = "button";
        buttonPlay.classList.add("col-4");
        buttonPlay.classList.add("partyButton");
        buttonPlay.classList.add("play");
        buttonPlay.value = "Play";
        buttonPlay.addEventListener("click", event => { this.AddingPlayer(room); });

        const buttonPublic = document.createElement("input");
        buttonPublic.type = "button";
        buttonPublic.classList.add("col-4");
        buttonPublic.classList.add("partyButton");
        buttonPublic.classList.add("public");
        buttonPublic.value = "See";
        buttonPublic.addEventListener("click", event => { this.AddingPublic(room); });

        li.appendChild(text);
        li.appendChild(buttonPlay);
        li.appendChild(buttonPublic);
        document.getElementById("messagesList").appendChild(li);
    }

    AddingPlayer(room) {
        console.log("Envoi demande");
        this.invoke("AddingPlayer", room.roomId);
    }

    JoinPlayers(room) {
        //for (var button of document.getElementsByClassName("partyButton")){
        //    button.disabled = true;
        //}
        console.log(room)
        const li = document.createElement("li");
        const text = document.createElement("p");
        text.textContent = "You have joined the room number " + room.roomId;
        const button = document.createElement("input");
        button.type = "button";
        button.value = "Click Here to quit the Game.";
        button.addEventListener("click", event => { this.LeavingGame(room) });
        document.getElementById("messagesList").appendChild(li);

        li.appendChild(text);
        li.appendChild(button);
    }

    NewPlayer(user, room) {
        const li = document.createElement("li");
        li.textContent = "The user " + user.userName + " has joined the room number " + room.roomId;
        document.getElementById("messagesList").appendChild(li);
    }

    AddingPublic(room) {
        this.invoke("AddingPublic", room.roomId);
    }

    NewPublic(user, room) {
        const li = document.createElement("li");
        li.textContent = "The user " + user.userName + " is looking the room number " + room.roomId;
        document.getElementById("messagesList").appendChild(li);
    }

    RoomComplete(room) {
        const li = document.createElement("li");
        li.textContent = "The Room " + room.roomId + " is complete. The game will begin.";
        document.getElementById("messagesList").appendChild(li);
    }

    LeavingGame(room) {
        this.invoke("LeavingGame", room.roomId);
    }

    GameIsLeft(room) {
        const li = document.createElement("li");
        li.textContent = "You Quit the room " + room.roomId + ".";
        document.getElementById("messagesList").appendChild(li);
    }

    LeftTheGame(room, user) {
        const li = document.createElement("li");
        li.textContent = "The user " + user.userName + "has left the room " + room.roomId +".";
        document.getElementById("messagesList").appendChild(li);
    }

    YourRoomIsDestroyed(room) {
        const li = document.createElement("li");
        li.textContent = "The room "+ room.roomId + " has been destroyed. We eject you of this useless room.";
        document.getElementById("messagesList").appendChild(li);
    }

    RoomDestroyed(room) {
        const li = document.createElement("li");
        li.textContent = "The room " + room.roomId + " has been destroyed.";
        document.getElementById("messagesList").appendChild(li);
    }

    JoinPublic(room) {
        console.log(room)
        const li = document.createElement("li");
        const text = document.createElement("p");
        text.textContent = "You are looking the room number " + room.roomId;
        const button = document.createElement("input");
        button.type = "button";
        button.value = "Click Here to quit the Game.";
        button.addEventListener("click", event => { this.LeavingGame(room) });
        document.getElementById("messagesList").appendChild(li);

        li.appendChild(text);
        li.appendChild(button);
    }

    AlreadyInRoom(room) {
        console.log("Already in room " + room.roomId);
    }

    RoomFulfill(room) {
        console.log("Room number " + room.roomId + " is fulfill.");
    }

    NotInThisRoom(room) {
        console.log("You are not in the room number " + room.roomId + ".");
    }

    UserIsUndefined(userId) {
        console.log("The user number " + userId + " is undefined.");
    }

    RoomIsUndefined(roomId) {
        console.log("The room number " + roomId + " is undefined.");
    }
}

const connection = new ConnectionServer("/cardgame", { logger: signalR.LogLevel.Information });




document.getElementById("newRoomButton").addEventListener("click", event => {
    connection.CreatingRoom();
    event.preventDefault();
});

connection.start().catch(err => console.error);