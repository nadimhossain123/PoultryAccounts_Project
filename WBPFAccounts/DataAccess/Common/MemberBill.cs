using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MemberBill
    {
        public MemberBill()
        {
        }

        public static void Delete(int BillId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBillId", SqlDbType.Int, BillId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberBill_Delete");
            }
        }

        public static void Generate(Entity.Common.MemberBill bill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, bill.Month);
                oDm.Add("@Year", SqlDbType.Int, bill.Year);

                if (bill.MemberId != 0)
                    oDm.Add("@MemberId", SqlDbType.Int, bill.MemberId);
                else
                    oDm.Add("@MemberId", SqlDbType.Int, DBNull.Value);

                oDm.Add("@CreatedBy", SqlDbType.Int, bill.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberBill_Generate");
            }
        }

        public static void DevelopmentFeeGenerate(Entity.Common.MemberBill bill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, bill.Month);
                oDm.Add("@Year", SqlDbType.Int, bill.Year);

                if (bill.MemberId != 0)
                    oDm.Add("@MemberId", SqlDbType.Int, bill.MemberId);
                else
                    oDm.Add("@MemberId", SqlDbType.Int, DBNull.Value);

                oDm.Add("@CreatedBy", SqlDbType.Int, bill.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberDevelopmentFee_Generate");
            }
        }

        public static void DevelopmentFeeUpdate(Entity.Common.MemberBill bill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromMonth", SqlDbType.Int, bill.Month);
                oDm.Add("@ToMonth", SqlDbType.Int, bill.MonthTo);
                oDm.Add("@Year", SqlDbType.Int, bill.Year);
                oDm.Add("@NewAmount", SqlDbType.Int, bill.Amount);
                if (bill.MemberId != 0)
                    oDm.Add("@MemberId", SqlDbType.Int, bill.MemberId);
                else
                    oDm.Add("@MemberId", SqlDbType.Int, DBNull.Value);

                oDm.Add("@CreatedBy", SqlDbType.Int, bill.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberDevelopmentFee_Update");
            }
        }
        public static void RenewalFeeUpdate(Entity.Common.MemberBill bill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromMonth", SqlDbType.Int, bill.Month);
                oDm.Add("@ToMonth", SqlDbType.Int, bill.MonthTo);
                oDm.Add("@Year", SqlDbType.Int, bill.Year);
                oDm.Add("@NewAmount", SqlDbType.Int, bill.Amount);
                if (bill.MemberId != 0)
                    oDm.Add("@MemberId", SqlDbType.Int, bill.MemberId);
                else
                    oDm.Add("@MemberId", SqlDbType.Int, DBNull.Value);

                oDm.Add("@CreatedBy", SqlDbType.Int, bill.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberRenewalFee_Update");
            }
        }

        public static DataTable DevelopmentFeeBillGetAll(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_DevelopmentFeeBill_GetAll");
            }
        }

        public static DataTable MemberMonthlyBill(int StateId, int DistrictId, int BlockId, int CategoryId, int Month, int year, string MemberName, int BillId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId == 0)
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);
                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);
                if (BlockId == 0)
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);
                if (CategoryId == 0)
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, CategoryId);
                if (Month == 0)
                    oDm.Add("@pMonth", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMonth", SqlDbType.Int, Month);
                if (year == 0)
                    oDm.Add("@pYear", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pYear", SqlDbType.Int, year);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, MemberName);
                if (BillId == 0)
                    oDm.Add("@pBillId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBillId", SqlDbType.Int, BillId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberBillDetails");
            }
        }

        public static DataTable MemberMonthlyDevelopmentBill(int StateId, int DistrictId, int BlockId, int CategoryId, int Month, int year, string MemberName, int BillId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId == 0)
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);
                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);
                if (BlockId == 0)
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);
                if (CategoryId == 0)
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, CategoryId);
                if (Month == 0)
                    oDm.Add("@pMonth", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMonth", SqlDbType.Int, Month);
                if (year == 0)
                    oDm.Add("@pYear", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pYear", SqlDbType.Int, year);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, MemberName);
                if (BillId == 0)
                    oDm.Add("@pBillId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBillId", SqlDbType.Int, BillId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberDevelopmentBillDetails");
            }
        }

        public static void MonthlyDevelopmentBillUpdate(int BillId, decimal Amount)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBillId", SqlDbType.Int, BillId);
                oDm.Add("@pAmount", SqlDbType.Decimal, Amount);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MonthlyDevelopmentFeeUpdate");
            }
        }

        public static void RenewalBillAdjustment(int MemberId, int CreatedBy, string FeesXml)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pCreatedBy", SqlDbType.Int, CreatedBy);
                oDm.Add("@pFeesXml", SqlDbType.Xml, FeesXml);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberFee_Adjustment");
            }
        }

        public static DataTable RenewalAdjustmentDetails(string MemberName, int StateId, int DistrictId, int BlockId, int MembershipCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 100, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 100, MemberName);
                if (StateId == 0)
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);
                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);
                if (BlockId == 0)
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);
                if (MembershipCategoryId == 0)
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberRenewalAdjustment_GetAll");
            }
        }

        public static bool CheckingDevelopmentFeeGeneration(Entity.Common.MemberBill bill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, bill.Month);
                oDm.Add("@Year", SqlDbType.Int, bill.Year);
                oDm.CommandType = CommandType.StoredProcedure;
                DataTable dt = oDm.ExecuteDataTable("usp_DevelopmentFeeGeneration_Checking");
                if (dt.Rows.Count > 5) return true;
                else return false;
            }
        }
    }
}