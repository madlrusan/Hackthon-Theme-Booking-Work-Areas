import { Box, Modal, Typography } from "@mui/material";
import { FC, useContext, useEffect, useState } from "react";
import { ModalsContext } from "../../context/ModalProvider";
import { Desk } from "../../models/Desk";
import "./AddFloorModal.scss";
export const AddFloorModal: FC = () => {
  const modalsContext = useContext(ModalsContext);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    modalsContext.setAddFloorOpen(false);
  };
  const [desks, setDesks] = useState<Desk[]>();
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
          <form>
            <div className="FormField">
              <label htmlFor="floorName">Floor Name: </label>
              <input type="text" id="floorName" />
            </div>
            <div>
              <h2>Note:</h2>
              <span>
                The office will be displayed as a row x column matrix based on
                your input
              </span>
            </div>
            <div className="FormField">
              <label htmlFor="deskRow">Desk rows: </label>
              <input type="number" id="deskRow" />
            </div>
            <div className="FormField">
              <label htmlFor="deskColumn">Desk columns: </label>
              <input type="number" id="deskColumn" />
            </div>
          </form>
          <div className="file-adding">
            <div className="Field">
              <h2>Location map</h2>
              <span>Chose offices that are permanently booked.</span>
            </div>
            <div className="btn">
              <button className="submitFormButton" disabled={false}>
                Save floor
              </button>
            </div>
          </div>
        </section>
      </Modal>
    </div>
  );
};
