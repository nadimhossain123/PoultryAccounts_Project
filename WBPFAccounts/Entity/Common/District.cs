using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class District
	{
		public District()
		{

		}

		private int  districtId;
		private string districtName;
		private int stateId;
		private string code;
		private string remarks;
		
		public int  DistrictId
		{
			get{ return districtId;}
			set{ districtId = value;}
		}
		public string DistrictName
		{
			get{ return districtName;}
			set{ districtName = value;}
		}
		public int StateId
		{
			get{ return stateId;}
			set{ stateId = value;}
		}
		public string Code
		{
			get{ return code;}
			set{ code = value;}
		}
		public string Remarks
		{
			get{ return remarks;}
			set{ remarks = value;}
		}
		
	}
}