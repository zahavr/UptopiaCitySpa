export interface ICardBuilding {
  id: number;
  countRooms: number;
  floor: number;
  typeAppartament: typeAppartament;
  cost: number;
  description: string;
  title: string;
  pictureUrl: string;
}

export interface IPoliceAppartament {
  title: string;
  cost: number;
  countRooms: number;
  typeAppartament: typeAppartament;
}

export enum typeAppartament {
  Econom = 0,
  Comfort = 1,
  Luxe = 2
}
