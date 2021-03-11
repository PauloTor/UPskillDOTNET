import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const DefinicoesScreen = ({navigation}) => {
    return (
        <View style={styles.container}>
            <Text>Definições Screen</Text>
            <Button title="Click Here" onPress={() => alert("Button Clicked")} />
        </View>
    );
};

export default DefinicoesScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});