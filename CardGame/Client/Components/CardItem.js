import React from 'react';
import { Image, TouchableOpacity, Text } from 'react-native';

import { cardPlayed } from '../actions';
import { connect } from 'react-redux';

class CardItem extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <TouchableOpacity
                style = {{padding: 2}}
                onPress = {() => this.props.cardPlayed({roomId: this.props.room.roomId, cardIndex: this.props.index})}>
                <Text>{this.props.card.value+this.props.card.color}</Text>
            </TouchableOpacity>
        );
    }
}

const mapStateToProps = ({cardGame, selectedRoom}) => ({cardGame: cardGame.cardGame, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps,{cardPlayed})(CardItem);