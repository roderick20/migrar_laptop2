const axios = require('axios');



// async function getEmpresaData() {

//     const token = 'apis-token-4638.pW3wKHXmNDVS1eu9gsFEdLMiKFxBNptd';
//     const ruc = '10460278975';
    
//     const apiUrl = `https://api.apis.net.pe/v2/sunat/ruc?numero=${ruc}`;
    
//     const headers = {
//       'Referer': 'http://apis.net.pe/api-ruc',
//       'Authorization': `Bearer ${token}`
//     };

//   try {
//     const response = await axios.get(apiUrl, { headers });
//     const empresa = response.data;
//     console.log(empresa);
//   } catch (error) {
//     console.error(error);
//   }
// }

// getEmpresaData();


async function getPersonaData() {

    const token = 'apis-token-4638.pW3wKHXmNDVS1eu9gsFEdLMiKFxBNptd';
    const ruc = '41544650';
    
    const apiUrl = `https://api.apis.net.pe/v2/reniec/dni?numero=${ruc}`;
    
    const headers = {
      'Referer': 'http://apis.net.pe/api-ruc',
      'Authorization': `Bearer ${token}`
    };

  try {
    const response = await axios.get(apiUrl, { headers });
    const empresa = response.data;
    console.log(empresa);
  } catch (error) {
    console.error(error);
  }
}

getPersonaData();