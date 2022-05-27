import { Route, Routes } from "react-router-dom";
import { Login } from "./components/authentication/Login";
import Layout from "./components/pages/Layout";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="register" element={<Login />} />
        </Route>
      </Routes>
    </>
  );
};

export { App };
