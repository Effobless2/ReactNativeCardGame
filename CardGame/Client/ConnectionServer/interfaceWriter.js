import store from "../store";
import {
    connection,
    newRoom,
    removeRoom,
    newUser,
    removeUser,
} from '../actions';

class InterfaceWriter{
    connectionBegin(currentUser, users, rooms){
        console.log("interface");
        store.dispatch(connection({user: currentUser, users: users, rooms: rooms}));
    }

    removeUser(user){
        store.dispatch(removeUser({user: user}));
    }
}

instance = new InterfaceWriter();

export default instance;