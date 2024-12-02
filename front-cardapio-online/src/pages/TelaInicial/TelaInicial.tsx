import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { API_URL } from "../../config/config";
import Categoria from "../../interfaces/Categoria";
import "../../styles.css";
import "./TelaInicial.css";

const TelaInicial: React.FC = () => {
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  useEffect(() => {
    // Carregar as categorias e seus itens
    const fetchCategorias = async () => {
      try {
        const categoriasData = await fetch(`${API_URL}/api/categorias`).then(
          (res) => res.json()
        );
        setCategorias(categoriasData);
      } catch (error) {
        console.error("Erro ao carregar categorias:", error);
      }
    };

    fetchCategorias();
  }, []);

  return (
    <div className="container">
      {/* Cabeçalho */}
      <header>BEM-VINDO AO CARDÁPIO ONLINE</header>

      {/* Botões de navegação */}
      <nav>
        <ul>
          <li>
            <Link to="/admin">Área do Garçom</Link>
          </li>
          <li>
            <Link to="/cliente/pedidos">Área do Cliente</Link>
          </li>
          <li>
            <Link to="/pedido">Fazer um Pedido</Link>
          </li>
        </ul>
      </nav>

      {/* Exibição das categorias e seus itens */}
      <div className="content">
        {categorias.map((categoria) => (
          <section key={categoria.id}>
            <h2>{categoria.nome}</h2>
            <ul className="item-grid">
              {categoria.itens.map((item) => (
                <li key={item.id} className="item-card">
                  {/* Exibição da imagem */}
                  <img
                    src={`${API_URL}${item.imagemUrl}`}
                    alt={item.nome}
                    className="item-image"
                  />
                  <h3>{item.nome}</h3>
                  <p>{item.descricao}</p>
                  <p>Preço: R${item.preco.toFixed(2)}</p>
                </li>
              ))}
            </ul>
          </section>
        ))}
      </div>
    </div>
  );
};

export default TelaInicial;
