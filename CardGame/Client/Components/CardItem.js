import React from 'react';
import { Image, TouchableOpacity } from 'react-native';

import { cardPlayed } from '../actions';
import { IMAGESPATH } from '../icons';
import { connect } from 'react-redux';

class CardItem extends React.Component{
    constructor(props){
        super(props);
        console.log("construct")
    }

    render(){
        console.log("rip")
        url = IMAGESPATH[this.props.card.value+this.props.card.color];
        return (
            <TouchableOpacity
                style = {{padding: 2}}
                onPress = {() => this.props.cardPlayed({roomId: this.props.room.roomId, cardIndex: this.props.key})}>
                <Image
                    source = {url}
                    style= {{width: 45, height:78}}
                />
            </TouchableOpacity>
        );
    }
}

const mapStateToProps = ({cardGame, selectedRoom}) => ({cardGame: cardGame.cardGame, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps,{cardPlayed})(CardItem);