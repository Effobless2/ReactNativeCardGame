import React from 'react';
import { View, Text } from 'react-native';
import CardItem from './CardItem';

class OtherPlayersSpace extends React.Component{


    render(){
        var content;
        if (this.props.player.hasLoosed()){
            content = <Text>{this.props.player.userName} a Perdu.</Text>
        }
        else{
            content = <View >
            <Text>{this.props.player.userName}</Text>
            <Text>Deck : {this.props.player.deckSize}</Text>
            <Text>Main : {this.props.player.handSize}</Text>
        </View>
        }
        return (
            <View style = {{alignItems:'center', padding: 20}}>
               {content}
               <Text>{this.props.player.playedCard !== null ? this.props.player.playedCard.value + this.props.player.playedCard.color : "?"}</Text>
            </View>
        )
    }
}

export default OtherPlayersSpace;