class ConnectionServer extends signalR.HubConnection {
    constructor(url, log) {
        super(url, log);

        this.on("ConnectionBegin", (currentUser, users, rooms) => {
            this.ConnectionBegin(currentUser, users, rooms);
        });

        this.on("Connect", (user) => {
            this.Connect(user);
        });

        this.on("Disconnect", (user) => {
            this.Disconnect(user);
        });

        this.on("PlayerRemoved", (roomId, userId) => {
            this.PlayerRemoved(roomId, userId);
        });

        this.on("PublicRemoved", (roomId, userId) => {
            this.PublicRemoved(roomId, userId);
        });

        this.on("RoomCreated", (room) => {
            this.RoomCreated(room);
        });

        this.on("NewPlayer", (user, room) => {
            this.NewPlayer(user, room);
        });

        this.on("NewPublic", (user, room) => {
            this.NewPublic(user, room);
        });

        this.on("Ready", (room) => {
            this.Ready(room);
        });

        this.on("EjectedFromRoom", (room) => {
            this.EjectedFromRoom(room);
        });

        this.on("RoomRemoved", (room) => {
            this.RoomRemoved(room);
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

    }

    ConnectionBegin(currentUser, users, rooms) {
        this.currentUser = currentUser;
        this.users = new Map();
        users.map((user) => {
            if (user.userId !== currentUser.userId){
                this.users.set(user.userId, user);
            }
        });
        this.rooms = new Map();
        rooms.map((room) => {
           this.rooms.set(room.roomId, room);
        });
        console.log(this.currentUser);
        console.log(this.rooms);
        console.log(this.users);
        console.log("connected");
    }

    Connect(user) {
        this.users.set(user.userId, user);
        console.log(user.userName + " is connected");
    }

    Disconnect(userId) {
        this.users.delete(userId);
        console.log(userId + " is disconnected");
    }

    PlayerRemoved(roomId, userId) {
        console.log(userId + " a quitté les joueurs de " + roomId);
    }

    PublicRemoved(roomId, userId) {
        console.log(userId + " a quitté le public de " + roomId);
    }

    CreatingRoom() {
        this.invoke("CreatingRoom");
    }

    RoomCreated(room) {
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

    NewPlayer(room, user) {
        console.log(user + " se joint aux joueurs de " + room);
        if (user == this.currentUser.userId) {
            const li = document.createElement("li");

            const text = document.createElement("h3");
            text.textContent = "Vous jouez désormais dans la room " + room + ".";

            const buttonPlay = document.createElement("input");
            buttonPlay.type = "button";
            buttonPlay.classList.add("col-4");
            buttonPlay.classList.add("partyButton");
            buttonPlay.classList.add("play");
            buttonPlay.value = " Quittez la en cliquant ici.";
            buttonPlay.addEventListener("click", event => { this.QuitParty(room); });

            text.classList.add("col-4");
            li.appendChild(text);
            li.appendChild(buttonPlay);
            document.getElementById("messagesList").appendChild(li);
        }
    }

    AddingPublic(room) {
        this.invoke("AddingPublic", room.roomId);
    }

    NewPublic(room, user) {
        console.log(user + " s'est joint au public de " + room);
        if (user == this.currentUser.userId) {
            const li = document.createElement("li");

            const text = document.createElement("h3");
            text.textContent = "Vous assistez à la partie de la room " + room + ".";

            const buttonPlay = document.createElement("input");
            buttonPlay.type = "button";
            buttonPlay.classList.add("col-4");
            buttonPlay.classList.add("partyButton");
            buttonPlay.classList.add("play");
            buttonPlay.value = " Quittez la en cliquant ici.";
            buttonPlay.addEventListener("click", event => { this.QuitPublic(room); });

            text.classList.add("col-4");
            li.appendChild(text);
            li.appendChild(buttonPlay);
            document.getElementById("messagesList").appendChild(li);
        }
    }

    Ready(room) {
        console.log("La partie " + room + " a commencé");
    }

    EjectedFromRoom(room) {
        console.log("Vous avez ejecté de " + room);
        const li = document.createElement("li");
        console.log(room);

        const text = document.createElement("h3");
        text.textContent = "Vous avez été ejectée de la room " + room;
        text.classList.add("col-4");

        li.appendChild(text);

    }

    RoomRemoved(room) {
        console.log("La room " + room + " a été supprimée");
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

    QuitPublic(room) {
        this.invoke("RemovingPublic", room);
    }

    QuitParty(room) {
        this.invoke("RemovingPlayer", room);
    }

    

    
}

const connection = new ConnectionServer("/cardgame", { logger: signalR.LogLevel.Information });




document.getElementById("newRoomButton").addEventListener("click", event => {
    connection.CreatingRoom();
    event.preventDefault();
});

connection.start().catch(err => console.error);