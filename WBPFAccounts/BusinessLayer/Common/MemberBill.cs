using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberBill
    {
        public MemberBill()
        {
        }

        public void Delete(int BillId)
        {
            DataAccess.Common.MemberBill.Delete(BillId);
        }

        public void Generate(Entity.Common.MemberBill bill)
        {
            DataAccess.Common.MemberBill.Generate(bill);
        }

        public void DevelopmentFeeGenerate(Entity.Common.MemberBill bill)
        {
            DataAccess.Common.MemberBill.DevelopmentFeeGenerate(bill);
        }
        public void DevelopmentFeeUpdate(Entity.Common.MemberBill bill)
        {
            DataAccess.Common.MemberBill.DevelopmentFeeUpdate(bill);
        }
        public void RenewalFeeUpdate(Entity.Common.MemberBill bill)
        {
            DataAccess.Common.MemberBill.RenewalFeeUpdate(bill);
        }


        public DataTable DevelopmentFeeBillGetAll(int MemberId)
        {
            return DataAccess.Common.MemberBill.DevelopmentFeeBillGetAll(MemberId);
        }

        public DataTable MemberMonthlyBill(int StateId, int DistrictId, int BlockId, int CategoryId, int Month, int year, string MemberName, int BillId)
        {
            return DataAccess.Common.MemberBill.MemberMonthlyBill(StateId, DistrictId, BlockId, CategoryId, Month, year, MemberName, BillId);
        }

        public DataTable MemberMonthlyDevelopmentBill(int StateId, int DistrictId, int BlockId, int CategoryId, int Month, int year, string MemberName, int BillId)
        {
            return DataAccess.Common.MemberBill.MemberMonthlyDevelopmentBill(StateId, DistrictId, BlockId, CategoryId, Month, year, MemberName, BillId);
        }

        public void MonthlyDevelopmentBillUpdate(int BillId, decimal Amount)
        {
            DataAccess.Common.MemberBill.MonthlyDevelopmentBillUpdate(BillId, Amount);
        }

        public void RenewalBillAdjustment(int MemberId, int CreatedBy, string FeesXml)
        {
            DataAccess.Common.MemberBill.RenewalBillAdjustment(MemberId, CreatedBy, FeesXml);
        }

        public DataTable RenewalAdjustmentDetails(string MemberName, int StateId, int DistrictId, int BlockId, int MembershipCategoryId)
        {
            return DataAccess.Common.MemberBill.RenewalAdjustmentDetails(MemberName, StateId, DistrictId, BlockId, MembershipCategoryId);
        }

        public bool CheckingDevelopmentFeeGeneration(Entity.Common.MemberBill bill)
        {
            return DataAccess.Common.MemberBill.CheckingDevelopmentFeeGeneration(bill);
        }
    }
}
