import React, { FC } from "react";

const LastTransaction: FC<any> = (props: any) => {
  console.log(props);

  return (
    <tr>
      <td> {props.lastTransaction.amount} </td>
      <td> {props.lastTransaction.id} </td>
      <td> {props.lastTransaction.bankAccountId} </td>
    </tr>
  );
};

export default LastTransaction;
