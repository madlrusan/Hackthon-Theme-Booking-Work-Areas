import { FC, useEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import { Header } from "../common/header/header";

const RequireAuthentication: FC = () => {
  const { isAuthenticated } = useAuth();
  const roles = localStorage.getItem("role")?.split(",");

  return isAuthenticated ? (
    <>
      <Header />
      <Outlet />
    </>
  ) : (
    <Navigate to="/login" replace />
  );
};
export default RequireAuthentication;
