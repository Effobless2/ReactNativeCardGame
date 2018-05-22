import React from 'react';
import { View, Text, TouchableOpacity } from 'react-native';
import { connect } from 'react-redux';
import { Styles } from '../Styles';
import { selectRoom } from '../actions';

class RoomScreen extends React.Component{
    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20, marginLeft: 10, marginRight: 10, flexDirection:"row", justifyContent:"space-between", alignItems:"center"}}>
                    <TouchableOpacity
                        onPress = {() => this.props.selectRoom(null)}>
                        <Text style = {{fontSize: 20}}>
                            Go Back
                        </Text>
                    </TouchableOpacity>
                </View>
                <View style = {Styles.container}>
                    <Text>
                        {this.props.cardGame.getRoom(this.props.selectedRoom).roomId}
                    </Text>
                </View>
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, selectedRoom}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps, { selectRoom })(RoomScreen);