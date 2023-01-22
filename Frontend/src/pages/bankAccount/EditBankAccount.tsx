import React, { FC, useEffect, useState } from "react";
import BankAccountManager from "../../main/utils/bankAccountManager";
import { useParams, useNavigate } from "react-router-dom";

const EditBankAccount: FC = () => {
  const navigate = useNavigate();

  let { id } = useParams();
  const [code, setCode] = useState("");
  const [name, setName] = useState("");
  const [balance, setBalance] = useState(0);
  const isActive = true;
  const clientId = 1035;
  const [currencyid, setCurrencyId] = useState(0);
  const fetchBankAccount = async (id: number) => {
    const results = await BankAccountManager.getUniqueBank(id);

    setCode(results.data.resultData.code);
    setName(results.data.resultData.name);
    setBalance(results.data.resultData.balance);
    setCurrencyId(results.data.resultData.currencyId);
  };

  useEffect(() => {
    fetchBankAccount(parseInt(id));
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const response = await BankAccountManager.editBank(parseInt(id), {
      code,
      name,
      balance,
      currencyid,
      isActive,
      clientId,
    });
    navigate("/bankAccounts");
  };

  return (
    <div>
      <div className="container" style={{ width: "300px" }}>
        <form onSubmit={(e) => handleSubmit(e)}>
          <div className="mb-3">
            <label htmlFor="code" className="form-label">
              CODE
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
            <label htmlFor="description" className="form-label">
              Name
            </label>
            <input
              type="text"
              className="form-control"
              id="description"
              value={name}
              onChange={(event) => setName(event.target.value)}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="exchangeRate" className="form-label">
              Balance
            </label>
            <input
              type="text"
              className="form-control"
              id="exchangeRate"
              value={balance}
              onChange={(event) =>
                setBalance(parseInt(event.currentTarget.value))
              }
            />
          </div>

          <input type="submit" className="btn btn-primary" value="Edit" />
        </form>
      </div>
    </div>
  );
};

export default EditBankAccount;
