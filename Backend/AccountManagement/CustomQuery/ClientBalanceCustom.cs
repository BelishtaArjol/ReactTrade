using Entities.Dto;
using Entities.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AccountManagement.CustomQuery
{
    public class ClientBalanceCustom
    {
        public async static Task<List<ClientCustomModel>> GetClientBalance()
        {



            var resultlist = new List<ClientCustomModel>();

            using (var sqlConnection1 = new SqlConnection("server=192.168.10.248;Initial Catalog=AccountManagement; Persist Security Info=False;User ID=devadmin;Password=devadmin;"))
            {
                using (var cmd = new SqlCommand()
                {
                    CommandText = "select c.Username, c.FirstName, b.Code, b.Name,curr.Code, b.Balance from Clients c, BankAccounts b, Currencies curr",
                    CommandType = CommandType.Text,
                    Connection = sqlConnection1
                })
                {

                    sqlConnection1.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var objClient = new ClientCustomModel();
                            var userName = reader[0];
                            var firstName = reader[1];
                            var bankAccountCode = reader[2];
                            var bankAccountName = reader[3];
                            var currencyCode = reader[4];
                            var bankAccountBalance = reader[5];

                            objClient.UserName = Convert.ToString(userName);
                            objClient.FirstName = Convert.ToString(firstName);
                            objClient.BankAccountCode = Convert.ToString(bankAccountCode);
                            objClient.BankAccountName = Convert.ToString(bankAccountName);
                            objClient.CurrencyCode = Convert.ToString(currencyCode);
                            objClient.Balance = Convert.ToDecimal(bankAccountBalance);

                            resultlist.Add(objClient);
                        }
                    }
                }
            }

            return resultlist;
        }

        public async static Task<List<TransactionByAccountIdDTO>> GetTransactionsByAccountId(int id)
        {



            var resultlist = new List<TransactionByAccountIdDTO>();

            using (var sqlConnection1 = new SqlConnection("server=192.168.10.248;Initial Catalog=AccountManagement; Persist Security Info=False;User ID=devadmin;Password=devadmin;"))
            {
                using (var cmd = new SqlCommand()
                {
                    CommandText = "select Action, Amount, DateCreated  from BankTransactions where BankAccountId = @accountId order by DateCreated desc",
                    CommandType = CommandType.Text,
                    Connection = sqlConnection1
                })
                {
                    cmd.Parameters.Add(new SqlParameter("@accountId", id));

                    sqlConnection1.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            var action = reader[0];
                            var amount = reader[1];
                            var dateCreated = reader[2];

                            var objClient = new TransactionByAccountIdDTO();
                            objClient.Action = Convert.ToString(action);
                            objClient.Amount = Convert.ToDecimal(amount);
                            objClient.Date = Convert.ToDateTime(dateCreated);


                            resultlist.Add(objClient);
                        }
                    }
                }
            }

            return resultlist;
        }

        public async static Task<List<AccountByClientIdDTO>> GetAccountByClientId(int id)
        {



            var resultlist = new List<AccountByClientIdDTO>();

            using (var sqlConnection1 = new SqlConnection("server=192.168.10.248;Initial Catalog=AccountManagement; Persist Security Info=False;User ID=devadmin;Password=devadmin;"))
            {
                using (var cmd = new SqlCommand()
                {
                    CommandText = "select ba.Code, ba.Name, curr.ExchangeRate, ba.Balance from Currencies curr, BankAccounts ba where ba.CurrencyId = curr.Id and ba.ClientId = @clientId",
                    CommandType = CommandType.Text,
                    Connection = sqlConnection1
                })
                {
                    cmd.Parameters.Add(new SqlParameter("@clientId", id));

                    sqlConnection1.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            var accountCode = reader[0];
                            var accountName = reader[1];
                            var currency = reader[2];
                            var currentBalance = reader[2];

                            var objAccount = new AccountByClientIdDTO();
                            objAccount.AccountCode = Convert.ToString(accountCode);
                            objAccount.AccountCode = Convert.ToString(accountName);
                            objAccount.CurrencyCode = Convert.ToDecimal(currency);
                            objAccount.Balance = Convert.ToDecimal(currentBalance);


                            resultlist.Add(objAccount);
                        }
                    }
                }
            }

            return resultlist;
        }
    }
}
