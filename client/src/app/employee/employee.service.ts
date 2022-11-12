import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { map, of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee, IEmployee } from '../shared/models/employee';
import { EmployeeParams } from '../shared/models/employeeParams';
import { Pagination } from '../shared/models/pagination';
import { selectEmployeeParams } from '../state/employee/employee.selectors';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  baseUrl = environment.apiUrl;
  pagination = new Pagination();
  employeeParams = new EmployeeParams();
  employees: IEmployee[] = [];
  constructor(private httpClient: HttpClient, private store: Store) {}

  getEmployees() {
    this.setEmployeeParams();
    console.log(this.employeeParams);
    let params = new HttpParams();
    if (this.employeeParams.departmentId !== 0) {
      params = params.append(
        'DepartmentId',
        this.employeeParams.departmentId.toString()
      );
    }
    if (this.employeeParams.designationId !== 0) {
      params = params.append(
        'DesignationId',
        this.employeeParams.designationId.toString()
      );
    }
    if (this.employeeParams.search) {
      params = params.append('search', this.employeeParams.search);
    }

    params = params.append('Sort', this.employeeParams.sort);
    params = params.append('PageIndex', this.employeeParams.pageIndex);
    params = params.append('PageSize', this.employeeParams.pageSize);

    return this.httpClient
      .get(this.baseUrl + 'employees', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }

  setEmployeeParams() {
    this.store.select(selectEmployeeParams).subscribe({
      next: (response) => {
        this.employeeParams = response;
      },
    });
  }
  getEmployeeParams() {
    return this.employeeParams;
  }
  addUpdateEmployee(employee: IEmployee) {
    let req: any;
    const formData: FormData = new FormData();

    formData.append('Id', employee.id.toString());
    formData.append('FirstName', employee.firstName);
    formData.append('LastName', employee.lastName);
    formData.append('Age', employee.age.toString());
    formData.append('DepartmentName', employee.departmentName);
    formData.append('DesignationName', employee.designationName);
    formData.append('ImageUrl', employee.imageUrl);
    formData.append('ImageFile', employee.imageFile);
    formData.append('Salary', employee.salary.toString());
    formData.append('DepartmentId', employee.departmentId.toString());
    formData.append('DesignationId', employee.designationId.toString());
    formData.append('removeImage', employee.removeImage.toString());
    //Update
    if (employee.id > 0) {
      req = new HttpRequest('PUT', this.baseUrl + 'employees', formData, {
        reportProgress: false,
      });
    } else {
      //Add
      req = new HttpRequest('POST', this.baseUrl + 'employees', formData, {
        reportProgress: false,
      });
    }
    return this.httpClient.request<IEmployee>(req).pipe(
      map((response: any) => {
        return response.body;
      })
    );
  }
  deleteEmployee(employee: IEmployee) {
    const options = {
      body: {
        id: employee.id,
        firstName: employee.firstName,
        lastName: employee.lastName,
        age: employee.age,
        salary: employee.salary,
        departmentId: employee.departmentId,
        designationId: employee.designationId,
        imageUrl: employee.imageUrl,
      },
    };
    return this.httpClient.delete(this.baseUrl + 'employees', options);
  }
  loadEmployeeFormPage() {
    return this.httpClient.get(this.baseUrl + 'employees/formpagedata');
  }
}
