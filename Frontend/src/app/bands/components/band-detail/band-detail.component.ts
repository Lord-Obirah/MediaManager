import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpResponse} from "@angular/common/http";
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {Observable, tap} from "rxjs";
import {IBand} from "../../interfaces/band";
import {DataService} from "../../../shared/services/data.service";
import {NotificationService} from "../../../shared/services/notification.service";

@Component({
  templateUrl: './band-detail.component.html',
  styleUrls: ['./band-detail.component.scss']
})
export class BandDetailComponent implements OnInit {
  public pageTitle: string = 'CD';
  public result$: Observable<HttpResponse<IBand>> = new Observable<HttpResponse<IBand>>(subscriber => {
    //TODO find another way to initialize the form for a new object
      subscriber.next(new HttpResponse<IBand>(
        {
          body: {
            id: '',
            name: ''
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
      name: ['', Validators.required] //TODO fix required in new form
    });
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id?.toLowerCase() != 'new') {
      //TODO find another way to initialize the form for a new object and show all necessary controls
      this.result$ = this.dataService.getData<IBand>("bands", id).pipe(tap(data => {
        if (data && data.body) {
          this.formGroup.patchValue(data.body);
        }
      }));
    }
  }

  submit() {
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      console.log(data);
      this.dataService.updateData<IBand>('bands', this.formGroup.value.id, data).subscribe({
        next: (res) => {
          this.router.navigate(['/bands']);
          this.notificationService.success(`Speichern erfolgreich: ${res.body?.name}`)
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
