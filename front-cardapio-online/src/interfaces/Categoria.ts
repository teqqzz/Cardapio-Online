import Item from "./Item";


export default interface Categoria {
  id: number;
  nome: string;
  criadoEm: string;
  atualizadoEm: string;
  itens: Item[];
}
