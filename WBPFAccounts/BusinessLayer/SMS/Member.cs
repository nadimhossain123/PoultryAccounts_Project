using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class Member
    {
        public Member()
        {
        }

        public int Save(Entity.SMS.Member EntityMember)
        {
            return DataAccess.SMS.Member.Save(EntityMember);
        }

        public DataTable GetAll(string MemberName, string MobileNo, int RegistrationType, string ExpiryDate, int SearchType)
        {
            return DataAccess.SMS.Member.GetAll(MemberName, MobileNo, RegistrationType, ExpiryDate, SearchType);
        }

        public Entity.SMS.Member GetAllById(int MemberId)
        {
            return DataAccess.SMS.Member.GetAllById(MemberId);
        }

        public Entity.SMS.Member GetAllByMobNo(string MobileNo)
        {
            return DataAccess.SMS.Member.GetAllByMobNo(MobileNo);
        }

        public void Delete(int MemberId)
        {
            DataAccess.SMS.Member.Delete(MemberId);
        }

        public DataTable getMobileNumbers(int Type)
        {
            return DataAccess.SMS.Member.getMobileNumbers(Type);
        }

        public void QuickUpdate(int MemberId)
        {
            DataAccess.SMS.Member.QuickUpdate(MemberId);
        }

        public void ChangePriority(int MemberId, int Priority)
        {
            DataAccess.SMS.Member.ChangePriority(MemberId, Priority);
        }
    }
}
