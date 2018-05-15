import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import { Styles } from '../Styles';
import { Tabs } from '../components/Navigator';
import { connect } from 'react-redux';

class Home extends React.Component{
    constructor(props){
        super(props);
        this.connection = props.connection;
        //console.log(this.props)
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20, marginLeft: 10}}>
                    <Text style = {{fontSize:20}}>{this.props.cardGame.currentUser.userName}</Text>
                </View>
                <Tabs/>
            </View>
        );
    }
}

const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected})
export default connect(mapStateToProps)(Home)