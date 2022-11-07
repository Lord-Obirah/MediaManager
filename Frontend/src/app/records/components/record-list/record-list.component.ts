import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {IRecordList} from "../../interfaces/recordList";
import {AbstractAppListComponent} from "../../../shared/components/app-list/abstract-app-list.component";
import {PaginationHeaderService} from "../../../shared/services/pagination-header.service";
import {ListDataService} from "../../../shared/services/list-data.service";

@Component({
  templateUrl: './record-list.component.html',
  styleUrls: ['./record-list.component.scss']
})
export class RecordListComponent extends AbstractAppListComponent<IRecordList> implements OnInit {
  constructor(paginationHeaderService: PaginationHeaderService,
              listDataService: ListDataService) {
    super(paginationHeaderService, listDataService);
  }

  override ngOnInit(): void {
    super.ngOnInit()
  }
}
