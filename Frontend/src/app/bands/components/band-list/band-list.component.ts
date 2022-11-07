import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {IBandList} from "../../interfaces/bandList";
import {AbstractAppListComponent} from "../../../shared/components/app-list/abstract-app-list.component";
import {PaginationHeaderService} from "../../../shared/services/pagination-header.service";
import {ListDataService} from "../../../shared/services/list-data.service";

@Component({
  templateUrl: './band-list.component.html',
  styleUrls: ['./band-list.component.scss']
})
export class BandListComponent extends AbstractAppListComponent<IBandList> implements OnInit {
  constructor(paginationHeaderService: PaginationHeaderService,
              listDataService: ListDataService) {
    super(paginationHeaderService, listDataService);
  }

  override ngOnInit(): void {
    super.ngOnInit()
  }
}
