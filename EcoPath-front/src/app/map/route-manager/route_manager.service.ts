import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable()
export class RouteManagerService {
  private api: string = 'http://192.168.50.50'
  private controller: string = `${this.api}/RouteBuilder`

  constructor(private http: HttpClient) {
  }

  public getRoute(model: any): Observable<any> {
    return this.http.post(this.controller, model);
  }
}
