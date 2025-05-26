import { ILabelProps } from "./ILabelProps";

export type IFieldProps<T, P = object> = ILabelProps &
  React.InputHTMLAttributes<T> &
  P;
