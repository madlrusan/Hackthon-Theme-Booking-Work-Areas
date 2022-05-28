import { Paper, styled } from "@mui/material";
import { FC } from "react";
import { Floor } from "../../models/Floor";

interface FloorElementProps {
  floor: Floor;
}
const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#C4DBF4',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
  border: theme.palette.info.dark,
}));
export const FloorElement: FC<FloorElementProps> = ({ floor }) => {
  return (
    // <div>
    //   <span>Floor: {floor.name} </span>
    //   <span>Number of desks: {floor.desks.length} </span>
    // </div>
    <>
    <Item style={{color: '#003973'}}><em>Floor: </em> {floor.name} <br/><em> Number of desks:</em> {floor.desks.length}</Item>
    </>
  );
};
