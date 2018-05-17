import * as SignalR from '@aspnet/signalr';
import { connect } from 'react-redux';
import writer from './interfaceWriter';
import {
    ALREADY_IN_ROOM,
    CONNECT,
    CONNECTION_BEGIN,
    DISCONNECT,
    EJECTED_FROM_ROOM,
    NEW_PLAYER,
    NEW_PUBLIC,
    NOT_IN_THIS_ROOM,
    PLAYER_REMOVED,
    PUBLIC_REMOVED,
    READY,
    ROOM_CREATED,
    ROOM_IS_FULFILL,
    ROOM_IS_UNDEFINED,
    ROOM_REMOVED,
} from './ConnectionConstants';

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

        this.on(ALREADY_IN_ROOM, (room) => {
            this.AlreadyInRoom(room);
        });
        
        this.on(CONNECT, (user) => {
            writer.newUser(user);
        });

        this.on(CONNECTION_BEGIN, (currentUser, users, rooms) => {
            writer.connectionBegin(currentUser, users, rooms);
        });
        
        this.on(DISCONNECT, (userId) => {
            writer.removeUser(userId);
        });
        
        this.on(EJECTED_FROM_ROOM, (room, user) => {
            this.LeftTheGame(room, user);
        });
        
        this.on(NEW_PLAYER, (user, room) => {
            this.NewPlayer(user, room);
        });
        
        this.on(NEW_PUBLIC, (user, room) => {
            this.NewPublic(user, room);
        });
        
        this.on(NOT_IN_THIS_ROOM, (room) => {
            this.NotInThisRoom(room);
        });
        
        this.on(PLAYER_REMOVED, (roomId, userId) => {
            console.log(userId);
        });
        
        this.on(PUBLIC_REMOVED, (roomid, userId) => {
            console.log(userId);
        });
        
        this.on(READY, (room) => {
            this.RoomComplete(room);
        });

        this.on(ROOM_CREATED, (room) => {
            writer.roomCreated(room);
        });
        
        this.on(ROOM_IS_FULFILL, (room) => {
            this.RoomFulfill(room);
        });


        this.on(ROOM_IS_UNDEFINED, (roomId) => {
            this.RoomIsUndefined(roomId);
        });

        this.on(ROOM_REMOVED, (room) => {
            writer.removeRoom(room);
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