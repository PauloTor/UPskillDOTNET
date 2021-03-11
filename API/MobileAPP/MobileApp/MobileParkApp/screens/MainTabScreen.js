import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import Icon from 'react-native-vector-icons/MaterialIcons';
import { createMaterialBottomTabNavigator } from '@react-navigation/material-bottom-tabs';
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';


import HomeScreen from './HomeScreen';
import ReservasScreen from './ReservasScreen';
import DefinicoesScreen from './DefinicoesScreen';
import ContaScreen from './ContaScreen';

const HomeStack = createStackNavigator();
const ReservasStack = createStackNavigator();
const DefinicoesStack = createStackNavigator();
const ContaStack = createStackNavigator();


const Tab = createMaterialBottomTabNavigator();

const MainTabScreen = () => (
    <Tab.Navigator initialRouteName="Home" activeColor="#fff">
      <Tab.Screen name="Home" component={HomeStackScreen} options={{
          tabBarLabel: 'Home',
          tabBarColor: '#009387',
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons name="home" color={color} size={26} />
          ),
        }}
      />
      <Tab.Screen name="Reservas" component={ReservasStackScreen} options={{
          tabBarLabel: 'Reservas',
           tabBarColor: '#1bb3f5',
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons name="book" color={color} size={26} />
          ),
        }}
      />
      <Tab.Screen name="Conta" component={ContaStackScreen} options={{
          tabBarLabel: 'Conta',
           tabBarColor: '#940025',
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons name="account" color={color} size={26} />
          ),
        }}
      />
      <Tab.Screen name="Definições" component={DefinicoesStackScreen} options={{
          tabBarLabel: 'Definições',
           tabBarColor: '#686869',
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons name="settings" color={color} size={26} />
          ),
        }}
      />
    </Tab.Navigator>
);

export default MainTabScreen;

const HomeStackScreen = ({navigation}) => (
  <HomeStack.Navigator screenOptions={{
        headerStyle: {
          backgroundColor: '#009387',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold'
        }
      }}>
        <HomeStack.Screen name="Home" component={HomeScreen} options={{
          headerLeft: () => (
            <Icon.Button name="menu" backgroundColor='#009387' onPress={() => navigation.openDrawer()} />
          )
        }} />
  </HomeStack.Navigator>
);

const ReservasStackScreen = ({navigation}) => (
  <ReservasStack.Navigator screenOptions={{
        headerStyle: {
          backgroundColor: '#1bb3f5',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold'
        }
      }}>
        <ReservasStack.Screen name="Reservas" component={ReservasScreen} options={{
          headerLeft: () => (
          <Icon.Button name="menu" backgroundColor='#1bb3f5' onPress={() => navigation.openDrawer()} />
          )
        }} />
  </ReservasStack.Navigator>
);

const ContaStackScreen = ({navigation}) => (
  <ContaStack.Navigator screenOptions={{
        headerStyle: {
          backgroundColor: '#940025',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold'
        }
      }}>
        <ContaStack.Screen name="Conta" component={ContaScreen} options={{
          headerLeft: () => (
          <Icon.Button name="menu" backgroundColor='#940025' onPress={() => navigation.openDrawer()} />
          )
        }} />
  </ContaStack.Navigator>
);

const DefinicoesStackScreen = ({navigation}) => (
  <DefinicoesStack.Navigator screenOptions={{
        headerStyle: {
          backgroundColor: '#686869',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold'
        }
      }}>
        <DefinicoesStack.Screen name="Definições" component={DefinicoesScreen} options={{
          headerLeft: () => (
          <Icon.Button name="menu" backgroundColor='#686869' onPress={() => navigation.openDrawer()} />
          )
        }} />
  </DefinicoesStack.Navigator>
);


