import React from 'react';
import { View, Text, ActivityIndicator } from 'react-native';
import { Platform, NativeModules } from 'react-native';
import { Styles } from '../Styles';

import connection from '../connectionServer/Connection'
import Home from './Home';
import { connect } from 'react-redux';

const { StatusBarManager } = NativeModules;
const STATUSBAR_HEIGHT = Platform.OS === 'ios' ? 20 : StatusBarManager.HEIGHT;

class MainScreen extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        console.log(this.props.connected);
        if (this.props.connected){
            return (
                <View style={{flex: 1, paddingTop: STATUSBAR_HEIGHT, backgroundColor: 'blue'}}>
                    <Home/>
                </View>
            );
        }
        else{
            return(
                <View style={Styles.connection}>
                    <ActivityIndicator/>
                </View>
            );
        }
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected})

export default connect(mapStateToProps)(MainScreen);