import React from 'react';
import {useState, useEffect } from 'react';
import styled from 'styled-components/native';

import AuthContext from '../AuthContext';

const Container = styled.View``;
const Text = styled.Text``;

const TochableOpacity = styled.TouchableOpacity`
  background: #2ec4b6;
  align-items: center;
  justify-content: center;
  margin: 16px 0;
  height: 40px;
  border-radius: 3px;
`;

const ButtonText = styled.Text`
  font-size: 16px;
  color: #fdfffc;
`;

function Profile() {
  const {user, setUser} = useState(null);
  const {loading, setLoading} = useState(true);
  const {error, setError} = useState(null);

  const {userToken, signOut} = React.useContext(AuthContext);

  useEffect(() => {
    async function loadUser() {
      const response = await fetch ('http://192.168.1.70:80/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if(!reponse.ok) {
        setError('Error loading profile');
      }

      const data = await response.json();
      setUser(data);
      setLoading(false)
    }
    loadUser();
  }, [userToken]);

  if (error) {
    return <Text>{error}</Text>;
  }

  if (loading) {
    return <Text>Loading...</Text>;
  }

  return (
    <Container>
      <Text>{user.username}</Text>
      <Text>{user.email}</Text>
      <TochableOpacity onPress={() => signOut()}>
        <ButtonText>Logout</ButtonText>
      </TochableOpacity>
    </Container>
  );
}

export default Profile;