export class ApplicationUser{
    constructor(Id, name){
        this.userId = Id;
        this.userName = name;
        this.roomsAsPlayer = [];
        this.roomsAsPublic = [];
    }
}