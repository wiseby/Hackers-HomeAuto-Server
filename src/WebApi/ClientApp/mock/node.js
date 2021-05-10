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
    id: 'be4ac492-7d38-48e6-a088-3ccbc96c8570',
    location: 1,
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
    id: '5b7ac0f9-35e5-4fc0-bc1e-60311e573d3f',
    location: 4,
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(19),
      },
    ],
  },
  {
    id: '24958885-de5a-4b10-beae-a910a1f90316',
    location: 3,
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: 22.1,
      },
      {
        device: 'video',
        dataType: 'binary-stream',
        value: 1,
      },
    ],
  },
  {
    id: 'b048df7d-c771-4d5a-9180-ae1c9e936f79',
    location: 3,
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(22),
      },
    ],
  },
  {
    id: 'bf1c0496-2bfd-470b-bc94-dc77419f694f',
    location: 5,
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(23),
      },
      {
        device: 'video',
        dataType: 'binary-stream',
        value: 1,
      },
    ],
  },
  {
    id: '644b6f02-dec4-40da-9dba-514494295a42',
    location: 6,
    readings: [
      {
        device: 'temperature',
        dataType: 'celcius',
        value: getRandomTemp(16),
      },
      {
        device: 'video',
        dataType: 'binary-stream',
        value: 1,
      },
    ],
  },
  {
    id: '3f2cdc2f-f26c-4f48-8b93-e1c315c18f21',
    location: 7,
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
    id: 'd8054d6c-fec9-4712-a4cc-d173de11cadc',
    location: 8,
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

const locations = [
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
    id: 6,
    name: 'garage',
  },
  {
    id: 7,
    name: 'workshop',
  },
  {
    id: 8,
    name: 'bedroom',
  },
];

module.exports = {
  nodes: () => nodes(),
  locations: () => locations,
};
