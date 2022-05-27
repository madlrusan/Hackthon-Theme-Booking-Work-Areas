import React, { useState } from "react";
import './header.scss';
import * as JQuery  from 'jquery'
import { Avatar } from "@mui/material";

const officeLocations = [
  "Company Location 1",
  "Company Location 2",
  "Company Location 3",
];

const userSettings = ["User Setting 1", "User Setting 2", "User Setting 3"];

export const Header = () => {
    const [anchorElNav, setAnchorElNav] = React.useState(null);
    const [anchorElUser, setAnchorElUser] = React.useState(null);

    const handleOpenNavMenu = (event : any) => {
        console.log("am deschis");
      setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event : any) => {
        console.log("am deschis");
      setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        console.log("am inchis");
      setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        console.log("am inchis");
      setAnchorElUser(null);
    };

  return (
    <>
      <section className="navigation">
        <div className="nav-container">
          <div className="brand">
            <a href="#!">Company Name</a>
          </div>
          <nav>
            <div className="nav-mobile">
              <a id="nav-toggle" href="#!">
                <span></span>
              </a>
            </div>
            <ul className="nav-list">
              <li>
                <a href="#!">Office</a>
                <ul className="nav-dropdown" onClick={(e) => handleOpenNavMenu(e.target)}>
                    {officeLocations.map((location) => (
                        <li key={location}>
                            <a href="#!" onClick={handleCloseNavMenu}>{location}</a>
                        </li>
                    ))}
                </ul>
              </li>
              <li>
                <a href="#!">Schedule</a>
              </li>
              <li>
                <a href="#!">People</a>
              </li>
              <li>
                <a href="#!">Maps</a>
              </li>
              <li>
                <a href="#!">Manage</a>
              </li>
              <li className="avatar">
                <a href="#!" className="avatar">
                    <Avatar className="img"></Avatar>
                    
                </a>
                <ul className="nav-dropdown" onClick={(e)=>handleOpenUserMenu(e.target)}>
                        {userSettings.map((setting) => (
                            <li key={setting}>
                                <a href="#!" onClick={handleCloseUserMenu}>{setting}</a>
                            </li>
                        ))}
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