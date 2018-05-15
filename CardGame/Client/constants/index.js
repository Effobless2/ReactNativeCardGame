import { Platform, NativeModules } from 'react-native';

const { StatusBarManager } = NativeModules;
export const STATUSBAR_HEIGHT = Platform.OS === 'ios' ? 20 : StatusBarManager.HEIGHT;

export const BLACK = "#000";

export const WHITE = "#FFF";

export const COMPONENTS = "#0FF";

export const MAIN = "#05D";