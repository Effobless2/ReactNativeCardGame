import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import { Styles } from '../Styles';

export class Home extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <View style = {Styles.container}>
                <Text>HomePage</Text>
            </View>
        );
    }
}