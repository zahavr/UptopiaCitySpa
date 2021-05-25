export interface IFullUserProfile {
  hasBusiness: boolean;
  hasAppartaments: boolean;
  roles: string[];
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  birthDate: Date;
  pictureUrl: string;
}
