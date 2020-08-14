using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SMS
{
   public class Member
    {
       public Member()
       {
       }

       public static int Save(Entity.SMS.Member EntityMember)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.InputOutput, EntityMember.MemberId);
               oDm.Add("@pMemberName", SqlDbType.VarChar, 50, EntityMember.MemberName);
               oDm.Add("@pLocation", SqlDbType.VarChar, 50, EntityMember.Location);
               oDm.Add("@pStartDate", SqlDbType.DateTime, EntityMember.StartDate);
               oDm.Add("@pEndDate", SqlDbType.DateTime, EntityMember.EndDate);
               oDm.Add("@pMobileNo", SqlDbType.VarChar, 50, EntityMember.MobileNo);
               oDm.Add("@pRegistrationType", SqlDbType.Int, EntityMember.RegistrationType);
               oDm.Add("@pRegistrationNo", SqlDbType.VarChar,50, EntityMember.RegistrationNo);
               oDm.Add("@pVoucherDetails", SqlDbType.VarChar,8000, EntityMember.VoucherDetails);
               oDm.CommandType = CommandType.StoredProcedure;

              int i= oDm.ExecuteNonQuery("usp_Member_Save");

               EntityMember.MemberId = (int)oDm["@pMemberId"].Value;
               return i;
           }
       }

       public static DataTable GetAll(string MemberName, string MobileNo,int RegistrationType,string ExpiryDate, int SearchType)
       {
           using (DataManager oDm = new DataManager())
           {
               if (MemberName.Trim().Length == 0)
               {
                   oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
               }
               else
               {
                   oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
               }


               if (MobileNo.Trim().Length == 0)
               {
                   oDm.Add("@pMobileNo", SqlDbType.VarChar, 50, DBNull.Value);
               }
               else
               {
                   oDm.Add("@pMobileNo", SqlDbType.VarChar, 50, MobileNo);
               }

               if (RegistrationType == 1)
               {
                   oDm.Add("@pRegistrationType", SqlDbType.Int, DBNull.Value);
               }
               else
               {
                   oDm.Add("@pRegistrationType", SqlDbType.Int, RegistrationType);
               }


               if (ExpiryDate.Trim().Length == 0)
               {
                   oDm.Add("@pExpireDate", SqlDbType.DateTime, DBNull.Value);
               }
               else { oDm.Add("@pExpireDate", SqlDbType.DateTime, Convert.ToDateTime(ExpiryDate)); }

               oDm.Add("@pSearchType", SqlDbType.Int, SearchType);
               
               oDm.CommandType = CommandType.StoredProcedure;

               return oDm.ExecuteDataTable("usp_Member_GetAll");

             
           }
       }

       public static Entity.SMS.Member GetAllById(int MemberId)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.Input, MemberId);

               SqlDataReader dr = oDm.ExecuteReader("usp_Member_GetAllById");

               Entity.SMS.Member EntityMember = new Entity.SMS.Member();          
              if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       EntityMember.MemberId = MemberId;
                       EntityMember.MemberName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                       EntityMember.Location = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                       EntityMember.StartDate = (dr[3] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[3].ToString());
                       EntityMember.EndDate = (dr[4] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[4].ToString());
                       EntityMember.MobileNo = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                       EntityMember.RegistrationType = (dr[7] == DBNull.Value) ? 2 : int.Parse(dr[7].ToString());
                       EntityMember.RegistrationNo = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                       EntityMember.VoucherDetails = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();

                   }
               }
              return EntityMember;
           }
       }

       public static Entity.SMS.Member GetAllByMobNo(string MobileNo)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pMobileNo", SqlDbType.VarChar, 50, ParameterDirection.Input, MobileNo);

               SqlDataReader dr = oDm.ExecuteReader("usp_Member_GetAllByMobNo");

               Entity.SMS.Member EntityMember = new Entity.SMS.Member();
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       EntityMember.MemberId = (dr[0] == DBNull.Value) ? 0 : int.Parse(dr[0].ToString());
                       EntityMember.MemberName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                       EntityMember.Location = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                       EntityMember.StartDate = (dr[3] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[3].ToString());
                       EntityMember.EndDate = (dr[4] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[4].ToString());
                       EntityMember.MobileNo = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                       EntityMember.RegistrationType = (dr[7] == DBNull.Value) ? 2 : int.Parse(dr[7].ToString());
                       EntityMember.RegistrationNo = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                       EntityMember.VoucherDetails = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();

                   }
               }
               return EntityMember;
           }
       }

       public static void Delete(int MemberId)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.Input, MemberId);

               oDm.ExecuteNonQuery("usp_Member_Delete");
           }
       }

       public static DataTable getMobileNumbers(int Type)
       {
           using (DataManager oDm = new DataManager())
           {
               if (Type == 1)
               {
                   oDm.Add("@pRegistrationType", SqlDbType.Int,ParameterDirection.Input, DBNull.Value);
               }
               else
               {
                   oDm.Add("@pRegistrationType", SqlDbType.Int, ParameterDirection.Input, Type);
               }

               oDm.CommandType = CommandType.StoredProcedure;

               return oDm.ExecuteDataTable("usp_GetMobileNumbers");


           }
       }

       public static void QuickUpdate(int MemberId)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pMemberId", SqlDbType.Int, ParameterDirection.Input, MemberId);

               oDm.ExecuteNonQuery("usp_Member_QuickActivate");
           }
       }

       public static void ChangePriority(int MemberId, int Priority)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@MemberId", SqlDbType.Int, MemberId);
               oDm.Add("@Priority", SqlDbType.Int, Priority);

               oDm.CommandType = CommandType.StoredProcedure;
               oDm.ExecuteNonQuery("usp_Member_ChangePriority");
           }
       }
    }
}
