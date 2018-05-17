import React from 'react';
import { StyleSheet, Text, View, Button, TouchableOpacity } from 'react-native';
import { Styles } from '../Styles';
import { Tabs } from '../Components/Navigator';
import { connect } from 'react-redux';
import { createRoom } from '../actions';

class Home extends React.Component{
    constructor(props){
        super(props);
        this.connection = props.connection;
        //console.log(this.props)
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20, marginLeft: 10, marginRight: 10, flexDirection:"row", justifyContent:"space-between", alignItems:"center"}}>
                    <Text style = {{fontSize:20}}>{this.props.cardGame.currentUser.userName}</Text>
                    <TouchableOpacity
                        onPress = {() => this.props.createRoom()}>
                        <Text style = {{fontSize: 20}}>
                            New Room
                        </Text>
                    </TouchableOpacity>
                </View>
                <Tabs/>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected})
export default connect(mapStateToProps, { createRoom })(Home)