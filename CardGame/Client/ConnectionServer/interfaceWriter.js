import store from "../store";
import {
    begin,
    confirmCard,
    connection,
    ejected,
    newRoom,
    newPlayer,
    newPublic,
    removeRoom,
    newUser,
    removeUser,
    removePublic,
    removePlayer,
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

    removePublic(roomId, userId){
        store.dispatch(removePublic({roomId: roomId, userId: userId}));
    }

    removePlayer(roomId, userId){
        store.dispatch(removePlayer({roomId: roomId, userId: userId}));
    }

    eject(roomId){
        store.dispatch(ejected(roomId))
    }

    Begin(roomId, handCards){
        store.dispatch(begin({roomId: roomId, cards:handCards}));
    };

    ConfirmCard(roomId, cardIndex){
        store.dispatch(confirmCard({roomId: roomId, cardIndex: cardIndex}));
    }


}

instance = new InterfaceWriter();

export default instance;