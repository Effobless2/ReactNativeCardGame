import { Bataille } from './Bataille'
import { cardPlayed } from '../../actions';

export class Room{
    constructor(roomId, playersMax, players, publics){
        this.roomId = roomId;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = publics;
        this.nbPublics = this.publics.length;
        this.nbPlayers = this.players.length;
        this.party = null;
        this.name = this.roomId.slice(0,5);

    }

    AddPublic(userId){
        this.publics.push(userId)
        this.nbPublics++;
    }

    AddPlayer(userId){
        console.log(this.publics);
        if (this.publics.includes(userId)){
            this.RemovePublic(userId);
        }
        this.players.push(userId)
        this.nbPlayers++;
    }

    RemovePublic(userId){
        this.publics.splice(this.publics.indexOf(userId), 1);
        this.nbPublics--;
    }

    RemovePlayer(userId){
        this.players.splice(this.players.indexOf(userId), 1);
        this.nbPlayers--;
    }

    begin(players){
        this.party = new Bataille(players);
    }

    AddHand(color, value){
        this.party.AddHand(colour, value);
    }

    confirmCard(playerId, cardIndex){
        this.party.PlayCard(playerId, cardIndex);
    }

    receiveHand(playerId, hand){
        this.party.receiveHand(playerId, hand);
    }

    playerHasPlayed(playerId){
        this.party.playerHasPlayed(playerId);
    }

    discover(playerId, cardPlayed){
        this.party.discover(playerId, cardPlayed);
    }

    roundWon(playerId){
        this.party.roundWon(playerId);
    }


}