import React from 'react';
import { StyleSheet, Text, View, Button } from 'react-native';
import connection from './ConnectionServer/Connection';
import { Home } from './Components/Home';


export default class App extends React.Component {
  constructor(props){
    super(props);
    this.connection = connection;
  }
  
  render() {
    return (
      <View style={styles.container}>
        <Button
          onPress = {() => this.connection.CreatingRoom()}
          title = "Action"
        />
        <Home/>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
