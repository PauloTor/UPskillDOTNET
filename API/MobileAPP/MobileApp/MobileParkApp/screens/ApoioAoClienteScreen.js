import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const ApoioAoClienteScreen = ({navigation}) => {
    return (
        <View style={styles.container}>
            <Text>Bem-vindo ao Apoio ao Cliente!</Text>
            <Button title="Click Here" onPress={() => alert("Button Clicked")} />
        </View>
    );
};

export default ApoioAoClienteScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});