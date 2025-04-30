const express = require('express');
const path = require('path');
const app = express();
console.log('▶ Starting admin.js');
app.use(express.urlencoded({ extended: false }));
app.use(
  '/',
  express.static(__dirname)
);
app.use(
  '/downtown/images/adminpanelimages',
  express.static(
    path.join(__dirname, '..', 'images', 'adminpanelimages')
  )
);
app.get('/login.html', (req, res) => {
  res.sendFile(path.join(__dirname,'login.html'), err => {
    if (err) console.error('sendFile error:', err);
    else    console.log('login.html sent ✔️');
  });
});

app.post('/login', (req, res) => {
   res.redirect('/mainpage.html');
});
const PORT = process.env.PORT || 4000;
app.listen(PORT, () => console.log(`✅ listening on :${PORT}`));
