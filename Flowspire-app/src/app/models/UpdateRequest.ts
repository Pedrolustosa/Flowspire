import { Gender } from "./User";

export interface UpdateRequestWrapper {
  request: UpdateRequest;
}

export interface UpdateRequest {
  firstName: string;
  lastName: string;
  email: string;
  birthDate?: Date | string;
  gender: Gender;
  addressLine1?: string;
  addressLine2?: string;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  roles: number[];
}