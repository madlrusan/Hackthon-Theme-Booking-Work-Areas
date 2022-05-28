import { useRef, useState, useEffect } from "react";
import {
  faCheck,
  faTimes,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import "./authentication.scss";
import axios from "../../api/axios";
import { Link, useNavigate } from "react-router-dom";
import { ApiUrls } from "../constants/ApiUrls";
//must start with a letter, and can continue with any kind of letter,digit or sign, between 4 and 23 characters
//must contain a lower case letter, upper case letter, number and a sign, must be between 8 and 24 characters
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;

const Register = () => {
  const navigate = useNavigate();
  const userRef = useRef<HTMLInputElement>(null);
  const errRef = useRef<HTMLParagraphElement>(null);

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);
  const [emailFocus, setEmailFocus] = useState(false);

  const [pwd, setPwd] = useState("");
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);

  const [matchPwd, setMatchPwd] = useState("");
  const [validMatch, setValidMatch] = useState(false);
  const [matchFocus, setMatchFocus] = useState(false);

  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  useEffect(() => {
    setValidEmail(email.length > 0);
  }, [email]);

  useEffect(() => {
    const result = PWD_REGEX.test(pwd);
    const match = pwd === matchPwd;
    setValidMatch(match);
    setValidPwd(result);
  }, [pwd, matchPwd]);

  useEffect(() => {
    setErrMsg("");
  }, [email, pwd, matchPwd]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const pwdValidation = PWD_REGEX.test(pwd);
    if (!pwdValidation) {
      setErrMsg("Invalid Entry");
      return;
    }
    try {
      const response = await axios.post(
        ApiUrls.REGISTER,
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
  };

  return (
    <div className="fullscreen">
    <div className="form">
      <section className="registerForm">
        <p
          ref={errRef}
          className={errMsg ? "errmsg" : "offscreen "}
          aria-live="assertive"
        >
          {errMsg}
        </p>
        <h1>Register</h1>
        <form onSubmit={handleSubmit}>
          <div className="formField">
            <label htmlFor="username">
              Email
              <FontAwesomeIcon
                icon={faCheck}
                className={validEmail ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validEmail || !email ? "hide" : "invalid"}
              />
            </label>
            <input
              type="text"
              id="username"
              ref={userRef}
              autoComplete="off"
              onChange={(e) => setEmail(e.target.value)}
              required
              aria-invalid={validEmail}
              aria-describedby="uidnote"
              onFocus={() => setEmailFocus(true)}
              onBlur={() => setEmailFocus(false)}
            />
            <p
              id="uidnote"
              className={
                emailFocus && email && !validEmail
                  ? "instructions"
                  : "offscreen"
              }
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              4 to 24 characters.
              <br />
              Must begin with a letter.
              <br />
              Letters,numbers,underscores,hypens allowed.
            </p>
          </div>
          <div className="formField">
            <label htmlFor="password">
              Password:
              <FontAwesomeIcon
                icon={faCheck}
                className={validPwd ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validPwd || !pwd ? "hide" : "invalid"}
              />
            </label>
            <input
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              required
              aria-invalid={validPwd}
              aria-describedby="pwdnote"
              onFocus={() => setPwdFocus(true)}
              onBlur={() => setPwdFocus(false)}
            />
            <p
              id="pwdnote"
              className={pwdFocus && !validPwd ? "instructions" : "offscreen"}
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              8 to 24 characters.
              <br />
              Must include uppercase and lowercase letters, a number and a
              special character.
              <br />
              Allowed special characters:
              <span aria-label="exclamation mark">! </span>
              <span aria-label="at symbol">@ </span>
              <span aria-label="hashtag"># </span>
              <span aria-label="dollar sign">$ </span>
              <span aria-label="percent">% </span>
            </p>
          </div>
          <div className="formField">
            <label htmlFor="confirm_password">
              Confirm Password:
              <FontAwesomeIcon
                icon={faCheck}
                className={validMatch && matchPwd ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validMatch || !matchPwd ? "hide" : "invalid"}
              />
            </label>
            <input
              type="password"
              id="confirm_password"
              onChange={(e) => setMatchPwd(e.target.value)}
              required
              aria-invalid={validMatch}
              aria-describedby="confirmnote"
              onFocus={() => setMatchFocus(true)}
              onBlur={() => setMatchFocus(false)}
            />
            <p
              id="confirmnote"
              className={
                matchFocus && !validMatch ? "instructions" : "offscreen"
              }
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              Must match the password field.
            </p>
          </div>
          <div className="btn">
            <button
              className="submitFormButton"
              disabled={!validEmail || !validPwd || !validMatch}
            >
              Sign up
            </button>
          </div>
        </form>
        <p className="already">
          Already registered?
          <br />
          <span className="line">
            <Link to="/login" replace={true}>
              Sign In
            </Link>
          </span>
        </p>
      </section>
    </div>
    </div>
  );
};
export default Register;
