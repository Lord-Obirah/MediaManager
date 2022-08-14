import { Injectable } from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  public error(message: string): void {
    this.toastr.error(message);
  }

  public success(message: string): void {
    this.toastr.success(message);
  }
}
