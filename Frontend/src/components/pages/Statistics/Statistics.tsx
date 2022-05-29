import { Card } from "@mui/material";

import { Chart } from "./Chart";

type GraphProps = {
  periode: string;
  data?: any;
};
const GraphItem = (props: GraphProps) => {
  const { periode } = props;
  return (
    <div>
      <h2>Statistics for a {periode}</h2>
      <Chart />
    </div>
  );
};

const Statistics = () => {
  return (
    <div className="fullscreen">
      <Card className="statisticsBox">
        <div
          style={{
            height: "600px",
            padding: "30px",
          }}
        >
          <div key={3}>
            <GraphItem periode="month" />
          </div>
        </div>
      </Card>
    </div>
  );
};
export default Statistics;
