import { CardGame } from './model/CardGame';
import { CONNECTION } from '../actions/types';

const INITIAL_STATE = {
    cardGame : null,
    connected : false
};

export default (state = INITIAL_STATE, action) => {
    switch(action.type){
        case CONNECTION : {
            let {currentUser, users, rooms} = action.payload;
            console.log("Connection Reducers");
            return {
                ...state,
                connected: true,
                cardGame: new CardGame(currentUser, users, rooms)
            };
        }
        default:
            return state;
    }
};
