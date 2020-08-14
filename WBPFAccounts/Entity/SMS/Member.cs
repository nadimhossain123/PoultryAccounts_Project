using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.SMS
{
   public class Member
    {
       public Member()
       {
       }

       private int _MemberId;
       public int MemberId
       {
           get { return _MemberId; }
           set { _MemberId = value; }
       }

       private string _MemberName;
       public string MemberName
       {
           get { return _MemberName; }
           set { _MemberName = value; }
       }

       private string _Location;
       public string Location
       {
           get { return _Location; }
           set { _Location = value; }
       }

       private DateTime _StartDate;
       public DateTime StartDate
       {
           get { return _StartDate; }
           set { _StartDate = value; }
       }

       private DateTime _EndDate;
       public DateTime EndDate
       {
           get { return _EndDate; }
           set { _EndDate = value; }
       }

       private string _MobileNo;
       public string MobileNo
       {
           get { return _MobileNo; }
           set { _MobileNo = value; }
       }

       private DateTime _ModDate;
       public DateTime ModDate
       {
           get { return _ModDate; }
           set { _ModDate = value; }
       }

       private int _RegistrationType;
       public int RegistrationType
       {
           get { return _RegistrationType; }
           set { _RegistrationType = value; }
       }

       private string _RegistrationNo;
       public string RegistrationNo
       {
           get { return _RegistrationNo; }
           set { _RegistrationNo = value; }
       }

       public string VoucherDetails { get; set; }
    }
}
