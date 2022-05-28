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
};
const init: AuthContextType = {
  isAuthenticated: false,
  setIsAuthenticated: () => {},
};
export const AuthContext = createContext<AuthContextType>(init);

export const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const navigate = useNavigate();
  useEffect(() => {
    const checkIfIsAuthenticated = async () => {
      try {
        const response = await axios.get(ApiUrls.CHECK_SESSION, {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
          withCredentials: true,
        });
        navigate("/");
      } catch (err: any) {
        if (!err?.response) {
          console.log("No Server Response");
        }
        navigate("/login");
      }
    };
    checkIfIsAuthenticated().then(); //FA SA FIE PERIODIC
  }, []);

  const ctx: AuthContextType = {
    isAuthenticated: isAuthenticated,
    setIsAuthenticated: setIsAuthenticated,
  };
  return <AuthContext.Provider value={ctx}>{children}</AuthContext.Provider>;
};
