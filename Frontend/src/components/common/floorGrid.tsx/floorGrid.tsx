import React from "react";
import styled from "styled-components";
import { Container, Row, Col } from "react-bootstrap";
type FloorGridProps = {
  rows: number;
  columns: number;
};
const Item = () => {
  return <div>birou</div>;
};

const FloorGrid = (props: FloorGridProps) => {
  const { rows, columns } = props;
  const renderColumns = (columns: number) => {
      let columnComponents : any = [];
    for (let i = 0; i < columns; i++) {
      columnComponents.push(
        <Col key={i}>
          {" "}
          <Item />{" "}
        </Col>
      );
    }
    return columnComponents;
    
  };
  const renderRows = (rows: number, columns: number) => {
     let  rowComponents : any = []
    for (let i = 0; i < rows; i++) {

    rowComponents.push(
      <Row>
        <div>{renderColumns(columns)} </div>
      </Row>
    );
    }
    return rowComponents;
  };
  return <Container>{renderRows(rows,columns)}</Container>;
};

export default FloorGrid;
