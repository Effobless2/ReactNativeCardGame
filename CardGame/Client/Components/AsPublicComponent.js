import React from 'react';
import { View, Text, ScrollView } from 'react-native';
import {Styles} from '../Styles';
import { connect } from 'react-redux';
import CurrentRoom from './SeenRoomItem';

class AsPublicComponent extends React.Component{

    renderRoomList(){
        let counter = 0;
        console.log(this.props.rooms.length)
        return this.props.rooms.map(element => {
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
                <Text>AsPublic</Text>
                <ScrollView style ={Styles.scroll} contentContainerStyle={Styles.scrollViewRoomItem}>
                    {this.renderRoomList()}
                </ScrollView>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame, rooms}) => ({cardGame :cardGame.cardGame, rooms: cardGame.cardGame.roomsAsPublic, cpt: cardGame.cpt})
export default connect(mapStateToProps)(AsPublicComponent)