import React from 'react';
import { View, Text, ActivityIndicator } from 'react-native';
import { Styles } from '../Styles';


import Home from './Home';
import TitleScreen from './TitleScreen';
import connection from '../ConnectionServer/Connection'
import { connect } from 'react-redux';
import RoomScreen from './RoomScreen';


class MainScreen extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        console.log(this.props.connected);
        if (this.props.connected){
            if(this.props.selectedRoom === null){
                return (
                    <View style={Styles.main}>
                        <Home/>
                    </View>
                );
            }
            else{
                return (
                    <View style={Styles.main}>
                        <RoomScreen/>
                    </View>
                )
            }
            
        }
        else{
            return(
                <View style={Styles.connection}>
                    <TitleScreen/>
                </View>
            );
        }
    }
}

const mapStateToProps = ({cardGame, selectedRoom}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, selectedRoom: cardGame.selectedRoom})

export default connect(mapStateToProps)(MainScreen);