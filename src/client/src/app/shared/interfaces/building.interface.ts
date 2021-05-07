export interface ICardBuilding {
  id: number;
  countRooms: number;
  floor: number;
  typeAppartament: typeAppartament;
  cost: number;
  description: string;
  title: string;
}

export enum typeAppartament {
  Econom,
  Comfort,
  Luxe
}
