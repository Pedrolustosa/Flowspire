export interface RegisterCustomerRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  phoneNumber: string;
  birthDate?: Date | string;    // Optional, use ISO string or Date object
  gender?: string;              // e.g., 'Male', 'Female', 'NotSpecified'
  addressLine1?: string;
  addressLine2?: string;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
}