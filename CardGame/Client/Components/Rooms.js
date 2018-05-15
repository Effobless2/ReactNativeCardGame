import { View, Text, ListView, ScrollView } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';
import { connect } from 'react-redux';

class Rooms extends React.Component{
    constructor(props){
        super(props);
        console.log("construct room");
    }

    renderRoomList(){
        let counter = 0;
        return Array.from(this.props.rooms).map(([id, value]) => {
            counter ++;
            return (
                <Text key = {counter}>{counter}, {value.roomId}</Text>
            );
        });
    }

    componentDidMount(){
       // console.log(this.connection)
    }

    render(){
        const {cardGame : {Rooms}} = this.props;
        
        const ds = new ListView.DataSource({rowHasChanged: (r1,r2) => r1 != r2});
        return (
            <View  style = {Styles.container}>
                <ScrollView>
                    {this.renderRoomList()}
                </ScrollView>
            </View>
            
        );
    }
}
const mapStateToProps = ({cardGame}) => ({cardGame :cardGame.cardGame, connected : cardGame.connected, rooms: cardGame.cardGame.Rooms})
export default connect(mapStateToProps)(Rooms)