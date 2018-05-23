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
            style = {{padding: 2}}
                        key = {index}
                        onPress = {() => this.props.cardPlayed({roomId: this.props.room.roomId, cardIndex: index})}>
                        <Image
                            source = {url}
                            style= {{width: 45, height:78}}
                        />
                    </TouchableOpacity>
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
                    <View style = {{flex: 1, alignItems:'center'}}>
                        <Text> Ma Zone Jouée</Text>
                    </View>
                    <View style = {{flex: 1, flexDirection:"row", alignItems:'center'}}>
                        {this.renderHandCards()}
                    </View>
                </View>
                
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, room, selectedRoom, cpt}) => ({cardGame :cardGame.cardGame, room: cardGame.cardGame.getRoom(cardGame.selectedRoom), selectedRoom: cardGame.selectedRoom, cpt: cardGame.cpt})
export default connect(mapStateToProps, { cardPlayed })(Table);