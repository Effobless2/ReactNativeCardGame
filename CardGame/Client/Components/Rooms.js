import { View, Text } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';

export class Rooms extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <View style = {Styles.container}>
                <Text>Rooms</Text>
            </View>
        );
    }
}