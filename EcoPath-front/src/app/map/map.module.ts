import {NgModule} from "@angular/core";
import {MapComponent} from "./map.component";
import {NzLayoutModule} from "ng-zorro-antd/layout";
import {RouteManagerModule} from "./route-manager/route-manager.module";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [MapComponent],
  imports: [
    NzLayoutModule,
    RouteManagerModule,
    CommonModule
  ],
  exports: [MapComponent]
})
export class MapModule {

}
