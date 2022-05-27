import { useRef, useState, useEffect, useContext } from "react";
import useAuth from "../../hooks/useAuth";
import "./authentication.scss";
import axios from "../../api/axios";
import { ApiUrls } from "../constants/ApiUrls";
import { Link } from "react-router-dom";

export const Login = () => {
  const { setIsAuthenticated } = useAuth();
  const userRef = useRef<HTMLInputElement>(null);
  const errRef = useRef<HTMLParagraphElement>(null);
  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [isValid, setIsValid] = useState(true);

  useEffect(() => {
    if (user !== "" && pwd !== "") {
      setIsValid(false);
    } else {
      setIsValid(true);
    }
  }, [user, pwd]);

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [user, pwd]);
  const onSuccess = (res: any) => {
    console.log("Login success:", res);
  };
  const onFailure = (res: any) => {
    console.log("Login failure:", res);
  };
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        ApiUrls.LOGIN,
        JSON.stringify({ user, pwd }),
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      const accessToken = response?.data?.accessToken;
      setIsAuthenticated({ user, pwd, accessToken });
      setUser("");
      setPwd("");
    } catch (err: any) {
      if (!err?.response) {
        setErrMsg("No server response");
      } else {
        setErrMsg("Login was not successful");
      }
      errRef.current?.focus();
    }
  };
  return (
    <div className="form">
      <section className="registerForm">
        <p
          ref={errRef}
          className={errMsg ? "errmsg" : "offscreen"}
          aria-live="assertive"
        >
          {errMsg}
        </p>
        <h1>Sign In</h1>
        <form onSubmit={handleSubmit}>
          <div className="formField">
            <label htmlFor="username">Username</label>
            <input
              type="text"
              id="username"
              ref={userRef}
              autoComplete="off"
              onChange={(e) => setUser(e.target.value)}
              value={user}
              required
            />
            <label htmlFor="password">Password</label>
            <input
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              required
            />
            <button className="submitFormButton" disabled={isValid}>
              Sign In
            </button>
          </div>
        </form>
        <p className="already">
          Need an Account? <br />
          <span className="line">
            <Link to="/register" replace>
              Sign Up
            </Link>
          </span>
        </p>
      </section>
    </div>
  );
};
