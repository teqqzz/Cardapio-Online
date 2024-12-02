import React from "react";
import "./PedidoResumo.css";
import Item from "../interfaces/Item";

interface PedidoResumoProps {
  pedido: Item[];
  onRemove: (itemId: number) => void;
  onConfirm: () => void;
}

const PedidoResumo: React.FC<PedidoResumoProps> = ({ pedido, onRemove, onConfirm }) => {
  const total = pedido.reduce((sum, item) => sum + item.preco, 0);

  return (
    <div className="pedido-resumo">
      <h2>Resumo do Pedido</h2>
      <ul>
        {pedido.map((item) => (
          <li key={item.id}>
            <img src={item.imagemUrl} alt={item.nome} style={{ width: "50px", height: "50px" }} />
            {item.nome} - R${item.preco.toFixed(2)}
            <button onClick={() => onRemove(item.id)}>Remover</button>
          </li>
        ))}
      </ul>
      <p>Total: R${total.toFixed(2)}</p>
      <button onClick={onConfirm} disabled={pedido.length === 0}>
        Chamar Garçom
      </button>
      
    </div>
  );
};

export default PedidoResumo;
