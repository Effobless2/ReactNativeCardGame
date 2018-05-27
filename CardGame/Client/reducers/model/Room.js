import { cardPlayed } from '../../actions';
import { Player } from './Player';
import { Card } from './Card';
export class Room{
    constructor(roomId, playersMax, players, publics){
        this.roomId = roomId;
        this.maxOfPlayers = playersMax;
        this.publics = publics;
        this.nbPublics = this.publics.length;
        this.players = new Map();
        players.forEach(element => {
            this.AddPlayer(new Player(element.userId, element.deckCount, element.handCount, element.playedCard))
        });
        this.nbPlayers = this.players.size;
        this.name = this.roomId.slice(0,5);

    }

    AddPublic(userId){
        this.publics.push(userId)
        this.nbPublics++;
    }

    AddPlayer(player){
        if (this.publics.includes(player.userId)){
            this.RemovePublic(player.userId);
        }
        this.players.set(player.userId, player);
        this.nbPlayers++;
    }

    RemovePublic(userId){
        this.publics.splice(this.publics.indexOf(userId), 1);
        this.nbPublics--;
    }

    RemovePlayer(userId){
        this.players.delete(userId);
        this.nbPlayers--;
    }





    begin(players){
        players.forEach(player => {
            this.players.get(player.userId).setDeck(player.deckCount);
            this.players.get(player.userId).setHand(player.handCount);
            this.players.get(player.userId).playedCard = null
        });
    }

    confirmCard(playerId, cardIndex){
        this.players.get(playerId).PlayCard(cardIndex);
    }

    receiveHand(playerId, hand){
        this.players.get(playerId).hand = [];
        this.players.get(playerId).setHand(0);
        hand.forEach(card =>{
            this.AddHand(playerId, new Card(card.colour, card.value));
        })
    }

    AddHand(playerId, card){
        this.players.get(playerId).AddHand(card);
    }

    playerHasPlayed(playerId){
        this.players.get(playerId).hasPlayed();
    }

    discover(playerId, cardPlayed){
        this.players.get(playerId).discover(new Card(cardPlayed.colour, cardPlayed.value));
    }

    roundWon(playerId){
        console.log("rien")
    }

    loose(userId){
        this.players.get(userId).loose();
    }


}