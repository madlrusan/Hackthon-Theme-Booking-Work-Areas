import { useEffect } from "react";
import { Navigate, Route, Router, Routes } from "react-router-dom";
import AddLocationForm from "./components/admin/AddLocationForm";
import { Login } from "./components/authentication/Login";
import Register from "./components/authentication/Register";
import Layout from "./components/containers/Layout";
import RequireAuthentication from "./components/containers/RequireAuthentication";
import LocationPage from "./components/pages/Location";
import Statistics from "./components/pages/Statistics/Statistics";
const App = () => {
  const roles = localStorage.getItem("role")?.split(",");
  useEffect(() => {
    console.log(roles);
    console.log(roles?.includes("Manager"));
  }, []);

  return (
      <Routes>
        <Route element={<Layout />}>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="/" element={<RequireAuthentication />}>
            <Route path="addLocation" element={<AddLocationForm />} />
            <Route path="location" element={<LocationPage />} />
            <Route path="statistics" element={<Statistics />} />
        </Route>
        </Route>
      </Routes>

  );
};

export { App };
