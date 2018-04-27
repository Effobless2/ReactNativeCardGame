import * as SignalR from '@aspnet/signalr'

class ConnectionServer extends SignalR.HubConnection{
    constructor(url){
        super(url)

        this.on("Connect", (user, message) => {
            this.Connect(user, message);
        });

        this.on("Disconnected", (user, message) => {
            this.Disconnect(user, message);
        });

        this.on("Action", (user, message) => {
            this.Action(user, message);
        });

        this.start()
    }

    Connect(user,message){
        Console.log(user + " se connecte et dit " + message);
    }

    Disconnect(user, message){
        Console.log(user + " se déconnecte et dit " + message);
    }

    Action(user, message){
        Console.log(user + " agit et dit " + message);
    }
}

export default connection = new ConnectionServer("http://c2d41253.ngrok.io")