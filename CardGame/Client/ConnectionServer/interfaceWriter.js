import store from "../store";
import {
    connection,
    newRoom,
    newPlayer,
    newPublic,
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

    newPublic(roomId, userId){
        store.dispatch(newPublic({roomId: roomId, userId: userId}));
    }

    newPlayer(roomId, userId){
        store.dispatch(newPlayer({roomId: roomId, userId: userId}));
    }


}

instance = new InterfaceWriter();

export default instance;