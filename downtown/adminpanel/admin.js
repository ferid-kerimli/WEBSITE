const express = require('express');
const path = require('path');
const session = require('express-session');

const app = express();
console.log('▶ Starting admin.js');

app.use(session({
  secret: 'pick-a-very-secret-string',
  resave:   false,
  saveUninitialized: false
}));

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
app.get('/login.html', (req, res, next) => {
  if (req.session.authenticated) {
    return res.redirect('/mainpage.html');
  }
  next();    
});

app.post('/login', (req, res) => {
  const {username,password}=req.body;
  const ALLOWED_USER = 'kerim';
  const ALLOWED_PASS = 'kerim123';

  if (username === ALLOWED_USER && password === ALLOWED_PASS) {
    req.session.authenticated = true;
    return res.redirect('/mainpage.html');
  }
  res.redirect('/login.html?error=1');
});
app.get('/mainpage.html', (req, res, next) => {
  if (!req.session.authenticated) {
    return res.redirect('/login.html');
  }
  next();
});
const PORT = process.env.PORT || 4000;
app.listen(PORT, () => console.log(`✅ listening on :${PORT}`));
