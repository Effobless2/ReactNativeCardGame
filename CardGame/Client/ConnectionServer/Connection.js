import * as SignalR from '@aspnet/signalr'

class ConnectionServer extends SignalR.HubConnection{
    constructor(url){
        super(url)

        this.on("Connect", (user) => {
            this.Connect(user);
        });

        this.on("Disconnected", (user) => {
            this.Disconnect(user);
        });

        this.on("ReceiveMessage", (message) => {
            this.ReceiveMessage(message);
        });

        this.start()
    }

    Connect(user){
        console.log(user + " s'est connecté.");
    }

    Disconnect(user){
        console.log(user + " s'est déconnecté.");
    }

    NewGroup(){
        this.invoke("NewGroup");
    }

    ReceiveMessage(message){
        console.log(message);
    }

    SendMessageForGroup(message){
        this.invoke("sendMessageToTheGroup", message);
    }

    SendMessageForEveryone(message) {
        this.invoke("SendMessage", message);
    }
}

export default connection = new ConnectionServer("http://192.168.1.62:5000/main/")