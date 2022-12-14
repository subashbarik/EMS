export interface ILogParams {
  pageIndex: number;
  pageSize: number;
  level: string;
}

export class LogParams implements ILogParams {
  pageIndex = 1;
  pageSize = 20;
  level = '';
}
