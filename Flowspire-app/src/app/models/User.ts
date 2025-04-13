export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  birthDate?: Date | string;
  gender?: Gender;
  addressLine1?: string;
  addressLine2?: string;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  roles: number[];
}

export enum Gender
{
    Male,
    Female,
    NotSpecified
}