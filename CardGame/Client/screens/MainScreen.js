import React from 'react';
import { View, Text, ActivityIndicator } from 'react-native';
import { Styles } from '../Styles';

import connection from '../connectionServer/Connection'
import Home from './Home';
import { connect } from 'react-redux';


class MainScreen extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        console.log(this.props.connected);
        if (this.props.connected){
            return (
                <View style={Styles.main}>
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