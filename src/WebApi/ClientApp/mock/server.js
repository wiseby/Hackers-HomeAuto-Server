const jsonServer = require('json-server');
const server = jsonServer.create();
const middlewares = jsonServer.defaults();
const port = 3000;

server.use(middlewares);
server.use(jsonServer.bodyParser);

server.listen(port, () => {
  console.log('JSON server is running');
});

server.get('/api/nodes', ({query, body}, res) => {
  console.log('query: ', query);
  console.log('body: ', body);
  res.send('Hello World!');
});