import { CardGame } from './model/CardGame';
import {
    CONFIRM_CARD,
    CONNECTION,
    CREATE_ROOM,
    NEW_PLAYER,
    NEW_PUBLIC,
    NEW_ROOM,
    NEW_USER,
    PLAYER_HAS_PLAYED,
    REMOVE_PLAYER,
    REMOVE_PUBLIC,
    REMOVE_ROOM,
    REMOVE_USER,
    EJECTED,
    SELECT_ROOM,
    BEGIN,
    RECEIVE_HAND,
} from '../actions/types';
import IMAGESPATH from '../icons';

const INITIAL_STATE = {
    cardGame : null,
    connected : false,
    cpt: 0,
    selectedRoom : null
};

export default (state = INITIAL_STATE, action) => {
    switch(action.type){
        case CONNECTION : {
            let {user, users, rooms} = action.payload;
            return {
                ...state,
                connected: true,
                cardGame: new CardGame(user, users, rooms),
            };
        }
        case NEW_ROOM: {
            state.cardGame.AddRoom(action.payload)

            return state;
        }
        case REMOVE_ROOM: {
            state.cardGame.RemoveRoom(action.payload);
            return {...state, cpt:state.cpt+1};
        }
        case NEW_USER: {
            state.cardGame.AddUser(action.payload);
            return {...state, cpt:state.cpt+1};
        }
        case REMOVE_USER: {
            state.cardGame.RemoveUser(action.payload);
            return {...state, cpt: state.cpt+1};
        }
        case NEW_PUBLIC: {
            let {roomId, userId} = action.payload;
            state.cardGame.NewPublic(roomId, userId);
            return {...state, cpt:state.cpt+1};
        }
        case NEW_PLAYER: {
            let {roomId, userId} = action.payload;
            console.log("room = " + roomId);
            console.log("user = " + userId);
            state.cardGame.NewPlayer(roomId, userId);
            return {...state, cpt:state.cpt+1};
        }
        case REMOVE_PUBLIC: {
            let {roomId, userId} = action.payload;
            state.cardGame.RemovePublic(roomId, userId);
            return {...state, cpt:state.cpt+1};
        }
        case REMOVE_PLAYER: {
            let {roomId, userId} = action.payload;
            state.cardGame.RemovePlayer(roomId, userId);
            return {...state, cpt:state.cpt+1};
        }
        case EJECTED: {
            console.log("ejected reducer");
            if (state.selectedRoom == action.payload){
                state.selectedRoom = null;
            }
            state.cardGame.eject(action.payload);
            return state
        }
        case SELECT_ROOM: {
            return {...state,
                    selectedRoom : action.payload}
        }
        case BEGIN: {
            state.cardGame.begin(action.payload.roomId, action.payload.players);
            return {...state,
                    cpt:state.cpt+1};
        }
        case CONFIRM_CARD: {
            state.cardGame.confirmCard(action.payload.roomId, action.payload.playerId, action.payload.cardIndex);
            return {...state,
                cpt:state.cpt+1};
        }
        case RECEIVE_HAND: {
            state.cardGame.receiveHand(action.payload.roomId, action.payload.playerId, action.payload.hand);
            return {...state,
                cpt:state.cpt+1};
        }
        case PLAYER_HAS_PLAYED: {
            state.cardGame.playerHasPlayed(action.payload.roomId, action.payload.playerId);
            return {...state,
                cpt:state.cpt+1};
        }
        default:
            return {...state, cpt:state.cpt+1};
    }
};
