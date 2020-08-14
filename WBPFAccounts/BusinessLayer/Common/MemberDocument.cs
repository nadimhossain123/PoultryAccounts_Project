using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Common
{
    public class MemberDocument
    {
        public MemberDocument()
        {
        }

        public void Save(Entity.Common.MemberDocument memberDocument)
        {
            DataAccess.Common.MemberDocument.Save(memberDocument);
        }

        public Entity.Common.MemberDocument GetAllById(int MemberId)
        {
            return DataAccess.Common.MemberDocument.GetAllById(MemberId);
        }
    }
}
