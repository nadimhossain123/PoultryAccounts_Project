using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class GeneralMember
    {
        public GeneralMember()
        {
        }

        public int Save(Entity.SMS.GeneralMember generalMember)
        {
            return DataAccess.SMS.GeneralMember.Save(generalMember);
        }

        public DataTable GetAll(string Name, string MobileNo)
        {
            return DataAccess.SMS.GeneralMember.GetAll(Name, MobileNo);
        }

        public DataTable GetAllById(int MemberId)
        {
            return DataAccess.SMS.GeneralMember.GetAllById(MemberId);
        }

        public void Delete(int MemberId)
        {
            DataAccess.SMS.GeneralMember.Delete(MemberId);
        }
    }
}
