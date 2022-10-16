import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { EmployeeService } from '../employee/employee.service';
import { IEmployee } from '../shared/models/employee';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private empService: EmployeeService) {}

  ngOnInit(): void {
    // this.empService.setEmployees();
  }
}
