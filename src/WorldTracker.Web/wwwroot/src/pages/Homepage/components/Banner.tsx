import Earth from "@/assets/Earth.svg";
import { Logo } from "@/components/Logo";
import { Link } from "react-router-dom";

export default function Banner() {
  return (
    <section className="relative max-w-7xl mx-auto flex flex-col md:flex-row items-center justify-between px-6 py-24 gap-16 md:min-h-screen">
      <img
        src={Earth}
        className="absolute md:right-0 top-0 h-full max-w-[50%] object-contain max-md:hidden"
      />
      <div className="flex flex-col max-w-xl text-dark-gray max-md:text-center max-md:items-center">
        <h1 className="text-4xl md:text-5xl font-bold leading-tight">
          Bem-vindo ao <Logo size="lg" />
        </h1>
        <p className="my-6 text-md md:text-xl leading-relaxed">
          Consulte o clima atual de qualquer lugar do mundo, explore dados
          completos de países e salve seus favoritos com praticidade.
        </p>
        <div className="flex flex-wrap justify-center md:justify-start gap-4">
          <Link
            to="/login"
            className="px-8 py-3 rounded-xl bg-sky-600 text-white font-semibold hover:bg-sky-700 transition-colors duration-200"
            aria-label="Ir para a página de login"
          >
            Entrar
          </Link>
          <Link
            to="/register"
            className="px-8 py-3 rounded-xl bg-blue-50 border border-sky-600 text-sky-600 font-semibold hover:bg-white transition-all duration-300"
            aria-label="Ir para a página de criação de conta"
          >
            Criar Conta
          </Link>
        </div>
      </div>
    </section>
  );
}
