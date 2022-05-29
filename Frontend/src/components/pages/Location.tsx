import {
  Card,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
} from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import axios from "../../api/axios";
import { LocationContext } from "../../context/LocationProvider";
import BookFloorGrid from "../common/floorGrid.tsx/bookFloorGrid";
import { ApiUrls } from "../constants/ApiUrls";
import "./Location.scss";

const LocationPage = () => {
  const navigate = useNavigate();
  const { selectedLocation } = useContext(LocationContext);
  const [selectedFloor, setSelectedFloor] = useState(
    selectedLocation.floors[0]
  );
  const [reservedDays, setReservedDays] = useState(0);
  const [selectedDeskId, setSelectedDeskId] = useState(0);

  useEffect(() => {
    getLocation(selectedLocation?.floors[0]?.id).then((r) => {
      setSelectedFloor(r?.data);
    });
  }, [selectedLocation]);
  useEffect(() => {
    getLocation(selectedFloor?.id).then((r) => {
      setSelectedFloor(r?.data);
    });
  }, []);

  const onSubmit = async () => {
    try {
      await axios.post(
        ApiUrls.RESERVATION,
        JSON.stringify({ deskId: selectedDeskId, numberOfDays: reservedDays })
      );
      navigate("/");
    } catch (err) {}
  };
  const getLocation = async (floorID: number) => {
    try {
      return await axios.get(`${ApiUrls.GET_FLOORBYID}${floorID}`);
    } catch (err) {
      console.error(err);
    }
  };
  const onFloorClick = async (floorID: number) => {
    getLocation(floorID).then((r) => {
      setSelectedFloor(r?.data);
    });
  };

  return (
    <div className="fullscreen">
      <Card className="window">
        <div>
          <h1>{selectedLocation.name}</h1>
          <div className="scrollable-list">
            <List>
              {selectedLocation.floors.map((floor) => (
                <ListItem
                  key={floor.id}
                  onClick={() => onFloorClick(floor.id)}
                  style={{ width: "500px" }}
                >
                  <ListItemButton
                    style={{ backgroundColor: "#4a3211", borderRadius: "10%" }}
                  >
                    <ListItemText>{floor.name}</ListItemText>
                  </ListItemButton>
                </ListItem>
              ))}
            </List>
          </div>
          <form>
            <div style={{ display: "flex", flexDirection: "column" }}>
              <label style={{ color: "#efc070" }} htmlFor="daysNr">
                How many days you want to book?
              </label>
              <input
                id="daysNr"
                type="number"
                style={{ width: "100px" }}
                onChange={(e) => {
                  const value = parseInt(e.target.value);
                  if (value === null) setReservedDays(0);
                  else {
                    setReservedDays(value);
                  }
                }}
              />
            </div>

            <button
              style={{ marginTop: "20px" }}
              disabled={selectedDeskId === 0 && reservedDays === 0}
              onClick={() => onSubmit()}
            >
              Book desk
            </button>
          </form>
        </div>
        {selectedFloor !== undefined && (
          <BookFloorGrid
            rows={selectedFloor.rows}
            columns={selectedFloor.columns}
            desks={selectedFloor.desks}
            saveDesk={(id: number) => setSelectedDeskId(id)}
          />
        )}
      </Card>
    </div>
  );
};
export default LocationPage;
