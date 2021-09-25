import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {NzLayoutModule} from "ng-zorro-antd/layout";
import {HeaderModule} from "./header/header.module";
import {MapModule} from "./map/map.module";
import {HttpClientModule} from "@angular/common/http";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NzLayoutModule,
    HeaderModule,
    MapModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
