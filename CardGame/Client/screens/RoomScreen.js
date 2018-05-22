import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../Styles';

class RoomScreen extends React.Component{
    render(){
        return (
            <View style = {Styles.container}>
                <Text>
                    RoomScreen
                </Text>
            </View>
        )
    }
}

export default RoomScreen;