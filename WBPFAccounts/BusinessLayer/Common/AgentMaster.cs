using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class AgentMaster
    {
        public AgentMaster()
        {
        }

        public void Save(Entity.Common.AgentMaster agent)
        {
            DataAccess.Common.AgentMaster.Save(agent);
        }

        public DataTable GetAll(string AgentName)
        {
            return DataAccess.Common.AgentMaster.GetAll(AgentName);
        }

        public DataTable GetAllById(int AgentId)
        {
            return DataAccess.Common.AgentMaster.GetAllById(AgentId);
        }

        public void ChangePassword(int AgentId, string AgentPassword)
        {
            DataAccess.Common.AgentMaster.ChangePassword(AgentId, AgentPassword);
        }
    }
}
