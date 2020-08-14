using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
  public  class BirdPrice
    {
      public BirdPrice()
      {
      }
      public void Save(DateTime Date, DataTable DtBirdPrice)
      {
          DataAccess.SMS.BirdPrice.Save(Date, DtBirdPrice);
      }
      public DataTable GetAll(DateTime Date)
      {
          return DataAccess.SMS.BirdPrice.GetAll(Date);
      }
      public DataTable GetAllMonthly(int Month, int Year, int DistrictId)
      {
          return DataAccess.SMS.BirdPrice.GetAllMonthly(Month, Year, DistrictId);
      }
      public DataTable GetRateByDay(int Day, int Month, string DistrictName)
      {
          return DataAccess.SMS.BirdPrice.GetRateByDay(Day, Month, DistrictName);
      }


    }
}
