import { Box, Modal, Typography } from "@mui/material";
import { FC, useContext, useEffect, useState } from "react";
import { ModalsContext } from "../../context/ModalProvider";
import { Desk } from "../../models/Desk";
import { Floor } from "../../models/Floor";
import FloorGrid, { deskCoordinates } from "../common/floorGrid.tsx/floorGrid";

import "./AddFloorModal.scss";
interface AddFloorModalProps {
  onSubmit: any;
}

export const AddFloorModal: FC<AddFloorModalProps> = ({ onSubmit }) => {
  const modalsContext = useContext(ModalsContext);

  const [desks, setDesks] = useState<Desk[]>([]);
  const [floorName, setFloorName] = useState("");
  const [rows, setRows] = useState(0);
  const [columns, setColumns] = useState(0);
  const [isValid, setIsValid] = useState(false);
  useEffect(() => {
    if (rows === 0 || columns === 0 || desks.length === 0 || floorName === "")
      setIsValid(false);
    else setIsValid(true);
  }, [rows, columns, desks, floorName]);

  const populateDesks = (hoteledDesks: deskCoordinates[]) => {
    let desksList: Desk[] = [];
    for (let i = 0; i < rows; i++) {
      for (let j = 0; j < columns; j++) {
        const isHoteled = hoteledDesks.filter((desk) => {
          return desk.row === i && desk.column === j;
        });

        const desk: Desk = {
          id: 0,
          floorId: 0,
          name: `${i} ${j}`,
          isHotelingDesk: isHoteled.length > 0 ? true : false,
          reserved: false,
        };
        desksList.push(desk);
      }
    }
    setDesks(desksList);
  };
  const saveFloor = () => {
    const floor: Floor = {
      id: 0,
      officeId: 0,
      name: floorName,
      desks: desks,
      rows: rows,
      columns: columns,
    };
    return floor;
  };
  const handleSubmit = () => {
    modalsContext.setAddFloorOpen(false);
    const returnedValue = saveFloor();
    onSubmit(returnedValue, rows, columns);
  };
  return (
    <div className="modal-form">
      <Modal
        open={modalsContext.isAddFloorOpen}
        onClose={() => modalsContext.setAddFloorOpen(false)}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
        className="form"
      >
        <section className="addFloor">
          <button
            className="submitFormButton"
            disabled={!isValid}
            onClick={() => handleSubmit()}
          >
            Save floor
          </button>
          <form>
            <div className="FormField">
              <label htmlFor="floorName">Floor Name: </label>
              <input
                type="text"
                id="floorName"
                onChange={(e) => {
                  setFloorName(e.target.value);
                }}
              />
            </div>
            <div>
              <h2>Note:</h2>
              <span>
                The office will be displayed as a row x column matrix based on
                your input
              </span>
            </div>
          </form>
          <form>
            <div className="file-adding">
              <div className="Field">
                <h2>Location map</h2>
                <span>Chose offices that are permanently booked.</span>
              </div>
            </div>
            <label htmlFor="deskRow">Desk rows</label>
            <input
              type="number"
              id="deskRow"
              min="0"
              max="10"
              value={rows}
              onChange={(e) => {
                const value = parseInt(e.target.value);
                if (value > 10) setRows(10);
                else if (value < 0) setRows(1);
                else setRows(value);
              }}
            />
            <label htmlFor="deskColumn">Desk columns</label>
            <input
              value={columns}
              type="number"
              id="deskColumn"
              min="0"
              max="10"
              onChange={(e) => {
                const value = parseInt(e.target.value);
                if (value > 10) setColumns(10);
                else if (value < 0) setColumns(1);
                else setColumns(value);
              }}
            />
          </form>
          <div>
            <h2>Location map</h2>
            <span>Chose offices that are permanently booked.</span>
          </div>
          <FloorGrid
            rows={rows}
            columns={columns}
            saveDesks={(hoteledDesks: deskCoordinates[]) => {
              populateDesks(hoteledDesks);
            }}
          />
          <div className="btn"></div>
        </section>
      </Modal>
    </div>
  );
};
