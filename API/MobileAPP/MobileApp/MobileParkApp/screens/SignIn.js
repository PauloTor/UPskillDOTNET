import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const SignInScreen = () => {
    return (
        <View style={styles.container}>
            <Text>SignIn</Text>
            <Button title="Click me" onPress={() => alert("Olá")} />
        </View>
    );
};

export default SignInScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});