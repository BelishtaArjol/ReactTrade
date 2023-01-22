import React, { useState, FC, useEffect } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import onLogout from "../../main/store/stores/user/login.store.on-logout";

const NavCart: FC = () => {
  const [NrOfProducstInCart, setNrOfProducstInCart] = useState(0);
  const { cartItem } = useSelector((state: any) => state.card);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  useEffect(() => {
    setNrOfProducstInCart(cartItem.length);
  }, [cartItem.length]);

  return (
    <div>
      <div className="row">
        <div className="col-md-12 mb-3" style={{ textAlign: "center" }}>
          <nav>
            <div className="nav_box">
              <span style={{margin: -100}} className="my_shop"> My Shopping React App </span>
              <div className="cart">
                <span>
                  <i
                    style={{ margin: 20 }}
                    onClick={() => {
                      dispatch(onLogout());
                      navigate("/");
                    }}
                  >
                    Logout
                  </i>
                  <i
                    style={{ margin: 50 }}
                    onClick={() => navigate("/Profile")}
                  >
                    Profile
                  </i>
                  <i onClick={() => navigate("/ProductToBuy")}> Cart </i>
                </span>
                <span> {NrOfProducstInCart} </span>
              </div>
            </div>
          </nav>
        </div>
      </div>
    </div>
  );
};

export default NavCart;
