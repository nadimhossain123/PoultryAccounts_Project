using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class FeesHeadMaster
    {
        public FeesHeadMaster()
        {
        }

        public static int Save(Entity.Common.FeesHeadMaster FeesHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FeesHeadId", SqlDbType.Int, ParameterDirection.Input, FeesHead.FeesHeadId);
                oDm.Add("@FeesHeadName", SqlDbType.VarChar, 50, ParameterDirection.Input, FeesHead.FeesHeadName);
                oDm.Add("@Frequency", SqlDbType.Char, 1, ParameterDirection.Input, FeesHead.Frequency);
                oDm.Add("@FeesType", SqlDbType.Int, ParameterDirection.Input, FeesHead.FeesType);
                oDm.Add("@IsMandatory", SqlDbType.Bit, ParameterDirection.Input, FeesHead.IsMandatory);
                oDm.Add("@IsActive", SqlDbType.Bit, ParameterDirection.Input, FeesHead.IsActive);
                oDm.Add("@FeesHeadTaxMapXml", SqlDbType.Xml, ParameterDirection.Input, FeesHead.FeesHeadTaxMapXml);
                oDm.Add("@MessageCode", SqlDbType.Int, ParameterDirection.InputOutput, 0);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_FeesHeadMaster_Save");
                return (int)oDm["@MessageCode"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_FeesHeadMaster_GetAll");
            }
        }

        public static DataSet GetAllById(int FeesHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FeesHeadId", SqlDbType.Int, ParameterDirection.Input, FeesHeadId);
                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds=new DataSet();

                return oDm.GetDataSet("usp_FeesHeadMaster_GetAllById", ref ds, "Table");
            }
        }
    }
}
