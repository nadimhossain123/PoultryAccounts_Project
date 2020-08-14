using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class MemberGroup
	{
		public MemberGroup()
		{

		}

		private int  memberGroupId;
		private string memberGroupName;
		private string remarks;
		
		public int  MemberGroupId
		{
			get{ return memberGroupId;}
			set{ memberGroupId = value;}
		}
		public string MemberGroupName
		{
			get{ return memberGroupName;}
			set{ memberGroupName = value;}
		}
		public string Remarks
		{
			get{ return remarks;}
			set{ remarks = value;}
		}
		
	}
}