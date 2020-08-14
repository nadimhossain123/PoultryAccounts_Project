using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class Tax
    {
        public Tax()
        { }

        public int Save(Entity.Accounts.Tax tax)
        {
            return  DataAccess.Accounts.Tax.Save(tax);
        }

        public DataTable GetAll()
        {
            return DataAccess.Accounts.Tax.GetAll();
        }

        public Entity.Accounts.Tax GetTaxById(int taxid)
        {
            return DataAccess.Accounts.Tax.GetTaxById(taxid);
        }

        public int Delete(int taxid)
        {
            return DataAccess.Accounts.Tax.Delete(taxid);
        }

        public DataTable Service_Tax_ByFeesHead_Report(Entity.Accounts.Tax tax)
        {
            return DataAccess.Accounts.Tax.Service_Tax_ByFeesHead_Report(tax);
        }

        public DataTable Service_Tax_ByLedgerId_Report(Entity.Accounts.Tax tax)
        {
            return DataAccess.Accounts.Tax.Service_Tax_ByLedgerId_Report(tax);
        }

        public DataTable GetAllDistinct()
        {
            return DataAccess.Accounts.Tax.GetAllDistinct();
        }
    }
}
