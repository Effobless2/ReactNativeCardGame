import { CardGame } from './model/CardGame';
import {
    CONNECTION,
    CREATE_ROOM,
    NEW_PLAYER,
    NEW_PUBLIC,
    NEW_ROOM,
    NEW_USER,
    REMOVE_PLAYER,
    REMOVE_PUBLIC,
    REMOVE_ROOM,
    REMOVE_USER,
} from '../actions/types';

const INITIAL_STATE = {
    cardGame : null,
    connected : false,
    cpt: 0
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

            return {...state, cpt:state.cpt+1};
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
            return {...state, cpt:state.cpt+1};
        }
        case CREATE_ROOM: {
            return {...state, cpt:state.cpt+1};
        }
        case NEW_PUBLIC: {
            let {roomId, userId} = action.payload;
            state.cardGame.NewPublic(roomId, userId);
            return {...state, cpt:state.cpt+1};
        }
        case NEW_PLAYER: {
            let {roomId, userId} = action.payload;
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
        default:
            return state;
    }
};
