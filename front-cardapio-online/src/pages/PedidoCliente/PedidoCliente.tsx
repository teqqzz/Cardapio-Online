import React, { useState } from "react";
import { API_URL } from "../../config/config";
import Pedido from "../../interfaces/Pedido";
import DetalhePedido from "../../interfaces/DetalhePedido";
import "./PedidoCliente.css";

const PedidoCliente: React.FC = () => {
  const [numeroMesa, setNumeroMesa] = useState<number>(0);
  const [pedido, setPedido] = useState<Pedido | null>(null);
  const [errorMessage, setErrorMessage] = useState<string>("");

  // Buscar pedido pelo número da mesa
  const fetchPedidoPorMesa = async () => {
    if (numeroMesa) {
      try {
        const response = await fetch(
          `${API_URL}/api/pedidos/mesa/${numeroMesa}`
        );
        const data = await response.json();

        if (data.length > 0) {
          setPedido(data[0]); // Exibe o primeiro pedido encontrado
          setErrorMessage("");
        } else {
          setErrorMessage(
            "Não encontramos nenhum pedido para este número de mesa."
          );
          setPedido(null);
        }
      } catch (error) {
        console.error("Erro ao carregar o pedido da mesa:", error);
        setErrorMessage("Houve um erro ao buscar o pedido.");
        setPedido(null);
      }
    }
  };

  // Renderiza os detalhes do pedido
  const renderDetalhesPedido = (detalhes: DetalhePedido[]) => {
    return (
      <ul>
        {detalhes.map((detalhe) => (
          <li key={detalhe.id} className="detalhe-item">
            <h4>{detalhe.item?.nome}</h4>
            <p>{detalhe.item?.descricao}</p>
            <p>Preço: R${detalhe.item?.preco.toFixed(2)}</p>
            <p>Quantidade: {detalhe.quantidade}</p>
          </li>
        ))}
      </ul>
    );
  };

  return (
    <div className="container">
      <header>Visualizar um Pedido</header>

      {/* Formulário para o cliente buscar o pedido pelo número da mesa */}
      <div className="busca-pedido">
        <label htmlFor="numeroMesa">Número da Mesa:</label>
        <input
          type="number"
          id="numeroMesa"
          value={numeroMesa}
          onChange={(e) => setNumeroMesa(Number(e.target.value))}
          placeholder="Digite o número da mesa"
        />
        <button onClick={fetchPedidoPorMesa}>Ver Pedido</button>
      </div>

      {/* Exibir erro, caso não tenha pedido para a mesa */}
      {errorMessage && <p className="error-message">{errorMessage}</p>}

      {/* Exibir o pedido da mesa, caso exista */}
      {pedido && (
        <div className="pedido-card">
          <h3>Pedido da Mesa {pedido.numeroMesaCliente}</h3>
          <p>Cliente: {pedido.nomeCliente}</p>
          <p>Data do Pedido: {new Date(pedido.dataPedido).toLocaleString()}</p>
          <p>Chamado Garçom: {pedido.chamadoGarcom ? "Sim" : "Não"}</p>
          <div className="detalhes-pedido">
            <h4>Detalhes do Pedido:</h4>
            {renderDetalhesPedido(pedido.detalhesPedido)}
          </div>
        </div>
      )}
    </div>
  );
};

export default PedidoCliente;
