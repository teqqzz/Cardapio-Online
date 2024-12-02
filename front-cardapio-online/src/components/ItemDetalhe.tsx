import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { API_URL } from "../config/config";
import Item from "../interfaces/Item";

const ItemDetalhe: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [item, setItem] = useState<Item | null>(null);

  useEffect(() => {
    fetch(`${API_URL}/api/itens/${id}`)
      .then((res) => {
        if (!res.ok) {
          throw new Error("Erro ao buscar item");
        }
        return res.json();
      })
      .then((data) => setItem(data))
      .catch((error) => console.error(error.message));
  }, [id]);

  if (!item) {
    return <p>Carregando detalhes do item...</p>;
  }

  return (
    <div>
      <h1>{item.nome}</h1>
      <img src={item.imagemUrl} alt={item.nome} style={{ width: "300px", height: "300px" }} />
      <p>{item.descricao}</p>
      <p>Ingredientes: {item.ingredientes}</p>
      <p>Preço: R${item.preco.toFixed(2)}</p>
    </div>
  );
};

export default ItemDetalhe;
