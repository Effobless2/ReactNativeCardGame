import connection from './Connection';

class InterfaceListener{
    createRoom(){
        connection.createRoom();
    }
}

instance = new InterfaceListener();

export default instance;