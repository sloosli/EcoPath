import {NgModule} from "@angular/core";
import {RouteManagerComponent} from "./route-manager.component";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [RouteManagerComponent],
  imports: [
    CommonModule
  ],
  exports: [RouteManagerComponent]
})
export class RouteManagerModule {
}
