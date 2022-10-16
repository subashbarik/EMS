import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DesignationParams } from '../shared/models/designationParams';

@Injectable({
  providedIn: 'root',
})
export class DesignationService {
  designationParams = new DesignationParams();
  baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) {}

  getDesignations() {
    let params = new HttpParams();
    if (this.designationParams.search) {
      params.append('Search', this.designationParams.search);
    }
    params = params.append('Sort', this.designationParams.sort);
    params = params.append('PageIndex', this.designationParams.pageIndex);
    params = params.append('PageSize', this.designationParams.pageSize);
    return this.httpClient
      .get(this.baseUrl + 'designations', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  setDepartmentParams(params: DesignationParams) {
    this.designationParams = params;
  }
}
