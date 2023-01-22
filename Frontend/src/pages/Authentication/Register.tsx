import { FC, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import IUser from "../../main/interfaces/IUser";
import { onRegister } from "../../main/store/stores/user/login.store.on-login";

const Register: FC = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [registerData, setRegisterData] = useState<IUser>({
    firstName: "",
    lastName: "",
    email: "",
    birthdate: "",
    phone: "",
    username: "",
    password: "",
  });

  const handleChange = (event: any) => {
    const value = event.target.value;
    setRegisterData({
      ...registerData,
      [event.target.name]: value,
    });
  };

  return (
    <>
      <form style={{ margin: 100 }}>
        <input
          type="text"
          name="firstName"
          value={registerData.firstName}
          placeholder="Firstname"
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

        <input
          type="text"
          name="lastName"
          value={registerData.lastName}
          placeholder="Lastname"
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

        <input
          type="email"
          name="email"
          value={registerData.email}
          placeholder="Email"
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

        <input
          type="date"
          name="birthdate"
          value={registerData.birthdate}
          placeholder="Birthdate"
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

        <input
          type="number"
          name="phone"
          value={registerData.phone}
          placeholder="Phone"
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

        <input
          type="text"
          name="username"
          value={registerData.username}
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

        <input
          type="password"
          name="password"
          value={registerData.password}
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

        <button
          type="submit"
          onClick={() => {
            dispatch(onRegister(registerData));
            navigate("/");
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
          Register
        </button>
      </form>
    </>
  );
};
export default Register;
