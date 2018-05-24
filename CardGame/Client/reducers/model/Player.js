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

    playedCard(cardIndex){
        this.playedCard = this.party.hand[cardIndex];
        this.hand.splice(cardIndex, 1);
    }
}