export interface IVacancy {
  vacancyId: number;
  vacancyTitle: string;
  vacancyDescription: string;
  salary: number;
  businessTitle: string;
  businessDescription: string;
  address: string;
}

export interface IUserRespondVacancy {
  id: number;
  title: string;
  description: string;
  salary: number;
}

export interface IUserBusinessVacancy {
  id: number;
  firstName: string;
  lastName: string;
  vacancyTitle: string;
}
