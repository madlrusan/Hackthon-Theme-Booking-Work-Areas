import { createContext, FC, useEffect, useState } from "react";
import axios from "../api/axios";
import { ApiUrls } from "../components/constants/ApiUrls";
import { Office } from "../models/Office";

type ProviderContextType = {
  locations: Office[];
  addLocation: any;
  selectedLocation: Office;
  setSelectedLocation: any;
  getLocations: any;
};
interface LocationProviderProps {
  children: any;
}
// @ts-ignore
export const LocationContext = createContext<ProviderContextType>(null);

export const LocationProvider: FC<LocationProviderProps> = ({ children }) => {
  const [locations, setLocations] = useState<Office[]>([]);
  const [selectedLocation, setSelectedLocation] = useState<Office>({
    id: 0,
    name: "",
    floors: [],
  });
  const getLocations = async () => {
    try {
      const response = await axios.get(ApiUrls.GET_OFFICES_WITH_FLOORS);

      setLocations(response.data);
    } catch (err) {
      console.error(err);
    }
  };
  useEffect(() => {
    getLocations().then();
  }, []);
  const ctx: ProviderContextType = {
    locations: locations,
    addLocation: (location: Office) => setLocations([...locations, location]),
    selectedLocation: selectedLocation,
    setSelectedLocation: (location: Office) => {
      setSelectedLocation(location);
    },
    getLocations: () => getLocations().then(),
  };
  return (
    <LocationContext.Provider value={ctx}>{children}</LocationContext.Provider>
  );
};
