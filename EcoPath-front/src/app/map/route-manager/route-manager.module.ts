import {NgModule} from "@angular/core";
import {RouteManagerComponent} from "./route-manager.component";
import {NzFormModule} from "ng-zorro-antd/form";
import {NzInputModule} from "ng-zorro-antd/input";

@NgModule({
  declarations: [RouteManagerComponent],
  imports: [
    NzFormModule,
    NzInputModule
  ],
  exports: [RouteManagerComponent]
})
export class RouteManagerModule {
}
