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

    removeRoom(roomId){
        store.dispatch(removeRoom( roomId ));
    }


}

instance = new InterfaceWriter();

export default instance;