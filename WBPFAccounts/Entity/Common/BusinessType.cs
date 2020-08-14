using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class BusinessType
	{
		public BusinessType()
		{

		}

		private int  businessTypeId;
		private string businessTypeName;
		private string remarks;
		
		public int  BusinessTypeId
		{
			get{ return businessTypeId;}
			set{ businessTypeId = value;}
		}
		public string BusinessTypeName
		{
			get{ return businessTypeName;}
			set{ businessTypeName = value;}
		}
		public string Remarks
		{
			get{ return remarks;}
			set{ remarks = value;}
		}
		
	}
}