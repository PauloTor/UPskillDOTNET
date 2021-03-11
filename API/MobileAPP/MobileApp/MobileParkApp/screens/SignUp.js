import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const SignUpScreen = () => {
    return (
        <View style={styles.container}>
            <Text>SignUp</Text>
            <Button title="Click me" onPress={() => alert("Olá")} />
        </View>
    );
};

export default SignUpScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});