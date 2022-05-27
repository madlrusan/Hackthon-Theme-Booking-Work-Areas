import { createContext, FC, useState } from "react";

interface AuthProviderProps {
  children: any;
}
type AuthContextType = {
  auth: any;
  setAuth: any;
};
const init: AuthContextType = {
  auth: {},
  setAuth: () => {},
};
export const AuthContext = createContext<AuthContextType>(init);

export const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
  const [auth, setAuth] = useState({});
  const ctx: AuthContextType = {
    auth: auth,
    setAuth: setAuth,
  };
  return <AuthContext.Provider value={ctx}>{children}</AuthContext.Provider>;
};
