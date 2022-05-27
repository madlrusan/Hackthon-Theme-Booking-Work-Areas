import { FC } from "react";
import { Floor } from "../../models/Floor";

interface FloorElementProps {
  floor: Floor;
  onClick: (id: number) => {};
}
export const FloorElement: FC<FloorElementProps> = ({ floor, onClick }) => {
  return (
    <div
      onClick={() => {
        onClick(floor.Id);
      }}
    >
      {floor.Name}
    </div>
  );
};
