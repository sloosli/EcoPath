import {Component, ViewChild} from '@angular/core';
import {MapComponent} from "./map/map.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'EcoPath-front';

  @ViewChild('map') public map: MapComponent | undefined;
}
