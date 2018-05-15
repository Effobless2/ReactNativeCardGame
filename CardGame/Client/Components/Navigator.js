import {createBottomTabNavigator} from 'react-navigation';
import { About } from './About';
import Rooms from './Rooms';
import Users from './Users';
import { Styles } from '../Styles';

export const Tabs = createBottomTabNavigator({
    Rooms : { screen: Rooms },
    Users : { screen: Users },
    About : { screen: About }
  }, {
    tabBarPosition: 'bottom',
    tabBarOptions: {
      showIcon: true,
      showLabel: true,
      indicatorStyle: Styles.tabBarIndicatorStyle,
      style: Styles.tabBarStyle,
      activeTintColor: "#000",
      tabStyle: Styles.tabsStyle,
    },
    
  })