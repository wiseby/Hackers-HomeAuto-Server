import { Reading } from './Reading';
import { ReadingDefinition } from './ReadingDefinition';

export class NodeDevice {
  clientId: string;
  location: string;
  isConfigured: boolean;
  createdAt: Date;
  readingDefinitions: ReadingDefinition[];
  latestReading: Reading;
  readings: Reading[];
}
