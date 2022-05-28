import { Box, Modal, Typography } from "@mui/material";
import { FC, useContext, useEffect, useState } from "react";
import { ModalsContext } from "../../context/ModalProvider";
import { Desk } from "../../models/Desk";
import FloorGrid, { deskCoordinates } from "../common/floorGrid.tsx/floorGrid";

export const AddFloorModal: FC = () => {
  const modalsContext = useContext(ModalsContext);
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    modalsContext.setAddFloorOpen(false);
  };
  const [desks, setDesks] = useState<Desk[]>();

  const [rows, setRows] = useState(0);
  const [columns, setColumns] = useState(0);
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
  return (
    <Modal
      open={modalsContext.isAddFloorOpen}
      onClose={() => modalsContext.setAddFloorOpen(false)}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <form>
          <label htmlFor="floorName">Floor Name</label>
          <input type="text" id="floorName" />
          <div>
            <h2>Note:</h2>
            <span>
              The office will be displayed as a row x column matrix based on
              your input
            </span>
          </div>
          <label htmlFor="deskRow">Desk rows</label>
          <input
            type="number"
            id="deskRow"
            max-value="10"
            onChange={(e) => {
              setRows(parseInt(e.target.value));
            }}
          />
          <label htmlFor="deskColumn">Desk columns</label>
          <input
            type="number"
            id="deskColumn"
            max-value="10 "
            onChange={(e) => {
              setColumns(parseInt(e.target.value));
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
        <div className="btn">
          <button className="submitFormButton" disabled={false}>
            Save floor
          </button>
        </div>
      </Box>
    </Modal>
  );
};

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  height: "70vh",
  width: "70vw",
};
