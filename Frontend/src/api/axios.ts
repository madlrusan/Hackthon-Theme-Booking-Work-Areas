import axios from "axios";
import { ApiUrls } from "../components/constants/ApiUrls";

export default axios.create({
  baseURL: ApiUrls.BASE_URL,
});
