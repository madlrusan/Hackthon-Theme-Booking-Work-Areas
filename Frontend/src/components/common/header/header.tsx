import React, { useState } from "react";
import "./header.scss";

import { Avatar, Menu, MenuItem, Typography } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import BurgerMenu from "./BurgerMenu";

const officeLocations = [
  "Company Location 1",
  "Company Location 2",
  "Company Location 3",
];
const menu = [
  { name: "Office", subMenu: officeLocations },
  { name: "Schedule" },
  { name: "People" },
  { name: "Maps" },
  { name: "Manage" },
];
export const Header = () => {
  const navigate = useNavigate();

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

  const handleCloseNavMenu = (id?: number) => {
    setAnchorElNav(null);
    if (id !== undefined) navigate(`/location/${id}`);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  return (
    <>
      <section className="navigation">
        <div className="nav-container">
          <div className="brand">
            <Link to="/">Company Name</Link>
          </div>
          <nav>
            {/* <BurgerMenu /> */}

            <ul className="nav-list">
              <li>
                <div
                  className="dropdownButton"
                  aria-controls="menu-office"
                  aria-haspopup="true"
                  onClick={handleOpenNavMenu}
                >
                  Office
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
                  {officeLocations.map((page) => (
                    <MenuItem
                      key={page}
                      onClick={(id) => handleCloseNavMenu(5)}
                    >
                      <Typography textAlign="center">{page}</Typography>
                    </MenuItem>
                  ))}
                </Menu>
              </li>
              <li>
                <Link to={"Schedule"}>Schedule</Link>
              </li>
              <li>
                <Link to="People">People</Link>
              </li>
              <li>
                <Link to="Maps">Maps</Link>
              </li>
              <li>
                <Link to="Manage">Manage</Link>
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
                      <Typography textAlign="center">Logout</Typography>
                    </MenuItem>
                  </Menu>
                </ul>
              </li>
            </ul>
          </nav>
        </div>
      </section>
      <script src="./header.js"> </script>
    </>
  );
};
