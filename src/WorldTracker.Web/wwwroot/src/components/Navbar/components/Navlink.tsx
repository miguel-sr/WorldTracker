interface INavlinkProps
  extends React.DetailedHTMLProps<
    React.AnchorHTMLAttributes<HTMLAnchorElement>,
    HTMLAnchorElement
  > {
  text: string;
}

export function Navlink({ text, ...props }: INavlinkProps) {
  return (
    <li className="md:text-lg text-sky-blue hover:opacity-75 transition-all duration-300 mx-3 my-2">
      <a className="cursor-pointer font-semibold" {...props}>
        {text}
      </a>
    </li>
  );
}
