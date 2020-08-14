using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess.SMS
{
   public class EggPrice
    {
       public EggPrice()
       { 
       }
       public static void Save(Entity.SMS.EggPrice EggPrice)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@pDate", SqlDbType.DateTime, EggPrice.date);
               oDm.Add("@pNECCPrice", SqlDbType.Decimal, EggPrice.NECCPrice);
               oDm.Add("@pbelowWt", SqlDbType.Decimal, EggPrice.belowWt);
               oDm.Add("@poverWt", SqlDbType.Decimal, EggPrice.overWt);
               oDm.Add("@pbelowAddRate", SqlDbType.Decimal, EggPrice.belowAddRate);
               oDm.Add("@poverAddRate", SqlDbType.Decimal, EggPrice.overAddRate);


               
               oDm.CommandType = CommandType.StoredProcedure;
               oDm.ExecuteNonQuery("usp_eggprice_SaveAllPrice");

           }

       }

       public static Entity.SMS.EggPrice GetAllById(DateTime Date)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pDate", SqlDbType.DateTime, ParameterDirection.Input, Date);

               SqlDataReader dr = oDm.ExecuteReader("usp_eggprice_GetegpriceByDate");
               Entity.SMS.EggPrice EggPrice = new Entity.SMS.EggPrice();

               if(dr.HasRows)
               {
                 while(dr.Read())
                 {
               
                     EggPrice.NECCPrice=(dr[0]== DBNull.Value)? 0 : decimal.Parse(dr[0].ToString());
                     EggPrice.belowWt=(dr[1]== DBNull.Value)? 0 : decimal.Parse(dr[1].ToString());
                     EggPrice.overWt=(dr[2]== DBNull.Value)? 0 : decimal.Parse(dr[2].ToString());
                     EggPrice.belowAddRate=(dr[3]== DBNull.Value)? 0 : decimal.Parse(dr[3].ToString());
                     EggPrice.overAddRate= (dr[4]== DBNull.Value)? 0 : decimal.Parse(dr[4].ToString());
                 
                 }
               }
                return EggPrice;
           }
       }

       public static DataTable GetEggPrice(int Month, int Year)
       {
           using (DataManager oDm = new DataManager())
           {
               string sql = (@"select NECCPrice,Date from eggprice where datepart(mm,[Date])=" + Month + "and datepart(YYYY,[Date])=" + Year + " order by [Date] asc");
               oDm.CommandType = CommandType.Text;

               return oDm.ExecuteDataTable(sql);


           }
       }
    }
}
