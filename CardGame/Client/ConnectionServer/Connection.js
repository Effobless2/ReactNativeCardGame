import * as SignalR from '@aspnet/signalr';
import { connect } from 'react-redux';
import writer from './interfaceWriter';

import {
    connection,
    newRoom,
    removeRoom,
    newUser,
    removeUser,
} from '../actions';
import { CardGame } from '../reducers/model/CardGame';
//import store from '../store';

class ConnectionServer extends SignalR.HubConnection{

    constructor(url){
        super(url);
        //this.store = store;

        this.on("Connect", (user) => {
            console.log(user.userName + " s'est connecté.");
            writer.newUser(user);
        });

        this.on("ConnectionBegin", (currentUser, users, rooms) => {
            userList = Object.values(users);
            roomList = Object.values(rooms);
            console.log("Vous êtes désormais connecté sous l'id "+ currentUser.userName);
            writer.connectionBegin(currentUser, userList, roomList);
        });

        this.on("Disconnect", (user) => {
            console.log(user.userName + " s'est déconnecté.");
            writer.removeUser(user);
        });

        this.on("ReceiveNewRoom", (room) => {
            writer.roomCreated(room);
        });

        this.on("NewRoomCreated", (room) => {
            writer.newRoom(room);
            console.log("The room number " + room.roomId + " has been created.");
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
            writer.removeRoom(room);
            console.log( "The room " + room.roomId + " has been destroyed.");
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

    
    CreatingRoom(){
        this.invoke("CreatingRoom");
    }

    AddingPublic(room) {
        this.invoke("AddingPublic", room.roomId);
    }

    LeavingGame(room) {
        this.invoke("LeavingGame", room.roomId);
    }
    
   
    JoinPlayers(room){
        console.log("You have Joined the room number " + room.roomId);
    }

    NewPlayer(user, room) {
        console.log("The user " + user.userId + " plays in the room number " + room.roomId);
    }

    NewPublic(user, room) {
        console.log("The user " + user.userName + " is looking the room number " + room.roomId)
    }

    RoomComplete(room) {
        console.log("The Room " + room.roomId + " is complete. The game will begin.");
    }

    GameIsLeft(room) {
        console.log("You Quit the room " + room.roomId + ".");
    }

    LeftTheGame(room, user) {
        console.log("The user " + user.userName + "has left the room " + room.roomId +".");
    }

    YourRoomIsDestroyed(room) {
        console.log("The room "+ room.roomId + " has been destroyed. We eject you of this useless room.");
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

HubConnection = new ConnectionServer("http://192.168.1.62:5000/cardgame/");
export default HubConnection;