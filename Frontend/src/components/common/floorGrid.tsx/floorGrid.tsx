import React from "react";
import styled from "styled-components";
import { Container, Row, Col } from "react-bootstrap";
import { Grid } from "@mui/material";
// import "bootstrap/dist/css/bootstrap.min.css";

type FloorGridProps = {
  rows: number;
  columns: number;
};
const Item = () => {
  return <div className="desk">birou</div>;
};

const FloorGrid = (props: FloorGridProps) => {
  const { rows, columns } = props;
  const renderColumns = (columns: number) => {
    let columnComponents: any = [];
    for (let i = 0; i < columns; i++) {
      columnComponents.push(
        <Col key={i} xs="auto">
          {" "}
          <Item />{" "}
        </Col>
      );
    }
    return columnComponents;
  };
  const renderRows = (rows: number, columns: number) => {
    let rowComponents: any = [];
    for (let i = 0; i < rows; i++) {
      rowComponents.push(
        <Row>
          <div>{renderColumns(columns)} </div>
        </Row>
      );
    }
    return rowComponents;
  };
  return (
    <Grid
      container
      className="grid-container"
      spacing={{ xs: 2, md: 3 }}
      columns={{ xs: 4, sm: 8, md: 12 }}
    >
      {renderRows(rows, columns)}
    </Grid>
  );
};

export default FloorGrid;
