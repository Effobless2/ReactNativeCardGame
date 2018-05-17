import { createStore, applyMiddleware } from 'redux';
import reducers from '../reducers';
import listener from '../ConnectionServer/interfaceListener';
import {
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER,
    REMOVE_USER,
    CREATE_ROOM
} from '../actions/types';

const middleware = store => next => action =>{
    switch(action.type){
        case CONNECTION :{
            console.log("connection middleware");
            return next(action);
        }
        case NEW_ROOM : {
            console.log("newRoom middleware");
            return next(action);
        }
        case REMOVE_ROOM : {
            console.log("removeRoom middleware");
            return next(action);
        }
        case NEW_USER : {
            console.log("newUser middleware");
            return next(action);
        }
        case REMOVE_USER : {
            console.log("removeUser middleware");
            return next(action);
        }
        case CREATE_ROOM : {
            console.log("createRoom middleware");
            listener.createRoom();
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