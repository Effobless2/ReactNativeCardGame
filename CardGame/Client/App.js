import React from 'react';
import {Text, View, Button } from 'react-native';
import connection from './ConnectionServer/Connection';
import { Home } from './Components/Home';
import { Styles } from './Styles';
import { Tabs } from './Components/Navigator';


export default class App extends React.Component {
  constructor(props){
    super(props);
    this.connection = connection;
  }
  
  render() {
    return (
      <View style={{flex: 1}}>
        <Button
          onPress = {() => this.connection.CreatingRoom()}
          title = "Action"
        />
        <Tabs/>
      </View>
    );
  }
}


