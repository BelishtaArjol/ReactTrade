import React, { FC, useState, useEffect } from "react";
import BankAccountManager from "../../main/utils/bankAccountManager";
import BankAccount from "../../main/components/BankAccount";
import { useNavigate } from "react-router-dom";

const BankAccounts: FC = () => {
  const [bankAccounts, setbankAccounts] = useState([]);
  const navigate = useNavigate();

  const fetchBankAccounts = async () => {
    const results = await BankAccountManager.getAllBanks();
    setbankAccounts(results.data.resultData.data);
  };

  useEffect(() => {
    fetchBankAccounts();
  }, []);

  return (
    <div>
      <div className="container">
        <div className="row">
          <div className="col-md">
            <table style={{ border: "1px solid black", width: "700px" }}>
              <thead>
                <tr>
                  <th> Code </th>
                  <th> Name </th>
                  <th> Balance </th>
                  <th> Edit </th>
                  <th> Delete </th>
                </tr>
              </thead>
              <tbody>
                {bankAccounts.map((bank) => {
                  return <BankAccount bankAccount={bank} key={bank.id} />;
                })}
              </tbody>
            </table>
          </div>
          <div className="col-md d-flex justify-content-center align-items-center">
            <button
              onClick={() => navigate("/AddBankAccount")}
              className="btn btn-primary"
            >
              Add Bank Account
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BankAccounts;
