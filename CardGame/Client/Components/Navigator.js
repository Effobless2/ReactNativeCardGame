import {createBottomTabNavigator} from 'react-navigation';
import { Home } from './Home'
import { About } from './About'
import { Rooms } from './Rooms'

export const Tabs = createBottomTabNavigator({
    Rooms : { screen: Rooms },
    About : { screen: About }
  }, {
    tabBarPosition: 'bottom',
    tabBarOptions: {
      showIcon: true,
      showLabel: true,
      indicatorStyle: {
        height: 5,
        backgroundColor: "#FFF",
      },
      style: {
        backgroundColor: "#a2273c",
        borderTopWidth: 1,
        borderColor: "#3f101c"
      }
    }
  })