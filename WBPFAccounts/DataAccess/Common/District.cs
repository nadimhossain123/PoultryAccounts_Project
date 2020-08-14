using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class District
    {
        public District()
        {

        }

        public static void Save(Entity.Common.District district)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.InputOutput, district.DistrictId);
                oDm.Add("@pDistrictName", SqlDbType.VarChar, 50, district.DistrictName);
                oDm.Add("@pStateId", SqlDbType.Int, district.StateId);
                oDm.Add("@pCode", SqlDbType.VarChar, 20, district.Code);
                if (district.Remarks == string.Empty)
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, district.Remarks);


                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("District_Save");

                district.DistrictId = (int)oDm["@pDistrictId"].Value;
            }
        }

        public static DataTable GetAll(int stateid)
        {
            using (DataManager oDm = new DataManager())
            {
                if (stateid == 0)
                    oDm.Add("@StateId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, ParameterDirection.Input, stateid);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("District_GetAll");
            }
        }

        public static DataTable SMSDistrictGetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("SMSDistrict_GetAll");
            }
        }

        public static Entity.Common.District GetDistrictById(int districtId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.Input, districtId);

                SqlDataReader dr = oDm.ExecuteReader("District_GetById");

                Entity.Common.District district = new Entity.Common.District();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        district.DistrictId = districtId;
                        district.DistrictName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        district.StateId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        district.Code = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        district.Remarks = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();

                    }
                }
                return district;
            }
        }

        public static void Delete(int districtId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.Input, districtId);

                oDm.ExecuteNonQuery("District_Delete");
            }
        }
    }
}