import ReactDOM from "react-dom";
import { BrowserRouter, Router } from "react-router-dom";
import { App } from "./App";
import { AuthProvider } from "./context/AuthProvider";
import "./styles.css";

ReactDOM.render(
  <BrowserRouter>
    <AuthProvider>
      <App />
    </AuthProvider>
  </BrowserRouter>,
  document.getElementById("root")
);
