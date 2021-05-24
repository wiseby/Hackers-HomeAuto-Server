import { Injectable } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpclientService } from './httpclient.service';
import { Response } from '@core/models/Response';

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
}
