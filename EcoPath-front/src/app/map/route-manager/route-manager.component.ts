import {Component} from "@angular/core";
import {RouteManagerService} from "./route_manager.service";
import {RouteModel} from "./route-manager.type";
import {take} from "rxjs/operators";

@Component({
  selector: 'app-route-manager',
  templateUrl: 'route-manager.component.html',
  styleUrls: ['route-manager.component.scss'],
  providers: [RouteManagerService]
})
export class RouteManagerComponent {
  public routeModel: RouteModel = {
    from: [],
    to: [],
  }
  public from: string = '';
  public to: string = '';

  constructor(private routeManagerService: RouteManagerService) {
  }

  public onSubmit(): void {
    this.routeManagerService.getRoute(this.routeModel).pipe(take(1)).subscribe(
      () => console.log('good'),
      (err) => console.log('fuck', err),
    );
  }
}
