import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useEffect, useRef, useState } from "react";
import { Floor } from "../../models/Floor";
import { FloorElement } from "./FloorElement";
import "./AddLocation.scss";
const AddLocationForm = () => {
  const locationRef = useRef<HTMLInputElement>(null);

  const [location, setLocation] = useState("");
  const [locationFocus, setLocationFocus] = useState(false);
  const [locationValid, setLocationValid] = useState(false);

  const [floors, setFloors] = useState<Floor[]>([
    {
      Id: 0,
      Name: "",
      Map: "",
      OfficeId: 0,
    },
  ]);
  const addNewFloor = () => {
    setFloors([
      ...floors,
      {
        Id: floors.length,
        Name: "Hello",
        Map: "",
        OfficeId: 0,
      },
    ]);
  };
  useEffect(() => {
    locationRef.current?.focus();
  }, []);
  // onMapChange = (file: File) => {
  //   formData.append("file", file);
  // };
  return (
    <div className="fullscreen">
      <div className="form">
        <section className="addForm">
          <h1>Add new office</h1>
          <form>
            <div className="formField">
              <label htmlFor="location">
                Location
                <FontAwesomeIcon
                  icon={faCheck}
                  className={locationValid ? "valid" : "hide"}
                />
                <FontAwesomeIcon
                  icon={faTimes}
                  className={locationValid || !location ? "hide" : "invalid"}
                />
              </label>
              <input
                type="text"
                id="location"
                ref={locationRef}
                onChange={(e) => setLocation(e.target.value)}
                required
                aria-invalid={locationValid}
                aria-describedby="locationnote"
                onFocus={() => {
                  setLocationFocus(true);
                }}
                onBlur={() => {
                  setLocationFocus(false);
                }}
              />
            </div>
          </form>
          <div className="formField">
            <label htmlFor="floor">Floors</label>
            <div className="scrollable">
              {floors.map((floor) => {
                return (
                  <FloorElement
                    key={floor.Id}
                    floor={floor}
                    onClick={async (id) => console.log(id)}
                  />
                );
              })}
            </div>
            <button
              onClick={addNewFloor}
              className="btn button-modal-prim"
              style={{ marginTop: "20px" }}
            >
              <span style={{ fontWeight: "bold" }}>Add new floor</span>
            </button>
          </div>
          <div className="formField">
            <label htmlFor="map">Map</label>
            <input
              type={"file"}
              id="map"
              // onChange={(e) => onMapChange(e.target.files?.[0])}
            />
          </div>
        </section>
      </div>
    </div>
  );
};

export default AddLocationForm;
