import React from 'react';
import { Image, TouchableOpacity } from 'react-native';

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
                <Image
                    source = {this.props.image}
                    style= {{width: 45, height:78}}
                />
            </TouchableOpacity>
        );
    }
}

const mapStateToProps = ({cardGame, selectedRoom}) => ({cardGame: cardGame.cardGame, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps,{cardPlayed})(CardItem);