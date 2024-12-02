import React from "react";
import "../ItemCard.css";
import Item from "../interfaces/Item";


interface ItemCardProps {
  item: Item;
  onAdd: (item: Item) => void;
}

const ItemCard: React.FC<ItemCardProps> = ({ item, onAdd }) => {
  return (
    <div className="item-card">
      <img src={item.imagemUrl} alt={item.nome} className="item-image" />
      <h3>{item.nome}</h3>
      <p>{item.descricao}</p>
      <p>R${item.preco.toFixed(2)}</p>
      <button onClick={() => onAdd(item)}>Adicionar</button>
    </div>
  );
};

export default ItemCard;
