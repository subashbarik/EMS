export interface IDesignation {
  id: number;
  name: string;
  description: string;
}
export class Designation implements IDesignation {
  id: number;
  name: string;
  description: string;

  constructor(id: number, name: string, description: string) {
    this.id = id;
    this.name = name;
    this.description = description;
  }
}
