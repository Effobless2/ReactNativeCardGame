export class Room{
    constructor(Id, playersMax, players, publics){
        this.roomId = Id;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = publics;
        this.nbPublics = this.publics.length;
        this.nbPlayers = this.players.length;

    }

    AddPublic(userId){
        this.publics.push(userId)
        this.nbPublics++;
    }


}