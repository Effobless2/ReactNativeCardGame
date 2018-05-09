import {TabNavigator} from 'react-navigation';
import { Home } from './Home'
import { About } from './About'
import { Rooms } from './Rooms'

export const Tabs = TabNavigator({
    Rooms : { screen: Rooms },
    About : { screen: About }
  }, {
    tabBarPosition: 'bottom',
    tabBarOptions: {
      showIcon: true,
      showLabel: true,
      indicatorStyle: {
        height: 2,
        backgroundColor: "#FFF",
        paddingHorizontal: 10
      },
      style: {
        backgroundColor: "#a2273c",
        borderTopWidth: 1,
        borderColor: "#3f101c"
      }
    }
  })