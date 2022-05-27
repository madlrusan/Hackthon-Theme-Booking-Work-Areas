import { useEffect } from "react";
import { Route, Router, Routes } from "react-router-dom";
import { Login } from "./components/authentication/Login";
import Register from "./components/authentication/Register";
import Layout from "./components/containers/Layout";
import RequireAuthentication from "./components/containers/RequireAuthentication";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
        </Route>
      </Routes>
    </>
  );
};

export { App };
