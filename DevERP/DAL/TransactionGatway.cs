using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class TransactionGatway : ConnectionGateway
    {
        public bool InsertTransaction(Transaction transaction)
        {
            Query = "Insert into Transactions (transactionDate,itemId,subItemId,amount,transactionCatagory,partyId,transactionType,bankId,remarks)" +
                    " values (@transactionDate,@itemId,@subItemId,@amount,@transactionCatagory,@partyId,@transactionType,@bankId,@remarks)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@transactionDate", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@itemId", transaction.ItemId);
            Command.Parameters.AddWithValue("@subItemId", transaction.SubItemId);
            Command.Parameters.AddWithValue("@amount", transaction.Amount);
            Command.Parameters.AddWithValue("@transactionCatagory", transaction.TransactionCatagory);
            Command.Parameters.AddWithValue("@partyId", transaction.PartyId);
            Command.Parameters.AddWithValue("@transactionType", transaction.TransactionType);
            Command.Parameters.AddWithValue("@bankId", transaction.BankId);
            Command.Parameters.AddWithValue("@remarks", transaction.Remarks);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery()>0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public bool UpdateTransaction(Transaction transaction)
        {
            Query = "Update Transactions set transactionDate=@transactionDate,itemId=@itemId,subItemId=@subItemId,amount=@amount,partyId=@partyId," +
                    "transactionType=@transactionType,bankId=@bankId,remarks=@remarks where transactionId = @transactionId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@transactionId", transaction.TransactionId);
            Command.Parameters.AddWithValue("@transactionDate", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@itemId", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@subItemId", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@amount", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@partyId", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@transactionType", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@bankId", transaction.TransactionDate);
            Command.Parameters.AddWithValue("@remarks", transaction.TransactionDate);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public bool DeleteTransaction(int transactionId)
        {
            Query = "delete from Transactions where transactionId = @transactionId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@transactionId", transactionId);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public List<Transaction> GetAllTransaction()
        {
            Query = "Select * from transactions";
            PrepareCommand(CommandType.Text);
            List<Transaction> transactions = new List<Transaction>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.TransactionId = Convert.ToInt32(Reader["transactionId"].ToString());
                    transaction.TransactionDate = Convert.ToDateTime(Reader["transactionDate"].ToString());
                    transaction.ItemId = Convert.ToInt32(Reader["ItemId"].ToString());
                    transaction.SubItemId = Convert.ToInt32(Reader["subItemId"].ToString());
                    transaction.Amount = Convert.ToDecimal(Reader["amount"].ToString());
                    transaction.TransactionCatagory = Reader["transactionCatagory"].ToString();
                    transaction.PartyId = Convert.ToInt32(Reader["partyId"].ToString());
                    transaction.TransactionType = Reader["transactionType"].ToString();
                    transaction.BankId = Convert.ToInt32(Reader["bankId"].ToString());
                    transaction.Remarks = Reader["remarks"].ToString();
                    transaction.LastModify = Convert.ToDateTime(Reader["lastModify"].ToString());
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
    }
}