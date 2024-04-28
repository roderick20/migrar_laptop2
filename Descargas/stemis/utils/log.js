const fs = require('fs');

function logError(archive, error) {
  const logFilePath = 'log.txt';

  const formattedData = `Fecha: ${new Date().toLocaleString()}, Archivo: ${archive.padEnd(20)}, Error: ${error}  \n`;


  fs.appendFile(logFilePath, formattedData, (err) => {
    if (err) {
      console.error('Error writing to log file:', err);
    } else {
      console.log('Log data has been written to the file:', logFilePath);
    }
  });
}

module.exports = {
  logError
};