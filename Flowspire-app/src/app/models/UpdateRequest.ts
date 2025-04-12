export interface UpdateRequestWrapper {
  request: UpdateRequest;
}

export interface UpdateRequest {
  firstName: string;
  lastName: string;
  birthDate?: Date | string;
  gender?: string;
  addressLine1?: string;
  addressLine2?: string;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  roles: number[];
}