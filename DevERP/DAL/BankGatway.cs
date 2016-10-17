using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class BankGatway :ConnectionGateway
    {
        public bool InsertBank(string bankName)
        {
            Query = "Insert into Bank (bankName) values (@bankName)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@bankName", bankName);
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
        public bool UpdateBank(Bank bank)
        {
            Query = "Update Bank set bankName=@bankName where bankId = @bankId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@bankName", bank.BankName);
            Command.Parameters.AddWithValue("@bankId", bank.BankId);
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
        public bool DeleteBank(int bankId)
        {
            Query = "delete from Bank where bankId = @bankId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@bankId", bankId);
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
        public List<Bank> GetAllBank()
        {
            Query = "Select * from Bank";
            PrepareCommand(CommandType.Text);
            List<Bank> banks = new List<Bank>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Bank bank = new Bank();
                    bank.BankId = Convert.ToInt32(Reader["bankId"].ToString());
                    bank.BankName = Reader["bankName"].ToString();
                    banks.Add(bank);
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
            return banks;
        }
    }
}