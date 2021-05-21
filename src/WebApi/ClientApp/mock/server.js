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

server.get('/api/v1/nodes', ({ query, body }, res) => {
  console.log('query: ', query);
  console.log('body: ', body);

  let nodes = [];
  node.nodesWithSinleReading().map((n) => {
    if (n.readings.length > 0) {
      n.latestReading = n.readings[0];
    }
    nodes.push(n);
  });

  const data = {
    data: nodes,
  };

  res.json(data);
});

server.get('/api/v1/nodes/:clientId', ({ query, body, params }, res) => {
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
