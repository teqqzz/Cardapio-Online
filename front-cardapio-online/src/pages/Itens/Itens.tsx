import React, { useState, useEffect } from "react";
import { API_URL } from "../../config/config";
import Item from "../../interfaces/Item";
import Categoria from "../../interfaces/Categoria";

function Itens() {
  const [itens, setItens] = useState<Item[]>([]);
  const [nome, setNome] = useState<string>("");
  const [descricao, setDescricao] = useState<string>("");
  // const [ingredientes, setIngredientes] = useState<string>("");
  const [preco, setPreco] = useState<number | string>("");
  const [categoriaId, setCategoriaId] = useState<number | string>("");
  const [imagem, setImagem] = useState<File | null>(null);
  const [editandoId, setEditandoId] = useState<number | null>(null);
  const [carregando, setCarregando] = useState<boolean>(false);
  const [erro, setErro] = useState<string | null>(null);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  useEffect(() => {
    carregarItens();
    carregarCategorias();
  }, []);

  const carregarItens = async () => {
    try {
      setCarregando(true);
      const resposta = await fetch(`${API_URL}/api/itens`);
      const dados = await resposta.json();
      setItens(dados);
    } catch (error) {
      setErro("Erro ao carregar itens.");
    } finally {
      setCarregando(false);
    }
  };

  const carregarCategorias = async () => {
    try {
      const resposta = await fetch(`${API_URL}/api/categorias`);
      const dados = await resposta.json();
      setCategorias(dados);
    } catch (error) {
      setErro("Erro ao carregar categorias.");
    }
  };

  const criarOuEditarItem = async () => {
    //quando editando, imagem não é obrigatória
    if (!editandoId) {
      if (!nome || !preco || !categoriaId || !imagem) {
        alert("Preencha os campos obrigatórios!");
        return;
      }
    } else {
      if (!nome || !preco || !categoriaId) {
        alert("Preencha os campos obrigatórios!");
        return;
      }
    }

    try {
      const formData = new FormData();
      formData.append("nome", nome);
      formData.append("descricao", descricao);
      formData.append("preco", preco.toString());
      formData.append("categoriaId", categoriaId.toString());
      // @ts-expect-error: Imagem é nula quando editando, e isso não em nada afeta a requisição
      formData.append("imagem", imagem); 
      // formData.append("ingredientes", ingredientes);

      const url = editandoId
        ? `${API_URL}/api/itens/${editandoId}`
        : `${API_URL}/api/itens`;
      const method = editandoId ? "PUT" : "POST";

      const resposta = await fetch(url, {
        method,
        body: formData,
      });

      if (!resposta.ok) throw new Error("Erro ao salvar item.");

      limparFormulario();
      carregarItens();
    } catch (error) {
      setErro("Erro ao salvar item.");
    }
  };

  const excluirItem = async (id: number) => {
    if (!window.confirm("Deseja realmente excluir este item?")) return;

    try {
      const resposta = await fetch(`${API_URL}/api/itens/${id}`, {
        method: "DELETE",
      });

      if (!resposta.ok) throw new Error("Erro ao excluir item.");

      carregarItens();
    } catch (error) {
      setErro("Erro ao excluir item.");
    }
  };

  const carregarItemParaEditar = (item: Item) => {
    setNome(item.nome);
    setDescricao(item.descricao || "");
    // setIngredientes(item.ingredientes || "");
    setPreco(item.preco);
    setCategoriaId(item.categoriaId);
    setEditandoId(item.id);
  };

  const limparFormulario = () => {
    setNome("");
    setDescricao("");
    // setIngredientes("");
    setPreco("");
    setCategoriaId("");
    setImagem(null);
    setEditandoId(null);
  };

  return (
    <div>
      <h1>Gerenciamento de Itens</h1>

      {erro && <p style={{ color: "red" }}>{erro}</p>}

      <div>
        <h2>{editandoId ? "Editar Item" : "Novo Item"}</h2>
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
        <textarea
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        ></textarea>
        {/* <textarea
          placeholder="Ingredientes"
          value={ingredientes}
          onChange={(e) => setIngredientes(e.target.value)}
        ></textarea> */}
        <input
          type="number"
          placeholder="Preço"
          step="any"
          value={preco || ""}
          onChange={(e) => setPreco(e.target.value)}
        />
        <select
          value={categoriaId || ""}
          onChange={(e) => setCategoriaId(e.target.value)}
        >
          <option value="" disabled>
            Selecione uma Categoria
          </option>
          {categorias.map((categoria) => (
            <option key={categoria.id} value={categoria.id}>
              {categoria.nome}
            </option>
          ))}
        </select>
        <input
          type="file"
          onChange={(e) => setImagem(e.target.files ? e.target.files[0] : null)}
        />
        <button onClick={criarOuEditarItem}>
          {editandoId ? "Salvar Alterações" : "Adicionar"}
        </button>
        {editandoId && <button onClick={limparFormulario}>Cancelar</button>}
      </div>

      <h2>Lista de Itens</h2>
      {carregando ? (
        <p>Carregando...</p>
      ) : (
        <table border="1" cellPadding="8" cellSpacing="0">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
              <th>Preço</th>
              <th>Categoria</th>
              <th>Descrição</th>
              {/* <th>Ingredientes</th> */}
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {itens.map((item) => (
              <tr key={item.id}>
                <td>{item.id}</td>
                <td>{item.nome}</td>
                <td>R${item.preco.toFixed(2)}</td>
                <td>{item.categoria.nome}</td>
                <td>{item.descricao}</td>
                {/* <td>{item.ingredientes}</td> */}
                <td>
                  <button onClick={() => carregarItemParaEditar(item)}>
                    Editar
                  </button>
                  <button onClick={() => excluirItem(item.id)}>Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Itens;
