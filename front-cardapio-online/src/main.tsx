import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { PedidoProvider } from "./components/PedidoContext";
import TelaInicial from "./pages/TelaInicial/TelaInicial";
import MenuPrincipal from "./pages/MenuPrincipal/MenuPrincipal.tsx";
import CriarPedido from "./pages/Pedido/Pedido";
import ItemDetalhe from "./components/ItemDetalhe.tsx";
import "./styles.css";
import Categorias from "./pages/Categorias/Categorias.tsx";
import Itens from "./pages/Itens/Itens.tsx";
import Admin from "./pages/Admin/Admin.tsx";
import PedidoCliente from "./pages/PedidoCliente/PedidoCliente.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <PedidoProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<TelaInicial />} />
          <Route path="/menu-principal" element={<MenuPrincipal />} />
          <Route path="/pedido" element={<CriarPedido />} />
          <Route path="/itens/:id" element={<ItemDetalhe />} />
          <Route path="/admin" element={<Admin />} />
          <Route path="/admin/categorias" element={<Categorias />} />
          <Route path="/admin/itens" element={<Itens />} />
          <Route path="/cliente/pedidos" element={<PedidoCliente />} />
        </Routes>
      </BrowserRouter>
    </PedidoProvider>
  </React.StrictMode>
);
