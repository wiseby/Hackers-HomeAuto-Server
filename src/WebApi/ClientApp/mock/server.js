/* eslint-disable @typescript-eslint/no-var-requires */
const jsonServer = require('json-server');
const node = require('./node');

const server = jsonServer.create();
const middlewares = jsonServer.defaults();
const port = 3000;

server.use(middlewares);
server.listen(port, () => {
  console.log('JSON Server is running on port ', port);
});

server.get('/api/nodes', ({ query, body }, res) => {
  console.log('query: ', query);
  console.log('body: ', body);

  const data = {
    data: node.nodes(),
  };

  res.json(data);
});

server.get('/api/nodes/:clientId', ({ query, body, params }, res) => {
  console.log('query: ', query);
  console.log('body: ', body);
  console.log('params: ', params);

  const data = {
    data: node
      .nodesWithReadings()
      .filter((node) => node.id === params['clientId']),
  };

  res.json(data);
});
