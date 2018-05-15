import React from 'react';
import { View, Text, Button } from 'react-native';
import { Styles } from '../Styles';

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
                    <Text>Number of Players Max : {this.room.maxOfPlayers}</Text>
                    <Text>Number of person in public : {this.room.nbPublics}</Text>

                </View>
                <Button
                    onPress = {() => this.pressButton()}
                    title = "See !"
                />
            </View>
        );
    }
}

export default RoomItem;