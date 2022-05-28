import { createContext, FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "../api/axios";
import { ApiUrls } from "../components/constants/ApiUrls";

interface AuthProviderProps {
  children: any;
}
type AuthContextType = {
  isAuthenticated: boolean;
  setIsAuthenticated: any;
  login: any;
  userRole: string;
  setUserRole: any;
};
const init: AuthContextType = {
  isAuthenticated: false,
  setIsAuthenticated: () => {},
  login: () => {},
  userRole: "",
  setUserRole: () => {},
};
export const AuthContext = createContext<AuthContextType>(init);

export const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(true);
  const [userRole, setUserRole] = useState("");
  const navigate = useNavigate();
  useEffect(() => {
    const checkIfIsAuthenticated = async () => {
      try {
        await axios.get(ApiUrls.CHECK_SESSION, {
          headers: {
            "Content-Type": "application/json",
          },
          withCredentials: true,
        });
        navigate("/");
        setIsAuthenticated(true);
      } catch (err: any) {
        if (!err?.response) {
          console.log("No Server Response");
        }
        localStorage.clear();
        setIsAuthenticated(false);
        navigate("/login");
      }
    };
    checkIfIsAuthenticated().then(); //FA SA FIE PERIODIC
  }, []);
  const ctx: AuthContextType = {
    isAuthenticated: isAuthenticated,
    setIsAuthenticated: setIsAuthenticated,
    login: (token: string) => {
      setIsAuthenticated(true);
      localStorage.setItem("token", token);
    },
    userRole: userRole,
    setUserRole: setUserRole,
  };
  return <AuthContext.Provider value={ctx}>{children}</AuthContext.Provider>;
};
