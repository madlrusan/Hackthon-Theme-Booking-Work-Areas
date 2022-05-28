import { useRef, useState, useEffect, useContext } from "react";
import "./authentication.scss";
import axios from "../../api/axios";
import { ApiUrls } from "../constants/ApiUrls";
import { Link, useNavigate } from "react-router-dom";

export const Login = () => {
  const navigate = useNavigate();
  const userRef = useRef<HTMLInputElement>(null);
  const errRef = useRef<HTMLParagraphElement>(null);
  const [email, setEmail] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [isValid, setIsValid] = useState(true);

  useEffect(() => {
    if (email !== "" && pwd !== "") {
      setIsValid(false);
    } else {
      setIsValid(true);
    }
  }, [email, pwd]);

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [email, pwd]);
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        ApiUrls.LOGIN,
        JSON.stringify({ email: email, password: pwd }),
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      if (response.data.success === true) {
        localStorage.setItem("token", response.data.token);
        navigate("/");
      }
    } catch (err: any) {
      if (!err?.response) {
        setErrMsg("No Server Response");
      }
      setErrMsg(err.response.data[0]);
    }
    setEmail("");
    setPwd("");
  };
  return (
    <div className="fullscreen">
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
                onChange={(e) => setEmail(e.target.value)}
                value={email}
                required
              />
            </div>
            <div className="formField">
              <label htmlFor="password">Password</label>
              <input
                type="password"
                id="password"
                onChange={(e) => setPwd(e.target.value)}
                value={pwd}
                required
              />
            </div>
            <div className="btn">
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
    </div>
  );
};
