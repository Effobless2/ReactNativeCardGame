import React from 'react';
import { View, Text, Button } from 'react-native';
import { connect } from 'react-redux';
import { Styles } from '../Styles';
import { escapePublic } from '../actions'

class SeenRoomItem extends React.Component{
    constructor(props){
        super(props)
        console.log("createur roomItem");
        this.room = props.room;

    }

    QuitGame(){
        this.props.escapePublic(this.room.roomId);
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
                        title = "Assist"/>
                    <Button
                        onPress = {() => this.QuitGame()}
                        title = "Quit"/>
                </View>
                 
            </View>
        )
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, cpt: cardGame.cpt})
export default connect(mapStateToProps, { escapePublic })(SeenRoomItem)