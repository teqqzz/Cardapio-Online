import Item from "./Item";

export default interface DetalhePedido {
  id: number;
  quantidade: number;
  item: Item;
}