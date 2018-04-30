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

        this.on("Action", (message) => {
            this.Action(message);
        });

        this.start()
    }

    Connect(user,message){
        console.log(user + " se connecte et dit " + message);
    }

    Disconnect(user, message){
        console.log(user + " se déconnecte et dit " + message);
    }

    Action(message){
        console.log(" agit et dit " + message);
    }

    SendAction(){
        console.log("SendingAction")
        this.invoke("Send", "Coucou!");
    }
}

export default connection = new ConnectionServer("http://01f152d3.ngrok.io/main/")