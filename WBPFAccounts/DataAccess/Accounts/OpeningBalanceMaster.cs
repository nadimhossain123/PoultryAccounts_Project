using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Accounts
{
    public class OpeningBalanceMaster
    {
        public static int Save(Entity.Accounts.OpeningBalanceMaster OpBal)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@OpBalId", SqlDbType.Int,ParameterDirection.Input, OpBal.OpBalId);
                oDm.Add("@CompanyId", SqlDbType.Int, OpBal.CompanyId);
                oDm.Add("@FinancialYrId", SqlDbType.Int, OpBal.FinancialYrId);
                oDm.Add("@LedgerId", SqlDbType.Int, OpBal.LedgerId);
                oDm.Add("@OpeningBalance", SqlDbType.Decimal, OpBal.OpeningBalance);
                oDm.Add("@OpeningBalanceType", SqlDbType.VarChar,10, OpBal.OpeningBalanceType);
                oDm.Add("@CreatedBy", SqlDbType.Int, OpBal.CreatedBy);
                oDm.Add("@MessageCode", SqlDbType.Int, ParameterDirection.Output);
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_MstOpeningBalance_Save");
                return (int)oDm["@MessageCode"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MstOpeningBalance_GetAll");
            }
        }

        public static Entity.Accounts.OpeningBalanceMaster GetById(int OpBalId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@OpBalId", SqlDbType.Int, ParameterDirection.Input, OpBalId);

                SqlDataReader dr = oDm.ExecuteReader("usp_MstOpeningBalance_GetById");

                Entity.Accounts.OpeningBalanceMaster OpBal = new Entity.Accounts.OpeningBalanceMaster();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OpBal. OpBalId = OpBalId;
                        OpBal.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : int.Parse(dr["CompanyId"].ToString());
                        OpBal.FinancialYrId = (dr["FinancialYrId"] == DBNull.Value) ? 0 : int.Parse(dr["FinancialYrId"].ToString());
                        OpBal.LedgerId = (dr["LedgerId"] == DBNull.Value) ? 0 : int.Parse(dr["LedgerId"].ToString());
                        OpBal.OpeningBalance = (dr["OpeningBalance"] == DBNull.Value) ? 0 : decimal.Parse(dr["OpeningBalance"].ToString());
                        OpBal.OpeningBalanceType = (dr["OpeningBalanceType"] == DBNull.Value) ? "0" : dr["OpeningBalanceType"].ToString();
                    }
                }
                return OpBal;
            }
        }
    }
}
