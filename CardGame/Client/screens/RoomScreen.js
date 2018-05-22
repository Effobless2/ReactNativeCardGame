import React from 'react';
import { View, Text, TouchableOpacity, Picker } from 'react-native';
import { connect } from 'react-redux';
import { Styles } from '../Styles';
import { selectRoom } from '../actions';

class RoomScreen extends React.Component{

    renderPlayRoomList(){
        let counter = 0;
        return this.props.cardGame.roomsAsPlayer.map((roomId) =>{
            counter++;
            return (
                <Picker.Item 
                    label= {'Play '+counter}
                    value = {roomId}
                    key = {counter} 
                />
            );
        });
    }

    renderSeeRoomList(){
        let counter = 0;
        return this.props.cardGame.roomsAsPublic.map((roomId) =>{
            counter++;
            return (
                <Picker.Item 
                    label= {'See '+counter}
                    value = {roomId}
                    key = {counter} 
                />
            );
        });
    }

    render(){
        return (
            <View style = {{flex: 1}}>
                <View style = {{height: 50, paddingTop: 20, marginLeft: 10, marginRight: 10, flexDirection:"row", justifyContent:"space-between", alignItems:"center"}}>
                    <TouchableOpacity
                    style = {{flex: 1}}
                        onPress = {() => this.props.selectRoom(null)}>
                        <Text style = {{fontSize: 20}}>
                            Go Back
                        </Text>
                    </TouchableOpacity>
                    <Picker
                    style= {{flex: 1}}
                        selectedValue = {this.props.selectedRoom}
                        onValueChange = {(itemValue, itemIndex) => {this.props.selectRoom(itemValue)}}>
                        {this.renderPlayRoomList()}
                        {this.renderSeeRoomList()}
                    </Picker>
                    
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