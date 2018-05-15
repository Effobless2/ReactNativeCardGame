import React from 'react';
import { View, Text } from 'react-native';

import { Styles } from '../Styles';

import { connect } from 'react-redux';

class Users extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <View style = {Styles.container}>
                <Text>Users</Text>
            </View>
        );
    }
}

export default Users;