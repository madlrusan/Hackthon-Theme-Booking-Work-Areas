import { useRef, useState, useEffect, useContext } from "react";
import useAuth from "../../hooks/useAuth";
import "./authentication.css";
import axios from "../../api/axios";
import { ApiUrls } from "../constants/ApiUrls";

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
      </form>
      <p>
        Need an Account? <br />
        <span className="line">
          <a href="">Sign Up</a>
        </span>
      </p>
      <div className="g_body">
        <button className="g-button" onClick={console.log}>
          <img
            className="g-logo"
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Google_%22G%22_Logo.svg/157px-Google_%22G%22_Logo.svg.png"
            alt="Google Logo"
          />
          <p className="g-text">Sign in with Google</p>
        </button>
      </div>
    </section>
  );
};
