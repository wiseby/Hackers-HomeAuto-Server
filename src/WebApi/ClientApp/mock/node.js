const getRandomTemp = (celcius) => {
  return celcius + Math.ceil(Math.random() * 10) * 0.1;
};

const getRandomHumidity = (humidity, range) => {
  const goUp = Math.ceil(Math.random() * 10) === 1 ? true : false;

  if (goUp) {
    return humidity + Math.ceil(Math.random() * (100 / range));
  }
  return humidity - Math.ceil(Math.random() * (100 / range));
};

const nodes = () => [
  {
    id: 'livingRoomTempSensor',
    location: 'livingroom',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(21),
      },
      {
        device: 'video',
        dataType: 'binary-stream',
        value: 1,
      },
    ],
  },
  {
    id: 'kitchenTempSensor',
    location: 'kitchen',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(19),
      },
    ],
  },
  {
    id: 'dininigroomWindow',
    location: 'diningroom',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: 22.1,
      },
    ],
  },
  {
    id: 'diningroomCeiling',
    location: 'diningroom',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(22),
      },
    ],
  },
  {
    id: 'officeTempSensor',
    location: 'office',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(23),
      },
    ],
  },
  {
    id: 'garageDHT',
    location: 'garage',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(16),
      },
      {
        device: 'humidity',
        dataType: 'percent',
        value: getRandomHumidity(65),
      },
    ],
  },
  {
    id: 'workshopDHT',
    location: 'workshop',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(18),
      },
      {
        device: 'humidity',
        dataType: 'percent',
        value: getRandomHumidity(34, 10),
      },
    ],
  },
  {
    id: 'bedroomDHT',
    location: 'bedroom',
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(21),
      },
      {
        device: 'humidity',
        dataType: 'percent',
        value: getRandomHumidity(78, 20),
      },
    ],
  },
];

const pendingNodes = () => [
  {
    id: 'hallwayEntranceDht',
    values: [
      {
        name: 'temperature',
        value: getRandomTemp(18),
      },
      {
        name: 'humidity',
        value: getRandomHumidity(45),
      },
    ],
  },
];

const locations = () => [
  {
    id: 1,
    name: 'livingroom',
  },
  {
    id: 2,
    name: 'kitchen',
  },
  {
    id: 3,
    name: 'diningroom',
  },
  {
    id: 4,
    name: 'office',
  },
  {
    id: 5,
    name: 'garage',
  },
  {
    id: 6,
    name: 'workshop',
  },
  {
    id: 7,
    name: 'bedroom',
  },
];

module.exports = {
  nodes: () => nodes(),
  locations: () => locations,
  pendingNodes: () => pendingNodes,
};
