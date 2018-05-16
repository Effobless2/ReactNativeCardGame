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
}

instance = new InterfaceWriter();

export default instance;