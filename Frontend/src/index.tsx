import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Router, Routes } from "react-router-dom";
import { App } from "./App";
import { Login } from "./components/authentication/Login";
import Register from "./components/authentication/Register";
import { Header } from "./components/common/header/header";
import { AuthProvider } from "./context/AuthProvider";
import { LocationProvider } from "./context/LocationProvider";
import { ModalsProvider } from "./context/ModalProvider";
import "./styles.css";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <ModalsProvider>
          <LocationProvider>
            <App />
          </LocationProvider>
        </ModalsProvider>
      </AuthProvider>
    </BrowserRouter>
  </React.StrictMode>
);
