import React from 'react';
import { Text, View } from 'react-native';
import CardItem from './CardItem';
import IMAGESPATH from '../icons';



class CurrentPlayer extends React.Component{

    renderHandCards(){
        if (this.props.player.hasLoosed()){
            return <Text>You Loose.</Text>
        }
        else if (this.props.player.hasWin()){
            return <Text>You Win !</Text>
        }
        return this.props.player.hand.map((card, index) => {
            return (
                <CardItem
                    key = {index}
                    index = {index}
                    card = {card}
                    room = {this.props.room}
                    image = {IMAGESPATH(card)}
                />)
        });
    }

    render(){
        return (
        <View style = {{flex: 1, width: "100%", alignItems:'center'}}>
            <View style = {{flex: 1, justifyContent:'space-between', flexDirection:'row', alignItems:'center'}}>
                <View style={{flex: 1, alignItems:'center'}}>
                    <Text>MaCarte</Text>
                            {/*<Image 
                                source = {
                                    (this.props.room.party === null || this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard === null) ? 
                                    IMAGESPATH(new Card("unknown","")) : 
                                    IMAGESPATH(this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard)
                                }
                                style = {{width: 45, height:78}}
                            />*/}
                    <Text>{(this.props.room.maxOfPlayers !== this.props.room.players.size || this.props.player.playedCard === null) ? 
                        "?" : 
                        this.props.player.playedCard.value + this.props.player.playedCard.color}
                    </Text>
                </View>
                <View style={{flex: 1, alignItems:'center'}}>
                    <Text> Mon Deck</Text>
                    <Text>{(this.props.room.maxOfPlayers !== this.props.room.players.size) ?
                        "En attente" :
                        this.props.player.deckSize}</Text>
                </View>
            </View>
            <View style = {{flex: 1, flexDirection:"row", alignItems:'center'}}>
                {this.props.room.maxOfPlayers !== this.props.room.players.size ? <Text>En attente</Text> : this.renderHandCards()}
            </View>
        </View>)
    }
}

export default CurrentPlayer;