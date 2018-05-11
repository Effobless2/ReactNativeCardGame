import React from 'react';
import {Text, View, Button, ActivityIndicator } from 'react-native';
import { ConnectionServer } from './ConnectionServer/Connection';
import { Home } from './Components/Home';
import { Styles } from './Styles';
import { Platform, NativeModules } from 'react-native';
import { TitleScreen } from './Components/TitleScreen';
const { StatusBarManager } = NativeModules;

const STATUSBAR_HEIGHT = Platform.OS === 'ios' ? 20 : StatusBarManager.HEIGHT;

export default class App extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      connection : new ConnectionServer("http://192.168.1.62:5000/cardgame/", this),
      loaded : false,
    }
  }
  
  render() {
    if (this.state.loaded){
      return (
        <View style={{flex: 1, paddingTop: STATUSBAR_HEIGHT, backgroundColor: 'blue'}}>
          <Home connection = {this.state.connection}/>
        </View>
      );
    }
    else{
      return(
        <View style={Styles.connection}>
          <ActivityIndicator/>
        </View>
      );
    }
    
  }
}


