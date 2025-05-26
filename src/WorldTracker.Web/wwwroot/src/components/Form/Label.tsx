import { ILabelProps } from "./interfaces/ILabelProps";

export default function Label({
  label,
  field,
  caption,
  required
}: ILabelProps) {
  return (
    <label htmlFor={field} className="relative">
      {label}
      {required && (
        <p className="text-red-600 text-sm absolute top-0 right-0 font-bold ml-2">
          *obrigatório
        </p>
      )}
      {caption !== undefined && (
        <p className="text-gray-600 text-sm line-clamp-1">{caption}</p>
      )}
    </label>
  );
}
