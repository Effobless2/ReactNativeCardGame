import { View, Text, ListView } from 'react-native';
import React from 'react';
import { Styles } from '../Styles';

export class Rooms extends React.Component{
    constructor(props){
        super(props);
        console.log("construct room")
        this.connection = props.screenProps
        const datas = [];
        console.log(this.connection.cardGame.Rooms)
        for (var room of this.connection.cardGame.Rooms.values()){
            datas.push(room);
            console.log("a")
        }
        this.state = {
            datas: datas
        }
    
    }

    componentDidMount(){
       // console.log(this.connection)
    }

    render(){
        
        const ds = new ListView.DataSource({rowHasChanged: (r1,r2) => r1 != r2});
        return (
            <View style = {Styles.container}>
                <Text>{this.connection.cardGame.currentUser.userName}</Text>
                <ListView
                    dataSource={ds.cloneWithRows(this.state.datas)}
                    renderRow = {(rowData, i, j) => <Text>{j}, {rowData.roomId}</Text>}
                />
            </View>
        );
    }
}