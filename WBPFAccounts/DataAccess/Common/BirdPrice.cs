using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
  public  class BirdPrice
    {
      public BirdPrice()
      { 
      }

      public static void Save(DateTime Date, DataTable DtBirdPrice)
      {
          using (DataManager oDm = new DataManager())
          {
              oDm.Add("@pDate", SqlDbType.DateTime, Date);
              
              string BirdPriceXml = String.Empty;
              if (DtBirdPrice != null && DtBirdPrice.Rows.Count > 0)
              {
                  using (DataSet ds = new DataSet())
                  {
                      ds.Tables.Add(DtBirdPrice);
                      BirdPriceXml = ds.GetXml();
                  }
              }

              oDm.Add("@pBirdPriceXml", SqlDbType.Xml, BirdPriceXml);

              oDm.CommandType = CommandType.StoredProcedure;
              oDm.ExecuteNonQuery("usp_birdprice_SaveAllPrice");
         
          }
      }

      public static DataTable GetAll(DateTime Date)
      { 
        using( DataManager oDm = new DataManager())
        {
            oDm.Add("@pDate", SqlDbType.DateTime, Date);
           

            oDm.CommandType = CommandType.StoredProcedure;
            return oDm.ExecuteDataTable("usp_birdprice_GetAllbirdpriceByDate");


        }
      }

      public static DataTable GetAllMonthly(int Month,int Year, int DistrictId)
      {
          using (DataManager oDm = new DataManager())
          {
              oDm.Add("@pMonth", SqlDbType.Int,Month);
              oDm.Add("@pYear", SqlDbType.Int, Year);
              oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);


              oDm.CommandType = CommandType.StoredProcedure;
              return oDm.ExecuteDataTable("usp_birdprice_GetAllbirdpriceByMonth");


          }
      }
      public static DataTable GetRateByDay(int day, int month, string DistrictName)
      {
          using (DataManager oDm = new DataManager())
          {
              oDm.Add("@pDay", SqlDbType.Int, day);
              oDm.Add("@pMonth", SqlDbType.Int, month);
              oDm.Add("@pDistrictName", SqlDbType.VarChar, DistrictName);
              oDm.CommandType = CommandType.StoredProcedure;
              return oDm.ExecuteDataTable("usp_birdprice_GetAllbirdpriceByDay");


          }

      }
    }
}
