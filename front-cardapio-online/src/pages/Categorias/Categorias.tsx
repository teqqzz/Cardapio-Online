import React, { useEffect, useState } from "react";
import Categoria from "../../interfaces/Categoria";
import { API_URL } from "../../config/config";

function Categorias() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [nome, setNome] = useState<string>("");
  const [editandoId, setEditandoId] = useState<number | null>(null);
  const [carregando, setCarregando] = useState<boolean>(false);
  const [erro, setErro] = useState<string | null>(null);

  // Carregar categorias ao montar o componente
  useEffect(() => {
    carregarCategorias();
  }, []);

  const carregarCategorias = async () => {
    try {
      setCarregando(true);
      const resposta = await fetch(`${API_URL}/api/categorias`);
      const dados = await resposta.json();
      setCategorias(dados);
    } catch (error) {
      setErro("Erro ao carregar categorias.");
    } finally {
      setCarregando(false);
    }
  };

  const criarCategoria = async () => {
    if (!nome) {
      alert("Preencha o nome da categoria!");
      return;
    }

    try {
      const resposta = await fetch(`${API_URL}/api/categorias`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ nome }),
      });

      if (!resposta.ok) throw new Error("Erro ao criar categoria.");

      setNome("");
      carregarCategorias();
    } catch (error) {
      setErro("Erro ao criar categoria.");
    }
  };

  const editarCategoria = async () => {
    if (!nome || editandoId === null) {
      alert("Preencha o nome da categoria!");
      return;
    }

    try {
      const resposta = await fetch(`${API_URL}/api/categorias/${editandoId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ nome }),
      });

      if (!resposta.ok) throw new Error("Erro ao editar categoria.");

      setNome("");
      setEditandoId(null);
      carregarCategorias();
    } catch (error) {
      setErro("Erro ao editar categoria.");
    }
  };

  const excluirCategoria = async (id: number) => {
    if (!window.confirm("Deseja realmente excluir esta categoria?")) return;

    try {
      const resposta = await fetch(`${API_URL}/api/categorias/${id}`, {
        method: "DELETE",
      });

      if (!resposta.ok) throw new Error("Erro ao excluir categoria.");

      carregarCategorias();
    } catch (error) {
      setErro("Erro ao excluir categoria.");
    }
  };

  const carregarCategoriaParaEditar = async (id: number) => {
    try {
      const resposta = await fetch(`${API_URL}/api/categorias/${id}`);
      if (!resposta.ok) throw new Error("Erro ao carregar categoria.");
      const categoria = await resposta.json();
      setNome(categoria.nome);
      setEditandoId(categoria.id);
    } catch (error) {
      setErro("Erro ao carregar categoria para edição.");
    }
  };

  return (
    <div>
      <h1>Gerenciamento de Categorias</h1>

      {erro && <p style={{ color: "red" }}>{erro}</p>}

      {/* Formulário para criar/editar categoria */}
      <div>
        <h2>{editandoId ? "Editar Categoria" : "Nova Categoria"}</h2>
        <input
          type="text"
          placeholder="Nome da Categoria"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
        <button onClick={editandoId ? editarCategoria : criarCategoria}>
          {editandoId ? "Salvar Alterações" : "Adicionar"}
        </button>
        {editandoId && (
          <button onClick={() => setEditandoId(null)}>Cancelar</button>
        )}
      </div>

      {/* Tabela de categorias */}
      <h2>Lista de Categorias</h2>
      {carregando ? (
        <p>Carregando...</p>
      ) : (
        <table border="1" cellPadding="8" cellSpacing="0">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
              <th>Criado Em</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {categorias.map((categoria) => (
              <tr key={categoria.id}>
                <td>{categoria.id}</td>
                <td>{categoria.nome}</td>
                <td>{new Date(categoria.criadoEm).toLocaleString()}</td>
                <td>
                  <button
                    onClick={() => carregarCategoriaParaEditar(categoria.id)}
                  >
                    Editar
                  </button>
                  <button onClick={() => excluirCategoria(categoria.id)}>
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Categorias;
