using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SMS
{
    public class district
    {
        public district()
        {
        }

        public static void Save(Entity.SMS.district Entitydistrict)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.InputOutput, Entitydistrict.DistrictId);
                oDm.Add("@pDistrictName", SqlDbType.VarChar, 50, Entitydistrict.DistrictName);
                oDm.Add("@pShortName", SqlDbType.VarChar, 50, Entitydistrict.ShortName);
                oDm.Add("@pIsActive", SqlDbType.Bit, Entitydistrict.IsActive);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("sp_district_save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_district_GetAll");
            }
        }

        public static Entity.SMS.district GetAllById(int DistrictId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.Input, DistrictId);
                SqlDataReader dr = oDm.ExecuteReader("usp_district_GetAllById");
                Entity.SMS.district Entitydistrict = new Entity.SMS.district();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        Entitydistrict.DistrictId = DistrictId;
                        Entitydistrict.DistrictName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Entitydistrict.ShortName = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Entitydistrict.IsActive = (dr[3] == DBNull.Value) ? true : Convert.ToBoolean(dr[3].ToString());

                    }



                }
                return Entitydistrict;

            }

        }

        public static DataTable GetAllDistrictMaster()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_DistrictMaster_GetAll");
            }
        }

        public static DataTable GetAllBlock(int districtId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDistrictId", SqlDbType.Int, districtId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Block_GetAll");
            }
        }
    }

}


















