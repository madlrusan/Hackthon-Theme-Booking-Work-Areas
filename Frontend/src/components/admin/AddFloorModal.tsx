import { Box, Modal, Typography } from "@mui/material";
import { FC, useContext, useEffect, useState } from "react";
import { ModalsContext } from "../../context/ModalProvider";
import { Desk } from "../../models/Desk";

export const AddFloorModal: FC = () => {
  const modalsContext = useContext(ModalsContext);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    modalsContext.setAddFloorOpen(false);
  };
  const [desks, setDesks] = useState<Desk[]>();
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
          <input type="number" id="deskRow" />
          <label htmlFor="deskColumn">Desk columns</label>
          <input type="number" id="deskColumn" />
        </form>
        <div>
          <h2>Location map</h2>
          <span>Chose offices that are permanently booked.</span>
        </div>
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
