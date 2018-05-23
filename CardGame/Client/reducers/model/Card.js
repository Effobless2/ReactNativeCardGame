export class Card{
    constructor(color, value){
        this.color = color;
        this.value = value;
        this.image = "../icons/cards/"+value+color+".png";
    }
}