import { Desk } from "./Desk";

export type Floor = {
  id: number;
  name: string;
  officeId: number;
  desks: Desk[];
};
