using System;
using System.Collections.Generic;
using System.Data;

namespace Entity.Common
{
	public class MemberMaster
	{
		public MemberMaster()
		{

		}

		private int  memberId;
		private string memberName;
		private string memberCode;
		private string mobileNo;
		private string phoneId;
		private string vATNo;
		private string pANNo;
		private string lICNo;
		private string villageOrStreet;
		private string plotNo;
		private string khaitanNo;
		private string mouza;
		private string jLNo;
		private string pO;
		private string pS;
		private string pIN;
		private int blockId;
		private int districtId;
		private int stateId;
		private string companyName;
		private int categoryId;
		private int memberGroupId;
		private int businessTypeId;
		private DateTime membershipDate;
		private DateTime effectiveDate;
		private decimal opBal;
		private string drORCr;
		private string layerCapacityNos;
        private string broilerCapacityNos;
        private string breederCapacityNos;
        private string eggSellerDailySalesNos;
        private string chickenSellerDailySalesNos;
        private string chickenSellerDailySalesKgs;
        private string feedProducerDailySalesMT;
        private string feedSellerDailySalesMT;
		private string otherCategory;
		private string remarks;
		private DateTime createDate;
		private int userId;
		private int companyId;
        private string ledgerTypeId;
        private int accountGroupId;
        private int accountSubGroupId;
        private bool isCostCenterApplicable;
        private int ledgerId;
        private bool isApproved;
        private bool active;
        private int branchId;
        private int finYearId;
        private int dataFlow;
        private string website;
        private int oldMemberId;
        private string imageExt;
        private string imageName;
        private string categoryName;
        private bool isMember;
        private bool isPriority;
        private DataTable subscriptionDetails;
        private bool isBlock;
        private int memberSMSCategoryId;
        private bool isGovtMember;
        private string gSTNo; 

        public bool IsBlock
        {
            get { return isBlock; }
            set { isBlock = value; }
        }

        public DataTable SubscriptionDetails
        {
            get { return subscriptionDetails; }
            set { subscriptionDetails = value; }
        }

        public bool IsPriority
        {
            get { return isPriority; }
            set { isPriority = value; }
        }

        public bool IsMember
        {
            get { return isMember; }
            set { isMember = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        public string ImageExt
        {
            get { return imageExt; }
            set { imageExt = value; }
        }

        public int OldMemberId
        {
            get { return oldMemberId; }
            set { oldMemberId = value; }
        }

        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        public int DataFlow
        {
            get { return dataFlow; }
            set { dataFlow = value; }
        }

        public int FinYearId
        {
            get { return finYearId; }
            set { finYearId = value; }
        }

        public int BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        public int LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }

        public bool IsCostCenterApplicable
        {
            get { return isCostCenterApplicable; }
            set { isCostCenterApplicable = value; }
        }

        public int AccountSubGroupId
        {
            get { return accountSubGroupId; }
            set { accountSubGroupId = value; }
        }

        public int AccountGroupId
        {
            get { return accountGroupId; }
            set { accountGroupId = value; }
        }

        public string LedgerTypeId
        {
            get { return ledgerTypeId; }
            set { ledgerTypeId = value; }
        }
		
		public int  MemberId
		{
			get{ return memberId;}
			set{ memberId = value;}
		}
		public string MemberName
		{
			get{ return memberName;}
			set{ memberName = value;}
		}
		public string MemberCode
		{
			get{ return memberCode;}
			set{ memberCode = value;}
		}
		public string MobileNo
		{
			get{ return mobileNo;}
			set{ mobileNo = value;}
		}
		public string PhoneId
		{
			get{ return phoneId;}
			set{ phoneId = value;}
		}
		public string VATNo
		{
			get{ return vATNo;}
			set{ vATNo = value;}
		}
		public string PANNo
		{
			get{ return pANNo;}
			set{ pANNo = value;}
		}
		public string LICNo
		{
			get{ return lICNo;}
			set{ lICNo = value;}
		}
		public string VillageOrStreet
		{
			get{ return villageOrStreet;}
			set{ villageOrStreet = value;}
		}
		public string PlotNo
		{
			get{ return plotNo;}
			set{ plotNo = value;}
		}
		public string KhaitanNo
		{
			get{ return khaitanNo;}
			set{ khaitanNo = value;}
		}
		public string Mouza
		{
			get{ return mouza;}
			set{ mouza = value;}
		}
		public string JLNo
		{
			get{ return jLNo;}
			set{ jLNo = value;}
		}
		public string PO
		{
			get{ return pO;}
			set{ pO = value;}
		}
		public string PS
		{
			get{ return pS;}
			set{ pS = value;}
		}
		public string PIN
		{
			get{ return pIN;}
			set{ pIN = value;}
		}
		public int BlockId
		{
			get{ return blockId;}
			set{ blockId = value;}
		}
		public int DistrictId
		{
			get{ return districtId;}
			set{ districtId = value;}
		}
		public int StateId
		{
			get{ return stateId;}
			set{ stateId = value;}
		}
		public string CompanyName
		{
			get{ return companyName;}
			set{ companyName = value;}
		}
		public int CategoryId
		{
			get{ return categoryId;}
			set{ categoryId = value;}
		}
		public int MemberGroupId
		{
			get{ return memberGroupId;}
			set{ memberGroupId = value;}
		}
		public int BusinessTypeId
		{
			get{ return businessTypeId;}
			set{ businessTypeId = value;}
		}
		public DateTime MembershipDate
		{
			get{ return membershipDate;}
			set{ membershipDate = value;}
		}
		public DateTime EffectiveDate
		{
			get{ return effectiveDate;}
			set{ effectiveDate = value;}
		}
		public decimal OpBal
		{
			get{ return opBal;}
			set{ opBal = value;}
		}
		public string DrORCr
		{
			get{ return drORCr;}
			set{ drORCr = value;}
		}
        public string LayerCapacityNos
		{
			get{ return layerCapacityNos;}
			set{ layerCapacityNos = value;}
		}
        public string BroilerCapacityNos
		{
			get{ return broilerCapacityNos;}
			set{ broilerCapacityNos = value;}
		}
        public string BreederCapacityNos
		{
			get{ return breederCapacityNos;}
			set{ breederCapacityNos = value;}
		}
        public string EggSellerDailySalesNos
		{
			get{ return eggSellerDailySalesNos;}
			set{ eggSellerDailySalesNos = value;}
		}
        public string ChickenSellerDailySalesNos
		{
			get{ return chickenSellerDailySalesNos;}
			set{ chickenSellerDailySalesNos = value;}
		}
        public string ChickenSellerDailySalesKgs
		{
			get{ return chickenSellerDailySalesKgs;}
			set{ chickenSellerDailySalesKgs = value;}
		}
        public string FeedProducerDailySalesMT
		{
			get{ return feedProducerDailySalesMT;}
			set{ feedProducerDailySalesMT = value;}
		}
        public string FeedSellerDailySalesMT
		{
			get{ return feedSellerDailySalesMT;}
			set{ feedSellerDailySalesMT = value;}
		}
		public string OtherCategory
		{
			get{ return otherCategory;}
			set{ otherCategory = value;}
		}
		public string Remarks
		{
			get{ return remarks;}
			set{ remarks = value;}
		}
		public DateTime CreateDate
		{
			get{ return createDate;}
			set{ createDate = value;}
		}
		public int UserId
		{
			get{ return userId;}
			set{ userId = value;}
		}
		public int CompanyId
		{
			get{ return companyId;}
			set{ companyId = value;}
		}

        public bool IsGovtMember
        {
            get { return isGovtMember; }
            set { isGovtMember = value; }
        }

        public int MemberSMSCategoryId
        {
            get { return memberSMSCategoryId; }
            set { memberSMSCategoryId = value; }
        }
        public string Email { get; set; }
        public string MobileNo2 { get; set; }
        public string Narration { get; set; }
        public DateTime MembershipCategoryEffectiveDate { get; set; }
        public string GSTNo
        {
            get { return gSTNo; }
            set { gSTNo = value; }
        }

		public int MembershipMonth
		{
			get;
			set;
		}
		public int MembershipYear
		{
			get;
			set;
		}
		public bool IsExecutiveMember { get; set; }
    }
}