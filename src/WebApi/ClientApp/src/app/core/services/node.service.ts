import { Injectable } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpclientService } from './httpclient.service';
import { Response } from '@core/models/Response';
import { ReadingDefinition } from '@core/models/ReadingDefinition';
import { Reading } from '@core/models/Reading';

@Injectable({
  providedIn: 'root',
})
export class NodeService {
  constructor(private httpService: HttpclientService) {}

  public getAllNodes(): Observable<NodeDevice[]> {
    return this.httpService.get<Response<NodeDevice[]>>('nodes').pipe(
      map((response: Response<NodeDevice[]>) => {
        return response.data;
      }),
    );
  }

  public getSingleNode(clientId: string): Observable<NodeDevice> {
    return this.httpService.get<Response<NodeDevice>>(`nodes/${clientId}`).pipe(
      map((response: Response<NodeDevice>) => {
        return response.data;
      }),
    );
  }

  public getNodeDefinitions(clientId: string): Observable<ReadingDefinition[]> {
    return this.httpService
      .get<Response<ReadingDefinition[]>>(`nodes/${clientId}/definitions`)
      .pipe(
        map((response: Response<ReadingDefinition[]>) => {
          return response.data;
        }),
      );
  }

  public getReadings(clientId: string): Observable<Reading[]> {
    return this.httpService
      .get<Response<Reading[]>>(`nodes/${clientId}/readings`)
      .pipe(
        map((response: Response<Reading[]>) => {
          return response.data;
        }),
      );
  }
}
