import store from "../store";
import {
    begin,
    confirmCard,
    connection,
    ejected,
    newRoom,
    newPlayer,
    newPublic,
    playerHasPlayed,
    removeRoom,
    newUser,
    receiveHand,
    removeUser,
    removePublic,
    removePlayer,
    discover,
    roundWon,
    loose,
} from '../actions';

class InterfaceWriter{
    connectionBegin(currentUser, users){
        store.dispatch(connection({user: currentUser, users: users}));
    }

    newUser(user){
        store.dispatch(newUser( user ));
    }

    removeUser(userId){
        store.dispatch(removeUser( userId ));
    }

    newRoom(room, players){
        store.dispatch(newRoom( {room: room, players: players} ));
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

    Begin(roomId, players){
        store.dispatch(begin({roomId: roomId, players: players}));
    };

    ConfirmCard(roomId, playerId, cardIndex){
        store.dispatch(confirmCard({roomId: roomId, playerId: playerId, cardIndex: cardIndex}));
    }

    receiveHand(roomId, playerId, hand){
        store.dispatch(receiveHand({roomId: roomId, playerId: playerId, hand: hand}));
    }

    playerHasPlayed(roomId, playerId){
        store.dispatch(playerHasPlayed({roomId: roomId, playerId: playerId}));
    }

    discover(roomId, playerId, cardPlayed){
        store.dispatch(discover({roomId: roomId, playerId: playerId, cardPlayed: cardPlayed}));
    }

    roundWon(roomId, playerId){
        store.dispatch(roundWon({roomId:roomId, playerId:playerId}));
    }

    loose(roomId, userId){
        store.dispatch(loose({roomId: roomId, userId: userId}));
    }


}

instance = new InterfaceWriter();

export default instance;