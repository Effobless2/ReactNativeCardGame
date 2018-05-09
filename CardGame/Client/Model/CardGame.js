import { ApplicationUser } from "./ApplicationUser";
import { Room } from "./Room";

export class CardGame{
    constructor(){
        this.Users = new Map();
        this.Rooms = new Map();
    }

    AddRoom(room){
        currentRoom = new Room(room.roomId, room.maxOfPlayers, room.players, room.public);
        this.Rooms.set(room.roomId, currentRoom);
        console.log(this.Rooms);
    }

    AddUser(user){
        currentUser = new ApplicationUser(user.userId, user.userName);
        this.Users.set(user.userId, currentUser);
        console.log(this.Users);
    }

    RemoveUser(user){
        this.Users.delete(user.userId);
        console.log(this.Users);
    }

    RemoveRoom(room){
        if (this.Rooms.get(room.roomId) !== undefined){
            this.Rooms.delete(room.roomId);
        }
        console.log(this.Rooms);
    }
}