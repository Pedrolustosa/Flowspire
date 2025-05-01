export interface RegisterRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  phoneNumber: string;
  birthDate?: Date | string;
  gender?: string;
  addressLine1?: string;
  addressLine2?: string;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  role?: string;
}