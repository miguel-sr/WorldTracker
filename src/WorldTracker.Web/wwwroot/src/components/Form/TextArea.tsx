import Label from "./Label";
import { IFieldProps } from "./interfaces/IFieldProps";

export default function TextArea({
  label,
  field,
  caption,
  className,
  ...props
}: IFieldProps<HTMLTextAreaElement>) {
  return (
    <div className={`flex flex-1 flex-col ${className}`}>
      <Label
        label={label}
        field={field}
        caption={caption}
        required={props.required}
      />
      <textarea
        id={field}
        name={field}
        className="border-2 border-gray-400 p-1 rounded-lg"
        {...props}
      />
    </div>
  );
}
