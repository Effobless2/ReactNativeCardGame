import { StyleSheet } from 'react-native';
import { STATUSBAR_HEIGHT } from './constants'


export const Styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: "#0ff",
      alignItems: 'center',
      justifyContent: 'center',
      margin: 10,
    },
    connection : {
      flex: 1,
      backgroundColor: '#fff',
      alignItems: 'center',
      justifyContent: 'center',
      margin: 10,
    },
    scrollViewRoomItem: {
      alignItems: 'center',
      justifyContent: 'center',
      width: "100%"
    },
    scroll: {
      flex: 1,
      width: "100%"
    },
    main: {
      flex: 1,
      paddingTop: STATUSBAR_HEIGHT,
      backgroundColor: 'blue'
    },
    tabBarIndicatorStyle:  {
      height: 5,
      
    },
    tabBarStyle: {
      backgroundColor: "#0FF",
      borderTopWidth: 1,
      borderColor: "#000"
    },
    roomItem: {
      padding:10,
      flex: 1,
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
      width: "100%"
    }
  });
