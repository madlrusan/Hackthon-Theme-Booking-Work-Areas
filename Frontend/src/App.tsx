import { useEffect } from "react";
import { Route, Router, Routes } from "react-router-dom";
import AddLocationForm from "./components/admin/AddLocationForm";
import { Login } from "./components/authentication/Login";
import Register from "./components/authentication/Register";
import Layout from "./components/containers/Layout";
import RequireAuthentication from "./components/containers/RequireAuthentication";
import LocationPage from "./components/pages/Location";

const App = () => {
  const roles = localStorage.getItem("role")?.split(",");
  return (
    <>
      <Routes>
        <Route element={<Layout />}>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="/" element={<RequireAuthentication />}>
            {roles?.includes("Manager") ? (
              <>
                <Route path="addLocation" element={<AddLocationForm />} />
              </>
            ) : (
              <></>
            )}
            <Route path="location" element={<LocationPage />} />
          </Route>
        </Route>
      </Routes>
    </>
  );
};

export { App };
