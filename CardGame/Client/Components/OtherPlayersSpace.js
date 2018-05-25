import React from 'react';
import { View, Text } from 'react-native';
import CardItem from './CardItem';

class OtherPlayersSpace extends React.Component{


    render(){
        console.log("svdv")
        return (
            <View style = {{alignItems:'center', padding: 20}}>
                <View >
                    <Text>{this.props.player.userName}</Text>
                    <Text>Deck : {this.props.player.deckSize}</Text>
                    <Text>Main : {this.props.player.handSize}</Text>
                </View>
                <Text>{this.props.player.playedCard !== null ? this.props.player.playedCard.value + this.props.player.playedCard.color : "?"}</Text>
            </View>
        )
    }
}

export default OtherPlayersSpace;