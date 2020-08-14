using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class State
	{
		public State()
		{

		}

        public void Save(Entity.Common.State state)
		{
            DataAccess.Common.State.Save(state);
		}

		public DataTable GetAll()
		{
            return DataAccess.Common.State.GetAll();
		}

        public Entity.Common.State GetStateById(int stateId)
		{
            return DataAccess.Common.State.GetStateById(stateId);
		}

		public void Delete(int stateId)
		{
            DataAccess.Common.State.Delete(stateId);
		}
	}
}