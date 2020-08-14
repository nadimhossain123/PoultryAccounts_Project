using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
     public class StudentOpeningBal
    {
         public int SaveOpBal(Entity.Accounts.StudentOpeningBal OpBal)
         {
             return DataAccess.Accounts.StudentOpeningBal.SaveOpBal(OpBal);
         }

         public DataSet StudentOpeningBalance_GetById(Entity.Accounts.StudentOpeningBal OpBal)
         {
             return DataAccess.Accounts.StudentOpeningBal.StudentOpeningBalance_GetById(OpBal);
         }
    }
}
