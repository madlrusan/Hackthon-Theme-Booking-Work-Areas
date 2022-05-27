import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../../hooks/useAuth";

const RequireAuthentication = () => {
  const authenticationContext = useAuth();

  return authenticationContext.isAuthenticated ? (
    <Outlet />
  ) : (
    <Navigate to="/login" replace />
  );
};
export default RequireAuthentication;
