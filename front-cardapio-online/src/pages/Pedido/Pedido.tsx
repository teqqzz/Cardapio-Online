import { useEffect, useState } from "react";
import { API_URL } from "../../config/config";
import Item from "../../interfaces/Item";
import Pedido from "../../interfaces/Pedido";
import DetalhePedido from "../../interfaces/DetalhePedido";

function CriarPedido() {
  const [itens, setItens] = useState<Item[]>([]);
  const [pedido, setPedido] = useState<Pedido | null>(null);
  const [detalhesPedido, setDetalhesPedido] = useState<DetalhePedido[]>([]);
  const [total, setTotal] = useState<number>(0);
  const [nomeCliente, setNomeCliente] = useState("");
  const [numeroMesa, setNumeroMesa] = useState<number | null>(null);
  const [clienteNome, setClienteNome] = useState<string>("");
  const [quantidades, setQuantidades] = useState<{ [itemId: number]: number }>(
    {}
  );

  // Carrega os itens ao montar o componente
  useEffect(() => {
    fetch(`${API_URL}/api/Itens`)
      .then((res) => res.json())
      .then((dados) => setItens(dados));
  }, []);

  // Atualiza o total do pedido quando os detalhes do pedido mudam
  useEffect(() => {
    if (pedido) {
      fetch(`${API_URL}/api/pedidos/${pedido.id}/total`)
        .then((res) => res.json())
        .then((valor) => setTotal(valor))
        .catch((error) => console.error("Erro ao calcular o total:", error));
    }
  }, [pedido, detalhesPedido]);

  // Função para criar um pedido
  const criarPedido = () => {
    if (!nomeCliente || !numeroMesa) {
      alert("Preencha o nome do cliente e número da mesa!");
      return;
    }

    fetch(`${API_URL}/api/pedidos`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nomeCliente,
        numeroMesaCliente: numeroMesa,
      }),
    })
      .then((res) => res.json())
      .then((novoPedido) => setPedido(novoPedido))
      .catch((error) => console.error("Erro ao criar pedido:", error));
  };

  // Função para adicionar um detalhe ao pedido
  const adicionarDetalhe = (itemId: number) => {
    if (!pedido) {
      alert("Crie o pedido antes de adicionar itens!");
      return;
    }

    const quantidade = quantidades[itemId] || 1;

    fetch(`${API_URL}/api/pedidos/adicionar-detalhe`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        pedidoId: pedido.id,
        itemId,
        quantidade,
      }),
    })
      .then((res) => res.json())
      .then((novoDetalhe) => {
        setDetalhesPedido(novoDetalhe);
        setQuantidades((prev) => ({ ...prev, [itemId]: 1 })); // Reseta a quantidade para 1
      })
      .catch((error) => console.error("Erro ao adicionar detalhe:", error));
  };

  // Chamar o garçom
  const chamarGarcom = () => {
    if (!pedido) {
      alert("Crie o pedido antes de chamar o garçom!");
      return;
    }
    console.log(pedido.id);
    fetch(`${API_URL}/api/pedidos/${pedido.id}/chamar-garcom`, {
      method: "PUT",
    })
      .then(() => alert("Garçom chamado com sucesso!"))
      .catch((error) => console.error("Erro ao chamar garçom:", error));
  };

  return (
    <div>
      <header>Realizar Pedido</header>

      {!pedido && (
        <div>
          <input
            type="text"
            placeholder="Nome do Cliente"
            value={nomeCliente}
            onChange={(e) => setNomeCliente(e.target.value)}
          />
          <input
            type="number"
            placeholder="Número da Mesa"
            value={numeroMesa || ""}
            onChange={(e) => setNumeroMesa(Number(e.target.value))}
          />
          <button onClick={criarPedido}>Criar Pedido</button>
        </div>
      )}

      {pedido && (
        <>
          <h2>Itens Disponíveis</h2>
          <ul>
            {itens.map((item) => (
              <li key={item.id}>
                {item.nome} - R${item.preco.toFixed(2)}{" "}
                <input
                  type="number"
                  min="1"
                  value={quantidades[item.id] || 1}
                  onChange={(e) =>
                    setQuantidades({
                      ...quantidades,
                      [item.id]: Math.max(1, Number(e.target.value)),
                    })
                  }
                />
                <button onClick={() => adicionarDetalhe(item.id)}>
                  Adicionar ao Pedido
                </button>
              </li>
            ))}
          </ul>

          <h2>Resumo do Pedido</h2>
          <p>Cliente: {pedido.nomeCliente}</p>
          <p>Mesa: {pedido.numeroMesaCliente}</p>
          <p>Total: R${total.toFixed(2)}</p>
          <ul>
            {detalhesPedido.map((detalhe) => (
              <li key={detalhe.id}>
                {detalhe.item.nome} - Quantidade: {detalhe.quantidade} - Preço:{" "}
                R${(detalhe.item.preco * detalhe.quantidade).toFixed(2)}
              </li>
            ))}
          </ul>

          <div>
            <button onClick={chamarGarcom}>Chamar Garçom</button>
          </div>
        </>
      )}
    </div>
  );
}

export default CriarPedido;
