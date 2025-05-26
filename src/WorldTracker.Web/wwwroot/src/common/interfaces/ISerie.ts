export default interface ISerie {
  id: string;
  titulo: string;
  descricao: string;
  assunto: string;
  categoria: string;
  capa: string;
  episodios: IEpisodio[];
}

export interface IEpisodio {
  titulo: string;
  descricao: string;
  data: string;
  idDoVideo: string;
}
