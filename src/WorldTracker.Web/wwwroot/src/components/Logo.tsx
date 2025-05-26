import clsx from "clsx";
import { LiaCloudSunRainSolid } from "react-icons/lia";

interface LogoProps {
  color?: "blue" | "white";
  size?: "sm" | "md" | "lg";
}

export function Logo({ color = "blue", size = "md" }: LogoProps) {
  const iconColor = clsx({
    "text-sky-blue": color === "blue",
    "text-white": color === "white",
  });

  const sizeConfig = {
    sm: { icon: 40, text: "text-xl" },
    md: { icon: 60, text: "text-3xl" },
    lg: { icon: 90, text: "text-5xl" },
  };

  const { icon, text } = sizeConfig[size];

  return (
    <button
      onClick={() => (location.href = "/")}
      className={clsx(
        "flex items-center cursor-pointer space-x-2 focus:outline-none select-none",
        iconColor
      )}
      aria-label="Voltar para página inicial"
      type="button"
    >
      <LiaCloudSunRainSolid size={icon} className={iconColor} />
      <span className={clsx("font-semibold", text)}>
        Globo<span className="font-bold">Clima</span>
      </span>
    </button>
  );
}
