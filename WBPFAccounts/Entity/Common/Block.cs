using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class Block
	{
		public Block()
		{

		}

		private int  blockId;
		private string blockName;
		private int districtId;
		private string code;
		private string remarks;
		
		public int  BlockId
		{
			get{ return blockId;}
			set{ blockId = value;}
		}
		public string BlockName
		{
			get{ return blockName;}
			set{ blockName = value;}
		}
		public int DistrictId
		{
			get{ return districtId;}
			set{ districtId = value;}
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