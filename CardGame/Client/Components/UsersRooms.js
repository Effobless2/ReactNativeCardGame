import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../Styles';
import { connect } from 'react-redux';

class UsersRooms extends React.Component{
    render(){
        return (
            <View style = {Styles.container}>
                <Text>
                    Hello World
                </Text>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, users: cardGame.cardGame.Users, cpt: cardGame.cpt})
export default connect(mapStateToProps)(UsersRooms)