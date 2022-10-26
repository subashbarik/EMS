import { IApiValidationErrorResponse } from './apierrorresponse';

export interface IUser {
  email: string;
  displayName: string;
  token: string;
  roles: string[];
  apiErrorResponse: any;
}
export interface ILogin {
  email: string;
  password: string;
}
export interface IRegister {
  displayname: string;
  email: string;
  password: string;
}
export class Login implements ILogin {
  email: string;
  password: string;
  constructor(email: string, password: string) {
    this.email = email;
    this.password = password;
  }
}
export class Register implements IRegister {
  displayname: string;
  email: string;
  password: string;
  constructor(displayname: string, email: string, password: string) {
    this.displayname = displayname;
    this.email = email;
    this.password = password;
  }
}
export enum Roles {
  Admin = 'Admin',
  User = 'User',
}
