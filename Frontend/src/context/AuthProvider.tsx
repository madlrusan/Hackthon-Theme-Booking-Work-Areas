import { createContext, FC, useState } from "react";

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
  const ctx: AuthContextType = {
    isAuthenticated: isAuthenticated,
    setIsAuthenticated: setIsAuthenticated,
  };
  return <AuthContext.Provider value={ctx}>{children}</AuthContext.Provider>;
};
