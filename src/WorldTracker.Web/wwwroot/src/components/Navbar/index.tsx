import { TOKEN_KEY } from "@/common/Constants";
import clsx from "clsx";
import { useEffect, useState } from "react";
import { Logo } from "../Logo";
import { Navlink } from "./components/Navlink";
import { Toggler } from "./components/Toggler";

export default function Navbar({
  position = "absolute",
}: {
  position?: "absolute" | "relative";
}) {
  const [userLogged, setUserLogged] = useState<boolean>(false);
  const [menuOpen, setMenuOpen] = useState(false);

  useEffect(() => {
    setUserLogged(localStorage.getItem(TOKEN_KEY) != null);
  }, []);

  const handleLogout = () => {
    localStorage.removeItem(TOKEN_KEY);
    setUserLogged(false);
  };

  return (
    <nav className={clsx("absolute p-2 z-10 w-full bg-white", position)}>
      <div className="px-2 flex flex-wrap items-center justify-between mx-auto relative max-w-7xl">
        <Logo color="blue" size="sm" />
        <Toggler menuOpen={menuOpen} setMenuOpen={setMenuOpen} />
        <ul
          id="navbar-menu"
          className={clsx(
            "flex items-center transition-all duration-300 max-md:overflow-y-auto max-md:flex-col max-md:w-full max-md:h-auto",
            menuOpen ? "max-h-screen max-md:pt-2" : "max-h-0"
          )}
          aria-hidden={!menuOpen}
        >
          <Navlink text="Início" href="/" />
          {userLogged ? (
            <>
              <Navlink text="Favoritos" href="/favorites" />
              <Navlink text="Perfil" href="/profile" />
              <Navlink text="Sair" onClick={handleLogout} />
            </>
          ) : (
            <>
              <Navlink text="Entrar" href="/login" />
              <Navlink text="Criar Conta" href="/register" />
            </>
          )}
        </ul>
      </div>
    </nav>
  );
}
