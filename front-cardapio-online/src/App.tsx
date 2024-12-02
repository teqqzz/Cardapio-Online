import React from "react";
import { Link } from "react-router-dom";
import "./styles.css";

const App: React.FC = () => {
  return (
    <div>
      <header>Cardápio Online</header>
      <nav>
        <ul>
          <li>
            <Link to="/">Menu Principal</Link>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default App;
