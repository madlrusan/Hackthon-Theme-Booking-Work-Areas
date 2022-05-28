import {
  Card,
  List,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemText,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import FloorGrid from "../common/floorGrid.tsx/floorGrid";
import "./Location.scss";

const Floors = [
  { id: 1, name: "Andy" },
  { id: 2, name: "Bob" },
  { id: 3, name: "Tom Hulk" },
  { id: 4, name: "Tom Hank" },
  { id: 5, name: "Audra" },
  { id: 6, name: "Anna" },
  { id: 7, name: "Tom" },
  { id: 8, name: "Tom Riddle" },
  { id: 9, name: "Bolo" },
];
const LocationPage = () => {
  // const params = useParams();
  useEffect(() => {
    // console.log(params.id);
  }, []);
  const [locationName, setLocationName] = useState("");
  const [foundLocation, setFoundLocation] = useState(Floors);

  const filter = (e: any) => {
    const keyword = e.target.value;
    if (keyword !== "") {
      const results = Floors.filter((floor) => {
        return floor.name.toLowerCase().startsWith(keyword.toLowerCase());
      });
      setFoundLocation(results);
    } else {
      setFoundLocation(Floors);
    }
    setLocationName(keyword);
  };
  return (
    <div className="fullscreen">
      <Card className="window">
        <div>
          {/* <h1>{params.id}</h1> */}
          <div className="scrollable-list">
            {/* we need search bar and filter buttons */}
            <List>
              {Floors.map((floor) => (
                <ListItem>
                  <ListItemButton>
                    <ListItemText>{floor.name}</ListItemText>
                  </ListItemButton>
                </ListItem>
              ))}
            </List>
          </div>
        </div>
        <div className="floor-content">
          <FloorGrid rows={10} columns={10} />
        </div>
      </Card>
    </div>
  );
};
export default LocationPage;
