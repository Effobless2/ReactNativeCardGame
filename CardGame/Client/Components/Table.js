import React from 'react';
import { connect } from 'react-redux';
import {Text, View, Button, Image, TouchableOpacity} from 'react-native';
import { Styles } from '../Styles';
import { cardPlayed } from '../actions';
import IMAGESPATH from '../icons';
import CardItem from './CardItem';
import { Card } from '../reducers/model/Card';

class Table extends React.Component{
    constructor(props){
        super(props);
        
    }

    renderHandCards(){
        return this.props.room.party.players.get(this.props.cardGame.currentUser.userId).hand.map((card, index) => {
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

    renderOtherPlayers(){
        return this.props.room.players.map(player => {
            if (player != this.props.cardGame.currentUser.userId){
                return <View key = {player}>
                    <View style = {{flex: 1, alignItems:'center'}}>
                        <Text>{ this.props.room.party === null ? "En attente" : "Deck de l'adversaire : " + this.props.room.party.players.get(player).deckSize }</Text>
                        <Text>{ this.props.room.party === null ? "En attente" : "Main de l'adversaire : " + this.props.room.party.players.get(player).handSize }</Text>
                        {/*<Image 
                                source = {
                                    (this.props.room.party === null || this.props.room.party.players.get(player).playedCard === null) ? 
                                    IMAGESPATH(new Card("?","")) : 
                                    IMAGESPATH(this.props.room.party.players.get(player).playedCard)
                                }
                                style = {Styles.cardSize}
                                />*/}
                        <Text>{this.props.room.party === null || this.props.room.party.players.get(player).playedCard === null ?
                                "?" :
                                this.props.room.party.players.get(player).playedCard.value + this.props.room.party.players.get(player).playedCard.color}</Text>
                    </View>
                    <View style={{flex:1, alignItems:'center'}}>
                        <Text>Zone Jouée de l'adversaire</Text>
                    </View>
                </View>
                    
            }
        })


        
    }

    render(){
        return (
            <View style={[Styles.container]}>
                <View style = {{flex: 1, width: "100%", alignItems:'center', justifyContent: 'center', flexDirection:"row"}}>
                    {this.renderOtherPlayers()}
                </View>
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
                            <Text>{(this.props.room.party === null || this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard === null) ? 
                                    "?" : 
                                    this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard.value + this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard.color}
                                    </Text>
                        </View>
                        <View style={{flex: 1, alignItems:'center'}}>
                            <Text> Mon Deck</Text>
                            <Text>{(this.props.room.party === null) ?
                                "En attente" :
                                this.props.room.party.players.get(this.props.cardGame.currentUser.userId).deckSize}</Text>
                        </View>
                    </View>
                    <View style = {{flex: 1, flexDirection:"row", alignItems:'center'}}>
                        {this.props.room.party === null ? <Text>En attente</Text> : this.renderHandCards()}
                    </View>
                </View>
                
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, room, selectedRoom, cpt}) => ({cardGame :cardGame.cardGame, room: cardGame.cardGame.getRoom(cardGame.selectedRoom), selectedRoom: cardGame.selectedRoom, cpt: cardGame.cpt})
export default connect(mapStateToProps, { cardPlayed })(Table);