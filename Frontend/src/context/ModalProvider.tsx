import { createContext, FC, useState } from "react";

type ModalsContextType = {
  isAddFloorOpen: boolean;
  setAddFloorOpen: any;
};
interface ModalsProviderProps {
  children: any;
}
// @ts-ignore
export const ModalsContext = createContext<ModalsContextType>(null);

export const ModalsProvider: FC<ModalsProviderProps> = ({ children }) => {
  const [isAddFloorOpen, setAddFloorOpen] = useState(false);

  const ctx: ModalsContextType = {
    isAddFloorOpen: isAddFloorOpen,
    setAddFloorOpen: setAddFloorOpen,
  };
  return (
    <ModalsContext.Provider value={ctx}>{children}</ModalsContext.Provider>
  );
};
