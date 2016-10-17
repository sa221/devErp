using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class PartyManager
    {
        readonly PartyGatway _partyGatway = new PartyGatway();

        public bool InsertParty(Party party)
        {
            return _partyGatway.InsertParty(party);
        }

        public bool UpdateParty(Party party)
        {
            return _partyGatway.UpdateParty(party);
        }
        public bool DeleteParty(int partyId)
        {
            return _partyGatway.DeleteParty(partyId);
        }

        public List<Party> GetAllParty()
        {
            return _partyGatway.GetAllParty();
        }
    }
}