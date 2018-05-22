import React from 'react';
import { View, Text, ScrollView } from 'react-native';
import {Styles} from '../Styles';
import { connect } from 'react-redux';
import CurrentRoom from './PlayedRoomItem';

class AsPlayerComponent extends React.Component{

    renderRoomList(){
        let counter = 0;
        console.log(this.props.cardGame.roomsAsPlayer.length)
        return this.props.cardGame.roomsAsPlayer.map(element => {
            counter ++;
            console.log("renderROomList")
            return (
                <CurrentRoom key={element} room ={this.props.cardGame.getRoom(element)}/>
            );
        });
    }
    render(){
        return (
            <View style={Styles.usersRoomComponents}>
                <Text>AsPlayer</Text>
                <ScrollView style ={Styles.scroll} contentContainerStyle={Styles.scrollViewRoomItem}>
                    {this.renderRoomList()}
                </ScrollView>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, cpt: cardGame.cpt})
export default connect(mapStateToProps)(AsPlayerComponent)