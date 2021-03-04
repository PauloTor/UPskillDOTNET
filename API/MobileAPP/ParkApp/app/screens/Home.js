import React from 'react';
import styled from 'styled-components/native';

import AuthContext from '../AuthContext';

const Container = styled.View``;
const Text = styled.Text``;

function Home() {
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
  const {signOut} = React.useContext(AuthContext);

  return (
    <Container>
      <Text>Home</Text>
    </Container>
  );
}

export default Home;