import { ApplicationUser } from "./ApplicationUser";
import { Room } from "./Room";
import { Player } from './Player';

export class CardGame{

    constructor(currentUser = null, users = null){
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
        }
    }

    AddRoom(room, players){
        currentRoom = new Room(room.roomId, room.maxOfPlayers, players, room.publicMembers);
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
        this.Rooms.delete(roomId);
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
            currentRoom.AddPlayer(new Player(currentUser, 0, 0, null));
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
        console.log("ejected from " + roomId);
        this.roomsAsPlayer.splice(this.roomsAsPlayer.indexOf(roomId), 1);
    }

    getRoom(roomId){
        return this.Rooms.get(roomId);
    }

    eject(roomId){
        if (this.roomsAsPlayer.indexOf(roomId) > -1){
            this.EjectFromPlayer(roomId);
        }
        else{
            this.EjectFromPublic(roomId);
        }
    }


    

    begin(roomId, players){
        this.getRoom(roomId).begin(players);
    }

    confirmCard(roomId, playerId, cardIndex){
        this.getRoom(roomId).confirmCard(playerId, cardIndex);
    }

    receiveHand(roomId, playerId, hand){
        this.getRoom(roomId).receiveHand(playerId, hand);
    }

    playerHasPlayed(roomId, playerId){
        this.getRoom(roomId).playerHasPlayed(playerId);
    }

    discover(roomId, playerId, cardPlayed){
        this.getRoom(roomId).discover(playerId, cardPlayed);
    }

    roundWon(roomId, playerId){
        this.getRoom(roomId).roundWon(playerId);
    }

    loose(roomId, userId){
        this.getRoom(roomId).loose(userId);
    }
}