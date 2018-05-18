import { createStore, applyMiddleware } from 'redux';
import reducers from '../reducers';
import listener from '../ConnectionServer/interfaceListener';
import {
    ADD_PUBLIC,
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER,
    REMOVE_USER,
    CREATE_ROOM,
    ADD_PLAYER
} from '../actions/types';

const middleware = store => next => action =>{
    switch(action.type){
        case CREATE_ROOM : {
            listener.createRoom();
            return next(action);
        }
        case ADD_PUBLIC: {
            listener.addPublic(action.payload);
            return next(action);
        }
        case ADD_PLAYER: {
            listener.addPlayer(action.payload);
            return next(action);
        }
        default:
            return next(action);
    }
}

const store = () => {
    console.log("Création store");
    return createStore(reducers, applyMiddleware(middleware));};

export default store();