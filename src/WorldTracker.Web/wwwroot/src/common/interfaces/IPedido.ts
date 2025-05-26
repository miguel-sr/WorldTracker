export default interface IPedido {
  id: string;
  regiao: number;
  informacoesContato: {
    nome: string;
    nomeDaEquipe?: string;
    email: string;
    finalidadeDoPedido?: string;
  },
  listaDeItens: Array<{
    id: string;
    codigo: number;
    quantidade: number;
  }>;
}
