import clsx from "clsx";
import "./style.css";

export function Toggler({
  menuOpen,
  setMenuOpen,
}: {
  menuOpen: boolean;
  setMenuOpen: React.Dispatch<React.SetStateAction<boolean>>;
}) {
  return (
    <button
      onClick={() => setMenuOpen(!menuOpen)}
      type="button"
      className={clsx(
        "inline-flex items-center aspect-square p-1 ml-3 text-sm text-gray-500 rounded-lg md:hidden",
        {
          menuOpen,
        }
      )}
      aria-label={menuOpen ? "Close menu" : "Open menu"}
    >
      <div className="menu-btn__burger" />
    </button>
  );
}
