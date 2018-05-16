import { CardGame } from './model/CardGame';
import {
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER,
    REMOVE_USER,
    CREATE_ROOM,
    ROOM_CREATED
} from '../actions/types';

const INITIAL_STATE = {
    cardGame : null,
    connected : false
};

export default (state = INITIAL_STATE, action) => {
    switch(action.type){
        case CONNECTION : {
            let {user, users, rooms} = action.payload;
            console.log("Connection Reducers");
            return {
                ...state,
                connected: true,
                cardGame: new CardGame(user, users, rooms)
            };
        }
        case NEW_ROOM: {
            console.log("newRoom reducer");
            state.cardGame.AddRoom(action.payload)

            return state;
        }
        case REMOVE_ROOM: {
            console.log("Remove Room reducer");
            state.cardGame.RemoveRoom(action.payload);
            return state;
        }
        case NEW_USER: {
            console.log("newUser reducer");
            state.cardGame.AddUser(action.payload);
            return state;
        }
        case REMOVE_USER: {
            console.log("removeUser reducer");
            state.cardGame.RemoveUser(action.payload);
            return state;
        }
        case CREATE_ROOM: {
            console.log("createRoom reducer");
            return state;
        }

        case ROOM_CREATED: {
            console.log("roomCreated reducer");
            state.cardGame.AddRoom(action.payload);
            return state;
        }
        default:
            return state;
    }
};
