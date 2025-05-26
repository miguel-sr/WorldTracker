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
        className="border-2 border-gray-400 p-2 rounded-lg h-10"
        {...props}
      />
    </div>
  );
}
