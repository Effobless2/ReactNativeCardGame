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
                    room = {this.props.room}
                    image = {IMAGESPATH(card)}
                />)
        });
    }

    render(){
        return (
            <View style={[Styles.container]}>
                <View style = {{flex: 1, width: "100%", alignItems:'center'}}>
                    <View style = {{flex: 1, alignItems:'center'}}>
                        <Text> Deck de l'adversaire </Text>
                    </View>
                    <View style={{flex:1, alignItems:'center'}}>
                        <Text>Zone Jouée de l'adversaire</Text>
                    </View>
                </View>
                <View style = {{flex: 1, width: "100%", alignItems:'center'}}>
                    <View style = {{flex: 1, justifyContent:'space-between', flexDirection:'row', alignItems:'center'}}>
                        <View style={{flex: 1, alignItems:'center'}}>
                            <Text>MaCarte</Text>
                            <Image 
                                source = {
                                    (this.props.room.party === null || this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard === null) ? 
                                    IMAGESPATH(new Card("unknown","")) : 
                                    IMAGESPATH(this.props.room.party.players.get(this.props.cardGame.currentUser.userId).playedCard)
                                }
                                style = {{width: 45, height:78}}
                                />
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