import { Outlet } from "react-router-dom";
import Register from "../authentication/Register";

const Layout = () => {
  return (
    <main>
      <Outlet />
    </main>
  );
};

export default Layout;
