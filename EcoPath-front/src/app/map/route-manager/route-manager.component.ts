import {Component, EventEmitter, Input, Output} from "@angular/core";
import {RouteManagerService} from "./route_manager.service";
import {take} from "rxjs/operators";

@Component({
  selector: 'app-route-manager',
  templateUrl: 'route-manager.component.html',
  styleUrls: ['route-manager.component.scss'],
  providers: [RouteManagerService]
})
export class RouteManagerComponent {
  @Input() public from: number[] = [];
  @Input() public to: number[] = [];
  public transportType: number = 0;

  @Output() onGetRoute: EventEmitter<any> = new EventEmitter<any>();

  constructor(private routeManagerService: RouteManagerService) {
  }

  public onSubmit(): void {
    this.routeManagerService.getRoute({
      from: this.from,
      to: this.to,
      transportType: this.transportType
    }).pipe(take(1)).subscribe(
      (params) => {
        console.log(params);
        this.onGetRoute.emit(params);
      },
      (err) => console.log('fuck', err),
    );
  }

  public onChangeType(id: number): void {
    this.transportType = id;
  }
}
