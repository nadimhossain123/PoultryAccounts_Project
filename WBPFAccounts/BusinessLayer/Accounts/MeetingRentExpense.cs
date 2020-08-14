using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Accounts
{
    public class MeetingRentExpense
    {
        public int SaveDetails(Entity.Accounts.MeetingRentExpense EntMeeting)
        {
            return DataAccess.Accounts.MeetingRentExpense.SaveDetails(EntMeeting);
        }

        public System.Data.DataTable GetAll(Entity.Accounts.MeetingRentExpense EntMeeting)
        {
            return DataAccess.Accounts.MeetingRentExpense.GetAll(EntMeeting);
        }

        public void Delete(int CBVHeaderID)
        {
            DataAccess.Accounts.MeetingRentExpense.Delete(CBVHeaderID);
        }

        public System.Data.DataTable GetAllById(int CBVHeaderID)
        {
            return DataAccess.Accounts.MeetingRentExpense.GetAllById(CBVHeaderID);
        }
    }
}
