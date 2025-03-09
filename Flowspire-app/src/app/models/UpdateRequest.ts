export interface UpdateRequestWrapper {
  requestWrapper: UpdateRequest;
}

export interface UpdateRequest {
  fullName: string;
  roles: number[];
}