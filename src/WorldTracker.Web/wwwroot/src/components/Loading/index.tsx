import "./style.css";

// import JusticeLogo from "@/assets/SVG/Logo Justice Azul.svg";

export default function Loading() {
  return (
    <div className="flex flex-col justify-center items-center bg-white inset-0 fixed z-50">
      {/* <img src={JusticeLogo} className="w-1/3 max-w-[200px] mb-5" /> */}
      <div className="dot-falling" />
    </div>
  );
}
