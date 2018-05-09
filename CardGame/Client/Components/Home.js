import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import { Styles } from '../Styles';
import { Tabs } from './Navigator';

export class Home extends React.Component{
    constructor(props){
        super(props);
        this.cardGame = props.game;
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20}}>
                    <Text style = {{fontSize:20}}>{this.cardGame.currentUser.userName}</Text>
                </View>
                <Tabs/>
            </View>
        );
    }
}