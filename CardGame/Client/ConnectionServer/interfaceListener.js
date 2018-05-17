import connection from './Connection';

class InterfaceListener{
    createRoom(){
        connection.createRoom();
    }

    addPublic(roomId){
        connection.addPublic(roomId);
    }
}

instance = new InterfaceListener();

export default instance;