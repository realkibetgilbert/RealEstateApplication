export interface LoginResponse{
  email: string;
  token: string;
  refreshToken: string;
  roles: string[];
}