export interface ApiResponse<T> {
  isSuccess: boolean;
  title: string;
  message: string;
  data: T;
}
