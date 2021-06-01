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
  node.nodesWithLatestReading().map((n) => {
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

  console.log('clientId param: ', params['clientId']);

  const nodes = node.nodesWithReadings();
  nodes.forEach((node) => console.log(node.clientId));
  let nodeMatch = nodes.filter((node) => node.clientId === params['clientId']);
  console.log('clientId match: ', nodeMatch);

  nodeMatch[0].readings = nodeMatch[0].readings.sort((a, b) => a - b);

  res.json({ data: nodeMatch[0] });
});

server.get(
  '/api/v1/nodes/:clientId/readings',
  ({ query, body, params }, res) => {
    console.log('query: ', query);
    console.log('body: ', body);
    console.log('params: ', params);

    console.log('clientId param: ', params['clientId']);
    const readings = [];
    const nodes = node.nodesWithReadings();
    let nodeMatch = nodes.filter(
      (node) => node.clientId === params['clientId'],
    )[0];

    nodeMatch.readings.forEach((node) => {
      const reading = {
        clientId: node.clientId,
        readingDefinitions: node.readingDefinitions,
        readings: node.readings,
        createdAt: node.createdAt,
      };
      readings.push(reading);
    });
    console.log('clientId match: ', nodeMatch);

    res.json({ data: readings });
  },
);
