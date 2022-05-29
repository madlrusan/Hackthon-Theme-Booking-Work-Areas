import {
  Card,
  List,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemText,
} from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "../../api/axios";
import { LocationContext } from "../../context/LocationProvider";
import BookFloorGrid from "../common/floorGrid.tsx/bookFloorGrid";
import FloorGrid from "../common/floorGrid.tsx/floorGrid";
import { ApiUrls } from "../constants/ApiUrls";
import "./Location.scss";

const LocationPage = () => {
  const { selectedLocation } = useContext(LocationContext);
  const [selectedFloor, setSelectedFloor] = useState(
    selectedLocation.floors[0]
  );
  const getLocation = async () => {
    try {
      return await axios.get(`${ApiUrls.GET_FLOORBYID}${selectedFloor.id}`);
    } catch (err) {
      console.error(err);
    }
  };
  const onFloorClick = async () => {
    getLocation().then((r) => {
      console.log(r);
    });
    // setSelectedFloor(floor);
  };

  useEffect(() => {
    getLocation().then((r) => {
      setSelectedFloor(r?.data);
    });
    // setSelectedFloor(response);
  }, []);
  // const filter = (e: any) => {
  //   const keyword = e.target.value;
  //   if (keyword !== "") {
  //     const results = Floors.filter((floor) => {
  //       return floor.name.toLowerCase().startsWith(keyword.toLowerCase());
  //     });
  //     setFoundLocation(results);
  //   } else {
  //     setFoundLocation(Floors);
  //   }
  //   setLocationName(keyword);
  // };
  return (
    <div className="fullscreen">
      <Card className="window">
        <div>
          <h1>{selectedLocation.name}</h1>
          <div className="scrollable-list">
            {/* we need search bar and filter buttons */}
            <List>
              {selectedLocation.floors.map((floor) => (
                <ListItem key={floor.id}>
                  <ListItemButton>
                    <ListItemText onClick={() => onFloorClick()}>
                      {floor.name}
                    </ListItemText>
                  </ListItemButton>
                </ListItem>
              ))}
            </List>
          </div>
        </div>
        <BookFloorGrid
          rows={selectedFloor.rows}
          columns={selectedFloor.columns}
          desks={selectedFloor.desks}
        />
      </Card>
    </div>
  );
};
export default LocationPage;
