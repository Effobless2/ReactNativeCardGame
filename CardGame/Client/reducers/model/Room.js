import { Card } from './Card'

export class Room{
    constructor(Id, playersMax, players, publics){
        this.roomId = Id;
        this.maxOfPlayers = playersMax;
        this.players = players;
        this.publics = publics;
        this.nbPublics = this.publics.length;
        this.nbPlayers = this.players.length;
        this.currentHand = [];
        this.personalPlayedCard = null;

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

    begin(handCard){
        handCard.forEach(card => {
            this.currentHand.push(new Card(card.colour, card.value));
        });
        console.log(this.currentHand);
    }

    confirmCard(cardIndex){
        this.personalPlayedCard = this.currentHand[cardIndex];
        this.currentHand.splice(cardIndex,1);
    }


}