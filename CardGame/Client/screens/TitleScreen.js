import React from 'react';
import {Text, View, Button, ActivityIndicator } from 'react-native';

export default class TitleScreen extends React.Component {
  
  render() {
    return (
      <View >
        <Text>Super Application de Jeu de Cartes</Text>
        <ActivityIndicator size="small" color="#000"/>
      </View>
    );
  }
}
