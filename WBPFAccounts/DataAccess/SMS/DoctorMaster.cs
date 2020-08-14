using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SMS
{
    public class DoctorMaster
    {
        public static int Save(Entity.SMS.DoctorMaster doctor)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDoctorId", SqlDbType.Int, ParameterDirection.InputOutput, doctor.DoctorId);
                oDm.Add("@pFullName", SqlDbType.VarChar, 100, doctor.FullName);
                oDm.Add("@pGroupId", SqlDbType.Int, doctor.GroupId);
                oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, doctor.MobileNo);
                oDm.Add("@pDistrictId", SqlDbType.Int, doctor.DistrictId);
                oDm.Add("@pBlockId", SqlDbType.Int, doctor.BlockId);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_DoctorMaster_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_DoctorMaster_GetAll");
            }
        }

        public static void Delete(int doctorId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDoctorId", SqlDbType.Int, ParameterDirection.Input, doctorId);
                oDm.ExecuteNonQuery("usp_DoctorMaster_Delete");
            }
        }

        public static Entity.SMS.DoctorMaster GetById(int doctorId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDoctorId", SqlDbType.Int, ParameterDirection.Input, doctorId);
                SqlDataReader dr = oDm.ExecuteReader("usp_DoctorMaster_GetById");
                Entity.SMS.DoctorMaster docEntity = new Entity.SMS.DoctorMaster();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        docEntity.DoctorId = doctorId;
                        docEntity.FullName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        docEntity.GroupId = (dr[2] == DBNull.Value) ? 0 : Convert.ToInt32(dr[2].ToString());
                        docEntity.MobileNo = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        docEntity.DistrictId = (dr[4] == DBNull.Value) ? 0 : Convert.ToInt32(dr[4].ToString());
                        docEntity.BlockId = (dr[5] == DBNull.Value) ? 0 : Convert.ToInt32(dr[5].ToString());
                    }
                }
                return docEntity;
            }
        }

        public static DataTable GetDocNumbers(int GroupId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (GroupId == 0)
                    oDm.Add("@pGroupId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pGroupId", SqlDbType.Int, GroupId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetDocMobileNumbers");
            }
        }


    }
}
