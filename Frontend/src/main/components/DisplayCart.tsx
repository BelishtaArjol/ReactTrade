import React, { FC, useEffect, useState } from "react";
import "./CartStyling.css";
import IBuyProduct from "../interfaces/IBuyProduct";
import { useDispatch } from "react-redux";
import {
  removeItem,
  incrementItem,
  decrementItem,
} from "../store/stores/card/cardStore";

interface ProductCartProps {
  kart: IBuyProduct[];
}

const DisplayCart: FC<ProductCartProps> = (props: any) => {
  const dispatch = useDispatch();
  const [totalPerProduct, setTotalPerProduct] = useState(0);

  useEffect(() => {
    setTotalPerProduct(props.kart.quantity * props.kart.price);
  }, [props.kart.quantity]);

  return (
    <>
      <tr>
        <td className="item">
          <div>
            <div className="pl-2">{props.kart.name}</div>
          </div>
        </td>

        <td>
          <button
            className="btn btn-success mx-2"
            onClick={() => dispatch(incrementItem(props.kart))}
          >
            +
          </button>
          <button
            className="btn btn-danger mx-2"
            onClick={() => dispatch(decrementItem(props.kart))}
          >
            -
          </button>
          <p style={{ color: "mediumblue" }}> {props.kart.quantity} </p>
        </td>

        <td>
          <span className="red"> {props.kart.price} </span>
        </td>

        <td>
          <button
            onClick={() => dispatch(removeItem(props.kart))}
            className="btn btn-info"
            type="button"
          >
            Remove Item
          </button>
        </td>

        <td className="font-weight-bold">{totalPerProduct}</td>
      </tr>
    </>
  );
};

export default DisplayCart;
