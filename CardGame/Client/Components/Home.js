import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import { Styles } from '../Styles';
import { Tabs } from './Navigator';

export class Home extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20}}>
                    <Text style = {{fontSize:20}}>UserName</Text>
                </View>
                <Tabs/>
            </View>
        );
    }
}