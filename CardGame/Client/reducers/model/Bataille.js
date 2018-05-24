import { Player } from './Player';
import { Card } from './Card';

export class Bataille{
    constructor(players){
        this.players = new Map();
        players.forEach(player => {
            this.players.set(player.userId, new Player(player.userId, player.deckCount, player.handCount));
        });
        console.log(this.players);
    }

    receiveHand(playerId, hand){
        hand.forEach(card =>{
            this.AddHand(playerId, new Card(card.colour, card.value));
        })
    }

    AddHand(playerId, card){
        this.players.get(playerId).AddHand(card);
    }

    PlayCard(playerId, cardIndex){
        this.players.get(playerId).PlayCard(cardIndex);
    }
}