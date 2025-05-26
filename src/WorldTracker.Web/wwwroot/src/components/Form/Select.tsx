import Label from "./Label";
import { IFieldProps } from "./interfaces/IFieldProps";

export interface IOption {
  value: string;
  text: string;
}

export interface ISelectProps {
  options: IOption[];
}

export default function Select({
  label,
  field,
  caption,
  options,
  className,
  ...props
}: IFieldProps<HTMLSelectElement, ISelectProps>) {
  return (
    <div className={`flex flex-1 flex-col ${className}`}>
      <Label
        label={label}
        field={field}
        caption={caption}
        required={props.required}
      />
      <select
        id={field}
        name={field}
        className="border-2 border-gray-400 p-1 rounded-lg"
        {...props}
      >
        <option value="" defaultChecked>
          Select:
        </option>
        {options.map((option: IOption) => (
          <option key={`${field}-${option.value}`} value={option.value}>
            {option.text}
          </option>
        ))}
      </select>
    </div>
  );
}
