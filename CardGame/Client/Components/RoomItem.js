import React from 'react';
import { View, Text, Button } from 'react-native';
import { Styles } from '../Styles';
import { addPublic, addPlayer } from '../actions';
import { connect } from 'react-redux';

class RoomItem extends React.Component{
    constructor (props){
        super(props);
        this.room = props.room;
        this.counter = this.props.counter
    }

    pressButton(){
        console.log("Button of room " + this.room.roomId + " has been pressed !!! Shame on you.")
    }

    render(){
        return (
            <View key={this.room.roomId} style = {Styles.roomItem}>
                <Text>{this.counter}</Text>
                <View>
                    <Text>Number of Players : {this.room.nbPlayers}/{this.room.maxOfPlayers}</Text>
                    <Text>Number of Public : {this.room.nbPublics}</Text>

                </View>
                <View>
                <Button
                    onPress = {() => this.props.addPublic(this.room.roomId)}
                    title = "See !"
                />
                <Button
                    onPress = {() => this.props.addPlayer(this.room.roomId)}
                    title = "Join !"
                />
                </View>
            </View>
        );
    }
}
const mapStateToProps = ({cardGame, cpt}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, rooms: cardGame.cardGame.Rooms, cpt: cardGame.cpt})
export default connect(mapStateToProps, {addPublic, addPlayer})(RoomItem)