import React, { FC, useState, useEffect } from "react";
import BankTransactionManager from "../../main/utils/BankTransactionManager";
import LastTransaction from "../../main/components/LastTransaction";

const LastTransactions: FC = () => {
  const [lastTransactions, setLastTransactions] = useState([]);

  const fetchTransactions = async () => {
    const results = await BankTransactionManager.getAllTransactions();
    //setLastTransactions(results.data.resultData.data);
    console.log(results.data.resultData.data);
    setLastTransactions(results.data.resultData.data);
  };

  useEffect(() => {
    fetchTransactions();
  }, []);

  return (
    <div>
      <table className="w-75" style={{ border: "1px solid black", }}>
        <thead className="bg-dark text-white mx-auto">
          <tr>
            <th>Amount </th>
            <th>Id</th>
            <th>Bank ID</th>
          </tr>
        </thead>
        <tbody>
          {lastTransactions &&
            lastTransactions.map((transaction) => {
              return (
                <LastTransaction
                  lastTransaction={transaction}
                  key={transaction.id}
                />
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default LastTransactions;
