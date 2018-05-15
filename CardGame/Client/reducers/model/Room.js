export class Room{
    constructor(Id, playersMax, players, publics){
        this.roomId = Id;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = publics;
        this.nbPublics = this.getNumberPublics();

    }

    getNumberPublics(){
        let count = 0
        for(i in this.publics){
            count ++;
        };
        return count;
    }


}