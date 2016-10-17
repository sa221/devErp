using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class PartyGatway :ConnectionGateway
    {
        public bool InsertParty(Party party)
        {
            Query = "Insert into Party (partyName,partyMobile,partyAddress) values (@partyName,@partyMobile,@partyAddress)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@partyName", party.PartyName);
            Command.Parameters.AddWithValue("@partyMobile", party.PartyMobile);
            Command.Parameters.AddWithValue("@partyAddress", party.PartyAddress);
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
        public bool UpdateParty(Party party)
        {
            Query = "Update Party set partyName=@partyName,partyMobile=@partyMobile,partyAddress=@partyAddress where partyId = @partyId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@partyId", party.PartyId);
            Command.Parameters.AddWithValue("@partyName", party.PartyName);
            Command.Parameters.AddWithValue("@partyMobile", party.PartyMobile);
            Command.Parameters.AddWithValue("@partyAddress", party.PartyAddress);
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
        public bool DeleteParty(int partyId)
        {
            Query = "delete from party where partyId = @partyId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@partyId", partyId);
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
        public List<Party> GetAllParty()
        {
            Query = "Select * from party";
            PrepareCommand(CommandType.Text);
            List<Party> parties = new List<Party>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Party party = new Party();
                    party.PartyId = Convert.ToInt32(Reader["partyId"].ToString());
                    party.PartyName = Reader["partyName"].ToString();
                    party.PartyMobile = Reader["partyMobile"].ToString();
                    party.PartyAddress = Reader["partyAddress"].ToString();
                    parties.Add(party);
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
            return parties;
        }
    }
}