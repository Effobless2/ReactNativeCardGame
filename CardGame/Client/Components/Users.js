import React from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Styles } from '../Styles';

import { connect } from 'react-redux';

class Users extends React.Component{
    constructor(props){
        super(props);
    }

    renderUserList(){
        let counter = 0;
        return Array.from(this.props.users).map(([id, value]) =>{
            counter++;
            if (id != this.props.cardGame.currentUser.userId){
                return (
                    <Text key={counter}>{value.userName}</Text>
                );
            }
        });
    }

    render(){
        return (
            <View style = {Styles.container}>
                <ScrollView style = {Styles.scroll} contentContainerStyle={Styles.scrollViewRoomItem}>
                    {this.renderUserList()}
                </ScrollView>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, users: cardGame.cardGame.Users, cpt: cardGame.cpt})
export default connect(mapStateToProps)(Users)