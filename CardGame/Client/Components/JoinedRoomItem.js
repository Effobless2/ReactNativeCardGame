import React from 'react';
import { View, Text, Button } from 'react-native';
import { connect } from 'react-redux';
import { Styles } from '../Styles';

class JoinedRoomItem extends React.Component{
    constructor(props){
        super(props)
        console.log("createur roomItem");
        this.room = props.room;

    }
    render(){
        return (
            <View style = {Styles.roomItem}>
                <View>
                    <Text>Number of Players : {this.room.nbPlayers}/{this.room.maxOfPlayers}</Text>
                    <Text>Number of Public : {this.room.nbPublics}</Text>
                </View>
                <View>
                    <Button
                        onPress = {() => console.log("JoinTheParty")}
                        title = "Start"/>
                    <Button
                        onPress = {() => console.log("Leave")}
                        title = "Quit"/>
                </View>
                 
            </View>
        )
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, cpt: cardGame.cpt})
export default connect(mapStateToProps)(JoinedRoomItem)