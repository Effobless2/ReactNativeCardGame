import React from 'react';
import { View, Text, Button } from 'react-native';

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
            <View key={this.room.roomId} style = {{flex: 1, flexDirection: "row", alignItems: "center", justifyContent: "center"}}>
                <Text>{this.counter}</Text>
                <Button
                    onPress = {() => this.pressButton()}
                    title = "See !"
                />
            </View>
        );
    }
}

export default RoomItem;