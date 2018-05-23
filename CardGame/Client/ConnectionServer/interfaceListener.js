import connection from './Connection';

class InterfaceListener{
    createRoom(){
        connection.createRoom();
    }

    addPublic(roomId){
        connection.addPublic(roomId);
    }

    addPlayer(roomId){
        connection.addPlayer(roomId);
    }

    escapePublic(roomId){
        connection.escapePublic(roomId);
    }

    escapePlayer(roomId){
        connection.escapePlayer(roomId);
    }

    cardPlayed(roomId, cardIndex){
        connection.cardPlayed(roomId, cardIndex);
    }
}

instance = new InterfaceListener();

export default instance;