import Label from "./Label";
import { IFieldProps } from "./interfaces/IFieldProps";

export default function Input({
  label,
  field,
  caption,
  className,
  ...props
}: IFieldProps<HTMLInputElement>) {
  return (
    <div className={`flex flex-1 flex-col ${className}`}>
      <Label
        label={label}
        field={field}
        caption={caption}
        required={props.required}
      />
      <input
        id={field}
        name={field}
        className="border border-gray-300 rounded-lg px-3 py-2 text-gray-700 placeholder-gray-400 bg-transparent focus:outline-none focus:ring-2 focus:ring-sky-500 transition"
        {...props}
      />
    </div>
  );
}
