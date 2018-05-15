import { StyleSheet } from 'react-native';
import { 
  STATUSBAR_HEIGHT,
  BLACK,
  WHITE,
  COMPONENTS,
  MAIN
} from './constants'


export const Styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: COMPONENTS,
      alignItems: 'center',
      justifyContent: 'center',
      margin: 10,
    },
    connection : {
      flex: 1,
      backgroundColor: WHITE,
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
      backgroundColor: MAIN
    },
    tabBarIndicatorStyle:  {
      height: 4,
      
    },
    tabBarStyle: {
      backgroundColor: COMPONENTS,
      borderTopWidth: 1,
      borderTopColor: BLACK,

    },
    roomItem: {
      padding:10,
      flex: 1,
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
      width: "100%"
    },
    tabsStyle: {
      borderRightWidth: 1,
      borderLeftWidth: 1,
      borderColor: BLACK,
      borderColor: BLACK,
    }
  });
