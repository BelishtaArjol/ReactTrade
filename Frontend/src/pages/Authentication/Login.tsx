import { FC, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import ILoginRequest from "../../main/interfaces/ILoginRequest";
import { onLogin } from "../../main/store/stores/user/login.store.on-login";

const TestPage: FC = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [loginData, setLogInData] = useState<ILoginRequest>({
    userName: "",
    password: "",
  });

  const handleChange = (event: any) => {
    setLogInData({
      ...loginData,
      [event.target.name]: event.target.value,
    });
  };

  const OnSubmitHandler = (e: any) => {
    e.preventDefault();
    dispatch(onLogin(loginData));
  };
  return (
    <>
      <form style={{ margin: 100 }} onSubmit={OnSubmitHandler}>
        <input
          type="text"
          name="userName"
          value={loginData.userName}
          placeholder="Username"
          onChange={handleChange}
          style={{
            margin: "auto",
            position: "relative",
            alignItems: "center",
            width: "20%",
            border: "3px solid green",
            padding: 3,
            display: "flex",
            justifyContent: "center",
          }}
        />

        <br />
        <br />

        <input
          type="password"
          name="password"
          value={loginData.password}
          placeholder="Password"
          onChange={handleChange}
          style={{
            margin: "auto",
            position: "relative",
            alignItems: "center",
            width: "20%",
            border: "3px solid green",
            padding: 3,
            display: "flex",
            justifyContent: "center",
          }}
        />

        <br />
        <br />

        <button
          style={{
            margin: "auto",
            position: "relative",
            alignItems: "center",
            width: "5%",
            border: "3px solid green",
            background: "lightGreen",
            padding: 3,
            display: "flex",
            justifyContent: "center",
          }}
          type="submit"
        >
          Login
        </button>

        <div>
          <br />
          <i
            style={{ fontSize: 20, display: "flex", justifyContent: "center" }}
          >
            You don't have an account?
          </i>
          <br />
          <br />
          <button
            onClick={() => {
              navigate("/register");
            }}
            style={{
              margin: "auto",
              position: "relative",
              alignItems: "center",
              width: "auto",
              border: "3px solid green",
              background: "lightGreen",
              padding: 3,
              display: "flex",
              justifyContent: "center",
            }}
          >
            Create one here
          </button>
        </div>
      </form>
    </>
  );
};

export default TestPage;
