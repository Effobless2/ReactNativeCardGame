import { CardGame } from './model/CardGame';
import {
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER,
    REMOVE_USER,
    CREATE_ROOM
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
        default:
            return state;
    }
};
