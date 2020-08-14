using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.SMS
{
   public class EggPrice
    {
       public EggPrice()
       { 
       
       }
       public DateTime date { get; set; }
       public decimal NECCPrice { get; set; }
       public decimal belowWt { get; set; }
       public decimal overWt { get; set; }
       public decimal belowAddRate { get; set; }
       public decimal overAddRate { get; set; }
   
   }

}
