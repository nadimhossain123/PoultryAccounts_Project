using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
   public  class StudentOpeningBal
    {
       public static int SaveOpBal(Entity.Accounts.StudentOpeningBal OpBal)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@CompanyID_FK", SqlDbType.Int, OpBal.CompanyID_FK);
               oDm.Add("@BranchID_FK", SqlDbType.Int, OpBal.BranchID_FK);
               oDm.Add("@FinYearID_FK", SqlDbType.Int, OpBal.FinYearID_FK);
               oDm.Add("@StudentId", SqlDbType.Int, OpBal.StudentId);
               oDm.Add("@SemNo", SqlDbType.Int, OpBal.SemNo);
               oDm.Add("@BillAmount", SqlDbType.Decimal, OpBal.BillAmount);
               oDm.Add("@CreatedBy", SqlDbType.Int, OpBal.CreatedBy);
               oDm.Add("@OpeningBalXML", SqlDbType.Xml, OpBal.OpeningBalXML);
               oDm.Add("@BillId", SqlDbType.Int, OpBal.BillId);

               oDm.CommandType = CommandType.StoredProcedure;
               int recCnt = oDm.ExecuteNonQuery("usp_MemberOpeningBalance");

               return recCnt;
           }
       }

       public static DataSet StudentOpeningBalance_GetById(Entity.Accounts.StudentOpeningBal OpBal)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@StudentId", SqlDbType.Int, OpBal.StudentId);

               oDm.CommandType = CommandType.StoredProcedure;
               DataSet ds = new DataSet();
               return oDm.GetDataSet("usp_StudentOpeningBalance_GetById", ref ds, "Table");
           }
       }
    }
}
