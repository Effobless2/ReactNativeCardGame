import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../Styles';
import AsPlayerComponent from './AsPlayerComponent';
import AsPublicComponent from './AsPublicComponent';

export default class UsersRooms extends React.Component{
    render(){
        return (
            <View style = {Styles.backUsersRoom}>
                <AsPlayerComponent/>
                <AsPublicComponent/>
            </View>
        );
    }
}

