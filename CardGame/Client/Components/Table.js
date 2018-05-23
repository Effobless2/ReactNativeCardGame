import React from 'react';
import { connect } from 'react-redux';
import {Text, View, Button, Image, TouchableOpacity} from 'react-native';
import { Styles } from '../Styles';
import { cardPlayed } from '../actions';
import { IMAGESPATH } from '../icons';

class Table extends React.Component{
    constructor(props){
        super(props);
        
    }

    renderHandCards(){
        return this.props.room.currentHand.map((card, index) => {
            url = IMAGESPATH[card.value+card.color];
            return <TouchableOpacity
                        key = {index}
                        onPress = {() => this.props.cardPlayed({roomId: this.props.room.roomId, cardIndex: index})}>
                        <Image
                            source = {url}
                            style= {{width: 50, height:80}}
                        />
                    </TouchableOpacity>
        });
    }

    render(){
        return (
            <View>
                <Text>{this.props.room.roomId}</Text>
                <View style = {{ flexDirection:"row"}}>
                    {this.renderHandCards()}
                </View>
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, room, selectedRoom, cpt}) => ({cardGame :cardGame.cardGame, room: cardGame.cardGame.getRoom(cardGame.selectedRoom), selectedRoom: cardGame.selectedRoom, cpt: cardGame.cpt})
export default connect(mapStateToProps, { cardPlayed })(Table);