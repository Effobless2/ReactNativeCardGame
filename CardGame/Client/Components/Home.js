import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';

export class Home extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <View>
                <Text>HomePage</Text>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      alignItems: 'center',
      justifyContent: 'center',
    },
  });