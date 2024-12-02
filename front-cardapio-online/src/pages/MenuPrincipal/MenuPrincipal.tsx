import { useState, useEffect } from "react";
import { API_URL } from "../../config/config";
import Item from "../../interfaces/Item";
import "../../styles.css"; // Certifique-se de que o caminho está correto

const MenuPrincipal = () => {
  const [itens, setItens] = useState<Item[]>([]);

  useEffect(() => {
    fetch(`${API_URL}/api/itens?categoriaId=1`) // Supondo categoriaId=1 para Menu Principal
      .then((res) => res.json())
      .then((data) => setItens(data))
      .catch((error) => console.error("Erro ao carregar itens:", error));
  }, []);

  const adicionarAoPedido = (itemId: number) => {
    console.log(`Item ${itemId} adicionado ao pedido.`);
  };

  return (
    <div>
      <h1>Menu Principal</h1>
      <ul>
        {itens.map((item) => (
          <li key={item.id}>
            <img
              src={`${API_URL}${item.imagemUrl}`}
              alt={item.nome}
              style={{ width: "100%", borderRadius: "10px" }}
            />
            <h3>{item.nome}</h3>
            <p>{item.descricao}</p>
            <p>Preço: R${item.preco.toFixed(2)}</p>
            <button onClick={() => adicionarAoPedido(item.id)}>Adicionar</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default MenuPrincipal;
