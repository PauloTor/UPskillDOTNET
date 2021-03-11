import React from 'react';
import {SafeAreaView, StyleSheet, ScrollView, View, Text, StatusBar, Button} from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createDrawerNavigator } from '@react-navigation/drawer';

import MainTabScreen from './screens/MainTabScreen';
import { DrawerContent } from './screens/DrawerContent';
import ApoioAoClienteScreen from './screens/ApoioAoClienteScreen';
import RootStackScreen from './screens/RootStackScreen';
const Drawer = createDrawerNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <RootStackScreen />
       {/* <Drawer.Navigator drawerContent={props => <DrawerContent {...props} />}>
        <Drawer.Screen name="MainHome" component={MainTabScreen} />
        <Drawer.Screen name="Apoio ao Cliente" component={ApoioAoClienteScreen} />
      </Drawer.Navigator> */}
    </NavigationContainer>
  );
}

export default App;
