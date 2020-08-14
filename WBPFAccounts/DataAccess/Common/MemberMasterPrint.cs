using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class MemberMasterPrint
    {
        public MemberMasterPrint()
        {
        }
        public static DataTable MemberMaster_GetAll_ForPrint(Entity.Common.MemberMaster membermaster,int isMember=2)
        {
            using (DataManager oDm = new DataManager())
            {
                if (membermaster.BlockId == 0)
                    oDm.Add("@BlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@BlockId", SqlDbType.Int, membermaster.BlockId);

                if (membermaster.DistrictId == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, membermaster.DistrictId);

                if (membermaster.StateId == 0)
                    oDm.Add("@StateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, membermaster.StateId);

                if (membermaster.CategoryId == 0)
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@MembershipCategoryId", SqlDbType.Int, membermaster.CategoryId);

                if (membermaster.MemberName == string.Empty)
                    oDm.Add("@MemberName", SqlDbType.VarChar, DBNull.Value);
                else
                    oDm.Add("@MemberName", SqlDbType.VarChar, membermaster.MemberName);

                if (isMember == 2)
                    oDm.Add("@IsMember", SqlDbType.Bit, DBNull.Value);
                else if (isMember == 1)
                    oDm.Add("@IsMember", SqlDbType.Bit, true);
                else if (isMember == 0)
                    oDm.Add("@IsMember", SqlDbType.Bit, false);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("MemberMaster_GetAll_ForPrint");
            }
        }

       
        }
    }

