import {Component, Input, OnInit} from '@angular/core';
import Map from 'ol/Map';
import {View} from "ol";
import TileLayer from "ol/layer/Tile";
import {OSM} from "ol/source";
import {transform} from 'ol/proj';
import {GeoJSON} from "ol/format";
import VectorSource from "ol/source/Vector";
import VectorLayer from "ol/layer/Vector";
import {Stroke, Style} from "ol/style";

@Component({
  selector: 'app-map',
  templateUrl: 'map.component.html',
  styleUrls: ['map.component.scss'],
})
export class MapComponent implements OnInit {
  public map: Map | undefined;
  public from: number[] = [];
  public to: number[] = [];
  @Input() public isRouteManager: boolean = true;
  private clickCounter: number = 0;

  public ngOnInit(): void {
    this.map = new Map({
      view: new View({
        center: [4188156.763058, 7508869.821665],
        zoom: 11,
      }),
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: 'map'
    });

    this.map.on('click', (event) => {
      if (this.clickCounter === 0) {
        this.from = transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
        this.clickCounter++
      } else if (this.clickCounter === 1) {
        this.to = transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
        this.clickCounter = 0;
      }
    })
  }

  public onGetRoute(event: any): void {
    const styles = {
      'LineString': new Style({
        stroke: new Stroke({
          color: 'green',
          width: 5,
        }),
      }),
    };

    const styleFunction = function () {
      return styles['LineString'];
    };
    const features = new GeoJSON().readFeatures(event.routes[0].geometry);
    features[0].getGeometry().transform("EPSG:4326", "EPSG:3857");
    const vectorSource = new VectorSource({
      features: features,
    });

    const vectorLayer = new VectorLayer({
      source: vectorSource,
      style: styleFunction,
    });

    this.map?.addLayer(vectorLayer);
    this.map?.render();
  }
}

