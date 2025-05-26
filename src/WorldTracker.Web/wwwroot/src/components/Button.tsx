import clsx from "clsx";

interface IButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  text: string;
  icon?: React.ReactNode;
  transparent?: boolean;
  active?: boolean;
}

export default function Button({
  text,
  icon,
  active = true,
  transparent = false,
  className,
  ...props
}: IButtonProps) {
  return (
    <button
      className={clsx(
        `hover:bg-opacity-75 transition-all duration-300 w-full py-3 px-10 font-medium ${className}`,
        {
          "text-gold-uniq border-2 border-gold-uniq": transparent,
          "text-white bg-gold-uniq": !transparent,
          "bg-opacity-75 cursor-default": !active,
          flex: Boolean(icon),
        }
      )}
      disabled={!active}
      {...props}
    >
      {icon}
      {text}
    </button>
  );
}
