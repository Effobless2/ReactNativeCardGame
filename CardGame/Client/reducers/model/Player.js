import { Card } from "./Card";

export class Player{
    constructor(id, deckSize, handSize){
        this.playerId = id;
        this.deckSize = deckSize;
        this.handSize = handSize;
        this.playedCard = null;
        this.hand = [];
        this.roundWin = false;
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

    discover(cardPlayed){
        console.log(cardPlayed)
        this.playedCard = cardPlayed;
        console.log(this.playedCard);
    }

    roundWon(){
        this.roundWin = true;
        console.log("roundWin")
    }
}