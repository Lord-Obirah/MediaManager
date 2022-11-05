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
  public pageTitle: string = 'CD';
  public result$: Observable<HttpResponse<IRecord>> = new Observable<HttpResponse<IRecord>>(subscriber => {
    //TODO find another way to initialize the form for a new object
      subscriber.next(new HttpResponse<IRecord>(
        {
          body: {
            id: '',
            title: '',
            mediaTypeId: '',
            releaseYear: undefined,
            tracks: []
          }
        }
      ))
    }
  );
  //public mediaTypes$: Observable<HttpResponse<IMediaType[]>> = new Observable<HttpResponse<IMediaType[]>>();

  public formGroup: UntypedFormGroup;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private dataService: DataService,
              protected formBuilder: UntypedFormBuilder,
              private notificationService: NotificationService) {

    this.formGroup = this.formBuilder.group({
      id: [''],
      title: ['', Validators.required], //TODO fix required in new form
      mediaTypeId: ['', Validators.required], //TODO implement app-select
      fskRatingId: ['', Validators.required] //TODO implement app-select
    });
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id?.toLowerCase() != 'new') {
      //TODO find another way to initialize the form for a new object and show all necessary controls
      this.result$ = this.dataService.getData<IRecord>("records", id).pipe(tap(data => {
        if (data && data.body) {
          this.formGroup.patchValue(data.body);
        }
      }));
    }
    //
    // this.mediaTypes$ = this.dataService.getDataList<IMediaType>('mediaTypes')
    // this.fskRatings$ = this.dataService.getDataList<IFskRating>('fskRatings')
  }

  submit() {
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      console.log(data);
      this.dataService.updateData<IRecord>('records', this.formGroup.value.id, this.formGroup.value).subscribe({
        next: (res) => {
          this.router.navigate(['/records']);
          this.notificationService.success(`Speichern erfolgreich: ${res.body?.title}`)
        },
        error: (err) => {
          console.log('HTTP Error', err);
          this.notificationService.error(`${err}`)
        },
        complete: () => console.log('HTTP request completed.')
      });
    }
  }
}
