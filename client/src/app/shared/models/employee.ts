import { command } from './command';
import { IDepartment } from './department';
import { IDesignation } from './designation';

export interface IEmployeeFormData {
  employee: IEmployee;
  command: command;
}
export class EmployeeFormData implements IEmployeeFormData {
  constructor(public employee: IEmployee, public command: command) {}
}
export interface IEmployeeFormPageModel {
  departments: IDepartment[];
  designations: IDesignation[];
  defaultImageUrl: string;
}
// export class EmployeeFormPageModel implements IEmployeeFormPageModel {
//   departments: IDepartment[];
//   designations: IDesignation[];
//   defaultImageUrl: string;
//   constructor(
//     departments: IDepartment[],
//     designations: IDesignation[],
//     defaultImageUrl: string
//   ) {
//     this.departments = departments;
//     this.designations = designations;
//     this.defaultImageUrl = defaultImageUrl;
//   }
// }
export interface IEmployee {
  id: number;
  firstName: string;
  lastName: string;
  age: number;
  designationName: string;
  departmentName: string;
  salary: number;
  imageUrl: string;
  imageFile: File;
  departmentId: number;
  designationId: number;
  removeImage: boolean;
}
export class Employee implements IEmployee {
  id: number;
  firstName: string;
  lastName: string;
  age: number;
  designationName: string;
  departmentName: string;
  imageUrl: string;
  imageFile: File;
  salary: number;
  departmentId: number;
  designationId: number;
  removeImage: boolean;
  constructor(
    id: number,
    firstname: string,
    lastname: string,
    age: number,
    designationName: string,
    departmentName?: string,
    imageUrl?: string,
    imageFile?: File,
    salary?: number,
    departmentId?: number,
    designationId?: number,
    removeImage?: boolean
  ) {
    this.id = id;
    this.firstName = firstname;
    this.lastName = lastname;
    this.age = age;
    this.designationName = designationName;
    this.departmentName = departmentName;
    this.imageUrl = imageUrl;
    this.imageFile = imageFile;
    this.salary = salary;
    this.departmentId = departmentId;
    this.designationId = designationId;
    this.removeImage = removeImage;
  }
}
