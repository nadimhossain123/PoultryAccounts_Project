using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
   public class EggPrice
    {
       public EggPrice()
       { 
       
       }
       public void Save(Entity.SMS.EggPrice EggPrice)
       {
           DataAccess.SMS.EggPrice.Save(EggPrice);
       }
       public Entity.SMS.EggPrice GetAllById(DateTime Date)
       {
           return DataAccess.SMS.EggPrice.GetAllById(Date);
       }
       public DataTable GetEggPrice(int Month, int Year)
       {
           return DataAccess.SMS.EggPrice.GetEggPrice(Month, Year);
       }
    }
}
