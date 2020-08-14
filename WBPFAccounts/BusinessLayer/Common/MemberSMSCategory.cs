using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberSMSCategory
    {
        public MemberSMSCategory()
        { }

        public int MemberSMSCategory_Save(Entity.Common.MemberSMSCategory memberSMSCategory)
        {
            return DataAccess.Common.MemberSMSCategory.MemberSMSCategory_Save(memberSMSCategory);
        }

        public DataTable MemberSMSCategory_GetAll()
        {
            return DataAccess.Common.MemberSMSCategory.MemberSMSCategory_GetAll();
        }

        public Entity.Common.MemberSMSCategory MemberSMSCategory_GetById(int memberSMSCategoryId)
        {
            return DataAccess.Common.MemberSMSCategory.MemberSMSCategory_GetById(memberSMSCategoryId);
        }

        public int MemberSMSCategory_Delete(int memberSMSCategoryId)
        {
            return DataAccess.Common.MemberSMSCategory.MemberSMSCategory_Delete(memberSMSCategoryId);
        }
    }
}
