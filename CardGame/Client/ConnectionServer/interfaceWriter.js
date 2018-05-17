import store from "../store";
import {
    connection,
    newRoom,
    removeRoom,
    newUser,
    removeUser,
    roomCreated,
} from '../actions';

class InterfaceWriter{
    connectionBegin(currentUser, users, rooms){
        store.dispatch(connection({user: currentUser, users: users, rooms: rooms}));
    }

    newUser(user){
        store.dispatch(newUser( user ));
    }

    removeUser(userId){
        store.dispatch(removeUser( userId ));
    }

    newRoom(room){
        store.dispatch(newRoom( room ));
    }

    removeRoom(room){
        store.dispatch(removeRoom( room ));
    }

    roomCreated(room){
        store.dispatch(roomCreated( room ));
    }


}

instance = new InterfaceWriter();

export default instance;