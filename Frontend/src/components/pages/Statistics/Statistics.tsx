import { Card, List, Stack } from '@mui/material';
import React from 'react';

type GraphProps = {
    periode : string;
    data? : any;
}
const GraphItem = (props: GraphProps) => {
    const {periode} = props;
    return(
        <div>
            <h2>Statistics for a {periode}</h2>
            <div>{periode}</div>
        </div>
        
    );
};

const Statistics = () => {
    return(
        <div className="fullscreen">
      <Card className="window">
        <div>
          <h1>Statistics</h1>
          
          <div className="scrollable-list">    
           <Stack direction="row" spacing={3}>
               <div key={1}>
                   
                    <GraphItem periode={"day"}/>
               </div>
                <div key={2}>
                    <GraphItem periode="week"/> 
               </div>
               <div key={3}>
                    <GraphItem periode="month"/>
               </div>
           </Stack>
          </div>
        </div>
      </Card>
    </div>
    );
};
export default Statistics;