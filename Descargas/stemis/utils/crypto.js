import crypto from 'crypto';

const key = 'xcv54xc6v5zxsefrxcv54xc6v5zxsefr';//longitud 32

// Función para encriptar
function encrypt(text) {
    const iv = crypto.randomBytes(16);
    const cipher = crypto.createCipheriv('aes-256-cbc', key, iv);
    let encrypted = cipher.update(text, 'utf8', 'hex');
    encrypted += cipher.final('hex');
    return iv.toString('hex') + encrypted;
  }
  
  // Función para desencriptar
  function decrypt(encryptedText) {
    const iv = Buffer.from(encryptedText.substr(0, 32), 'hex');
    const decipher = crypto.createDecipheriv('aes-256-cbc', key, iv);
    let decrypted = decipher.update(encryptedText.substr(32), 'hex', 'utf8');
    decrypted += decipher.final('utf8');
    return decrypted;
  }

export  {
    encrypt,
    decrypt
}