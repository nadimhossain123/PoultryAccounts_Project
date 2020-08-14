using System;
using System.Collections.Generic;

namespace Entity.Common
{
	public class State
	{
		public State()
		{

		}

		private int  stateId;
		private string stateName;
		
		public int  StateId
		{
			get{ return stateId;}
			set{ stateId = value;}
		}
		public string StateName
		{
			get{ return stateName;}
			set{ stateName = value;}
		}
		
	}
}