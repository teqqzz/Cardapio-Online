import React from "react";
import "./Button.css";

interface ButtonProps {
  children: React.ReactNode;
  onClick: () => void;
  disabled?: boolean;
  className?: string;
}

const Button: React.FC<ButtonProps> = ({ children, onClick, disabled = false, className = "" }) => {
  return (
    <button onClick={onClick} disabled={disabled} className={`custom-button ${className}`}>
      {children}
    </button>
  );
};

export default Button;
