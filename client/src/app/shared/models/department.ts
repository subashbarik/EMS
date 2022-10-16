export interface IDepartment {
  id: number;
  name: string;
  description: string;
  companyId: number;
}
export class Department implements IDepartment {
  id: number;
  name: string;
  description: string;
  companyId: number;
  constructor(
    id: number,
    name: string,
    description: string,
    companyId: number
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.companyId = companyId;
  }
}
