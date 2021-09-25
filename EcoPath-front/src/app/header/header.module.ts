import {NgModule} from "@angular/core";
import {HeaderComponent} from "./header.component";
import {NzLayoutModule} from "ng-zorro-antd/layout";
import {NzMenuModule} from "ng-zorro-antd/menu";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [HeaderComponent],
  imports: [NzLayoutModule, NzMenuModule, CommonModule],
  exports: [HeaderComponent]
})
export class HeaderModule {
}
