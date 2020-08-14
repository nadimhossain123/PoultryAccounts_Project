using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
  public  class BirdPrice
    {
      public BirdPrice()
      {
      }
      public void Save(DateTime Date, DataTable DtBirdPrice)
      {
          DataAccess.Common.BirdPrice.Save(Date, DtBirdPrice);
      }
      public DataTable GetAll(DateTime Date)
      {
          return DataAccess.Common.BirdPrice.GetAll(Date);
      }
      public DataTable GetAllMonthly(int Month, int Year, int DistrictId)
      {
          return DataAccess.Common.BirdPrice.GetAllMonthly(Month, Year, DistrictId);
      }
      public DataTable GetRateByDay(int Day, int Month, string DistrictName)
      {
          return DataAccess.Common.BirdPrice.GetRateByDay(Day, Month, DistrictName);
      }


    }
}
