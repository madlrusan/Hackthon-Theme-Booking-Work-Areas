import { Grid } from "@mui/material";
import { FC, useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { Desk } from "../../../models/Desk";

interface BookFloorGridProps {
  desks: Desk[];
  rows: number;
  columns: number;
}
interface ItemProps {
  desk: Desk | undefined;
}
const Item: FC<ItemProps> = ({ desk }) => {
  const [isBooked, setIsBooked] = useState<Boolean | null>(
    desk?.reserved || desk?.isHotelingDesk || false
  );

  const bookDesk = () => {};
  return (
    <div
      className={isBooked === true ? " desk booked" : "desk"}
      onClick={() => {
        if (isBooked) {
        } else {
          bookDesk();
        }
      }}
    >
      {desk?.name}
    </div>
  );
};
const BookFloorGrid: FC<BookFloorGridProps> = ({ desks, rows, columns }) => {
  const saveDesks = (bookedDesks: Desk) => {
    console.log(bookedDesks);
  };
  const renderColumns = (row: number, columns: number, desks: Desk[]) => {
    let columnComponents: any = [];
    for (let i = 0; i < columns; i++) {
      if (desks?.length > 0) {
        const desk = desks[row * columns + i];
        console.log(row, columns, i);
        console.log(row * columns + i);
        columnComponents.push(
          <Col key={i} xs="auto">
            <Item key={i} desk={desk} />
          </Col>
        );
      }
    }
    return columnComponents;
  };
  const renderRows = (rows: number, columns: number, desks: Desk[]) => {
    let rowComponents: any = [];
    for (let i = 0; i < rows; i++) {
      rowComponents.push(
        <Row key={i}>
          <div style={{ flexDirection: "row", display: "flex" }}>
            {renderColumns(i, columns, desks)}
          </div>
        </Row>
      );
    }
    return rowComponents;
  };
  return (
    <div style={{ padding: "10px 20px" }}>
      <Grid
        container
        className="grid-container"
        spacing={{ xs: 2, md: 3 }}
        columns={{ xs: 4, sm: 8, md: 12 }}
      >
        {renderRows(rows, columns, desks)}
      </Grid>
    </div>
  );
};

export default BookFloorGrid;
