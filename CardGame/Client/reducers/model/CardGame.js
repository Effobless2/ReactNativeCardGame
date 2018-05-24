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
                console.log("current")
                this.EjectFromPublic(roomId);
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
        if ((currentRoom !== undefined && currentUser !== undefined) || userId === this.currentUser.userId){ 
            currentRoom.AddPlayer(userId);
            if(userId === this.currentUser.userId){
                if (this.roomsAsPublic.includes(roomId)){
                    this.EjectFromPublic(roomId);
                }
                this.roomsAsPlayer.push(roomId);
            }
        }
    }

    RemovePlayer(roomId, userId){
        currentRoom = this.Rooms.get(roomId);
        currentUser = this.Users.get(userId);
        if ((currentRoom !== undefined && currentUser !== undefined) || userId === this.currentUser.userId){ 
            currentRoom.RemovePlayer(userId);
            if(userId === this.currentUser.userId){
                this.EjectFromPlayer(roomId);
            }
        }
    }

    EjectFromPublic(roomId){
        console.log("ejected from " + roomId);
        this.roomsAsPublic.splice(this.roomsAsPublic.indexOf(roomId), 1);
    }

    EjectFromPlayer(roomId){
        this.roomsAsPlayer.splice(this.roomsAsPlayer.indexOf(roomId), 1);
    }

    getRoom(roomId){
        return this.Rooms.get(roomId);
    }

    eject(roomId){
        if (this.roomsAsPlayer.indexOf(roomId) > -1){
            this.roomsAsPlayer.splice(this.roomsAsPlayer.indexOf(roomId), 1);
        }
        else{
            this.roomsAsPublic.splice(this.roomsAsPublic.indexOf(roomId), 1);
        }
    }

    begin(roomId, handCard, deckSize){
        this.getRoom(roomId).begin(handCard, deckSize);
    }

    confirmCard(roomId, cardIndex){
        this.getRoom(roomId).confirmCard(cardIndex);
    }


}