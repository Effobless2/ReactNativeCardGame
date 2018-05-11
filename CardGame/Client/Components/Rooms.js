import { View, Text } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';

export class Rooms extends React.Component{
    constructor(props){
        super(props);
        console.log("construct room")
        this.connection = props.screenProps
    
    }

    componentDidMount(){
       // console.log(this.connection)
    }

    render(){
        return (
            <View style = {Styles.container}>
                <Text>{this.connection.cardGame.currentUser.userName}</Text>
            </View>
        );
    }
}