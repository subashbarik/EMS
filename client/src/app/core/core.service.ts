import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IGlobal } from '../shared/models/global';

@Injectable({
  providedIn: 'root',
})
export class CoreService {
  private formResetSubject = new Subject<void>();
  public formReset$ = this.formResetSubject.asObservable();
  baseUrl = environment.apiUrl;
  globalData: IGlobal;
  constructor(private httpClient: HttpClient) {}
  formReset() {
    this.formResetSubject.next();
  }
  loadGlobalData() {
    return this.httpClient.get(this.baseUrl + 'global');
    // return this.httpClient.get(this.baseUrl + 'global').pipe(
    //   map((response: any) => {
    //     console.log('service');
    //     console.log(response);
    //     return response;
    //   })
    // );
  }
}
