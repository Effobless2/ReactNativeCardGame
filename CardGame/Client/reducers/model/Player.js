import { Card } from "./Card";
import { ApplicationUser } from './ApplicationUser';
export class Player extends ApplicationUser{
    constructor(ApplicationUser, deckSize, handSize, playedCard){
        super(ApplicationUser.userId, ApplicationUser.userName);
        this.deckSize = deckSize;
        this.handSize = handSize;
        this.playedCard = playedCard;
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

    setDeck(deckCount){
        this.deckSize = deckCount;
    }

    setHand(handCount){
        this.handSize = handCount;
    }
}