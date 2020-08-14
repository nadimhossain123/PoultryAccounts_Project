using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
	public class State
	{
		public State()
		{

		}

        public static void Save(Entity.Common.State state)
		{
			using (DataManager oDm = new DataManager())
			{
				oDm.Add("@pStateId", SqlDbType.Int, ParameterDirection.InputOutput, state.StateId);
				oDm.Add("@pStateName", SqlDbType.VarChar, 30, state.StateName);
				

				oDm.CommandType = CommandType.StoredProcedure;

				oDm.ExecuteNonQuery("State_Save");

				state.StateId = (int)oDm["@pStateId"].Value;
			}
		}

		public static DataTable GetAll()
		{
			using (DataManager oDm = new DataManager())
			{
				oDm.CommandType = CommandType.StoredProcedure;

				return oDm.ExecuteDataTable("State_GetAll");
			}
		}

        public static Entity.Common.State GetStateById(int stateId)
		{
			using (DataManager oDm = new DataManager())
			{

				oDm.CommandType = CommandType.StoredProcedure;

				oDm.Add("@pStateId", SqlDbType.Int, ParameterDirection.Input, stateId);

				SqlDataReader dr = oDm.ExecuteReader("State_GetById");

                Entity.Common.State state = new Entity.Common.State();
				if (dr.HasRows)
				{
					while (dr.Read())
					{
						state.StateId = stateId;
						state.StateName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
						
					}
				}
				return state;
			}
		}

		public static void Delete(int stateId)
		{
			using (DataManager oDm = new DataManager())
			{

				oDm.CommandType = CommandType.StoredProcedure;

				oDm.Add("@pStateId", SqlDbType.Int, ParameterDirection.Input, stateId);

				oDm.ExecuteNonQuery("State_Delete");
			}
		}
	}
}