import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpResponse} from "@angular/common/http";
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {Observable, tap} from "rxjs";
import {IMovie} from "../../interfaces/movie";
import {IMediaType} from "../../interfaces/mediatype";
import {IFskRating} from "../../interfaces/fskRating";
import {DataService} from "../../../shared/services/data.service";
import {NotificationService} from "../../../shared/services/notification.service";
import {AppDetailComponent} from "../../../shared/components/app-detail/app-detail.component";

@Component({
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.scss']
})
export class MovieDetailComponent extends AppDetailComponent<IMovie> implements OnInit {
  public pageTitle: string = 'Film';
  public mediaTypes$: Observable<HttpResponse<IMediaType[]>> = new Observable<HttpResponse<IMediaType[]>>();
  public fskRatings$: Observable<HttpResponse<IFskRating[]>> = new Observable<HttpResponse<IFskRating[]>>();

  constructor(activatedRoute: ActivatedRoute,
              router: Router,
              dataService: DataService,
              notificationService: NotificationService,
              protected formBuilder: UntypedFormBuilder) {

    super(activatedRoute, router, dataService, notificationService);

    this.entityType = 'movies';

    this.formGroup = this.formBuilder.group({
      id: [''],
      title: ['', Validators.required], //TODO fix required in new form
      mediaTypeId: ['', Validators.required], //TODO implement app-select
      fskRatingId: ['', Validators.required] //TODO implement app-select
    });
  }

  override ngOnInit(): void {
    super.ngOnInit();

    if(!this.id){
      this.result$ = new Observable<HttpResponse<IMovie>>(subscriber => {
          //TODO find another way to initialize the form for a new object
          subscriber.next(new HttpResponse<IMovie>(
            {
              body: {
                id: '',
                title: '',
                mediaTypeId: '',
                fskRatingId: ''
              }
            }
          ))
        }
      );
    }
    this.mediaTypes$ = this.dataService.getDataList<IMediaType>('mediaTypes')
    this.fskRatings$ = this.dataService.getDataList<IFskRating>('fskRatings')
  }
}
