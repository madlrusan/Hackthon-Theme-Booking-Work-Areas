import { FC } from "react";
import { Floor } from "../../models/Floor";

interface FloorElementProps {
  floor: Floor;
  onClick: (id: number) => {};
}
export const FloorElement: FC<FloorElementProps> = ({ floor, onClick }) => {
  return (
    <div>
      <span>Floor: {floor.name} </span>
      <span>Number of desks: {floor.desks.length} </span>
    </div>
  );
};
