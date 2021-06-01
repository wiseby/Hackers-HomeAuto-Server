import { Injectable } from '@angular/core';
import { JsonPatch } from '@core/models/JsonPatch';
import { NodeDevice } from '@core/models/NodeDevice';
import { ReadingDefinition } from '@core/models/ReadingDefinition';
import {
  faDoorOpen,
  faMapMarkedAlt,
  faQuestionCircle,
  faThermometerHalf,
  faWater,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpclientService } from './httpclient.service';

@Injectable({
  providedIn: 'root',
})
export class DefinitionService {
  public defaultIcon = faQuestionCircle;
  public availableIcons: IconDefinition[] = [
    faQuestionCircle,
    faThermometerHalf,
    faWater,
    faDoorOpen,
    faMapMarkedAlt,
  ];

  constructor(private httpService: HttpclientService) {}

  public getDefinitions(node: NodeDevice): ReadingDefinition[] {
    const definitions: ReadingDefinition[] = [];
    for (const [key] of Object.entries(node.latestReading.values)) {
      const definition = node.readingDefinitions.find(
        (def) => def.name === key,
      );
      if (definition !== undefined) {
        definitions.push(definition);
      } else {
        const definition: ReadingDefinition = new ReadingDefinition();
        definition.name = key;
        definitions.push(definition);
      }
    }
    return definitions;
  }

  public updateDefinitions(
    clientId: string,
    definitions: ReadingDefinition[],
  ): Observable<ReadingDefinition[]> {
    return this.httpService
      .update<ReadingDefinition[]>(`nodes/${clientId}/definitions`, definitions)
      .pipe(
        map((response: JsonPatch<ReadingDefinition[]>) => {
          return response.data;
        }),
      );
  }
}
