import React from 'react';
import {StyleSheet, View, Text, Button} from 'react-native';

const ReservasScreen = ({navigation}) => {
  return (
    <View style={styles.container}>
      <Text>Reservas</Text>
      <Button title="Ir para Reservas...Novamente" onPress={() => navigation.push("Details")} />
      <Button title="Ir para Home" onPress={() => navigation.navigate("Home")} />
      <Button title="Voltar" onPress={() => navigation.goBack()} />
      <Button title="Voltar para ecrã anterior" onPress={() => navigation.popToTop()} />
    </View>
  );
};

export default ReservasScreen;

const styles = StyleSheet.create({
    container: {
        flex:1, 
        alignItems: 'center', 
        justifyContent: 'center'
    },
});