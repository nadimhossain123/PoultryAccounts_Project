using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public static void GenerateSemFees(Entity.Common.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CompanyID_FK", SqlDbType.Int, SemFees.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, SemFees.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, SemFees.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, SemFees.DataFlow);

                if (SemFees.MembershipCategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, SemFees.MembershipCategoryId);

                if (SemFees.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, SemFees.BlockId);

                if (SemFees.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, SemFees.DistrictId);

                if (SemFees.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, SemFees.StateId);

                oDm.Add("@Month", SqlDbType.VarChar, SemFees.Month);
                oDm.Add("@Year", SqlDbType.Int, SemFees.Year);
                oDm.Add("@CreatedBy", SqlDbType.Int, SemFees.CreatedBy);
                oDm.Add("@BillDate", SqlDbType.DateTime, SemFees.BillDate);
                oDm.Add("@RowsAffected", SqlDbType.Int, ParameterDirection.InputOutput, SemFees.RowsAffected);

                oDm.ExecuteNonQuery("usp_MonthlySubscriptionFeesGeneration_Step1");
                SemFees.RowsAffected = (int)oDm["@RowsAffected"].Value;
            }
        }

        public static void GenerateYearlyFees(Entity.Common.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CompanyID_FK", SqlDbType.Int, SemFees.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, SemFees.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, SemFees.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, SemFees.DataFlow);

                if (SemFees.MembershipCategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, SemFees.MembershipCategoryId);

                if (SemFees.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, SemFees.BlockId);

                if (SemFees.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, SemFees.DistrictId);

                if (SemFees.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, SemFees.StateId);

                oDm.Add("@Year", SqlDbType.Int, SemFees.Year);
                oDm.Add("@CreatedBy", SqlDbType.Int, SemFees.CreatedBy);
                oDm.Add("@BillDate", SqlDbType.DateTime, SemFees.BillDate);
                oDm.Add("@RowsAffected", SqlDbType.Int, ParameterDirection.InputOutput, SemFees.RowsAffected);

                oDm.ExecuteNonQuery("usp_YearlySubscriptionFeesGeneration_Step1");
                SemFees.RowsAffected = (int)oDm["@RowsAffected"].Value;
            }
        }

        public static DataTable GetConsolidated_StudentOutstandingReport(Entity.Common.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                if (SemFees.MembershipCategoryId == 0)
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, SemFees.MembershipCategoryId);


                if (SemFees.BlockId == 0)
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, SemFees.BlockId);


                if (SemFees.DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, SemFees.DistrictId);


                if (SemFees.StateId == 0)
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, SemFees.StateId);

                if (SemFees.Month == string.Empty)
                    oDm.Add("@pMonth", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@pMonth", SqlDbType.VarChar, SemFees.Month);

                if (SemFees.Year == 0)
                    oDm.Add("@pYear", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pYear", SqlDbType.Int, SemFees.Year);

                if (SemFees.FeesHeadId == 0)
                    oDm.Add("@pFeesHeadId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pFeesHeadId", SqlDbType.Int, SemFees.FeesHeadId);

                if (SemFees.FromDate == null)
                    oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("pFromDate", SqlDbType.DateTime, SemFees.FromDate);

                if (SemFees.ToDate == null)
                    oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("@pToDate", SqlDbType.DateTime, SemFees.ToDate);


                return oDm.ExecuteDataTable("usp_GetConsolidatedStudentOutstandingReport_1");
            }
        }

        public static int GenerateMemberFees_Manual(Entity.Common.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CompanyID_FK", SqlDbType.Int, SemFees.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, SemFees.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, SemFees.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, SemFees.DataFlow);

                if (SemFees.MembershipCategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, SemFees.MembershipCategoryId);

                if (SemFees.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, SemFees.BlockId);

                if (SemFees.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, SemFees.DistrictId);

                if (SemFees.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, SemFees.StateId);

                //oDm.Add("@Month", SqlDbType.VarChar, SemFees.Month);
                //oDm.Add("@Year", SqlDbType.Int, SemFees.Year);
                oDm.Add("@SemNo", SqlDbType.Int, SemFees.SemNo);
                oDm.Add("@CreatedBy", SqlDbType.Int, SemFees.CreatedBy);
                oDm.Add("@BillDate", SqlDbType.DateTime, SemFees.BillDate);
                oDm.Add("@pMemberId", SqlDbType.Int, SemFees.MemberId);
                oDm.Add("@RowsAffected", SqlDbType.Int, ParameterDirection.InputOutput, SemFees.RowsAffected);

                return oDm.ExecuteNonQuery("usp_MonthlySubscriptionFeesGeneration_Step1_Manual");
                //SemFees.RowsAffected = (int)oDm["@RowsAffected"].Value;
            }
        }

        public static DataTable SMSFeeReceipt(string VoucherNo)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pVoucherNo", SqlDbType.VarChar, 50, VoucherNo);

                return oDm.ExecuteDataTable("usp_SMSReceipt");
            }
        }
    }
}
