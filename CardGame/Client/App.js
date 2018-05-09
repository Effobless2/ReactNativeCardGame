import React from 'react';
import {Text, View, Button } from 'react-native';
import connection from './ConnectionServer/Connection';
import { Home } from './Components/Home';
import { Styles } from './Styles';
import { Platform, NativeModules } from 'react-native';
const { StatusBarManager } = NativeModules;

const STATUSBAR_HEIGHT = Platform.OS === 'ios' ? 20 : StatusBarManager.HEIGHT;

export default class App extends React.Component {
  constructor(props){
    super(props);
    this.connection = connection;
  }
  
  render() {
    return (
      <View style={{flex: 1, paddingTop: STATUSBAR_HEIGHT, backgroundColor: 'blue'}}>
        <Home/>
      </View>
    );
  }
}


