import { createContext, FC, useEffect, useState } from "react";
import axios from "../api/axios";
import { ApiUrls } from "../components/constants/ApiUrls";
import { Office } from "../models/Office";

type ProviderContextType = {
  locations: Office[];
  addLocation: any;
};
interface LocationProviderProps {
  children: any;
}
// @ts-ignore
export const LocationContext = createContext<ProviderContextType>(null);

export const LocationProvider: FC<LocationProviderProps> = ({ children }) => {
  const [locations, setLocations] = useState<Office[]>([]);
  useEffect(() => {
    const getLocations = async () => {
      try {
        const response = await axios.get(ApiUrls.GET_OFFICES_WITH_FLOORS);

        setLocations(response.data);
      } catch (err) {
        console.error(err);
      }
    };
    getLocations().then();
  }, []);
  const ctx: ProviderContextType = {
    locations: locations,
    addLocation: (location: Office) => setLocations([...locations, location]),
  };
  return (
    <LocationContext.Provider value={ctx}>{children}</LocationContext.Provider>
  );
};
