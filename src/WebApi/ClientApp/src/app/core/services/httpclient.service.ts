import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpclientService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) {}

  private readonly baseVersionUrl = `${this.baseUrl}/api/v1`;

  httpOptions = {
    header: new HttpHeaders({
      'Content-Type': 'application/vnd.api+json',
    }),
  };

  public get<T>(resource: string, queryParams?: string): Observable<T> {
    const options = queryParams
      ? {
          header: this.httpOptions.header,
          params: new HttpParams({ fromString: queryParams }),
        }
      : {};

    return this.http.get<T>(`${this.baseVersionUrl}/${resource}`, options);
  }
}
