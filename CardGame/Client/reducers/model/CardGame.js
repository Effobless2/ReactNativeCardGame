import { ApplicationUser } from "./ApplicationUser";
import { Room } from "./Room";

export class CardGame{

    constructor(currentUser = null, users = null, rooms = null){
        this.Users = new Map();
        this.Rooms = new Map();
        this.currentUser = null;
        this.roomsAsPlayer = [];
        this.roomsAsPublic = [];
        
        if (currentUser != null){
            this.currentUser = new ApplicationUser(currentUser.userId, currentUser.userName);
    
            for(var index in users){
                if (index != this.currentUser.userId){
                    this.AddUser(users[index]);
                }
            }
    
            for(var index in rooms){
                this.AddRoom(rooms[index]);
            }
        }
    }

    AddRoom(room){
        currentRoom = new Room(room.roomId, room.maxOfPlayers, room.players, room.public);
        this.Rooms.set(room.roomId, currentRoom);
    }

    AddUser(user){
        currentUser = new ApplicationUser(user.userId, user.userName);
        this.Users.set(user.userId, currentUser);
    }

    RemoveUser(userId){
        this.Users.delete(userId);
    }

    RemoveRoom(roomId){
        if (this.Rooms.get(roomId) !== undefined){
            this.Rooms.delete(roomId);
        }
    }

    RemovePublic(roomId, userId){
        currentRoom = this.Rooms.get(roomId);
        currentUser = this.Users.get(userId);
        if ((currentRoom !== undefined && currentUser !== undefined) || userId === this.currentUser.userId){ 
            currentRoom.RemovePublic(userId);
            if (userId === this.currentUser.userId){
                this.roomsAsPublic.splice(this.this.roomsAsPublic.indexOf(roomId, 1));
            }
         }
    }

    NewPublic(roomId, userId){
        currentRoom = this.Rooms.get(roomId);
        currentUser = this.Users.get(userId);
        if ((currentRoom !== undefined && currentUser !== undefined) || userId === this.currentUser.userId){
            currentRoom.AddPublic(userId);
            if (userId === this.currentUser.userId){
                this.roomsAsPublic.push(roomId);
            }
        }
    }

    NewPlayer(roomId, userId){
        currentRoom = this.Rooms.get(roomId);
        currentUser = this.Users.get(userId);
        console.log(this.currentRoom);
        if ((currentRoom !== undefined && currentUser !== undefined) || userId === this.currentUser.userId){ 
            currentRoom.AddPlayer(userId);
            console.log(currentRoom)
            if(userId === this.currentUser.userId){
                this.EjectFromPublic(roomId);
                this.roomsAsPlayer.push(userId);
            }
        }
    }

    EjectFromPublic(roomId){
        this.publics.splice(this.roomsAsPublic.indexOf(roomId), 1);
    }


}