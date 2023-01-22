import React from "react";
import { useNavigate } from "react-router-dom";

const Profile = () => {
  const navigate = useNavigate();
  return (
    <div>
      <form style={{ margin: 100 }}>
      <button
        onClick={() => {
          navigate("/bankAccounts");
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
        See your bank accounts
      </button>
      <br></br>
      <button
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
        onClick={() => {
          navigate("/lastTrasactions");
        }}
      >
        See your last transactions
      </button>
      </form>
    </div>
  );
};

export default Profile;
