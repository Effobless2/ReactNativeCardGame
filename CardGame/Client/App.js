import React from 'react';
import {  } from 'react-native';
import { Provider } from 'react-redux';

import store from './store'

import MainScreen from './screens/MainScreen';

export default class App extends React.Component {
  constructor(props){
    super(props);
    
  }
  
  render() {
    return (
      <Provider store = {store}>
        <MainScreen/>
      </Provider>
    );
  }
}




