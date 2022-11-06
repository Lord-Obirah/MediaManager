import {Component, OnInit} from '@angular/core';
import {DataService} from "../../../shared/services/data.service";
import {IRecordList} from "../../interfaces/recordList";
import {AppListComponent} from "../../../shared/components/app-list/app-list.component";

@Component({
  templateUrl: './record-list.component.html',
  styleUrls: ['./record-list.component.scss']
})
export class RecordListComponent extends AppListComponent<IRecordList> implements OnInit {
  constructor(dataService: DataService) {
    super(dataService);
  }

  override ngOnInit(): void {
    super.ngOnInit()
  }
}
