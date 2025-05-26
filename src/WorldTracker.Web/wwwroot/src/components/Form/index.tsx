import Input from "./Input";
import Select from "./Select";
import TextArea from "./TextArea";

interface IFormProps extends React.FormHTMLAttributes<HTMLFormElement> {
  children: JSX.Element | JSX.Element[];
}

function Form({ children, ...props }: IFormProps) {
  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
      }}
      {...props}
    >
      {children}
    </form>
  );
}

export { Form, Input, Select, TextArea };
