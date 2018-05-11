import { View, Text } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';

export class About extends React.Component{
    constructor(props){
        super(props);
        console.log("construct About")
    }

    render(){
        return (
            <View style = {Styles.container}>
                <Text>About</Text>
            </View>
        );
    }
}