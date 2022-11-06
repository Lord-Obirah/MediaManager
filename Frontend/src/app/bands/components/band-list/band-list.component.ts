import {Component, OnInit} from '@angular/core';
import {DataService} from "../../../shared/services/data.service";
import {AppListComponent} from "../../../shared/components/app-list/app-list.component";
import {IBandList} from "../../interfaces/bandList";

@Component({
  templateUrl: './band-list.component.html',
  styleUrls: ['./band-list.component.scss']
})
export class BandListComponent extends AppListComponent<IBandList> implements OnInit {
  constructor(dataService: DataService) {
    super(dataService);
  }

  override ngOnInit(): void {
    super.ngOnInit()
  }
}
