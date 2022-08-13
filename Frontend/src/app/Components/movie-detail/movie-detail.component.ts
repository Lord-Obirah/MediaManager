import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {IMovie} from "../../interfaces/movie";
import {DataService} from "../../services/data.service";
import {Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IMediaType} from "../../interfaces/mediatype";

@Component({
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.scss']
})
export class MovieDetailComponent implements OnInit {
  public pageTitle: string = 'Film';
  public result$: Observable<HttpResponse<IMovie>> = new Observable<HttpResponse<IMovie>>();
  public mediaTypes$: Observable<HttpResponse<IMediaType[]>> = new Observable<HttpResponse<IMediaType[]>>();

  public formGroup: FormGroup;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private dataService: DataService,
              protected formBuilder: FormBuilder) {

    this.formGroup = this.formBuilder.group({
      id: [],
      title: [Validators.required],
      mediaTypeId: [Validators.required]
    });
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    this.result$ = this.dataService.getData<IMovie>("movies", id).pipe(tap(data => {
      if(data && data.body)
      {
        this.formGroup.patchValue(data.body);
      }}));

    this.mediaTypes$ = this.dataService.getDataList<IMediaType>('mediaTypes')
  }

  submit() {
    if (this.formGroup.valid) {
      console.log(this.formGroup.value);
      this.dataService.updateData<IMovie>('movies', this.formGroup.value.id, this.formGroup.value).subscribe(
        res => {
          this.router.navigate(['/movies']);
        },
        err => console.log('HTTP Error', err),
        () => console.log('HTTP request completed.')
      );
      // .subscribe(
      //   x => this.router.navigate(['/movies'],
      //   err => console.error(err);
      // );
    }
  }
}
