import React from 'react';
import { View, Text, TouchableOpacity, Picker } from 'react-native';
import { connect } from 'react-redux';
import { Styles } from '../Styles';
import { selectRoom } from '../actions';
import Table from '../Components/Table';

class RoomScreen extends React.Component{

    renderPlayRoomList(){
        let counter = 0;
        return this.props.asPlayer.map((roomId) =>{
            counter++;
            return (
                <Picker.Item 
                    color = "#f00"
                    label= {'Play '+counter}
                    value = {roomId}
                    key = {roomId} 
                />
            );
        });
    }

    renderSeeRoomList(){
        let counter = 0;
        return this.props.asPublic.map((roomId) =>{
            counter++;
            return (
                <Picker.Item
                    color = "#0f0"
                    label= {'See '+counter}
                    value = {roomId}
                    key = {roomId} 
                />
            );
        });
    }

    render(){
        let room = this.props.cardGame.getRoom(this.props.selectedRoom);
        let content;
        if (room === undefined){
            content = <Text>This room has been closed</Text>
        }
        else{
            content = <Table room={room}/>
        }
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
                    style= {{flex: 1,}}
                        selectedValue = {this.props.selectedRoom}
                        onValueChange = {(itemValue, itemIndex) => {this.props.selectRoom(itemValue)}}>
                        {this.renderPlayRoomList()}
                        {this.renderSeeRoomList()}
                    </Picker>
                    
                </View>
                <View style = {Styles.container}>
                    {content}
                </View>
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, asPlayer, asPublic, selectedRoom}) => ({cardGame :cardGame.cardGame,  asPlayer: cardGame.cardGame.roomsAsPlayer, asPublic: cardGame.cardGame.roomsAsPublic, selectedRoom: cardGame.selectedRoom})
export default connect(mapStateToProps, { selectRoom })(RoomScreen);