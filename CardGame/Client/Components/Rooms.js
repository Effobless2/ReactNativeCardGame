import { View, Text, ListView, ScrollView } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';
import { connect } from 'react-redux';
import RoomItem from './RoomItem';

class Rooms extends React.Component{
    constructor(props){
        super(props);
    }

    renderRoomList(){
        let counter = 0;
        return Array.from(this.props.rooms).map(([id, value]) => {
            counter ++;
            console.log("renderRoomList");
            return (
                <RoomItem 
                    room={value}
                    key={id}
                    counter={counter}
                />
            );
        });
    }

    render(){
        
        return (
            <View  style = {Styles.container}>
                <ScrollView style ={Styles.scroll} contentContainerStyle={Styles.scrollViewRoomItem}>
                    {this.renderRoomList()}
                </ScrollView>
            </View>
            
        );
    }
}
const mapStateToProps = ({cardGame, rooms, cpt}) => ({cardGame :cardGame.cardGame, rooms: cardGame.cardGame.Rooms, cpt: cardGame.cpt})
export default connect(mapStateToProps)(Rooms)