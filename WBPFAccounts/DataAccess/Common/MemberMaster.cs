using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MemberMaster
    {
        public MemberMaster()
        {

        }

        public static void ChangePassword(int MemberId, string Password)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, MemberId);
                oDm.Add("@Password", SqlDbType.VarChar, 50, Password);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberMaster_ChangePassword");
            }
        }

        public static void MemberActivate(int MemberId, bool IsActive)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pIsActive", SqlDbType.Bit, IsActive);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberMaster_Activate");
            }
        }

        public static void MemberApprove(int MemberId, int CreatedBy)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pCreatedBy", SqlDbType.Int, CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberMaster_Approve");
            }
        }

        public static void Save(Entity.Common.MemberMaster membermaster)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int,ParameterDirection.InputOutput, membermaster.MemberId);
                oDm.Add("@pMemberName", SqlDbType.VarChar, 200, membermaster.MemberName);
                oDm.Add("@pMemberCode", SqlDbType.VarChar, 100, ParameterDirection.Input, membermaster.MemberCode);
                oDm.Add("@pMobileNo", SqlDbType.VarChar, 100, membermaster.MobileNo);
                oDm.Add("@pPhoneId", SqlDbType.VarChar, 100, membermaster.PhoneId);
                oDm.Add("@pVATNo", SqlDbType.VarChar, 100, membermaster.VATNo);
                oDm.Add("@pPANNo", SqlDbType.VarChar, 100, membermaster.PANNo);
                oDm.Add("@pLICNo", SqlDbType.VarChar, 100, membermaster.LICNo);
                oDm.Add("@pGSTNo", SqlDbType.VarChar, 100, membermaster.GSTNo);
                oDm.Add("@pVillageOrStreet", SqlDbType.VarChar, 200, membermaster.VillageOrStreet);
                oDm.Add("@pPlotNo", SqlDbType.VarChar, 100, membermaster.PlotNo);
                oDm.Add("@pKhaitanNo", SqlDbType.VarChar, 100, membermaster.KhaitanNo);
                oDm.Add("@pMouza", SqlDbType.VarChar, 100, membermaster.Mouza);
                oDm.Add("@pJLNo", SqlDbType.VarChar, 100, membermaster.JLNo);
                oDm.Add("@pPO", SqlDbType.VarChar, 100, membermaster.PO);
                oDm.Add("@pPS", SqlDbType.VarChar, 100, membermaster.PS);
                oDm.Add("@pPIN", SqlDbType.VarChar, 10, membermaster.PIN);
                oDm.Add("@pBlockId", SqlDbType.Int, membermaster.BlockId);
                oDm.Add("@pDistrictId", SqlDbType.Int, membermaster.DistrictId);
                oDm.Add("@pStateId", SqlDbType.Int, membermaster.StateId);
                oDm.Add("@pCompanyName", SqlDbType.VarChar, 200, membermaster.CompanyName);
                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);
                oDm.Add("@IsMember", SqlDbType.Bit, membermaster.IsMember);
                if (membermaster.IsMember)
                {
                    oDm.Add("@pMemberGroupId", SqlDbType.Int, membermaster.MemberGroupId);
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, membermaster.BusinessTypeId);
                }
                else
                {
                    oDm.Add("@pMemberGroupId", SqlDbType.Int, DBNull.Value);
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                }
                oDm.Add("@pMembershipDate", SqlDbType.Date, membermaster.MembershipDate);
                oDm.Add("@pEffectiveDate", SqlDbType.DateTime, membermaster.EffectiveDate);
                //oDm.Add("@pOpBal", SqlDbType.Decimal, membermaster.OpBal);
                //oDm.Add("@pDrORCr", SqlDbType.VarChar, 2, membermaster.DrORCr);
                if (membermaster.LayerCapacityNos == string.Empty)
                    oDm.Add("@pLayerCapacityNos", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pLayerCapacityNos", SqlDbType.VarChar, 50, membermaster.LayerCapacityNos);

                if (membermaster.BroilerCapacityNos == string.Empty)
                    oDm.Add("@pBroilerCapacityNos", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pBroilerCapacityNos", SqlDbType.VarChar, 50, membermaster.BroilerCapacityNos);

                if (membermaster.BreederCapacityNos == string.Empty)
                    oDm.Add("@pBreederCapacityNos", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pBreederCapacityNos", SqlDbType.VarChar, 50, membermaster.BreederCapacityNos);

                if (membermaster.EggSellerDailySalesNos == string.Empty)
                    oDm.Add("@pEggSellerDailySalesNos", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pEggSellerDailySalesNos", SqlDbType.VarChar, 50, membermaster.EggSellerDailySalesNos);

                if (membermaster.ChickenSellerDailySalesNos == string.Empty)
                    oDm.Add("@pChickenSellerDailySalesNos", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pChickenSellerDailySalesNos", SqlDbType.VarChar, 50, membermaster.ChickenSellerDailySalesNos);

                if (membermaster.ChickenSellerDailySalesKgs == string.Empty)
                    oDm.Add("@pChickenSellerDailySalesKgs", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pChickenSellerDailySalesKgs", SqlDbType.VarChar, 50, membermaster.ChickenSellerDailySalesKgs);

                if (membermaster.FeedProducerDailySalesMT == string.Empty)
                    oDm.Add("@pFeedProducerDailySalesMT", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pFeedProducerDailySalesMT", SqlDbType.VarChar, 50, membermaster.FeedProducerDailySalesMT);

                if (membermaster.FeedSellerDailySalesMT == string.Empty)
                    oDm.Add("@pFeedSellerDailySalesMT", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pFeedSellerDailySalesMT", SqlDbType.VarChar, 50, membermaster.FeedSellerDailySalesMT);

                oDm.Add("@pOtherCategory", SqlDbType.VarChar, 100, membermaster.OtherCategory);
                oDm.Add("@pRemarks", SqlDbType.VarChar, 200, membermaster.Remarks);
                oDm.Add("@pUserId", SqlDbType.Int, membermaster.UserId);
                oDm.Add("@pCompanyId", SqlDbType.Int, membermaster.CompanyId);
                oDm.Add("@BranchID_FK", SqlDbType.Int, membermaster.BranchId);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, membermaster.FinYearId);
                oDm.Add("@DataFlow", SqlDbType.Int, membermaster.DataFlow);
                oDm.Add("@pWebsite", SqlDbType.VarChar, 100, membermaster.Website);
                if (membermaster.ImageExt == string.Empty)
                    oDm.Add("@ImageExt", SqlDbType.VarChar, 100, DBNull.Value);
                else
                    oDm.Add("@ImageExt", SqlDbType.VarChar, 100, membermaster.ImageExt);

                if (membermaster.MemberSMSCategoryId == 0)
                    oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, membermaster.MemberSMSCategoryId);

                oDm.Add("@IsGovtMember", SqlDbType.Bit, membermaster.IsGovtMember);
                oDm.Add("@pEmail", SqlDbType.VarChar, 50, membermaster.Email);
                oDm.Add("@pMobileNo2", SqlDbType.VarChar, 20, membermaster.MobileNo2);
                oDm.Add("@pNarration", SqlDbType.VarChar, 255, membermaster.Narration);
                oDm.Add("@pMembershipCategoryEffectiveDate", SqlDbType.Date, membermaster.MembershipCategoryEffectiveDate);

                oDm.Add("@pIsExecutiveMember", SqlDbType.Bit, membermaster.IsExecutiveMember);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("MemberMaster_Save");

                membermaster.MemberId = (int)oDm["@pMemberId"].Value;
            }
        }

        public static void MemberMaster_BlockChange_Save(Entity.Common.MemberMaster membermaster)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pOldMemberId", SqlDbType.Int, membermaster.OldMemberId);
                oDm.Add("@pMemberCode", SqlDbType.VarChar, 100, ParameterDirection.InputOutput, membermaster.MemberCode);
                oDm.Add("@pVillageOrStreet", SqlDbType.VarChar, 200, membermaster.VillageOrStreet);
                oDm.Add("@pPlotNo", SqlDbType.VarChar, 100, membermaster.PlotNo);
                oDm.Add("@pKhaitanNo", SqlDbType.VarChar, 100, membermaster.KhaitanNo);
                oDm.Add("@pMouza", SqlDbType.VarChar, 100, membermaster.Mouza);
                oDm.Add("@pJLNo", SqlDbType.VarChar, 100, membermaster.JLNo);
                oDm.Add("@pPO", SqlDbType.VarChar, 100, membermaster.PO);
                oDm.Add("@pPS", SqlDbType.VarChar, 100, membermaster.PS);
                oDm.Add("@pPIN", SqlDbType.VarChar, 10, membermaster.PIN);
                oDm.Add("@pBlockId", SqlDbType.Int, membermaster.BlockId);
                oDm.Add("@pDistrictId", SqlDbType.Int, membermaster.DistrictId);
                oDm.Add("@pStateId", SqlDbType.Int, membermaster.StateId);
                oDm.Add("@pEffectiveDate", SqlDbType.DateTime, membermaster.EffectiveDate);
                oDm.Add("@pOpBal", SqlDbType.Decimal, membermaster.OpBal);
                oDm.Add("@pDrORCr", SqlDbType.VarChar, 2, membermaster.DrORCr);
                oDm.Add("@pUserId", SqlDbType.Int, membermaster.UserId);
                oDm.Add("@pCompanyId", SqlDbType.Int, membermaster.CompanyId);
                oDm.Add("@BranchID_FK", SqlDbType.Int, membermaster.BranchId);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, membermaster.FinYearId);
                oDm.Add("@DataFlow", SqlDbType.Int, membermaster.DataFlow);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("MemberMaster_BlockChange_Save");

                membermaster.MemberCode = (string)oDm["@pMemberCode"].Value;
            }
        }

        public static DataTable GetAll(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.MemberId == 0)
                    oDm.Add("@MemberId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MemberId", SqlDbType.Int, membermaster.MemberId);
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                if (isMember == 2)
                    oDm.Add("@IsMember", SqlDbType.Bit, DBNull.Value);
                else if (isMember == 1)
                    oDm.Add("@IsMember", SqlDbType.Bit, true);
                else if (isMember == 0)
                    oDm.Add("@IsMember", SqlDbType.Bit, false);
                if (membermaster.MobileNo == string.Empty)
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 50, membermaster.MobileNo);
                if (membermaster.BusinessTypeId == 0)
                    oDm.Add("@BusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BusinessTypeId", SqlDbType.Int, membermaster.BusinessTypeId);
                oDm.CommandType = CommandType.StoredProcedure;
                //return oDm.ExecuteDataTable("MemberMaster_GetAll");
                return oDm.ExecuteDataTable("MemberMaster_GetAllNew");
            }
        }
        public static DataTable GetAllMemberWithAmount(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                if (isMember == 2)
                    oDm.Add("@IsMember", SqlDbType.Bit, DBNull.Value);
                else if (isMember == 1)
                    oDm.Add("@IsMember", SqlDbType.Bit, true);
                else if (isMember == 0)
                    oDm.Add("@IsMember", SqlDbType.Bit, false);
                if (membermaster.MobileNo == string.Empty)
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@MobileNo", SqlDbType.VarChar, 50, membermaster.MobileNo);
                if (membermaster.BusinessTypeId == 0)
                    oDm.Add("@BusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BusinessTypeId", SqlDbType.Int, membermaster.BusinessTypeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberMaster_GetAllMemberWithAmount");
            }
        }

























        public static DataTable MemberFeesSummery_GetAll(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                if (isMember == 2)
                    oDm.Add("@IsMember", SqlDbType.Bit, DBNull.Value);
                else if (isMember == 1)
                    oDm.Add("@IsMember", SqlDbType.Bit, true);
                else if (isMember == 0)
                    oDm.Add("@IsMember", SqlDbType.Bit, false);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberFeesSummery_GetAll");
            }
        }

        public static DataTable MemberMaster_GetAll_ForSMS(int finyearID, bool includeGovtMembers, int memberSMSCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FinYearID", SqlDbType.Int, finyearID);
                oDm.Add("@IncludeGovtMembers", SqlDbType.Bit, includeGovtMembers);
                if(memberSMSCategoryId==0)
                    oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, memberSMSCategoryId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberMaster_GetAll_ForSMS");
            }
        }

        public static DataTable MemberMaster_GetAll_ForReport(Entity.Common.MemberMaster membermaster)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberMaster_GetAll_ForReport");
            }
        }

        public static Entity.Common.MemberMaster GetMemberMasterById(int memberId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.Input, memberId);

                SqlDataReader dr = oDm.ExecuteReader("MemberMaster_GetById");

                Entity.Common.MemberMaster memberMaster = new Entity.Common.MemberMaster();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        memberMaster.MemberId = memberId;
                        memberMaster.MemberName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        memberMaster.MemberCode = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        memberMaster.MobileNo = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        memberMaster.PhoneId = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        memberMaster.VATNo = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                        memberMaster.PANNo = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();
                        memberMaster.LICNo = (dr[7] == DBNull.Value) ? "" : dr[7].ToString();
                        memberMaster.GSTNo = (dr["GSTNo"] == DBNull.Value) ? "" : dr["GSTNo"].ToString();
                        memberMaster.VillageOrStreet = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                        memberMaster.PlotNo = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();
                        memberMaster.KhaitanNo = (dr[10] == DBNull.Value) ? "" : dr[10].ToString();
                        memberMaster.Mouza = (dr[11] == DBNull.Value) ? "" : dr[11].ToString();
                        memberMaster.JLNo = (dr[12] == DBNull.Value) ? "" : dr[12].ToString();
                        memberMaster.PO = (dr[13] == DBNull.Value) ? "" : dr[13].ToString();
                        memberMaster.PS = (dr[14] == DBNull.Value) ? "" : dr[14].ToString();
                        memberMaster.PIN = (dr[15] == DBNull.Value) ? "" : dr[15].ToString();
                        memberMaster.BlockId = (dr[16] == DBNull.Value) ? 0 : int.Parse(dr[16].ToString());
                        memberMaster.DistrictId = (dr[17] == DBNull.Value) ? 0 : int.Parse(dr[17].ToString());
                        memberMaster.StateId = (dr[18] == DBNull.Value) ? 0 : int.Parse(dr[18].ToString());
                        memberMaster.CompanyName = (dr[19] == DBNull.Value) ? "" : dr[19].ToString();
                        memberMaster.CategoryId = (dr[20] == DBNull.Value) ? 0 : int.Parse(dr[20].ToString());
                        memberMaster.MemberGroupId = (dr[21] == DBNull.Value) ? 0 : int.Parse(dr[21].ToString());
                        memberMaster.BusinessTypeId = (dr[22] == DBNull.Value) ? 0 : int.Parse(dr[22].ToString());
                        memberMaster.MembershipDate = (dr[23] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[23]);
                        memberMaster.EffectiveDate = (dr[24] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[24]);
                        memberMaster.OpBal = (dr[25] == DBNull.Value) ? 0 : decimal.Parse(dr[25].ToString());
                        memberMaster.DrORCr = (dr[26] == DBNull.Value) ? "" : dr[26].ToString();
                        memberMaster.LayerCapacityNos = (dr[27] == DBNull.Value) ? "" : dr[27].ToString();
                        memberMaster.BroilerCapacityNos = (dr[28] == DBNull.Value) ? "" : dr[28].ToString();
                        memberMaster.BreederCapacityNos = (dr[29] == DBNull.Value) ? "" : dr[29].ToString();
                        memberMaster.EggSellerDailySalesNos = (dr[30] == DBNull.Value) ? "" : dr[30].ToString();
                        memberMaster.ChickenSellerDailySalesNos = (dr[31] == DBNull.Value) ? "" : dr[31].ToString();
                        memberMaster.ChickenSellerDailySalesKgs = (dr[32] == DBNull.Value) ? "" : dr[32].ToString();
                        memberMaster.FeedProducerDailySalesMT = (dr[33] == DBNull.Value) ? "" : dr[33].ToString();
                        memberMaster.FeedSellerDailySalesMT = (dr[34] == DBNull.Value) ? "" : dr[34].ToString();
                        memberMaster.OtherCategory = (dr[35] == DBNull.Value) ? "" : dr[35].ToString();
                        memberMaster.Remarks = (dr[36] == DBNull.Value) ? "" : dr[36].ToString();
                        memberMaster.CreateDate = (dr[37] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[37]);
                        memberMaster.UserId = (dr[38] == DBNull.Value) ? 0 : int.Parse(dr[38].ToString());
                        memberMaster.CompanyId = (dr[39] == DBNull.Value) ? 0 : int.Parse(dr[39].ToString());
                        memberMaster.LedgerId = (dr[40] == DBNull.Value) ? 0 : int.Parse(dr[40].ToString());
                        memberMaster.Website = (dr[41] == DBNull.Value) ? "" : dr[41].ToString();
                        memberMaster.ImageName = (dr[42] == DBNull.Value) ? "" : dr[42].ToString();
                        memberMaster.CategoryName = (dr[43] == DBNull.Value) ? "" : dr[43].ToString();
                        memberMaster.IsMember = (dr[44] == DBNull.Value) ? false : bool.Parse(dr[44].ToString());
                        memberMaster.MemberSMSCategoryId = (dr[45] == DBNull.Value) ? 0 : int.Parse(dr[45].ToString());
                        memberMaster.IsGovtMember = (dr[46] == DBNull.Value) ? false : bool.Parse(dr[46].ToString());
                        memberMaster.Email = (dr[47] == DBNull.Value) ? "" : dr[47].ToString();
                        memberMaster.IsApproved = (dr[48] == DBNull.Value) ? false : bool.Parse(dr[48].ToString());
                        memberMaster.MobileNo2 = (dr["MobileNo2"] == DBNull.Value) ? "" : dr["MobileNo2"].ToString();
                        memberMaster.Narration = (dr["Narration"] == DBNull.Value) ? "" : dr["Narration"].ToString();
                        memberMaster.MembershipCategoryEffectiveDate = (dr["MembershipCategoryEffectiveDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["MembershipCategoryEffectiveDate"]);
                        memberMaster.IsExecutiveMember = (dr["IsExecutiveMember"] == DBNull.Value) ? false : bool.Parse(dr["IsExecutiveMember"].ToString());
                    }
                }
                return memberMaster;
            }
        }

        public static void Delete(int memberId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.Input, memberId);

                oDm.ExecuteNonQuery("MemberMaster_Delete");
            }
        }

        public static DataTable GetDistrictAndStateByBlockId(int blockid)
        {
            using (DataManager oDm = new DataManager())
            {
                if (blockid == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, ParameterDirection.Input, blockid);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("GetDistrictAndStateByBlockId");
            }
        }

        public static void MemberMasterLedgerDefaultConfiguration_Update(Entity.Common.MemberMaster membermaster)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@LedgerTypeId", SqlDbType.VarChar, membermaster.LedgerTypeId);
                oDm.Add("@AccountGroupId", SqlDbType.Int, membermaster.AccountGroupId);
                if (membermaster.AccountSubGroupId == 0)
                    oDm.Add("@AccountSubGroupId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@AccountSubGroupId", SqlDbType.Int, membermaster.AccountSubGroupId);
                oDm.Add("@IsCostCenterApplicable", SqlDbType.Bit, membermaster.IsCostCenterApplicable);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("MemberMasterLedgerDefaultConfiguration_Update");
            }
        }

        public static Entity.Common.MemberMaster MemberMasterLedgerDefaultConfiguration_Get()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("MemberMasterLedgerDefaultConfiguration_Get");

                Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

                if (dr.HasRows)
                    if (dr.Read())
                    {
                        membermaster.LedgerTypeId = dr[1].ToString();
                        membermaster.AccountGroupId = int.Parse(dr[2].ToString());
                        membermaster.AccountSubGroupId = (dr[3].ToString() == "") ? 0 : int.Parse(dr[3].ToString());
                        membermaster.IsCostCenterApplicable = bool.Parse(dr[4].ToString());
                    }
                return membermaster;
            }
        }

        public static int MemberMaster_Priority_Update(int memberid, bool ispriority)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, ParameterDirection.Input, memberid);
                oDm.Add("@IsPriority", SqlDbType.Bit, ParameterDirection.Input, ispriority);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_MemberMaster_Priority_Update");
            }
        }

        public static int SMSSubscription_Save(Entity.Common.MemberMaster membermaster)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(membermaster.SubscriptionDetails);
                oDm.Add("@pSMSSubscriptionDetails", SqlDbType.Xml, ds.GetXml());

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("SMSSubscription_Save");
            }
        }

        public static DataTable SMSSubscription_GetAll(int finyearid)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FinYearID", SqlDbType.Int, finyearid);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("SMSSubscription_GetAll");
            }
        }

        public static int SMSSubscription_Block(int memberId, bool isBlock)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, ParameterDirection.Input, memberId);
                oDm.Add("@IsBlock", SqlDbType.Bit, ParameterDirection.Input, isBlock);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_SMSSubscription_Block");
            }
        }

        public static DataTable GetAllOutstandingReport(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                if (isMember == 2)
                    oDm.Add("@IsMember", SqlDbType.Bit, DBNull.Value);
                else if (isMember == 1)
                    oDm.Add("@IsMember", SqlDbType.Bit, true);
                else if (isMember == 0)
                    oDm.Add("@IsMember", SqlDbType.Bit, false);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberMaster_GetAllOutstandingReport");
            }
        }

        public static DataTable DevelopmentMemberGetAll(int StateId, int DistrictId, int BlockId, int CategoryId, string MemberName, string MobileNo,int BusinessTypeId,DateTime FromDate,DateTime ToDate)
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
                    oDm.Add("@pCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pCategoryId", SqlDbType.Int, CategoryId);
                //if (MemberName == "")
                //    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, DBNull.Value);
                //else
                //    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, MemberName);
                //if (MobileNo == "")
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 100, DBNull.Value);
                //else
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 100, MobileNo);
                if (BusinessTypeId == 0)
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);
                if(FromDate==DateTime.MinValue)
                oDm.Add("@pFromDate", SqlDbType.Date, DateTime.MinValue);
                else
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (ToDate == DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.Date, DateTime.MaxValue);
                else
                    oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("DevelopmentMember_GetAll");
            }
        }
        public static DataTable RenewalMemberGetAll(int StateId, int DistrictId, int BlockId, int CategoryId, string MemberName, string MobileNo, int BusinessTypeId, DateTime FromDate, DateTime ToDate)
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
                    oDm.Add("@pCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pCategoryId", SqlDbType.Int, CategoryId);
                //if (MemberName == "")
                //    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, DBNull.Value);
                //else
                //    oDm.Add("@pMemberName", SqlDbType.VarChar, 200, MemberName);
                //if (MobileNo == "")
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 100, DBNull.Value);
                //else
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 100, MobileNo);
                if (BusinessTypeId == 0)
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);
                if (FromDate == DateTime.MinValue)
                    oDm.Add("@pFromDate", SqlDbType.Date, DateTime.MinValue);
                else
                    oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (ToDate == DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.Date, DateTime.MaxValue);
                else
                    oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("RenewalMember_GetAll");
            }
        }
    }
}