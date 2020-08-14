using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class MembershipCategory
	{
		public MembershipCategory()
		{

		}

		private int  membershipCategoryId;
		private string categoryName;
		private decimal admissionFees;
		private decimal monthlySubscriptions;
		private string categoryRemarks;
        private bool sMSApplicable;

        public bool SMSApplicable
        {
            get { return sMSApplicable; }
            set { sMSApplicable = value; }
        }
		
		public int  MembershipCategoryId
		{
			get{ return membershipCategoryId;}
			set{ membershipCategoryId = value;}
		}
		public string CategoryName
		{
			get{ return categoryName;}
			set{ categoryName = value;}
		}
		public decimal AdmissionFees
		{
			get{ return admissionFees;}
			set{ admissionFees = value;}
		}
		public decimal MonthlySubscriptions
		{
			get{ return monthlySubscriptions;}
			set{ monthlySubscriptions = value;}
		}
		public string CategoryRemarks
		{
			get{ return categoryRemarks;}
			set{ categoryRemarks = value;}
		}
		
	}
}