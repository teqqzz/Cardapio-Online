import DetalhePedido from "./DetalhePedido";

export default interface Pedido {
  id: number;
  nomeCliente: string;
  dataPedido: string; // ISO string
  numeroMesaCliente: number;
  chamadoGarcom: boolean;
  detalhesPedido: DetalhePedido[];
}
