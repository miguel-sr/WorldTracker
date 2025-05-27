import { Logo } from "../Logo";
import "./style.css";

export default function Loading() {
  return (
    <div className="flex flex-col justify-center items-center bg-white inset-0 fixed z-50">
      <Logo />
      <div className="dot-falling" />
    </div>
  );
}
