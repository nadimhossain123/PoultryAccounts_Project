using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Company
    {
        public Company()
        {
        }

        public int Save(Entity.Common.Company Company)
        {
            return DataAccess.Common.Company.Save(Company);
        }

        public Entity.Common.Company GetAllById(int CompanyId)
        {
            return DataAccess.Common.Company.GetAllById(CompanyId);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.Company.GetAll();
        }

        public static int GetCompanyId(string CompanyType)
        {
            return DataAccess.Common.Company.GetCompanyId(CompanyType);
        }
    }
}
