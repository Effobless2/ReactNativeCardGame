import React from 'react';
import { connect } from 'react-redux';
import {Text, View, Button} from 'react-native';
import { Styles } from '../Styles';
import { cardPlayed } from '../actions';

class Table extends React.Component{
    constructor(props){
        super(props);
        
    }

    renderHandCards(){
        return this.props.room.currentHand.map((card, index) => {
            return <Button
                        title = {card.color + " : " + card.value}
                        onPress = {() => this.props.cardPlayed({roomId: this.props.room.roomId, cardIndex: index})}
                        key = {index}
                    />
        });
    }

    render(){
        return (
            <View>
                <Text>{this.props.room.roomId}</Text>
                <View>
                    {this.renderHandCards()}
                </View>
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, room, selectedRoom, cpt}) => ({cardGame :cardGame.cardGame, room: cardGame.cardGame.getRoom(cardGame.selectedRoom), selectedRoom: cardGame.selectedRoom, cpt: cardGame.cpt})
export default connect(mapStateToProps, { cardPlayed })(Table);