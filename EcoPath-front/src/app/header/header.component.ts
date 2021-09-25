import {Component} from "@angular/core";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-header',
  templateUrl: 'header.component.html',
  styleUrls: ['header.component.scss']
})
export class HeaderComponent {
  public readonly isCreateEcoPathActive: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public readonly isEcoZoneActive: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public onClickCreateEcoPath(): void {
    this.deselectTabs();
    this.isCreateEcoPathActive.next(true);
  }

  public onClickEcoZone(): void {
    this.deselectTabs();
    this.isEcoZoneActive.next(true);
  }

  private deselectTabs(): void {
    this.isCreateEcoPathActive.next(false);
    this.isEcoZoneActive.next(false);
  }

}
