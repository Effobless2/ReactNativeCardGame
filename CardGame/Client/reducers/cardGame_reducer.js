import { CardGame } from './model/CardGame';
import { CONNECTION, NEW_ROOM } from '../actions/types';

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
        default:
            return state;
    }
};
