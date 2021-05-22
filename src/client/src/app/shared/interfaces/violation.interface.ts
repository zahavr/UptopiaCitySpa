export interface IViolation {
  id: number;
  description: string;
  penalty: number;
  typeViolation: typeViolation;
  dateExpired: Date;
  setDate: Date;
}

export enum typeViolation {
  Low = 0,
  Medium = 1,
  High = 2
}
