import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../Styles';
import { connect } from 'react-redux';

class UsersRooms extends React.Component{
    render(){
        return (
            <View style = {Styles.backUsersRoom}>
                <View style = {Styles.usersRoomComponents}>
                    <Text> As Player </Text>
                </View>
                <View style = {Styles.usersRoomComponents}>
                    <Text> As Public </Text>
                </View>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, users: cardGame.cardGame.Users, cpt: cardGame.cpt})
export default connect(mapStateToProps)(UsersRooms)