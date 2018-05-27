import React from 'react';
import { connect } from 'react-redux';
import {Text, View, Button, Image, TouchableOpacity} from 'react-native';
import { Styles } from '../Styles';
import { cardPlayed } from '../actions';
import IMAGESPATH from '../icons';
import { Card } from '../reducers/model/Card';
import OtherPlayersSpace from './OtherPlayersSpace';
import CurrentPlayer from './currentPlayer';

class Table extends React.Component{
    constructor(props){
        super(props);
        
    }

    renderOtherPlayers(){
        return Array.from(this.props.room.players).map(([id, player]) => {
            if (id !== this.props.cardGame.currentUser.userId){
                return (<OtherPlayersSpace key={id} player={player}/>);
            }
        });
    }

    render(){
        return (
            <View style={[Styles.container]}>
                <View style = {{flex: 1, width: "100%", alignItems:'center', justifyContent: 'center', flexDirection:"row"}}>
                    {this.renderOtherPlayers()}
                </View>
                {this.props.room.players.get(this.props.cardGame.currentUser.userId) != null ?
                    <CurrentPlayer
                        player = {this.props.room.players.get(this.props.cardGame.currentUser.userId)}
                        room = {this.props.room}
                    /> :
                    <Text></Text>
                }
                
                
            </View>
        )
    }
}

const mapStateToProps = ({cardGame, room, selectedRoom, cpt}) => ({cardGame :cardGame.cardGame, room: cardGame.cardGame.getRoom(cardGame.selectedRoom), selectedRoom: cardGame.selectedRoom, cpt: cardGame.cpt})
export default connect(mapStateToProps, { cardPlayed })(Table);