export interface ServiceResponse<T> {
  isSuccess: boolean;
  title: string;
  message: string;
  data: T;
}
