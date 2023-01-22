import React, { FC, useState } from "react";
import IProduct from "../interfaces/IProduct";
import { useDispatch, useSelector } from "react-redux";
import { buyProduct, updateItem } from "../store/stores/card/cardStore";

interface ProductProps {
  product: IProduct[];
}

const Product: FC<ProductProps> = (props: any) => {
  const [count, setCount] = useState(0);
  const dispatch = useDispatch();
  const { cartItem } = useSelector((state: any) => state.card);

  const handleDecrement = () => {
    if (count > 0) setCount(count - 1);
    else setCount(0);
  };

  const handleClick = (produkt: Object) => {
    let id!: string, name: string, price: number;
    Object.entries(produkt).forEach(([key, value]) => {
      if (
        key != "base64Image" &&
        key != "shortDescription" &&
        key != "longDescription" &&
        key != "categoryId"
      ) {
        if (key == "id") id = value;
        else if (key == "name") name = value;
        else if (key == "price") price = value;
      }
    });

    const quantity: number = count;
    const pr = {
      id,
      name,
      price,
      quantity,
    };

    if (cartItem.length !== 0) {
      for (var i = 0; i < cartItem.length; i++) {
        var item = cartItem[i];
        if (item.id === parseInt(id)) {
          dispatch(updateItem(pr));
          break;
        } else if (i + 1 == cartItem.length) {
          dispatch(buyProduct(pr));
        }
      }
    } else if (cartItem.length === 0) dispatch(buyProduct(pr));
  };

  return (
    <div className="card mb-5" style={{ width: "20rem" }}>
      <img
        src={`data:image/png;base64,${props.product.base64Image}`}
        className="card-img-top"
        alt="..."
      />
      <div className="card-body">
        <h5 className="card-title">{props.product.name} </h5>
        <p className="card-text">{props.product.shortDescription}</p>
        <p>
          Price:
          <strong style={{ fontSize: "20px" }}>{props.product.price}</strong>
        </p>
        <div style={{ display: "flex", marginRight: "10px" }}>
          <button
            className="btn btn-success mx-1"
            onClick={() => setCount(count + 1)}
          >
            +
          </button>
          <input type="text" value={count} onChange={() => setCount(count)} />
          <button className="btn btn-danger mx-1" onClick={handleDecrement}>
            -
          </button>
        </div>
        <button
          onClick={() => handleClick(props.product)}
          type="button"
          className="btn btn-primary mt-2"
        >
          Buy
        </button>
      </div>
    </div>
  );
};

export default Product;
