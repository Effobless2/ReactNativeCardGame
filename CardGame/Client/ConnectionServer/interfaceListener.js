import connection from './Connection';

class InterfaceListener{
    createRoom(){
        connection.CreatingRoom();
    }
}

instance = new InterfaceListener();

export default instance;