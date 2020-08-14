using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class BusinessType
    {
        public BusinessType()
        {

        }

        public static void Save(Entity.Common.BusinessType businesstype)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBusinessTypeId", SqlDbType.Int, ParameterDirection.InputOutput, businesstype.BusinessTypeId);
                oDm.Add("@pBusinessTypeName", SqlDbType.VarChar, 50, businesstype.BusinessTypeName);
                if (businesstype.Remarks == string.Empty)
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, businesstype.Remarks);


                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("BusinessType_Save");

                businesstype.BusinessTypeId = (int)oDm["@pBusinessTypeId"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("BusinessType_GetAll");
            }
        }

        public static Entity.Common.BusinessType GetBusinessTypeById(int businessTypeId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pBusinessTypeId", SqlDbType.Int, ParameterDirection.Input, businessTypeId);

                SqlDataReader dr = oDm.ExecuteReader("BusinessType_GetById");

                Entity.Common.BusinessType businessType = new Entity.Common.BusinessType();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        businessType.BusinessTypeId = businessTypeId;
                        businessType.BusinessTypeName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        businessType.Remarks = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();

                    }
                }
                return businessType;
            }
        }

        public static void Delete(int businessTypeId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pBusinessTypeId", SqlDbType.Int, ParameterDirection.Input, businessTypeId);

                oDm.ExecuteNonQuery("BusinessType_Delete");
            }
        }
    }
}