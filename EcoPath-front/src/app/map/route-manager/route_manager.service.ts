import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable()
export class RouteManagerService {

  constructor(private http: HttpClient) {
  }

  public getRoute(model: any): Observable<any> {
    return this.http.post('http://192.168.50.140:80/api/RouteBuilder', model);
  }
}
