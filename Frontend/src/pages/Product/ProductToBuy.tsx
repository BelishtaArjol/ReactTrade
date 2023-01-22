import React, { FC, useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import DisplayCart from "../../main/components/DisplayCart";
import BankTransactionManager from "../../main/utils/BankTransactionManager";
import BankAccountManager from "../../main/utils/bankAccountManager";
import { clearCard } from "../../main/store/stores/card/cardStore";

const ProductToBuy: FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [karta, setKarta] = useState([]);
  const {cartItem, total } = useSelector((state: any) => state.card);
  const [bankAccounts, setbankAccounts] = useState([]);
  const [bankId, setBankId] = useState(64);
  const [error, setError] = useState("");

  const fetchBankAccounts = async () => {
    const results = await BankAccountManager.getAllBanks();
    setbankAccounts(results.data.resultData.data);
  };

  useEffect(() => {
    setKarta(cartItem);
    fetchBankAccounts();
    console.log("BANKA", bankAccounts);
  }, [cartItem]);

  const handleClick = async () => {
    const bankTransaction = {
      bankAccountId: bankId,
      action: 1,
      amount: total,
      isActive: true,
    };

    const response = await BankTransactionManager.addTransaction(bankTransaction);
    setError(response.message);
    if (error != "") {
      alert("Transaction unsuccessful");
    } else {
      dispatch(clearCard());
      alert("Transaction Successful");
    }
  };

  return (
    <div>
      <div className="wrapper">
        <div className="d-flex align-items-center justify-content-between">
          <div className="d-flex flex-column">
            <div className="h3">My Cart</div>
          </div>
        </div>

        <div className="table-responsive">
          <table className="table activitites">
            <thead>
              <tr>
                <th scope="col" className="text-uppercase header">
                  Item
                </th>
                <th scope="col" className="text-uppercase">
                  Quantity
                </th>
                <th scope="col" className="text-uppercase">
                  Price Each
                </th>
                <th scope="col" className="text-uppercase">
                  Remove Item
                </th>
                <th scope="col" className="text-uppercase">
                  Total
                </th>
              </tr>
            </thead>
            <tbody>
              {karta.map((kart) => {
                return <DisplayCart kart={kart} key={kart.id} />;
              })}
            </tbody>
          </table>
        </div>

        <div className="mb-5">Total to be payed : {total}</div>

        <div className="row">
          <div className="col-md-6">
            <p>Choose bank to pay</p>
            <select
              onChange={(event) =>
                setBankId(parseInt(event.currentTarget.value))
              }
            >
              {bankAccounts.map((bank) => {
                return (
                  <option value={bank.id} key={bank.id}>
                    {bank.name}
                  </option>
                );
              })}
            </select>
            <br />

            <div className="my-3">
              <button className="btn btn-success" onClick={() => handleClick()}>
                Approve Payment
              </button>
              <button className="btn btn-link" onClick={() => {navigate("/");}}>
                Return to dashboard
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductToBuy;
