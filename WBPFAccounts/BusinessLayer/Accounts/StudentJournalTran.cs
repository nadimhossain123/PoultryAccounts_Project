using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class StudentJournalTran
    {
        public int  Save(Entity.Accounts.StudentJournalTrn objStuJournal)
        {
           return  DataAccess.Accounts.StudentJournalTran.Save(objStuJournal);
        }
    }
}
