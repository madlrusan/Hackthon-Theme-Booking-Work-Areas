import { useEffect } from "react";
import { useParams } from "react-router-dom";

const LocationPage = () => {
  const params = useParams();
  useEffect(() => {
    console.log(params.id);
  }, []);
  return <div>{params.id}</div>;
};
export default LocationPage;
