import {Component, Input, OnInit} from '@angular/core';
import {IMovie} from "../../../movies/interfaces/movie";
import {NotificationService} from "../../services/notification.service";
import {UntypedFormGroup} from "@angular/forms";
import {Observable, tap} from "rxjs";
import {DataService} from "../../services/data.service";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpResponse} from "@angular/common/http";

@Component({
  selector: 'app-detail',
  templateUrl: './app-detail.component.html',
  styleUrls: ['./app-detail.component.scss']
})
export class AppDetailComponent<T> implements OnInit {
  public entityType: string;
  protected id: string | null;
  public formGroup: UntypedFormGroup;
  public result$: Observable<HttpResponse<T>>

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              protected dataService: DataService,
              private notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');

    if (this.id?.toLowerCase() != 'new') {
      //TODO find another way to initialize the form for a new object and show all necessary controls
      this.result$ = this.dataService.getData<T>(this.entityType, this.id).pipe(tap(data => {
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
      this.dataService.updateData<IMovie>(this.entityType, this.formGroup.value.id, this.formGroup.value).subscribe({
        next: (res) => {
          this.router.navigate([this.entityType]);
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
