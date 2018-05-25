import * as SignalR from '@aspnet/signalr';
import { connect } from 'react-redux';
import writer from './interfaceWriter';
import {
    ALREADY_IN_ROOM,
    CARD_CONFIRMED,
    CARD_PLAYED,
    CONNECT,
    CONNECTION_BEGIN,
    CREATE_ROOM,
    DISCONNECT,
    EJECTED_FROM_ROOM,
    NEW_PLAYER,
    NEW_PUBLIC,
    NEW_ROOM,
    NOT_IN_THIS_ROOM,
    PARTY_BEGIN,
    PLAYER_REMOVED,
    PUBLIC_REMOVED,
    RECEIVE_HAND,
    READY,
    REMOVING_PLAYER,
    REMOVING_PUBLIC,
    ROOM_IS_FULFILL,
    ROOM_IS_UNDEFINED,
    ROOM_REMOVED,
    ROUND_WON,
    ADDING_PUBLIC,
    ADDING_PLAYER,
    PLAYER_HAS_PLAYED,
    DISCOVER,
} from './ConnectionConstants';

class ConnectionServer extends SignalR.HubConnection{
    
    constructor(url){
        super(url);

        this.on(ALREADY_IN_ROOM, (roomId) => {
            console.log(roomId);
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
        
        this.on(EJECTED_FROM_ROOM, (roomId) => {
            writer.eject(roomId);
        });
        
        this.on(NEW_PLAYER, (roomId, userId) => {
            writer.newPlayer(roomId, userId);
        });
        
        this.on(NEW_PUBLIC, (roomId, userId) => {
            writer.newPublic(roomId, userId);
        });
        
        this.on(NEW_ROOM, (room) => {
            writer.newRoom(room);
        });

        this.on(NOT_IN_THIS_ROOM, (roomId) => {
            console.log(roomId);
        });
        
        this.on(PLAYER_REMOVED, (roomId, userId) => {
            writer.removePlayer(roomId, userId);
        });
        
        this.on(PUBLIC_REMOVED, (roomId, userId) => {
            writer.removePublic(roomId, userId);
        });
        
        this.on(READY, (roomId) => {
            console.log(roomId);
        });
        
        this.on(ROOM_IS_FULFILL, (roomId) => {
            console.log(roomId);
        });


        this.on(ROOM_IS_UNDEFINED, (roomId) => {
            console.log(roomId);
        });

        this.on(ROOM_REMOVED, (roomId) => {
            writer.removeRoom(roomId);
        });

        this.on(PARTY_BEGIN, (roomId, players) =>{
            writer.Begin(roomId, players);
        })

        this.on(CARD_CONFIRMED, (roomId, playerId, cardIndex) =>{
            writer.ConfirmCard(roomId, playerId, cardIndex);
        });

        this.on(RECEIVE_HAND, (roomId, playerId, hand) => {
            writer.receiveHand(roomId, playerId, hand);
        });

        this.on(PLAYER_HAS_PLAYED, (roomId, playerId) => {
            writer.playerHasPlayed(roomId, playerId);
        });

        this.on(DISCOVER, (roomId, playerId, cardPlayed) => {
            writer.discover(roomId, playerId, cardPlayed);
        })

        this.on(ROUND_WON, (roomId, playerId) =>{
            writer.roundWon(roomId, playerId);
        })



        this.start()
    }

    
    createRoom(){
        this.invoke(CREATE_ROOM);
    }

    addPublic(roomId) {
        this.invoke(ADDING_PUBLIC, roomId);
    }

    addPlayer(roomId){
        this.invoke(ADDING_PLAYER, roomId);
    }

    escapePublic(roomId) {
        this.invoke(REMOVING_PUBLIC, roomId);
    }

    escapePlayer(roomId){
        this.invoke(REMOVING_PLAYER, roomId);
    }

    cardPlayed(roomId, cardIndex){
        this.invoke(CARD_PLAYED, roomId, cardIndex);
    }
}

HubConnection = new ConnectionServer("http://192.168.1.62:5000/cardgame/");
export default HubConnection;