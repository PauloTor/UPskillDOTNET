import React from 'react';
import {StyleSheet, View} from 'react-native';
import {DrawerContentScrollView, DrawerItem} from '@react-navigation/drawer';
import {Avatar, Title, Caption, Paragraph, Drawer, Text, TouchableRipple, Switch} from 'react-native-paper';
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import Feather from 'react-native-vector-icons/Feather';

export function DrawerContent(props) {

    const [isDarkTheme, setIsDarkTheme] = React.useState(false);
    const toggleTheme = () => {
        setIsDarkTheme(!isDarkTheme);
    }
    return (
        <View style={{flex:1}}>
            <DrawerContentScrollView {...props}>
                <View style={styles.drawerContent}>
                    <View style={styles.userInfoSection}>
                        <View style={{flexDirection:'row',marginTop: 15}}>
                            <Avatar.Image source={require('../assets/Eu.png')} size={50} />
                            <View style={{marginLeft:15, flexDirection: 'column'}}>
                                <Title style={styles.title}>Victor Duarte</Title>
                                <Caption style={styles.caption}>@Duarterafc</Caption>
                            </View>
                        </View>
                    </View>
                    <Drawer.Section style={styles.drawerSection}>
                        <DrawerItem icon={({color, size}) => (
                            <MaterialCommunityIcons
                                name="home-outline"
                                color={color}
                                size={size}
                                />
                        )} 
                                label="Home"
                                onPress={() => {props.navigation.navigate("Home")}} />
                        <DrawerItem icon={({color, size}) => (
                            <MaterialCommunityIcons
                                name="account-outline"
                                color={color}
                                size={size}
                                />
                        )} 
                                label="Conta"
                                onPress={() => {props.navigation.navigate("Conta")}} />
                        <DrawerItem icon={({color, size}) => (
                            <MaterialCommunityIcons
                                name="bookmark-outline"
                                color={color}
                                size={size}
                                />
                        )} 
                                label="Reservas"
                                onPress={() => {props.navigation.navigate("Reservas")}} />
                        <DrawerItem icon={({color, size}) => (
                            <Feather
                                name="settings"
                                color={color}
                                size={size}
                                />
                        )} 
                                label="Definições"
                                onPress={() => {props.navigation.navigate("Definições")}} />
                        <DrawerItem icon={({color, size}) => (
                            <MaterialCommunityIcons
                                name="face-agent"
                                color={color}
                                size={size}
                                />
                        )} 
                                label="Apoio ao Cliente"
                                onPress={() => {props.navigation.navigate("Apoio ao Cliente")}} />
                    </Drawer.Section>
                    <Drawer.Section title="Preferências">
                        <TouchableRipple onPress={() => {toggleTheme()}}>
                            <View style={styles.preference}>
                                <Text>Modo Escuro</Text>
                                <View pointerEvents="none">
                                     <Switch value={isDarkTheme} />
                                </View>
                            </View>
                        </TouchableRipple>
                    </Drawer.Section>
                </View>
            </DrawerContentScrollView>
            <Drawer.Section style={styles.bottomDrawerSection}>
                <DrawerItem icon={({color, size}) => (
                    <MaterialCommunityIcons
                    name="exit-to-app"
                    color={color}
                    size={size}
                    />
                )} 
                    label="Sign Out"
                    onPress={() => {}} />
            </Drawer.Section>
        </View>
    );
}

const styles = StyleSheet.create({
    drawerContent: {
        flex: 1,
    },
    userInfoSection: {
        paddingLeft: 20,
    },
    title: {
        fontSize: 16,
        marginTop: 3,
        fontWeight: 'bold',
    },
    caption: {
        fontSize: 14,
        lineHeight: 14,
    },
    row: {
        marginTop: 20,
        flexDirection: 'row',
        alignItems: 'center',
    },
    section: {
        flexDirection: 'row',
        alignItems: 'center',
        marginRight: 15,
    },
    paragraph: {
        fontWeight: 'bold',
        marginRight: 3,
    },
    drawerSection: {
        marginTop: 15,
    },
    bottomDrawerSection: {
        marginBottom: 15,
        borderTopColor: '#f4f4f4',
        borderTopWidth: 1,
    },
    preference: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingVertical: 12,
        paddingHorizontal: 16,
    },
});