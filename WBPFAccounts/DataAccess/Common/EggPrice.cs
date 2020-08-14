using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess.Common
{
   public class EggPrice
    {
       public EggPrice()
       { 
       }
       public static void Save(Entity.Common.EggPrice EggPrice)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@pDate", SqlDbType.DateTime, EggPrice.date);
               oDm.Add("@pNECCPrice", SqlDbType.Decimal, EggPrice.NECCPrice);
                oDm.Add("@pNECCPrice2", SqlDbType.Decimal, EggPrice.NECCPrice2);
                oDm.Add("@pNECCPrice3", SqlDbType.Decimal, EggPrice.NECCPrice3);
               oDm.Add("@pbelowWt", SqlDbType.Decimal, EggPrice.belowWt);
               oDm.Add("@poverWt", SqlDbType.Decimal, EggPrice.overWt);
               oDm.Add("@pbelowAddRate", SqlDbType.Decimal, EggPrice.belowAddRate);
               oDm.Add("@poverAddRate", SqlDbType.Decimal, EggPrice.overAddRate);


               
               oDm.CommandType = CommandType.StoredProcedure;
               oDm.ExecuteNonQuery("usp_eggprice_SaveAllPrice");

           }

       }

       public static Entity.Common.EggPrice GetAllById(DateTime Date)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.CommandType = CommandType.StoredProcedure;

               oDm.Add("@pDate", SqlDbType.DateTime, ParameterDirection.Input, Date);

               SqlDataReader dr = oDm.ExecuteReader("usp_eggprice_GetegpriceByDate");
               Entity.Common.EggPrice EggPrice = new Entity.Common.EggPrice();

               if(dr.HasRows)
               {
                 while(dr.Read())
                 {
               
                     EggPrice.NECCPrice=(dr[0]== DBNull.Value)? 0 : decimal.Parse(dr[0].ToString());
                        EggPrice.NECCPrice2 = (dr[1] == DBNull.Value) ? 0 : decimal.Parse(dr[1].ToString());
                        EggPrice.NECCPrice3 = (dr[2] == DBNull.Value) ? 0 : decimal.Parse(dr[2].ToString());
                        EggPrice.belowWt=(dr[3]== DBNull.Value)? 0 : decimal.Parse(dr[3].ToString());
                     EggPrice.overWt=(dr[4]== DBNull.Value)? 0 : decimal.Parse(dr[4].ToString());
                     EggPrice.belowAddRate=(dr[5]== DBNull.Value)? 0 : decimal.Parse(dr[5].ToString());
                     EggPrice.overAddRate= (dr[6]== DBNull.Value)? 0 : decimal.Parse(dr[6].ToString());
                 
                 }
               }
                return EggPrice;
           }
       }

        //(SELECT AVG(NECCPrice) FROm eggprice where datepart(mm, [Date])="+ Month + "and datepart(YYYY, [Date])=" + Year + @")AS AvgNECCPrice

       public static DataTable GetEggPrice(int Month, int Year)
       {
           using (DataManager oDm = new DataManager())
           {
                string sql = (@"select NECCPrice,NECCPrice2,NECCPrice3,Date
                                from eggprice where 
                                datepart(mm,[Date])=" + Month + " and datepart(YYYY,[Date])=" + Year + " order by [Date] asc");

                oDm.CommandType = CommandType.Text;

               return oDm.ExecuteDataTable(sql);
            
           }
       }
        public static void SaveMessageForNotification(DateTime DDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pInsertDate", SqlDbType.DateTime, ParameterDirection.Input, DDate);
                oDm.ExecuteNonQuery("InsertDataIntoMobileNotificationTable");
            }
        }
    }
}
