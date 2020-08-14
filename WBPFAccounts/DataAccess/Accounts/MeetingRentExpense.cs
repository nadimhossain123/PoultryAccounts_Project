using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Accounts
{
    public class MeetingRentExpense
    {
        public static int SaveDetails(Entity.Accounts.MeetingRentExpense EntMeeting)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CBVHeaderID", SqlDbType.Int, EntMeeting.CBVHeaderID);
                oDm.Add("@PaymentDate", SqlDbType.Date, EntMeeting.PaymentDate);
                oDm.Add("@DistrictAmountXML", SqlDbType.Xml, EntMeeting.DistrictAmountXML);
                oDm.Add("@CreatedBy", SqlDbType.Int, EntMeeting.CreatedBy);

                return oDm.ExecuteNonQuery("usp_MeetingRentExpense_Save");
            }
        }
        public static DataTable GetAll(Entity.Accounts.MeetingRentExpense EntMeeting)
        {
            using (DataManager oDm = new DataManager())
            {
                if (EntMeeting.LedgerId > 0)
                    oDm.Add("@LedgerId", SqlDbType.Int, EntMeeting.LedgerId);
                else
                    oDm.Add("@LedgerId", SqlDbType.Int, DBNull.Value);

                if (EntMeeting.DistrictId > 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, EntMeeting.DistrictId);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);

                if (EntMeeting.FromDate != DateTime.MinValue)
                    oDm.Add("@FromDate", SqlDbType.Date, EntMeeting.FromDate);
                else
                    oDm.Add("@FromDate", SqlDbType.Date, DBNull.Value);

                if (EntMeeting.ToDate != DateTime.MinValue)
                    oDm.Add("@ToDate", SqlDbType.Date, EntMeeting.ToDate);
                else
                    oDm.Add("@ToDate", SqlDbType.Date, DBNull.Value);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MeetingRentExpense_Report");
            }
        }

        public static void Delete(int CBVHeaderID)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CBVHeaderID", SqlDbType.Int, CBVHeaderID);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MeetingRentExpense_Delete");
            }
        }

        public static DataTable GetAllById(int CBVHeaderID)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CBVHeaderID", SqlDbType.Int, ParameterDirection.Input, CBVHeaderID);

                return oDm.ExecuteDataTable("usp_MeetingRentExpense_GetAllById");
            }
        }
    }
}
