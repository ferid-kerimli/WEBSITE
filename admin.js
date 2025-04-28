const express = require('express');
const path = require('path');
const app = express();
console.log('▶ Starting admin.js');
app.use(
  '/downtown/images/adminpanelimages',
  express.static(
    path.join(__dirname, 'downtown', 'images', 'adminpanelimages')
  )
);

app.get('/login.html', (req, res) => {
  res.sendFile(path.join(__dirname,'login.html'), err => {
    if (err) console.error('sendFile error:', err);
    else    console.log('login.html sent ✔️');
  });
});
app.get('/mainpage.html', (req, res) => {
  res.sendFile(path.join(__dirname,'downtown','adminpanel','mainpage.html'), err => {
    if (err) console.error('sendFile error:', err);
    else    console.log('mainpage.html sent ✔️');
  });
});
app.post('/login', (req, res) => {
  return res.redirect('/mainpage.html');
}); 
const PORT = process.env.PORT || 4000;
app.listen(PORT, () => console.log(`✅ listening on :${PORT}`));
