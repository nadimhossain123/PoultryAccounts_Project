using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
   public class BirdPrice
    {
       public BirdPrice()
       { 
       
       }

       public int DistrictId { get; set; }
       public int FarmRate { get; set; }
       public int RetailerRate { get; set; }
       public int DressedRate { get; set; }
       public DateTime Date { get; set; }
    }
}
