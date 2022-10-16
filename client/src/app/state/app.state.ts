import { IGlobalState } from './appglobal/appglobal.reducer';
import { IDepartmentState } from './department/department.reducer';
import { IDesignationState } from './designation/designation.reducer';
import { IEmployeeState } from './employee/employee.reducer';

export interface IAppState {
  empState: IEmployeeState;
  deptState: IDepartmentState;
  desigState: IDesignationState;
  globalState: IGlobalState;
}
