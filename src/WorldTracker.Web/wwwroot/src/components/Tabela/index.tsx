import { useEffect, useState } from "react";
import Cabecalho from "./Cabecalho";
import Linhas from "./Linhas";

export default function Tabela({
  titulos,
  colunas,
  linhas,
}: {
  titulos: string[];
  colunas: string[];
  linhas: Record<string, any>[];
}) {
  const [dadosExistentes, setDadosExistentes] = useState(false);

  useEffect(() => {
    setDadosExistentes(linhas && linhas.length > 0);
  }, [linhas]);

  return (
    <div className="overflow-x-auto mt-5">
      {dadosExistentes ? (
        <table className="w-full bg-zinc-100 border border-zinc-300 rounded-lg">
          <Cabecalho colunas={titulos} />
          <Linhas colunas={colunas} linhas={linhas} />
        </table>
      ) : (
        <div className="bg-zinc-100 border-zinc-300 border text-center py-2">
          NENHUM REGISTRO ENCONTRADO
        </div>
      )}
    </div>
  );
}
