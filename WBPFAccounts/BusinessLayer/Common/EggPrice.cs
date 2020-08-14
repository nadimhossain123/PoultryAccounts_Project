using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
   public class EggPrice
    {
       public EggPrice()
       { 
       
       }
       public void Save(Entity.Common.EggPrice EggPrice)
       {
           DataAccess.Common.EggPrice.Save(EggPrice);
       }
       public Entity.Common.EggPrice GetAllById(DateTime Date)
       {
           return DataAccess.Common.EggPrice.GetAllById(Date);
       }
       public DataTable GetEggPrice(int Month, int Year)
       {
           return DataAccess.Common.EggPrice.GetEggPrice(Month, Year);
       }
        public void SaveMessageForNotification(DateTime DDate)
        {
             DataAccess.Common.EggPrice.SaveMessageForNotification(DDate);

        }
    }
}
