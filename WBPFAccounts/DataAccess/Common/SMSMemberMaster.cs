using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class SMSMemberMaster
    {
        public SMSMemberMaster()
        {
        }

        public static int Save(Entity.Common.SMSMember smsmember, DateTime StartDate, DateTime EndDate, string txtRemarks)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pSMSMemberId", SqlDbType.Int, ParameterDirection.InputOutput, smsmember.SMSMemberId);
                oDm.Add("@pParentMemberId", SqlDbType.Int, smsmember.ParentMemberId);
                oDm.Add("@pMemberName", SqlDbType.VarChar, 150, smsmember.MemberName);
                oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, smsmember.MobileNo);
                oDm.Add("@pAddress", SqlDbType.VarChar, 500, smsmember.Address);
                oDm.Add("@pMemberType", SqlDbType.Int, smsmember.MemberType);
                oDm.Add("@pIsActive", SqlDbType.Bit, smsmember.IsActive);
                oDm.Add("@pIsNECCMember", SqlDbType.Bit, smsmember.IsNECCMember);
                oDm.Add("@pDistrictId", SqlDbType.Int, smsmember.DistrictId);

                oDm.Add("@pStartDate", SqlDbType.DateTime, StartDate);
                oDm.Add("@pEndDate", SqlDbType.DateTime, EndDate);
                oDm.Add("@pRemarks", SqlDbType.VarChar, 500, txtRemarks);
                oDm.Add("@pCreatedBy", SqlDbType.Int, smsmember.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("SMSMember_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("SMSMember_GetAll");
            }
        }

        public static Entity.Common.SMSMember GetSMSMemberById(int sMSMemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pSMSMemberId", SqlDbType.Int, ParameterDirection.Input, sMSMemberId);
                SqlDataReader dr = oDm.ExecuteReader("SMSMember_GetById");

                Entity.Common.SMSMember sMSMember = new Entity.Common.SMSMember();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sMSMember.SMSMemberId = sMSMemberId;
                        sMSMember.ParentMemberId = (dr[1] == DBNull.Value) ? 0 : int.Parse(dr[1].ToString());
                        sMSMember.MemberName = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        sMSMember.MobileNo = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        sMSMember.Address = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        sMSMember.MemberType = (dr[5] == DBNull.Value) ? 0 : int.Parse(dr[5].ToString());
                        sMSMember.IsActive = (dr[6] == DBNull.Value) ? false : Convert.ToBoolean(dr[6]);
                        sMSMember.StartDate = (dr[7] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[7]);
                        sMSMember.EndDate = (dr[8] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[8]);
                        sMSMember.Remarks = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();
                        sMSMember.IsNECCMember = (dr[10] == DBNull.Value) ? false : Convert.ToBoolean(dr[10]);
                        sMSMember.DistrictId = (dr[11] == DBNull.Value) ? 0 : Convert.ToInt32(dr[11]);
                    }
                }
                return sMSMember;
            }
        }
        public static void Delete(int sMSMemberId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pSMSMemberId", SqlDbType.Int, ParameterDirection.Input, sMSMemberId);

                oDm.ExecuteNonQuery("SMSMember_Delete");
            }
        }

        public static DataTable GetAllMember(string MemberName, string MobileNo, int MemberType, int DistrictId, int MemberCategoryId)
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
                if (MemberType == 0)
                    oDm.Add("@pMemberType", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMemberType", SqlDbType.Int, MemberType);

                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (MemberCategoryId == 0)
                { oDm.Add("@pIsNECCMember", SqlDbType.Bit, DBNull.Value); }
                else if (MemberCategoryId == 1)
                { oDm.Add("@pIsNECCMember", SqlDbType.Bit, true); }
                else { oDm.Add("@pIsNECCMember", SqlDbType.Bit, false); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("SMSMember_GetAll");
            }
        }

        public static DataTable GetAllMemberFroExpireDetails(string MemberName, string MobileNo, int MemberType, int DistrictId, int MemberCategoryId,DateTime FromDate,DateTime ToDate)
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
                if (MemberType == 0)
                    oDm.Add("@pMemberType", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMemberType", SqlDbType.Int, MemberType);

                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (MemberCategoryId == 0)
                { oDm.Add("@pIsNECCMember", SqlDbType.Bit, DBNull.Value); }
                else if (MemberCategoryId == 1)
                { oDm.Add("@pIsNECCMember", SqlDbType.Bit, true); }
                else { oDm.Add("@pIsNECCMember", SqlDbType.Bit, false); }

                if (FromDate == DateTime.MinValue)
                    oDm.Add("@pFromDate", SqlDbType.Date, DateTime.MinValue);
                else
                    oDm.Add("@pFromDate", SqlDbType.Date, FromDate);

                if (ToDate == DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.Date, DateTime.MaxValue);
                else
                    oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("SMSMemberExpireDetails_GetAll");
            }
        }





        public static DataTable GetMobileNumbersForSMS()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetMobileNumbersForSMS");
            }
        }

        public static DataTable GetAllDeviceIdForNotification()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetAllDeviceIdForNotification");
            }
        }

        public static DataTable GetAllMobileNumbersForSMS()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetAllMobileNumbersForSMS");
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
                return oDm.ExecuteDataTable("usp_SMSMemberLogDetails_GetAll");
            }

        }

        public static DataTable MemberTypeWiseMobileNumbersForSMS(int MemberType)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberType", SqlDbType.Int, ParameterDirection.Input, MemberType);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetMobileNumbersForSMS_MemberTypeWise");
            }
        }
    }
}
