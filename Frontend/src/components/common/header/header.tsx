import React, { useContext } from "react";
import "./header.scss";

import { Avatar, Menu, MenuItem, Typography } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import BurgerMenu from "./BurgerMenu";
import { LocationContext } from "../../../context/LocationProvider";
import { Office } from "../../../models/Office";

export const Header = () => {
  const navigate = useNavigate();
  const locationContext = useContext(LocationContext);

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(
    null
  );
  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = (floor?: Office) => {
    setAnchorElNav(null);
    if (floor !== undefined) {
      console.log(floor);
      locationContext.setSelectedLocation(floor);
      navigate(`/location`);
    }
  };

  const handleCloseUserMenu = () => {
    localStorage.removeItem("token");
    navigate("/login");
    setAnchorElUser(null);
  };
  const isAdmin = () => {
    const roles = localStorage.getItem("role")?.split(",");
    return roles?.includes("Manager");
  };
  return (
    <div className="sticky-container">
      <section className="navigation">
        <div className="nav-container">
          <div className="brand">
            <Link to="/">Company Name</Link>
          </div>
          <nav>
            {/* <BurgerMenu /> */}

            <ul className="nav-list">
              {locationContext.locations.length > 0 ? (
                <li>
                  <div
                    className="dropdownButton"
                    aria-controls="menu-office"
                    aria-haspopup="true"
                    onClick={handleOpenNavMenu}
                  >
                    Locations
                  </div>

                  <Menu
                    onMouseLeave={() => handleCloseNavMenu()}
                    id="menu-office"
                    anchorEl={anchorElNav}
                    anchorOrigin={{
                      vertical: "bottom",
                      horizontal: "left",
                    }}
                    keepMounted
                    transformOrigin={{
                      vertical: "top",
                      horizontal: "left",
                    }}
                    open={Boolean(anchorElNav)}
                    onClose={() => handleCloseNavMenu()}
                    sx={{
                      display: { xs: "block" },
                    }}
                  >
                    {locationContext.locations.map((location) => {
                      return (
                        <MenuItem
                          key={location.id}
                          onClick={() => handleCloseNavMenu(location)}
                        >
                          <Typography
                            textAlign="center"
                            style={{ color: "#003973" }}
                          >
                            {location.name}
                          </Typography>
                        </MenuItem>
                      );
                    })}
                  </Menu>
                </li>
              ) : (
                ""
              )}
              {isAdmin() && (
                <li>
                  <Link to={"AddLocation"}>Add location</Link>
                </li>
              )}
              <li>
                <Link to="Statistics">Statistics</Link>
              </li>
              <li className="avatar">
                <div className="avatar dropdownButton">
                  <Avatar className="img" onClick={handleOpenUserMenu}></Avatar>
                </div>
                <ul className="nav-dropdown">
                  <Menu
                    sx={{ mt: "45px" }}
                    id="menu-avatar"
                    anchorEl={anchorElUser}
                    anchorOrigin={{
                      vertical: "top",
                      horizontal: "right",
                    }}
                    keepMounted
                    transformOrigin={{
                      vertical: "top",
                      horizontal: "right",
                    }}
                    open={Boolean(anchorElUser)}
                    onClose={handleCloseUserMenu}
                  >
                    <MenuItem onClick={handleCloseUserMenu}>
                      <Typography
                        textAlign="center"
                        style={{ color: "#003973" }}
                      >
                        Logout
                      </Typography>
                    </MenuItem>
                  </Menu>
                </ul>
              </li>
            </ul>
          </nav>
        </div>
      </section>
      <script src="./header.js"> </script>
    </div>
  );
};
