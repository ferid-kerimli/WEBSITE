const express = require('express');
const path = require('path');
const app = express();
console.log('▶ Starting admin.js');
app.get('/login.html', (req, res) => {
  console.log('Got request for /login.html, headers:', req.headers);

  res.sendFile(path.join(__dirname,'login.html'), err => {
    if (err) console.error('sendFile error:', err);
    else    console.log('login.html sent ✔️');
  });
});
const PORT = process.env.PORT || 4000;
app.listen(PORT, () => console.log(`✅ listening on :${PORT}`));
