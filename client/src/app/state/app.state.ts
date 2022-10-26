import { IAccountState } from './account/account.reducer';
import { IGlobalState } from './appglobal/appglobal.reducer';
import { IDepartmentState } from './department/department.reducer';
import { IDesignationState } from './designation/designation.reducer';
import { IEmployeeState } from './employee/employee.reducer';

export interface IAppState {
  accState: IAccountState;
  empState: IEmployeeState;
  deptState: IDepartmentState;
  desigState: IDesignationState;
  globalState: IGlobalState;
}
