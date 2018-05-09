import * as SignalR from '@aspnet/signalr'
import { ApplicationUser } from '../Model/ApplicationUser';

class ConnectionServer extends SignalR.HubConnection{
    constructor(url){
        super(url)

        this.usersList = new Map();

        this.on("Connect", (user) => {
            this.Connect(user);
        });

        this.on("Disconnect", (user) => {
            console.log("Déconnexion");
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

        this.start()
    }

    Connect(user){
        console.log(user.userName + " s'est connecté.");
        newUser = new ApplicationUser(user.userId, user.userName);
        this.usersList.set(user.userId, newUser);
        console.log(this.usersList);
    }

    Disconnect(user){
        console.log(user.userName + " s'est déconnecté.");
        this.usersList.delete(user.userId);
    }

    ReceiveNewRoom(room){
        console.log("You have created the room number " + room.roomId );
    }

    CreatingRoom(){
        this.invoke("CreatingRoom");
    }

    NewRoomCreated(room){
        console.log("The room number " + room.roomId + " has been created.");
    }

    JoinPlayers(room){
        console.log("You have Joined the room number " + room.roomId);
    }

    NewPlayer(user, room) {
        console.log("The user " + user.userId + " plays in the room number " + room.roomId);
        console.log(user);
        console.log(room);
    }

    AddingPublic(room) {
        this.invoke("AddingPublic", room.roomId);
    }

    NewPublic(user, room) {
        console.log("The user " + user.userName + " is looking the room number " + room.roomId)
        console.log(user);
        console.log(room);
    }

    RoomComplete(room) {
        console.log("The Room " + room.roomId + " is complete. The game will begin.");
    }

    LeavingGame(room) {
        this.invoke("LeavingGame", room.roomId);
    }

    GameIsLeft(room) {
        console.log("You Quit the room " + room.roomId + ".");
    }

    LeftTheGame(room, user) {
        console.log("The user " + user.userName + "has left the room " + room.roomId +".");
        console.log(room);
        console.log(user);
    }

    YourRoomIsDestroyed(room) {
        console.log("The room "+ room.roomId + " has been destroyed. We eject you of this useless room.");
    }

    RoomDestroyed(room) {
        console.log( "The room " + room.roomId + " has been destroyed.");
    }

    JoinPublic(room) {
        console.log("You are looking the room number " + room.roomId)
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

export default connection = new ConnectionServer("http://192.168.1.62:5000/cardgame/")