import { useEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import { Header } from "../common/header/header";

const RequireAuthentication = () => {
  const { isAuthenticated } = useAuth();

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
