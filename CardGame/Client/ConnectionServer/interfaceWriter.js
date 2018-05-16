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

    newUser(user){
        store.dispatch(newUser( user ));
    }

    removeUser(user){
        store.dispatch(removeUser( user ));
    }

    newRoom(room){
        store.dispatch(newRoom( room ));
    }

    removeRoom(room){
        store.dispatch(removeRoom( room ));
    }


}

instance = new InterfaceWriter();

export default instance;