import React from 'react';
import { View, Text } from 'react-native';
import {Styles} from '../Styles';

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

export default UsersRooms;