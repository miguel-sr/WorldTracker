import GerenciadorDeQuery from "@/common/hooks/GerenciadorDeQuery";
import IPeca from "@/common/interfaces/IPeca";

const PARAMETRO_REGIAO = "regiao";
const STRING_VAZIA = "";
const ESPACO_VAZIO = " ";

export default function ObterRegiaoComoKeyofEstoque() {
  const { query } = GerenciadorDeQuery();

  const regiao = query.get(PARAMETRO_REGIAO);

  if (regiao == null) return;

  return regiao
    .toLowerCase()
    .replaceAll(ESPACO_VAZIO, STRING_VAZIA) as keyof IPeca["estoque"];
}
