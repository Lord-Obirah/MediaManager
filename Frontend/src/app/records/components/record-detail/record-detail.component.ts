import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpResponse} from "@angular/common/http";
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {Observable, tap} from "rxjs";
import {IRecord} from "../../interfaces/record";
import {DataService} from "../../../shared/services/data.service";
import {NotificationService} from "../../../shared/services/notification.service";

@Component({
  templateUrl: './record-detail.component.html',
  styleUrls: ['./record-detail.component.scss']
})
export class RecordDetailComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private dataService: DataService,
              protected formBuilder: UntypedFormBuilder,
              private notificationService: NotificationService) {
  }

  ngOnInit(): void {
  }

  submit() {
  }
}
