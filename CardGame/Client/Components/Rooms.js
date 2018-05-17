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
            return (
                <RoomItem 
                    room={value}
                    key={id}
                    counter={counter}
                />
            );
        });
    }

    componentDidMount(){
       // console.log(this.connection)
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
const mapStateToProps = ({cardGame, cpt}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, rooms: cardGame.cardGame.Rooms, cpt: cardGame.cpt})
export default connect(mapStateToProps)(Rooms)