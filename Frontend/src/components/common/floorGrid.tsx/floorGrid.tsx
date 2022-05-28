import React, { FC, useEffect, useState } from "react";
import styled from "styled-components";
import "./floorGrid.scss";
import { Container, Row, Col } from "react-bootstrap";
import { Grid } from "@mui/material";
// import "bootstrap/dist/css/bootstrap.min.css";

type FloorGridProps = {
  rows: number;
  columns: number;
  saveDesks: any;
};
type ItemProps = {
  row: number;
  column: number;
  bookDesk: any;
  bookedDesks: deskCoordinates[];
};
export type deskCoordinates = {
  row: number;
  column: number;
};
const Item: FC<ItemProps> = ({ row, column, bookDesk, bookedDesks }) => {
  const [isBooked, setIsBooked] = useState<Boolean | null>();
  useEffect(() => {
    const booked = bookedDesks.filter((desk) => {
      return desk.row === row && desk.column === column;
    });
    if (booked.length > 0) {
      setIsBooked(true);
    }
  }, [bookedDesks]);

  return (
    <div
      className={isBooked === true ? " desk booked" : "desk"}
      onClick={() => {
        if (isBooked) {
          setIsBooked(!isBooked);
        }
        bookDesk(isBooked, row, column);
      }}
    >
      {row},{column}
    </div>
  );
};

const FloorGrid = (props: FloorGridProps) => {
  const [bookedDesks, setBookedDesks] = useState<deskCoordinates[]>([]);
  const bookDesk = (isBooked: boolean, row: number, column: number) => {
    if (isBooked) {
      // debugger;
      const newBookedDesks = bookedDesks.filter((desk) => {
        console.log(desk);
        return desk.row !== row || desk.column !== column;
      });
      setBookedDesks(newBookedDesks);
    } else {
      setBookedDesks([...bookedDesks, { row, column }]);
    }
  };
  const { rows, columns, saveDesks } = props;
  const renderColumns = (row: number, columns: number) => {
    let columnComponents: any = [];
    for (let i = 0; i < columns; i++) {
      columnComponents.push(
        <Col key={i} xs="auto">
          <Item
            // key={i}
            row={row}
            column={i}
            bookDesk={(isBooked: boolean, row: number, column: number) =>
              bookDesk(isBooked, row, column)
            }
            bookedDesks={bookedDesks}
          />
        </Col>
      );
    }
    return columnComponents;
  };
  const renderRows = (rows: number, columns: number) => {
    let rowComponents: any = [];
    for (let i = 0; i < rows; i++) {
      rowComponents.push(
        <Row key={i}>
          <div style={{ flexDirection: "row", display: "flex" }}>
            {renderColumns(i, columns)}{" "}
          </div>
        </Row>
      );
    }
    return rowComponents;
  };
  return (
    <div style={{ padding: "20px" }}>
      <Grid
        container
        className="grid-container"
        spacing={{ xs: 2, md: 3 }}
        columns={{ xs: 4, sm: 8, md: 12 }}
      >
        {renderRows(rows, columns)}
        <button onClick={() => saveDesks(bookedDesks)}>Save desks</button>
      </Grid>
    </div>
  );
};

export default FloorGrid;
