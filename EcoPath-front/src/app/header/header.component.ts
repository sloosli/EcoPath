import {Component, EventEmitter, Output} from "@angular/core";
import {BehaviorSubject} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {take} from "rxjs/operators";

@Component({
  selector: 'app-header',
  templateUrl: 'header.component.html',
  styleUrls: ['header.component.scss']
})
export class HeaderComponent {
  public readonly isCreateEcoPathActive: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  public readonly isEcoZoneActive: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isRouteManager: boolean = true;

  @Output() onGetZone = new EventEmitter();

  constructor(private http: HttpClient) {
  }

  public onClickCreateEcoPath(): void {
    this.deselectTabs();
    this.isCreateEcoPathActive.next(true);
    this.isRouteManager = true;
  }

  public onClickEcoZone(): void {
    this.deselectTabs();
    this.isEcoZoneActive.next(true);
    this.isRouteManager = false;
    this.http.get(`http://192.168.50.140:80/api/AirZones`).pipe(take(1)).subscribe((params) => this.onGetZone.emit(params))
  }

  private deselectTabs(): void {
    this.isCreateEcoPathActive.next(false);
    this.isEcoZoneActive.next(false);
  }

}
