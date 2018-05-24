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
    }

    PlayCard(cardIndex){
        this.playedCard = this.hand[cardIndex];
        this.hand.splice(cardIndex, 1);
    }
}