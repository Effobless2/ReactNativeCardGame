import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import { Styles } from '../Styles';
import { Tabs } from '../components/Navigator';

export class Home extends React.Component{
    constructor(props){
        super(props);
        this.connection = props.connection;
        //console.log(this.props)
        console.log("kol")
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20}}>
                    <Text style = {{fontSize:20}}>{this.connection.cardGame.currentUser.userName}</Text>
                </View>
                <Tabs screenProps = {this.connection}/>
            </View>
        );
    }
}