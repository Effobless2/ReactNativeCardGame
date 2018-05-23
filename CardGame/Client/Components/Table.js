import React from 'react';
import { connect } from 'react-redux';
import {Text, View, Button} from 'react-native';
import { Styles } from '../Styles';
import { cardPlayed } from '../actions';

class Table extends React.Component{
    constructor(props){
        super(props);
        this.room = this.props.cardGame.getRoom(this.props.selectedRoom);
        
    }

    renderHandCards(room){
        return room.currentHand.map((card, index) => {
            return <Button
                        title = {card.color + " : " + card.value}
                        onPress = {() => this.props.cardPlayed({roomId: room.roomId, cardIndex: index})}
                        key = {index}
                    />
        });
    }

    render(){
        let room = this.props.cardGame.getRoom(this.props.selectedRoom);
        return (
            <View>
                <Text>{room.roomId}</Text>
                <View>
                    {this.renderHandCards(room)}
                </View>
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, asPlayer, asPublic, selectedRoom}) => ({cardGame :cardGame.cardGame,  asPlayer: cardGame.cardGame.roomsAsPlayer, asPublic: cardGame.cardGame.roomsAsPublic, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps, { cardPlayed })(Table);