import { FaLinkedin, FaGithub } from "react-icons/fa";

export default function Footer() {
  return (
    <footer className="bg-dark-gray text-white py-8 px-4 text-center">
      <div className="max-w-5xl mx-auto flex flex-col items-center gap-4">
        <div className="flex gap-6">
          <a
            href="https://www.linkedin.com/in/miguel-s-ramos/"
            target="_blank"
            rel="noopener noreferrer"
          >
            <FaLinkedin size={28} />
          </a>
          <a
            href="https://github.com/miguel-sr"
            target="_blank"
            rel="noopener noreferrer"
          >
            <FaGithub size={28} />
          </a>
        </div>
        <p className="text-sm md:text-base">
          Desenvolvido por Miguel Ramos • Fornecendo dados meteorológicos
          globais em tempo real
        </p>
        <p className="text-xs">
          © {new Date().getFullYear()} GloboClima. Todos os direitos reservados.
        </p>
      </div>
    </footer>
  );
}
