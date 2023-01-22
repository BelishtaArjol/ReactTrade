import React, { FC, useEffect, useState } from "react";
import ProductManager from "../../main/utils/productManager";
import Product from "../../main/components/Product";
import "./navCart.css";
import NavCart from "./NavCart";

const DashboardPage: FC = () => {
  const [products, setProducts] = useState([]);

  const fetchProducts = async () => {
    const results = await ProductManager.getAllProducts();
    setProducts(results.data.resultData.data);
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  return (
    <>
      <div>
        <NavCart />
        <div className="row">
          {products.map((product) => {
            return (
              <div className="col-md-3" key={product.id}>
                <Product product={product} />
              </div>
            );
          })}
        </div>
      </div>
    </>
  );
};

export default DashboardPage;
