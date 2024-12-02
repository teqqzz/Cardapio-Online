import React, { useEffect, useState } from "react";
import { API_URL } from "../../config/config";
import Pedido from "../../interfaces/Pedido";
import DetalhePedido from "../../interfaces/DetalhePedido";
import "./Admin.css";
import { Link } from "react-router-dom";
const Admin: React.FC = () => {
  const [pedidos, setPedidos] = useState<Pedido[]>([]);
  const [numeroMesa, setNumeroMesa] = useState<number>(0);
  const [pedidoMesa, setPedidoMesa] = useState<Pedido | null>(null);

  // Carregar pedidos de todos os clientes
  useEffect(() => {
    const fetchPedidos = async () => {
      try {
        const response = await fetch(`${API_URL}/api/pedidos`);
        const data = await response.json();
        setPedidos(data);
      } catch (error) {
        console.error("Erro ao carregar os pedidos:", error);
      }
    };

    fetchPedidos();
  }, []);

  // Carregar pedidos pelo número da mesa
  const fetchPedidosPorMesa = async () => {
    if (numeroMesa) {
      try {
        const response = await fetch(
          `${API_URL}/api/pedidos/mesa/${numeroMesa}`
        );
        const data = await response.json();
        setPedidoMesa(data[0] || null); // Exibe o primeiro pedido da mesa
      } catch (error) {
        console.error("Erro ao carregar os pedidos da mesa:", error);
      }
    }
  };

  // Exibição dos detalhes do pedido
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
      <header>Pedidos</header>
      <nav>
        <ul>
          <li>
            <Link to="/admin/categorias">Gerenciar Categorias</Link>
          </li>
          <li>
            <Link to="/admin/itens">Gerenciar Pratos</Link>
          </li>
          <li>
            <Link to="/">Tela Inicial</Link>
          </li>
        </ul>
      </nav>
      {/* Formulário para buscar pedido pela mesa */}
      <div className="busca-pedido">
        <label htmlFor="numeroMesa">Número da Mesa:</label>
        <input
          type="number"
          id="numeroMesa"
          value={numeroMesa}
          onChange={(e) => setNumeroMesa(Number(e.target.value))}
          placeholder="Digite o número da mesa"
        />
        <button onClick={fetchPedidosPorMesa}>Buscar Pedidos</button>
      </div>

      <div className="pedidos">
        {/* Exibir o pedido da mesa no topo, se existir */}
        {pedidoMesa && (
          <div className="pedido-card">
            <h3>Pedido da Mesa {pedidoMesa.numeroMesaCliente}</h3>
            <p>Cliente: {pedidoMesa.nomeCliente}</p>
            <p>
              Data do Pedido: {new Date(pedidoMesa.dataPedido).toLocaleString()}
            </p>
            <p>Chamado Garçom: {pedidoMesa.chamadoGarcom ? "Sim" : "Não"}</p>
            <div className="detalhes-pedido">
              <h4>Detalhes do Pedido:</h4>
              {renderDetalhesPedido(pedidoMesa.detalhesPedido)}
            </div>
          </div>
        )}

        {/* Exibir todos os outros pedidos abaixo */}
        <h1>Todos os Pedidos</h1>
        <div className="todos-pedidos">
          {pedidos.length > 0 ? (
            pedidos.map((pedido) => (
              <div key={pedido.id} className="pedido-card">
                <h3>Pedido de {pedido.nomeCliente}</h3>
                <p>Mesa: {pedido.numeroMesaCliente}</p>
                <p>
                  Data do Pedido: {new Date(pedido.dataPedido).toLocaleString()}
                </p>
                <p>Chamado Garçom: {pedido.chamadoGarcom ? "Sim" : "Não"}</p>
                <div className="detalhes-pedido">
                  <h4>Detalhes do Pedido:</h4>
                  {renderDetalhesPedido(pedido.detalhesPedido)}
                </div>
              </div>
            ))
          ) : (
            <p>Não há pedidos para exibir.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default Admin;
