import React, { createContext, useContext, useState } from "react";
import Item from "../interfaces/Item";

interface PedidoContextProps {
  pedido: Item[];
  adicionarAoPedido: (item: Item) => void;
  limparPedido: () => void;
}

const PedidoContext = createContext<PedidoContextProps | undefined>(undefined);

export const PedidoProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [pedido, setPedido] = useState<Item[]>([]);

  const adicionarAoPedido = (item: Item) => {
    setPedido((prev) => [...prev, item]);
  };

  const limparPedido = () => {
    setPedido([]);
  };

  return (
    <PedidoContext.Provider value={{ pedido, adicionarAoPedido, limparPedido }}>
      {children}
    </PedidoContext.Provider>
  );
};

export const usePedido = (): PedidoContextProps => {
  const context = useContext(PedidoContext);
  if (!context) {
    throw new Error("usePedido deve ser usado dentro de um PedidoProvider");
  }
  return context;
};
