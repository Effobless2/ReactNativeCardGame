export class Room{
    constructor(Id, playersMax, players, publics){
        this.roomId = Id;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = new Map();
        for (var index in publics){
            this.AddPublic(publics[index])
        }
        this.nbPublics = this.publics.size;

    }

    AddPublic(user){
        this.publics.set(user.userId, user);
    }


}