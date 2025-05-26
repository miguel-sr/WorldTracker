export default interface IPeca {
  id: string;
  nome: string;
  descricao: string;
  categoria: string;
  codigo: number;
  limitePorPedido: number;
  estoque: {
    centrooeste: number;
    nordeste: number;
    norte: number;
    sudeste: number;
    sul: number;
  };
  linkDaImagem: string;
}
