import React, { FC, useEffect, useState } from "react";
import BankAccountManager from "../../main/utils/bankAccountManager";
import CurrencyManager from "../../main/utils/currencyManager";
import { useNavigate } from "react-router-dom";

const AddBankAccount: FC = () => {
  const [code, setCode] = useState("");
  const [name, setName] = useState("");
  const [balance, setBalance] = useState(null);
  const [currencies, setCurrencies] = useState([]);
  const [currencyId, setCurrencyId] = useState(27);

  const navigate = useNavigate();

  const fetchCurrencies = async () => {
    const results = await CurrencyManager.getAllCurrencies();
    setCurrencies(results.data.resultData.data);
  };

  useEffect(() => {
    fetchCurrencies();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const isActive = true;
    const clientId = 1035;
    await BankAccountManager.addBank({
      code,
      name,
      balance,
      currencyId,
      clientId,
      isActive,
    });
    navigate("/bankAccounts");
  };

  return (
    <div>
      <div className="container" style={{ width: "300px" }}>
        <form onSubmit={(e) => handleSubmit(e)}>
          <div className="mb-3">
            <label htmlFor="code" className="form-label">
              Code
            </label>
            <input
              type="text"
              className="form-control"
              id="code"
              value={code}
              onChange={(event) => setCode(event.target.value)}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="name" className="form-label">
              Name
            </label>
            <input
              type="text"
              className="form-control"
              id="name"
              value={name}
              onChange={(event) => setName(event.target.value)}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="balance" className="form-label">
              Balance
            </label>
            <input
              type="text"
              className="form-control"
              id="balance"
              value={balance}
              onChange={(event) => setBalance(parseInt(event.target.value))}
            />
          </div>

          <div className="mb-3">
            <select
              onChange={(event) =>
                setCurrencyId(parseInt(event.currentTarget.value))
              }
            >
              {currencies.map((currency) => {
                return (
                  <option value={currency.id} key={currency.id}>
                    {currency.code}
                  </option>
                );
              })}
            </select>
          </div>

          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddBankAccount;
