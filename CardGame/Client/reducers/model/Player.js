import { Card } from "./Card";

export class Player{
    constructor(id, deckSize, handSize){
        this.playerId = id;
        this.deckSize = deckSize;
        this.handSize = handSize;
        this.playedCard = null;
        this.hand = [];
    }

    AddHand(card){
        this.hand.push(card);
        console.log(card)
    }

    PlayCard(cardIndex){
        this.playedCard = this.hand[cardIndex];
        this.hand.splice(cardIndex, 1);
    }

    hasPlayed(){
        this.playedCard = new Card("unknown", "");
        this.handSize --;
        console.log(this.playedCard);
    }
}