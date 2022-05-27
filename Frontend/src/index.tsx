import ReactDOM from "react-dom";
import { BrowserRouter, Router } from "react-router-dom";
import { App } from "./App";
import { Header } from "./components/common/header/header";
import { AuthProvider } from "./context/AuthProvider";
import "./styles.css";

ReactDOM.render(
  <>
    <BrowserRouter>
      <AuthProvider>
        <Header />
        <App />
      </AuthProvider>
    </BrowserRouter>
  </>,
  document.getElementById("root")
);
