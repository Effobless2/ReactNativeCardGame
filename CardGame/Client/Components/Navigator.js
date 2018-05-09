import {TabNavigator} from 'react-navigation';
import { Home } from './Home'
import { About } from './About'

export const Tabs = TabNavigator({
    Home : { screen: Home },
    About : { screen: About }
  }, {
    tabBarPosition: 'bottom',
    tabBarOptions: {
      showIcon: true,
      showLabel: true,
      indicatorStyle: {
        height: 2,
        backgroundColor: "#FFF"
      },
      style: {
        backgroundColor: "#a2273c",
        borderTopWidth: 1,
        borderColor: "#3f101c"
      }
    }
  })