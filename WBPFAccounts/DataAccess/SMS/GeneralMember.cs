using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.SMS
{
    public class GeneralMember
    {
        public GeneralMember()
        {
        }

        public static int Save(Entity.SMS.GeneralMember generalMember)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, generalMember.MemberId);
                oDm.Add("@Name", SqlDbType.VarChar,  generalMember.Name);
                oDm.Add("@CompanyName", SqlDbType.VarChar,  generalMember.CompanyName);
                oDm.Add("@Village", SqlDbType.VarChar,  generalMember.Village);
                oDm.Add("@PostOffice", SqlDbType.VarChar,  generalMember.PostOffice);
                oDm.Add("@PoliceStation", SqlDbType.VarChar,  generalMember.PoliceStation);
                oDm.Add("@PinCode", SqlDbType.VarChar,  generalMember.PinCode);
                oDm.Add("@BlockName", SqlDbType.VarChar,  generalMember.BlockName);
                oDm.Add("@DistrictName", SqlDbType.VarChar,  generalMember.DistrictName);
                oDm.Add("@StateName", SqlDbType.VarChar,  generalMember.StateName);
                oDm.Add("@Code", SqlDbType.VarChar,  generalMember.Code);
                oDm.Add("@MobileNo", SqlDbType.VarChar,  generalMember.MobileNo);
                oDm.Add("@PhoneNo", SqlDbType.VarChar,  generalMember.PhoneNo);
                oDm.Add("@CategoryId", SqlDbType.Int, generalMember.CategoryId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_GeneralMember_Save");
            }
        }

        public static DataTable GetAll(string Name, string MobileNo)
        {
            using (DataManager oDm = new DataManager())
            {
                if (Name.Trim().Length > 0)
                    oDm.Add("@Name", SqlDbType.VarChar, 100, Name);
                else
                    oDm.Add("@Name", SqlDbType.VarChar, 100, DBNull.Value);

                if (MobileNo.Trim().Length > 0)
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 30, MobileNo);
                else
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 30, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GeneralMember_GetAll");
            }
        }

        public static DataTable GetAllById(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GeneralMember_GetAllById");
            }
        }

        public static void Delete(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_GeneralMember_Delete");
            }
        }
    }
}
