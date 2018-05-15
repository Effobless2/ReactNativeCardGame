import { CardGame } from './model/CardGame';

const INITIAL_STATE = {
    cardGame : new CardGame(),
    connected : false
};

export default (state = INITIAL_STATE, action) => {
    switch(action.type){
        default:
        return state;
    }
};
