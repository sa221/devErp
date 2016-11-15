using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class TransactionViewGatway : ConnectionGateway
    {
        
        public List<Transaction> GetAllTransaction(string query)
        {
            string preQuery = "select t.transactionId,t.transactionDate,t.itemId,i.itemName,t.subItemId,s.subItemName,t.amount,t.catagory,t.partyId,p.partyName,t.paymentType,t.bankId,b.bankName,t.remarks,t.chequeStatus,t.lastModify from Transactions as t left outer join Item as i on t.itemId=i.itemId left outer join SubItem as s on t.subItemId=s.subItemId left outer join Bank as b on t.bankId=b.bankId left outer join Party as p on t.partyId=p.partyId ";
            Query = preQuery+query;
            PrepareCommand(CommandType.Text);
            List<Transaction> transactions = new List<Transaction>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.TransactionId = Reader["transactionId"] != DBNull.Value
                        ? Convert.ToInt32(Reader["transactionId"].ToString())
                        : 0;
                    transaction.TransactionDate = Reader["transactionDate"] != DBNull.Value
                        ? Convert.ToDateTime(Reader["transactionDate"].ToString())
                        : DateTime.MaxValue;
                    transaction.ItemId = Reader["itemId"] != DBNull.Value
                        ? Convert.ToInt32(Reader["itemId"].ToString())
                        : 0;
                    transaction.ItemName = Reader["itemName"] != DBNull.Value
                        ? Reader["itemName"].ToString()
                        : string.Empty;
                    transaction.SubItemId = Reader["subItemId"] != DBNull.Value
                        ? Convert.ToInt32(Reader["subItemId"].ToString())
                        : 0;
                    transaction.SubItemName = Reader["subItemName"] != DBNull.Value
                        ? Reader["subItemName"].ToString()
                        : string.Empty;
                    transaction.Amount = Reader["amount"] != DBNull.Value
                        ? Convert.ToDecimal(Reader["amount"].ToString())
                        : 0;
                    transaction.Catagory = Reader["catagory"] != DBNull.Value
                        ? Reader["catagory"].ToString()
                        : string.Empty;
                    transaction.PartyId = Reader["partyId"] != DBNull.Value
                        ? Convert.ToInt32(Reader["partyId"].ToString())
                        : 0;
                    transaction.PartyName = Reader["partyName"] != DBNull.Value
                        ? Reader["partyName"].ToString()
                        : string.Empty;
                    transaction.PaymentType = Reader["paymentType"] != DBNull.Value
                        ? Reader["paymentType"].ToString()
                        : string.Empty;
                    transaction.BankId = Reader["bankId"] != DBNull.Value
                        ? Convert.ToInt32(Reader["bankId"].ToString())
                        : 0;
                    transaction.BankName = Reader["bankName"] != DBNull.Value
                        ? Reader["bankName"].ToString()
                        : string.Empty;
                    transaction.Remarks = Reader["remarks"] != DBNull.Value
                        ? Reader["remarks"].ToString()
                        : string.Empty;
                    transaction.ChequeStatus = Reader["chequeStatus"] != DBNull.Value
                       ? Reader["chequeStatus"].ToString()
                       : string.Empty;
                    transaction.LastModify = Reader["lastModify"] != DBNull.Value
                        ? Convert.ToDateTime(Reader["lastModify"].ToString())
                        : DateTime.MaxValue;
                    transactions.Add(transaction);
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseAllConnection();
                
            }
            return transactions;
        }
        public decimal GetBalance(string query, out bool isSuccss)
        {
            Query = "select ((select ISNULL(SUM(amount),0) from (select * from GetPassTransaction) as t " + query + " and t.catagory='income' )-(select ISNULL(SUM(amount),0) from (select * from GetPassTransaction) as t " + query + " and t.catagory='expence')) as balance";
            
            PrepareCommand(CommandType.Text);
            isSuccss = true;
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    return (decimal)Reader["balance"];
                }
                isSuccss = false;
                return 0;
            }
            catch (Exception)
            {
                isSuccss = false;
                return 0;
            }
            finally
            {
                CloseAllConnection();

            }
        }
    }
}