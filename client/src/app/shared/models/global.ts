import { IServerAppConfiguration } from './serverappconfiguration';

export interface IGlobal {
  cnfList: { [key: string]: string | number };
  serverAppConfigurations: IServerAppConfiguration;
}
