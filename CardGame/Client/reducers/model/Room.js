export class Room{
    constructor(Id, playersMax, players, publics){
        this.roomId = Id;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = publics;
        this.nbPublics = this.publics.length;
        this.nbPlayers = this.players.length;

    }

    AddPublic(user){
        this.publics.set(user.userId, user);
        this.nbPublics++;
    }


}