import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const ContaScreen = ({navigation}) => {
    return (
        <View style={styles.container}>
            <Text>Conta</Text>
            <Button title="Click Here" onPress={() => alert("Button Clicked")} />
        </View>
    );
};

export default ContaScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});