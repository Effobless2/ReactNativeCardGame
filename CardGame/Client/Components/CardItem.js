import React from 'react';
import { Image, TouchableOpacity } from 'react-native';

import { IMAGESPATH } from '../icons';

export class CardItem extends React.Component{
    constructor(props){
        super(props);
        this.room = props.room;
        this.card = props.card;
        this.index = props.key;
    }

    render(){
        url = IMAGESPATH[card.value+card.color];
        return (
            <TouchableOpacity
                style = {{padding: 2}}
                onPress = {() => this.props.cardPlayed({roomId: this.room.roomId, cardIndex: this.index})}>
                <Image
                    source = {url}
                    style= {{width: 45, height:78}}
                />
            </TouchableOpacity>
        );
    }
}