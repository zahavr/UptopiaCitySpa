export interface IBusiness {
  id: number;
  name: string;
  description: string;
  address: string;
  maxCountOfWorker: number;
}

export interface IBusinessWorker {
  id: number;
  firstName: string;
  lastName: string;
  salary: number;
  positionAtWork: number;
}
