import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DepartmentParams } from '../shared/models/departmentParams';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  constructor(private httpClient: HttpClient) {}
  departmentParams = new DepartmentParams();
  baseUrl = environment.apiUrl;
  getDepartments() {
    let params = new HttpParams();
    if (this.departmentParams.search) {
      params.append('Search', this.departmentParams.search);
    }
    params = params.append('Sort', this.departmentParams.sort);
    params = params.append('PageIndex', this.departmentParams.pageIndex);
    params = params.append('PageSize', this.departmentParams.pageSize);
    return this.httpClient
      .get(this.baseUrl + 'departments', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
  setDepartmentParams(params: DepartmentParams) {
    this.departmentParams = params;
  }
}
