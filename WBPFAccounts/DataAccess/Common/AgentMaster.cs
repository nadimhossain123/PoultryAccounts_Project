using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class AgentMaster
    {
        public AgentMaster()
        {
        }

        public static void Save(Entity.Common.AgentMaster agent)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pAgentId", SqlDbType.Int, ParameterDirection.Input, agent.AgentId);
                oDm.Add("@pAgentCode", SqlDbType.VarChar, 50, ParameterDirection.InputOutput, agent.AgentCode);
                oDm.Add("@pAgentName", SqlDbType.VarChar, 100, ParameterDirection.Input, agent.AgentName);

                oDm.Add("@pAddress", SqlDbType.VarChar, 255, ParameterDirection.Input, agent.Address);
                oDm.Add("@pPhoneNo", SqlDbType.VarChar, 20, ParameterDirection.Input, agent.PhoneNo);
                oDm.Add("@pStateId", SqlDbType.Int, ParameterDirection.Input, agent.StateId);
                oDm.Add("@pDistrictId", SqlDbType.Int, ParameterDirection.Input, agent.DistrictId);
                oDm.Add("@pBlockId", SqlDbType.Int, ParameterDirection.Input, agent.BlockId);
                oDm.Add("@pBankName", SqlDbType.VarChar, 100, ParameterDirection.Input, agent.BankName);
                oDm.Add("@pBranchName", SqlDbType.VarChar, 100, ParameterDirection.Input, agent.BranchName);
                oDm.Add("@pBranchAddress", SqlDbType.VarChar, 255, ParameterDirection.Input, agent.BranchAddress);
                oDm.Add("@pIFSCCode", SqlDbType.VarChar, 20, ParameterDirection.Input, agent.IFSCCode);
                oDm.Add("@pIsActive", SqlDbType.Bit, ParameterDirection.Input, agent.IsActive);
                oDm.Add("@pCreatedBy", SqlDbType.Int, ParameterDirection.Input, agent.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_AgentMaster_Save");
                agent.AgentCode = (string)oDm["@pAgentCode"].Value;
            }
        }

        public static DataTable GetAll(string AgentName)
        {
            using (DataManager oDm = new DataManager())
            {
                if (string.IsNullOrEmpty(AgentName.Trim()))
                {
                    oDm.Add("@pAgentName", SqlDbType.VarChar, 100, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pAgentName", SqlDbType.VarChar, 100, AgentName);
                }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_AgentMaster_GetAll");
            }
        }

        public static DataTable GetAllById(int AgentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pAgentId", SqlDbType.Int, AgentId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_AgentMaster_GetAllById");
            }
        }

        public static void ChangePassword(int AgentId, string AgentPassword)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pAgentId", SqlDbType.Int, AgentId);
                oDm.Add("@pAgentPassword", SqlDbType.VarChar, 50, AgentPassword);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_AgentMaster_ChangePassword");
            }
        }
    }
}
