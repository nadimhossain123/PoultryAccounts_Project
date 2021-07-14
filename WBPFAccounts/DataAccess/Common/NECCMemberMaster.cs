using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Common
{
    public class NECCMemberMaster
    {
        public static int Save(Entity.Common.NECCMemberMaster smsmember, string txtRemarks)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pNECCMemberId", SqlDbType.Int, ParameterDirection.InputOutput, smsmember.NECCMemberId);
                oDm.Add("@pMemberName", SqlDbType.VarChar, 150, smsmember.MemberName);
                oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, smsmember.MobileNo);
                oDm.Add("@pAddress", SqlDbType.VarChar, 500, smsmember.Address);
                oDm.Add("@pIsActive", SqlDbType.Bit, smsmember.IsActive);
                oDm.Add("@pRemarks", SqlDbType.VarChar, 500, txtRemarks);
                oDm.Add("@pDistrictId", SqlDbType.Int, smsmember.DistrictId);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("NECCMember_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("NECCMember_GetAll");
            }
        }

        public static Entity.Common.NECCMemberMaster GetNECCMemberById(int NECCMemberId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pNECCMemberId", SqlDbType.Int, ParameterDirection.Input, NECCMemberId);

                SqlDataReader dr = oDm.ExecuteReader("NECCMember_GetById");

                Entity.Common.NECCMemberMaster NECCMember = new Entity.Common.NECCMemberMaster();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        NECCMember.NECCMemberId = NECCMemberId;
                        NECCMember.DistrictId = (dr[1] == DBNull.Value) ? 0 : int.Parse(dr["DistrictId"].ToString());
                        NECCMember.MemberName = (dr[2] == DBNull.Value) ? "" : dr["MemberName"].ToString();
                        NECCMember.MobileNo = (dr[3] == DBNull.Value) ? "" : dr["MobileNo"].ToString();
                        NECCMember.Address = (dr[4] == DBNull.Value) ? "" : dr["Address"].ToString();
                        NECCMember.IsActive = (dr[5] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsActive"]);
                        NECCMember.Remarks = (dr[6] == DBNull.Value) ? "" : dr["Remarks"].ToString();

                    }
                }
                return NECCMember;
            }
        }

        public static void Delete(int NECCMemberId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pNECCMemberId", SqlDbType.Int, ParameterDirection.Input, NECCMemberId);

                oDm.ExecuteNonQuery("NECCMember_Delete");
            }
        }

        public static DataTable GetAllMember(string MemberName, string MobileNo, int DistrictId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                if (MobileNo == "")
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);
                else
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                //if (MemberType == 0)
                //    oDm.Add("@pMemberType", SqlDbType.Int, DBNull.Value);
                //else
                //    oDm.Add("@pMemberType", SqlDbType.Int, MemberType);

                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("NECCMember_GetAll");
            }
        }

        public static DataTable GetMobileNumbersForNECC()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetMobileNumbersForNECC");
            }
        }

        public static DataTable GetAllMobileNumbersForNECC()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetAllMobileNumbersForNECC");
            }

        }
        public static DataTable GetAllMemberLogDetails(string MemberName, string MobileNo, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                if (MobileNo == "")
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);
                else
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                if (FromDate != DateTime.MinValue)
                    oDm.Add("@pFromDate", SqlDbType.DateTime, FromDate);
                else
                    oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value);
                if (ToDate != DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.DateTime, ToDate);
                else
                    oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_NECCMemberLogDetails_GetAll");
            }

        }

        //public static DataTable MemberTypeWiseMobileNumbersForNECC(int MemberType)
        //{
        //    using (DataManager oDm = new DataManager())
        //    {
        //        oDm.Add("@pMemberType", SqlDbType.Int, ParameterDirection.Input, MemberType);
        //        oDm.CommandType = CommandType.StoredProcedure;
        //        return oDm.ExecuteDataTable("usp_GetMobileNumbersForNECC_MemberTypeWise");
        //    }
        //}
    }
}
