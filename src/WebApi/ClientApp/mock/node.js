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

const getRandomDateTime = (start, end, startHour, endHour) => {
  var date = new Date(+start + Math.random() * (end - start));
  var hour = (startHour + Math.random() * (endHour - startHour)) | 0;
  date.setHours(hour);
  return date;
};

const generateNodeWithReadings = (
  id,
  location,
  isConfigured,
  readingsLenth,
  humidity,
  temperature,
  startDate,
  endDate,
) => {
  let node = {
    clientId: id,
    isConfigured: isConfigured,
    location: location,
    readingDefinitions: [
      {
        name: 'temperature',
        dataType: 'celcius',
        icon: 'Temperature',
      },
      {
        name: 'humidity',
        dataType: 'percent',
        icon: 'Humidity',
      },
    ],
    readings: [],
    latestReading: {
      values: {
        temperature: getRandomTemp(temperature).toString(),
        humidity: getRandomHumidity(
          humidity.percent,
          humidity.interval,
        ).toString(),
      },
      createdAt: new Date(),
    },
  };

  for (let i = 0; i < readingsLenth; i++) {
    let reading = {
      values: {
        temperature: getRandomTemp(temperature).toString(),
        humidity: getRandomHumidity(
          humidity.percent,
          humidity.interval,
        ).toString(),
      },
      createdAt: getRandomDateTime(startDate, endDate, 0, 23),
    };
    node.readings.push(reading);
  }
  return node;
};

const readingDefDefault = [
  {
    name: 'temperature',
    dataType: 'celcius',
    icon: 'Temperature',
  },
  {
    name: 'humidity',
    dataType: 'percent',
    icon: 'Humidity',
  },
  {
    name: 'doorStatus',
    dataType: 'onoff',
    icon: 'OpenClose',
  },
];

const nodes = () => [
  {
    clientId: 'livingRoomTempSensor',
    location: 'livingroom',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'kitchenTempSensor',
    location: 'kitchen',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'dininigroomWindow',
    location: 'diningroom',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'diningroomCeiling',
    location: 'diningroom',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'officeTempSensor',
    location: 'office',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'garageDHT',
    location: 'garage',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'workshopDHT',
    location: 'workshop',
    readingDefinitions: readingDefDefault,
  },
  {
    clientId: 'bedroomDHT',
    location: 'bedroom',
    readingDefinitions: readingDefDefault,
  },
];

const nodesWithLatestReading = () => [
  generateNodeWithReadings(
    'HallwayDHT',
    'MainHallWay',
    false,
    0,
    { percent: 67, interval: 5 },
    19,
    new Date(),
    new Date('2021/03/15'),
  ),
  generateNodeWithReadings(
    'kitchenTempSensor',
    'kitchen',
    false,
    0,
    { percent: 70, interval: 10 },
    21,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'dininigroomWindow',
    'diningroom',
    false,
    0,
    { percent: 79, interval: 15 },
    16,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'diningroomCeiling',
    'diningroom',
    true,
    0,
    { percent: 79, interval: 15 },
    22,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'officeTempSensor',
    'office',
    true,
    0,
    { percent: 79, interval: 15 },
    20,
    new Date(),
    new Date('2021/04/01'),
  ),
  generateNodeWithReadings(
    'garageDHT',
    null,
    false,
    0,
    { percent: 49, interval: 5 },
    16,
    new Date(),
    new Date('2021/04/01'),
  ),
  generateNodeWithReadings(
    'workshopDHT',
    null,
    false,
    0,
    { percent: 39, interval: 5 },
    16,
    new Date(),
    new Date('2021/03/15'),
  ),
  generateNodeWithReadings(
    'bedroomDHT',
    'bedroom',
    true,
    0,
    { percent: 78, interval: 20 },
    21,
    new Date(),
    new Date('2021/03/15'),
  ),
  {
    clientId: 'terrace',
    isConfigured: true,
    location: 'outdoor',
    readingDefinitions: readingDefDefault,
    latestReading: {
      values: {
        temperature: getRandomTemp(21),
        humidity: getRandomHumidity(67, 5),
        doorStatus: 'open',
      },
      createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
    },
    readings: [],
  },
];

const nodesWithReadings = () => [
  generateNodeWithReadings(
    'HallwayDHT',
    'MainHallWay',
    false,
    20,
    { percent: 67, interval: 5 },
    19,
    new Date(),
    new Date('2021/03/15'),
  ),
  generateNodeWithReadings(
    'kitchenTempSensor',
    'kitchen',
    false,
    40,
    { percent: 70, interval: 10 },
    21,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'dininigroomWindow',
    'diningroom',
    false,
    40,
    { percent: 79, interval: 15 },
    16,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'diningroomCeiling',
    'diningroom',
    true,
    20,
    { percent: 79, interval: 15 },
    22,
    new Date(),
    new Date('2021/02/01'),
  ),
  generateNodeWithReadings(
    'officeTempSensor',
    'office',
    true,
    80,
    { percent: 79, interval: 15 },
    20,
    new Date(),
    new Date('2021/04/01'),
  ),
  generateNodeWithReadings(
    'garageDHT',
    null,
    false,
    4,
    { percent: 49, interval: 5 },
    20,
    new Date(),
    new Date('2021/04/01'),
  ),
  generateNodeWithReadings(
    'workshopDHT',
    null,
    false,
    43,
    { percent: 39, interval: 5 },
    16,
    new Date(),
    new Date('2021/03/15'),
  ),
  generateNodeWithReadings(
    'bedroomDHT',
    'bedroom',
    true,
    20,
    { percent: 78, interval: 20 },
    21,
    new Date(),
    new Date('2021/03/15'),
  ),
  {
    clientId: 'livingRoomTempSensor',
    isConfigured: true,
    location: 'livingroom',
    readingDefinitions: [
      {
        name: 'temperature',
        dataType: 'celcius',
      },
      {
        name: 'humidity',
        dataType: 'percent',
      },
    ],
    readings: [
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
      },
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
      },
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
      },
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
      },
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
      },
      {
        values: {
          temperature: getRandomTemp(21),
          humidity: getRandomHumidity(67, 5),
        },
        createdAt: getRandomDateTime(new Date(), new Date('2021/02/01'), 0, 23),
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
  nodesWithReadings: () => nodesWithReadings(),
  nodesWithLatestReading: () => nodesWithLatestReading(),
  locations: () => locations,
};
